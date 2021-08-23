using Infra.Exception;
using Persistencia;
using Servico.Atividade.Validacao;
using Servico.DTO.Usuario;
using System;
using System.Linq;

namespace Servico.Atividade
{
    public class RemoverAtividadeServico
    {
        private readonly UsuarioCorrente _usuarioCorrente;
        private readonly Contexto _contexto;

        public RemoverAtividadeServico(UsuarioCorrente usuarioCorrente, Contexto contexto)
        {
            _usuarioCorrente = usuarioCorrente;
            _contexto = contexto;
        }

        public void Executar(Guid id)
        {
            var validacao = new RemoverAtividadeValidacaoBanco(_contexto, id);
            if (!validacao.IsValido)
                throw new SistemaException(validacao.Erros);

            using (var transacao = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var Atividade = _contexto.Atividade
                        .FirstOrDefault(x => x.Id == id) ?? throw new SistemaException("Atividade não encontrada para remover.");

                    Atividade.Excluido = true;
                    Atividade.UsuarioExclusao = _usuarioCorrente.Nome;
                    Atividade.DataExclusao = DateTime.Now;

                    _contexto.SaveChanges();

                    transacao.Commit();
                }
                catch (Exception ex)
                {
                    transacao.Rollback();
                    throw new SistemaException("Falha ao remover atividade.");
                }
            }
        }
    }
}