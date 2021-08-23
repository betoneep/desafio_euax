using System;
using System.Collections.Generic;
using System.Text;

namespace Servico
{
    public abstract class BaseValidacao
    {
        public List<string> Erros = new List<string>();
        public bool IsValido => Erros.Count <= 0;

    }
}
