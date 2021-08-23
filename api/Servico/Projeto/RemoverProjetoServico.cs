using Infra.Exception;
using Persistencia;
using Servico.DTO.Usuario;
using Servico.Projeto.Validacao;
using System;
using System.Linq;

namespace Servico.Projeto
{
    public class RemoverProjetoServico
    {
        private readonly UsuarioCorrente _usuarioCorrente;
        private readonly Contexto _contexto;

        public RemoverProjetoServico(UsuarioCorrente usuarioCorrente, Contexto contexto)
        {
            _usuarioCorrente = usuarioCorrente;
            _contexto = contexto;
        }

        public void Executar(Guid id)
        {
            var validacao = new RemoverProjetoValidacaoBanco(_contexto, id);
            if (!validacao.IsValido)
                throw new SistemaException(validacao.Erros);

            using (var transacao = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var Projeto = _contexto.Projeto
                        .FirstOrDefault(x => x.Id == id) ?? throw new SistemaException("Projeto não encontrado para remover.");

                    Projeto.Excluido = true;
                    Projeto.UsuarioExclusao = _usuarioCorrente.Nome;
                    Projeto.DataExclusao = DateTime.Now;

                    _contexto.SaveChanges();

                    transacao.Commit();
                }
                catch (Exception ex)
                {
                    transacao.Rollback();
                    throw new SistemaException("Falha ao remover projeto.");
                }

            }
        }
    }
}