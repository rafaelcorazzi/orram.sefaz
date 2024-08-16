using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Orram.Sefaz.Consulta.Domain.Result
{
    public class EventosNFe
    {
        public EventosNFe()
        {
            Eventos = new List<ProtocoloEventos>();
        }
        public List<ProtocoloEventos> Eventos { get; set; }
    }
}
