using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Entidade
{

    [Table("atividade")]
    public class Atividade : EntidadeBase
    {
        [ForeignKey("ProjetoId")]
        public virtual Projeto Projeto { get; set; }

        public Guid ProjetoId { get; set; }

        public string Nome { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? DataInicio { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? DataFim { get; set; }

        public bool? Finalizada { get; set; }
    }
}
