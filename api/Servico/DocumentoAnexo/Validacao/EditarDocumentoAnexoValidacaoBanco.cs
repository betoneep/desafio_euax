using Persistencia;
using Servico.DTO.DocumentoAnexo;
using System.Linq;

namespace Servico.DocumentoAnexo.Validacao
{
    class EditarDocumentoAnexoValidacaoBanco : BaseValidacao
    {
        public EditarDocumentoAnexoValidacaoBanco(Contexto contexto, DocumentoAnexoDTO dto)
        {
            if (contexto.DocumentoAnexo.Any(x => x.Id.Equals(dto.Id) && !x.Excluido && 1 == 2))
                Erros.Add($"xxx.");
        }
    }
}
