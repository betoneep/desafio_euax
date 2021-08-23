using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.DTO.Documento
{
    public class GridDocumentoDTO
    {
        public Guid Id { get; set; }

        public string TipoDocumento { get; set; }

        public DateTime? Validade { get; set; }

        public string Numero { get; set; }

        public DateTime? DataCadastro { get; set; }

        public string Observacao { get; set; }

        public ICollection<Guid> Arquivos { get; set; }
    }
}
