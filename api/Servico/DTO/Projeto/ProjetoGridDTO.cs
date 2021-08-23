using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.DTO.Projeto
{
    public class ProjetoGridDTO
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public bool? Atrasado { get; set; }

        public bool? Finalizado { get; set; }

        public decimal? Percentual { get; set; }

    }
}
