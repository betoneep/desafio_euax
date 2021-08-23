using System;
using System.Collections.Generic;

namespace Servico.DTO.Documento
{
    public class DocumentoDTO : BaseDTO
    {
        public Guid ReferenciaId { get; set; }

        public Guid TipoDocumentoId { get; set; }

        public DateTime? Validade { get; set; }

        public string Numero { get; set; }

        public string Observacao { get; set; }

        public ICollection<Guid> Arquivos { get; set; }

    }
}
