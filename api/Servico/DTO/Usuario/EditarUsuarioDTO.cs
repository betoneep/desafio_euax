using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.DTO.Usuario
{
    public class EditarUsuarioDTO
    {
        public string Nome { get; set; }

        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public bool Ativo { get; set; }
    }
}
