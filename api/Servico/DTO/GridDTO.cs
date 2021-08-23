using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.DTO
{
    public class GridDTO<T>
    {
        public int Pagina { get; set; }

        public int Total { get; set; }

        public int ItensPorPagina { get; set; }

        public ICollection<T> Itens { get; set; }
    }
}
