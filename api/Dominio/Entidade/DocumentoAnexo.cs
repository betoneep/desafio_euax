using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidade
{
    [Table("documento_anexo")]
    public class DocumentoAnexo : EntidadeBase
    {
        public Guid ArquivoId { get; set; }

        [ForeignKey("DocumentoId")]
        public virtual Documento Documento { get; set; }

        public Guid? DocumentoId { get; set; }

    }
}
