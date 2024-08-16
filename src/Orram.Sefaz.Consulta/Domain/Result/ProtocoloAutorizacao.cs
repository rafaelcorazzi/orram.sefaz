using System;
using System.Collections.Generic;
using System.Text;

namespace Orram.Sefaz.Consulta.Domain.Result
{
    public class ProtocoloAutorizacao
    {
       
        public DateTime DataAutorizacao { get; set; }
        public string Protocolo { get; set; }
        public string Hash { get; set; }
        public string Motivo { get; set; }
        public int CodigoStatus { get; set; }
    }
}
