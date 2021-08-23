using System;

namespace Servico.DTO.Login
{
    public class LoginValidoDTO
    {
        public Guid UsuarioId { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public DateTime DataExpiracao { get; set; }

        public Guid SessaoId { get; set; }

        public string Token { get; set; }
    }
}
