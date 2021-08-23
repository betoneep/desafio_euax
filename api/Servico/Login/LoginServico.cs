using Dominio.Entidade;
using Infra.Exception;
using Microsoft.EntityFrameworkCore;
using NETCore.Encrypt;
using Persistencia;
using Servico.DTO.Login;
using Servico.JWT;
using Servico.Login.Validacao;
using System;
using System.Linq;

namespace Servico.Login
{
    public class LoginServico
    {
        private readonly Contexto _contexto;

        public LoginServico(Contexto contexto)
        {
            _contexto = contexto;
        }


        public LoginValidoDTO Autenticar(LoginDTO dto)
        {
            var validador = new LoginValidacaoCampos(dto);
            if (!validador.IsValido)
                throw new SistemaException(validador.Erros);

            var usuario = ValidarUsuario(dto);
            var acesso = ValidarAcesso(usuario);
            acesso = new AutenticacaoFabrica(acesso).Constuir();
            RemoverTokenUsuario(acesso);
            CriarTokenUsuario(acesso);
            return acesso;
        }

        public void CriarTokenUsuario(LoginValidoDTO acesso)
        {
            var token = new Token
            {
                DataCadastro = DateTime.Now,
                SessaoId = acesso.SessaoId,
                UsuarioCadastro = acesso.Email,
                UsuarioId = acesso.UsuarioId,
                Valor = acesso.Token,
                DataExpiracao = acesso.DataExpiracao
            };

            _contexto.Token.Add(token);
            _contexto.SaveChanges();
        }

        public void RemoverTokenUsuario(LoginValidoDTO acesso)
        {
            var tokens = _contexto.Token
                .Where(x => !x.Excluido)
                .Where(x => x.UsuarioId == acesso.UsuarioId)
                .ToList();

            tokens.ForEach((item) =>
            {
                item.Excluido = true;
                item.DataExclusao = DateTime.Now;
                item.UsuarioExclusao = acesso.Email;
            });
            _contexto.SaveChanges();
        }

        public LoginValidoDTO ValidarAcesso(Dominio.Entidade.Usuario usuario)
        {
            LoginValidoDTO dto = MontarLoginValido(usuario);

            if (dto == null)
                throw new SistemaException("Limite de acesso atingido na conta.");

            return dto;
        }

        private LoginValidoDTO MontarLoginValido(Dominio.Entidade.Usuario usuario)
        {
            return new LoginValidoDTO
            {
                Email = usuario.Email,
                Nome = usuario.Nome,
                UsuarioId = usuario.Id
            };
        }

        public Dominio.Entidade.Usuario ValidarUsuario(LoginDTO dto)
        {
            var senha = EncryptProvider.Md5(dto.Senha).ToUpper();

            var usuario = _contexto.Usuario
                .AsNoTracking()
                .Where(x => x.Email == dto.Email.ToLower().Trim())
                .Where(x => x.Senha.ToUpper().Equals(senha))
                .FirstOrDefault() ?? throw new SistemaException("E-mail/Senha inválido.");

            if (!usuario.Ativo)
                throw new SistemaException("Usuário está inativo.");

            if (usuario.Excluido)
                throw new SistemaException("Usuário está excluído.");

            return usuario;
        }
    }
}
