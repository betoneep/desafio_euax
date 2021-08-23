using Infra.Exception;
using Infra.Utils;
using NETCore.Encrypt;
using Persistencia;
using Servico.DTO.Usuario;
using Servico.Usuario.Validacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.Usuario
{
    public class NovoUsuarioServico
    {
        private readonly UsuarioCorrente _usuarioCorrente;
        private readonly Contexto _contexto;

        public NovoUsuarioServico(UsuarioCorrente usuarioCorrente, Contexto contexto)
        {
            _usuarioCorrente = usuarioCorrente;
            _contexto = contexto;
        }

        public void Executar(NovoUsuarioDTO dto)
        {
            var validacaoCampos = new NovoUsuarioValidacaoCampos(dto);
            if (!validacaoCampos.IsValido)
                throw new SistemaException(validacaoCampos.Erros);

            var validacaoBanco = new NovoUsuarioValidacaoBanco(_contexto, dto);
            if (!validacaoBanco.IsValido)
                throw new SistemaException(validacaoBanco.Erros);

            var usuario = new Dominio.Entidade.Usuario
            {
                Ativo = true,
                DataCadastro = DateTime.Now,
                Email = dto.Email.ToLower().Trim(),
                Nome = dto.Nome,
                Excluido = false,
                UsuarioCadastro = _usuarioCorrente.Nome,
                Senha = EncryptProvider.Md5(dto.Senha).ToUpper()
            };

            _contexto.Usuario.Add(usuario);
            _contexto.SaveChanges();
        }
    }
}
