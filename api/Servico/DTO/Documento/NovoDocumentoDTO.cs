using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.DTO.Documento
{
    public class NovoDocumentoDTO
    {
        public Guid Id { get; set; }

        public Guid PessoaId { get; set; }

        public Guid TipoDocumentoId { get; set; }

        public DateTime? Validade { get; set; }

        public string Numero { get; set; }

        public string Observacao { get; set; }
    }
}
