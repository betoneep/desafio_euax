using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.DTO.DocumentoAnexo
{
    public class DocumentoAnexoGridDTO
    {
        public Guid ArquivoId { get; set; }

        public string Nome { get; set; }

        public string Tipo { get; set; }
    }
}
