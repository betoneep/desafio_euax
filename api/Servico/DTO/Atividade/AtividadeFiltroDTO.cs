using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.DTO.Atividade
{
    public class AtividadeFiltroDTO
    {
        public Guid ProjetoId { get; set; }

        public string Nome { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public bool? Finalizada { get; set; }

        public bool? Atrasado { get; set; }

        public int Pagina { get; set; }

        public int ItensPorPagina { get; set; }
    }
}
