using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dominio.Entidade
{
    public class Arquivo : EntidadeBase
    {
        public byte[] Binario { get; set; }

        [MaxLength(50)]
        public string MimeType { get; set; }

        [MaxLength(150)]
        public string Nome { get; set; }

        [MaxLength(250)]
        public string Caminho { get; set; }

        [MaxLength(50)]
        public string Hash { get; set; }

    }
}
