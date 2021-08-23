using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Exception
{
    public class SistemaException : System.Exception
    {
        public readonly IEnumerable<string> Erros;

        public SistemaException(string mensagem) : base(mensagem)
        {
            this.Erros = new List<string> { mensagem };
        }

        public SistemaException(IEnumerable<string> erros)
        {
            this.Erros = erros;
        }
    }
}
