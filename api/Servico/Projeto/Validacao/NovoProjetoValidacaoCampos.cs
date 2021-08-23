using Servico.DTO.Projeto;

namespace Servico.Projeto.Validacao
{
    public class NovoProjetoValidacaoCampos : BaseValidacao
    {
        public NovoProjetoValidacaoCampos(ProjetoDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                Erros.Add("Informe um nome.");

        }
    }
}
