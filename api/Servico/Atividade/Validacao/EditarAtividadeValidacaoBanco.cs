using Persistencia;
using Servico.DTO.Atividade;
using System.Linq;

namespace Servico.Atividade.Validacao
{
    public class EditarAtividadeValidacaoBanco : BaseValidacao
    {
        public EditarAtividadeValidacaoBanco(Contexto contexto, AtividadeDTO dto)
        {
            if (contexto.Atividade.Any(x => !x.Id.Equals(dto.Id) && !x.Excluido && x.Nome.Equals(dto.Nome)))
            {
                Erros.Add($"Já existe uma atividade com este nome.");
            }
        }
    }
}
