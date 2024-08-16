using System;
using System.Collections.Generic;
using System.Text;

namespace Orram.Sefaz.Consulta.Domain.Result
{
    public class SituacaoNFe
    {
        public string Motivo { get; set; }
        public int CodigoStatus { get; set; }
        public DateTime DataSituacao { get; set; }
    }
}
