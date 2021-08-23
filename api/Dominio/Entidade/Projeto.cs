using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Entidade
{

    [Table("projeto")]
    public class Projeto : EntidadeBase
    {
        public string Nome { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? DataInicio { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? DataFim { get; set; }

    }
}
