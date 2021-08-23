using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.DTO.Arquivo
{
    public class BaixarAquivoDTO
    {
        public byte[] Binario { get; set; }

        public string MimeType { get; set; }
    }
}
