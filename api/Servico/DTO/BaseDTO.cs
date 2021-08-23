using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.DTO
{
    public class BaseDTO
    {
        public Guid Id { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public DateTime? DataCadastro { get; set; }

        public DateTime? DataExclusao { get; set; }

        public bool Excluido { get; set; }

        public string UsuarioCadastro { get; set; }

        public string UsuarioExclusao { get; set; }

        public string UsuarioAlteracao { get; set; }
    }
}
