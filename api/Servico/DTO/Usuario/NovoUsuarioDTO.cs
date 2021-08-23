using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.DTO.Usuario
{
    public class NovoUsuarioDTO
    {
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }
    }
}
