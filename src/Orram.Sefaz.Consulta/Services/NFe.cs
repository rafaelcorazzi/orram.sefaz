using Newtonsoft.Json;
using Orram.Sefaz.Consulta.Adapters;
using Orram.Sefaz.Consulta.Domain.Entities;
using Orram.Sefaz.Consulta.Domain.Result;
using Orram.Sefaz.Consulta.Modules;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace Orram.Sefaz.Consulta.Services
{
    public class NFe
    {
        SefazComunication sefaz = new SefazComunication();

        public RetornoConsultaNFe ConsultaSefaz(ConsultaNfe nfe, X509Certificate2 certificado)
        {
            var retorno = string.Empty;
            string consultar = TSoap.soapXmlConsultaNFe(1, nfe.ChaveAcesso);
            retorno = sefaz.NFeConsultaProtocolo(consultar, int.Parse(nfe.ChaveAcesso.Substring(0, 2)), certificado, nfe.TipoEmissao);

            XmlDocument document = new XmlDocument();
            document.LoadXml(retorno);
            var retornoConsulta = new RetornoConsultaNFe();
           
            XmlNamespaceManager namespaces = new XmlNamespaceManager(document.NameTable);
            namespaces.AddNamespace("env", "http://www.w3.org/2003/05/soap-envelope");
          
            XmlNodeList nl_infProt = document.SelectNodes("descendant::env:Envelope/env:Body", namespaces);
            foreach (XmlNode infProt in nl_infProt)
            {
                foreach (XmlNode var in infProt)
                {
                    foreach(XmlNode var2 in var)
                    {
                        Console.WriteLine(var2.Name);
                        if(var2.Name == "retConsSitNFe")
                        {
                            foreach (XmlNode var3 in var2)
                            {
                                if ((var3.Name) == "chNFe") retornoConsulta.ChaveAcesso = var3.InnerText;
                                if ((var3.Name) == "xMotivo") retornoConsulta.SituacaoNFe.Motivo = var3.InnerText;
                                if ((var3.Name) == "cStat") retornoConsulta.SituacaoNFe.CodigoStatus = int.Parse(var3.InnerText);
                                if ((var3.Name) == "dhRecbto") retornoConsulta.SituacaoNFe.DataSituacao = DateTime.Parse(var3.InnerText);

                                if (var3.Name == "protNFe")
                                {
                                    foreach (XmlNode var4 in var3)
                                    {
                                        foreach (XmlNode var5 in var4)
                                        {
                                            if ((var5.Name) == "dhRecbto") retornoConsulta.ProtocoloAutorizacao.DataAutorizacao = DateTime.Parse(var5.InnerText);
                                            if ((var5.Name) == "nProt") retornoConsulta.ProtocoloAutorizacao.Protocolo = var5.InnerText;
                                            if ((var5.Name) == "cStat") retornoConsulta.ProtocoloAutorizacao.CodigoStatus = int.Parse(var5.InnerText);
                                            if ((var5.Name) == "digVal") retornoConsulta.ProtocoloAutorizacao.Hash = var5.InnerText;
                                            if ((var5.Name) == "xMotivo") retornoConsulta.ProtocoloAutorizacao.Motivo = var5.InnerText;
                                        }
                                    }
                                }

                                if (var3.Name == "procEventoNFe")
                                {
                                    foreach (XmlNode var4 in var3)
                                    {
                                        if(var4.Name == "retEvento")
                                        {
                                            foreach (XmlNode var5 in var4)
                                            {
                                                var protocoloEventos = new ProtocoloEventos();

                                                foreach (XmlNode var6 in var5)
                                                {
                                                    if ((var6.Name) == "dhRegEvento") protocoloEventos.RegistroEvento = DateTime.Parse(var6.InnerText);
                                                    if ((var6.Name) == "nSeqEvento") protocoloEventos.SequenciaEvento = int.Parse(var6.InnerText);
                                                    if ((var6.Name) == "nProt") protocoloEventos.Protocolo = var6.InnerText;
                                                    if ((var6.Name) == "tpEvento") protocoloEventos.CodigoEvento = var6.InnerText;
                                                    if ((var6.Name) == "xEvento") protocoloEventos.Evento = var6.InnerText;
                                                    if(var6.Name == "tpEvento")
                                                    {
                                                        if (var6.InnerText.Equals("110111"))
                                                        {
                                                            protocoloEventos.Evento = "Cancelamento";
                                                        }
                                                    }
                                                    if ((var6.Name) == "CNPJDest") protocoloEventos.CnpjDestinatario = var6.InnerText;
                                                    
                                                }
                                                
                                                retornoConsulta.EventosNFe.Eventos.Add(protocoloEventos);
                                            }
                                        }
                                       
                                    }
                                }
                            }
                            
                        }
                    }
                  

                }
            }

            return retornoConsulta;
        }
    }
}
