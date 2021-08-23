using Persistencia;
using Servico.DTO.Documento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servico.Documento.Validacao
{
    public class EditarDocumentoValidacaoBanco : BaseValidacao
    {
        public EditarDocumentoValidacaoBanco(Contexto contexto, DocumentoDTO dto)
        {
            if (contexto.Documento.Any(x => !x.Id.Equals(dto.Id) && x.Numero == dto.Numero && !x.Excluido))
                Erros.Add($"Já existe um documento com mesmo número cadastrado.");
        }
    }
}
