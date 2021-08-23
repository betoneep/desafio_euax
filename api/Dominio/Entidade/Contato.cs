using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Entidade
{
    [Table("contato")]
    public class Contato : EntidadeBase
    {
        [MaxLength(20)]
        public string Telefone { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(250)]
        public string Observacao { get; set; }

        public Guid ReferenciaId { get; set; }

    }
}
