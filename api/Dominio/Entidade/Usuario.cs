using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidade
{
    [Table("usuario")]
    public class Usuario : EntidadeBase
    {
        [MaxLength(80)]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        [MaxLength(120)]
        public string Email { get; set; }

        [MaxLength(35)]
        public string Senha { get; set; }

        public virtual ICollection<Token> Tokens { get; set; }

    }
}
