using Servico.DTO.Documento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Servico.DTO.DocumentoAnexo
{
    public class DocumentoAnexoDTO : BaseDTO
    {
        public string Hash { get; set; }

        public string Extensao { get; set; }

        public string Nome { get; set; }

        public DocumentoDTO Documento { get; set; }

        public Guid? DocumentoId { get; set; }

    }
}
