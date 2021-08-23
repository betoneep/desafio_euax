using Servico.DTO.Documento;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.Documento.Validacao
{
    public class EditarDocumentoValidacaoCampos : BaseValidacao
    {
        public EditarDocumentoValidacaoCampos(DocumentoDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Numero))
                Erros.Add("Informe um número.");
            else if (dto.Numero.Length > 100)
                Erros.Add("Número não pode ter mais de 100 caracteres.");

            if (dto.Observacao.Length > 15)
                Erros.Add("Observação não pode ter mais de 100 caracteres.");

            if (dto.ReferenciaId == Guid.Empty)
                Erros.Add("Informe referência do documento.");

        }
    }
}
