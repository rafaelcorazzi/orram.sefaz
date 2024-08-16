using System;
using System.IO;
using System.Text;

namespace Orram.Sefaz.Consulta.Adapters
{
    public class TSoap
    {
        public static String soapXmlConsultaNFe(int ambiente, string ChaveNFe)
        {

            String result = String.Empty;
            MemoryStream stream = new MemoryStream(); // The writer closes this for us

            using (System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(stream, Encoding.UTF8))
            {



                writer.WriteStartDocument();
                writer.WriteStartElement("soap:Envelope");
                writer.WriteAttributeString("xmlns:soap", "http://www.w3.org/2003/05/soap-envelope");
                writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                writer.WriteAttributeString("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
                writer.WriteStartElement("soap:Header");
                writer.WriteStartElement("nfeCabecMsg");
                writer.WriteAttributeString("xmlns", "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4");
                writer.WriteElementString("cUF", ChaveNFe.Substring(0, 2));
                writer.WriteElementString("versaoDados", "4.00");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteStartElement("soap:Body");

                writer.WriteStartElement("nfeDadosMsg");
                writer.WriteAttributeString("xmlns", "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4");
                writer.WriteStartElement("consSitNFe");
                writer.WriteAttributeString("xmlns", "http://www.portalfiscal.inf.br/nfe");
                writer.WriteAttributeString("versao", "4.00");
                writer.WriteElementString("tpAmb", ambiente.ToString());
                writer.WriteElementString("xServ", "CONSULTAR");
                writer.WriteElementString("chNFe", ChaveNFe);
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Flush();

                StreamReader reader = new StreamReader(stream, Encoding.UTF8, true);
                stream.Seek(0, SeekOrigin.Begin);

                result += reader.ReadToEnd();

            }

            return result;
        }
    }
}
