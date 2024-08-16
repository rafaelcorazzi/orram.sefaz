using System;
using System.Collections.Generic;
using System.Text;

namespace Orram.Sefaz.Consulta.Domain.Result
{
    public class ProtocoloEventos
    {
       
        public string CodigoEvento { get; set; }
        public string Evento { get; set; }
        public int SequenciaEvento { get; set; }
        public string CnpjDestinatario { get; set; }
        public DateTime RegistroEvento { get; set; }
        public string Protocolo { get; set; }
    }
}
