using System;
using System.Collections.Generic;
using System.Text;

namespace Orram.Sefaz.Consulta.Domain.Result
{
    public class RetornoConsultaNFe
    {
        public RetornoConsultaNFe()
        {
            ProtocoloAutorizacao = new ProtocoloAutorizacao();
            SituacaoNFe = new SituacaoNFe();
            EventosNFe = new EventosNFe();
        }
        public ProtocoloAutorizacao ProtocoloAutorizacao { get; set; }
        public SituacaoNFe SituacaoNFe { get; set; }
        public EventosNFe EventosNFe { get; set; }
        public string ChaveAcesso { get; set; }
    }
}
