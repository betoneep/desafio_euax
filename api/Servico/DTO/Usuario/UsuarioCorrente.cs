using Infra.Constante;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Servico.DTO.Usuario
{
    public class UsuarioCorrente
    {
        public Guid Id { get; private set; }

        public string Nome { get; private set; }

        public string Email { get; private set; }

        public List<string> Permissoes { get; private set; }

        public UsuarioCorrente(IHttpContextAccessor httpContextAccessor)
        {
            try
            {
                Email = httpContextAccessor.HttpContext.User.FindFirst(JWTConstante.CLAIM_EMAIL).Value;
                Nome = httpContextAccessor.HttpContext.User.FindFirst(JWTConstante.CLAIM_NOME).Value;
            }
            catch (Exception)
            {

            }
        }

    }
}
