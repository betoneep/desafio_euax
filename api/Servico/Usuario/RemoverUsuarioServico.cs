using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Infra.Exception;
using Microsoft.EntityFrameworkCore;
using Servico.DTO.Usuario;

namespace Servico.Usuario
{
    public class RemoverUsuarioServico
    {
        private readonly Contexto _contexto;
        private readonly UsuarioCorrente _usuarioConrrente;

        public RemoverUsuarioServico(Contexto contexto, UsuarioCorrente usuarioConrrente)
        {
            _contexto = contexto;
            _usuarioConrrente = usuarioConrrente;
        }

        public void Executar(Guid id)
        {
            var usuario = _contexto.Usuario
                .Where(x => !x.Excluido)
                .FirstOrDefault(x => x.Id == id)
                ?? throw new SistemaException("Usuário não encontrado para remover.");

            usuario.Excluido = true;
            usuario.UsuarioExclusao = _usuarioConrrente.Nome;
            usuario.DataExclusao = DateTime.Now;

            _contexto.SaveChanges();
        }
    }
}
