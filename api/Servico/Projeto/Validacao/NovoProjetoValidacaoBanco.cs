using Persistencia;
using Servico.DTO.Projeto;
using System.Linq;

namespace Servico.Projeto.Validacao
{
    public class NovoProjetoValidacaoBanco : BaseValidacao
    {
        public NovoProjetoValidacaoBanco(Contexto contexto, ProjetoDTO dto)
        {
            if (contexto.Projeto.Any(x => !x.Excluido && x.Nome.Equals(dto.Nome)))
            {
                Erros.Add($"Já existe um projeto com esse nome.");
            }
        }
    }
}
