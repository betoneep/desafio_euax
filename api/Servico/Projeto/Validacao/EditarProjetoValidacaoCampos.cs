using Servico.DTO.Projeto;

namespace Servico.Projeto.Validacao
{
    public class EditarProjetoValidacaoCampos : BaseValidacao
    {
        public EditarProjetoValidacaoCampos(ProjetoDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                Erros.Add("Informe um nome.");
        }
    }
}
