using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Entidade
{
    public class EntidadeBase
    {
        [Key]
        [Column("Id", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? DataAlteracao { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? DataCadastro { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? DataExclusao { get; set; }

        public bool Excluido { get; set; }

        [MaxLength(120)]
        public string UsuarioCadastro { get; set; }

        [MaxLength(120)]
        public string UsuarioExclusao { get; set; }

        [MaxLength(120)]
        public string UsuarioAlteracao { get; set; }
    }
}
