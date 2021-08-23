using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidade
{
    [Table("documento")]
    public class Documento : EntidadeBase
    {
        [Column(TypeName = "timestamp without time zone")]
        public DateTime? Validade { get; set; }

        public Guid TipoDocumentoId { get; set; }

        [MaxLength(100)]
        public string Numero { get; set; }

        [MaxLength(100)]
        public string Observacao { get; set; }

        public Guid ReferenciaId { get; set; }

        public virtual ICollection<DocumentoAnexo> Arquivos { get; set; }

    }
}
