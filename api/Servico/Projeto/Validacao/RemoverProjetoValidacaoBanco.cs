using Persistencia;
using System;
using System.Linq;

namespace Servico.Projeto.Validacao
{
    public class RemoverProjetoValidacaoBanco : BaseValidacao
    {
        public RemoverProjetoValidacaoBanco(Contexto contexto, Guid id)
        {
            if (contexto.Atividade.Any(x => x.Finalizada == true && x.ProjetoId.Equals(id) && !x.Excluido))
            {
                Erros.Add($"Projeto com atividade finalizada.");
            }
        }
    }
}
