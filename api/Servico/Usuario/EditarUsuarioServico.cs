using Persistencia;
using Servico.DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Infra.Exception;
using NETCore.Encrypt;
using Servico.Usuario.Validacao;

namespace Servico.Usuario
{
    public class EditarUsuarioServico
    {
        private readonly Contexto _contexto;
        private readonly UsuarioCorrente _usuarioCorrente;

        public EditarUsuarioServico(Contexto contexto, UsuarioCorrente usuarioCorrente)
        {
            _contexto = contexto;
            _usuarioCorrente = usuarioCorrente;
        }

        public void Executar(EditarUsuarioDTO dto)
        {
            var validacaoCampos = new EditarUsuarioValidacaoCampos(dto);
            if (!validacaoCampos.IsValido)
                throw new SistemaException(validacaoCampos.Erros);

            var validacaoBanco = new EditarUsuarioValidacaoBanco(_contexto, dto);
            if (!validacaoBanco.IsValido)
                throw new SistemaException(validacaoBanco.Erros);

            var usuario = _contexto.Usuario
                .Where(x => !x.Excluido)
                .FirstOrDefault(x => x.Id == dto.Id)
                ?? throw new SistemaException("Usuário não encontrado para editar.");

            usuario.DataAlteracao = DateTime.Now;
            usuario.UsuarioAlteracao = _usuarioCorrente.Nome;
            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.Ativo = dto.Ativo;
            if (usuario.Senha != dto.Senha)
                usuario.Senha = EncryptProvider.Md5(dto.Senha).ToUpper();

            _contexto.SaveChanges();
        }
    }
}
