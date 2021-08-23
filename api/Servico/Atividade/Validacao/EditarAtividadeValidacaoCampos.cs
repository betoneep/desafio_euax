using Servico.DTO.Atividade;

namespace Servico.Atividade.Validacao
{
    public class EditarAtividadeValidacaoCampos : BaseValidacao
    {
        public EditarAtividadeValidacaoCampos(AtividadeDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                Erros.Add("Informe um nome.");
        }
    }
}
