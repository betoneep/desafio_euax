using Persistencia;
using Servico.DTO.Atividade;
using System.Linq;

namespace Servico.Atividade.Validacao
{
    public class NovaAtividadeValidacaoBanco : BaseValidacao
    {
        public NovaAtividadeValidacaoBanco(Contexto contexto, AtividadeDTO dto)
        {
            if (contexto.Atividade.Any(x => !x.Excluido && x.Nome.Equals(dto.Nome)))
            {
                Erros.Add($"Já existe uma atividade com este nome.");
            }
        }
    }
}
