using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.DTO
{
    public class AppSettings
    {
        public string JWT { get; set; }

        public Guid PortalId { get; set; }

        public int EmailPorta { get; set; }

        public string EmailSmtp { get; set; }

        public string Email { get; set; }

        public string EmailSenha { get; set; }

        public string BaseUrl { get; set; }
    }
}
