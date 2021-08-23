using Persistencia;
using Servico.DTO.Projeto;
using System.Linq;

namespace Servico.Projeto.Validacao
{
    public class EditarProjetoValidacaoBanco : BaseValidacao
    {
        public EditarProjetoValidacaoBanco(Contexto contexto, ProjetoDTO dto)
        {
            if (contexto.Projeto.Any(x => !x.Id.Equals(dto.Id) && !x.Excluido && x.Nome.Equals(dto.Nome)))
            {
                Erros.Add($"Já existe um projeto com esse nome.");
            }
        }
    }
}
