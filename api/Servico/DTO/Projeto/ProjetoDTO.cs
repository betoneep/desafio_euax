using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.DTO.Projeto
{
    public class ProjetoDTO : BaseDTO
    {
        public string Nome { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

    }
}
