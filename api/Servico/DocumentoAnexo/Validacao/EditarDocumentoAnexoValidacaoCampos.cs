using Servico.DTO.DocumentoAnexo;

namespace Servico.DocumentoAnexo.Validacao
{
    public class EditarDocumentoAnexoValidacaoCampos : BaseValidacao
    {
        public EditarDocumentoAnexoValidacaoCampos(DocumentoAnexoDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                Erros.Add("Informe um nome.");
            else if (dto.Nome.Length > 100)
                Erros.Add("O nome não pode ter mais de 100 caracteres.");

        }
    }
}
