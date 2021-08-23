using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.DTO.Atividade
{
    public class AtividadeGridDTO
    {
        public Guid ProjetoId { get; set; }

        public string Projeto { get; set; }

        public string Nome { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public bool? Finalizada { get; set; }
    }
}
