using Infra.Exception;
using Persistencia;
using Servico.DTO.Usuario;
using System.Linq;
using System;
using Servico.Documento.Validacao;

namespace Servico.Documento
{
    public class RemoverDocumentoServico
    {
        private readonly UsuarioCorrente _usuarioCorrente;
        private readonly Contexto _contexto;

        public RemoverDocumentoServico(UsuarioCorrente usuarioCorrente, Contexto contexto)
        {
            _usuarioCorrente = usuarioCorrente;
            _contexto = contexto;
        }

        public void Executar(Guid id)
        {
            var validacao = new RemoverDocumentoValidacaoBanco(_contexto, id);
            if (!validacao.IsValido)
                throw new SistemaException(validacao.Erros);

            using (var transacao = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var documento = _contexto.Documento
                        .Where(x => !x.Excluido)
                        .FirstOrDefault(x => x.Id == id)
                        ?? throw new SistemaException("Documento não encontrado para remover.");

                    documento.Excluido = true;
                    documento.UsuarioExclusao = _usuarioCorrente.Nome;
                    documento.DataExclusao = DateTime.Now;

                    _contexto.SaveChanges();

                    transacao.Commit();
                }
                catch (Exception ex)
                {
                    transacao.Rollback();
                    throw new SistemaException("Falha ao remover documento.");
                }

            }
        }
    }
}