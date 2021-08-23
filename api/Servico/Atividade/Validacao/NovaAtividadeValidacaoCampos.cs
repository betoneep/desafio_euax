using Servico.DTO.Atividade;

namespace Servico.Atividade.Validacao
{
    public class NovaAtividadeValidacaoCampos : BaseValidacao
    {
        public NovaAtividadeValidacaoCampos(AtividadeDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                Erros.Add("Informe um nome.");
        }
    }
}
