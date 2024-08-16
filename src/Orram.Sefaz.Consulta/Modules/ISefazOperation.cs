using System;
using System.Security.Cryptography.X509Certificates;

namespace Orram.Sefaz.Consulta.Modules
{
    public interface ISefazOperation
    {
        string NFeConsultaProtocolo(String xml, int cUF, X509Certificate2 certificado, int tpEmiss = 1);
    }
}
