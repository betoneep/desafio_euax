using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Entidade
{
    [Table("token")]
    public class Token : EntidadeBase
    {
        public string Valor { get; set; }

        [ForeignKey("UsuarioId")]       
        public Usuario Usuario { get; set; }

        public Guid UsuarioId { get; set; }

        public DateTime DataExpiracao { get; set; }

        public Guid SessaoId { get; set; }
    }
}
