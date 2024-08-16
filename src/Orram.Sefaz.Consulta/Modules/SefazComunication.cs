using Orram.Sefaz.Consulta.Adapters;
using Orram.Sefaz.Consulta.Certificados;
using Orram.Sefaz.Consulta.Domain.Enums;
using Orram.Sefaz.Consulta.Exceptions;
using Orram.Sefaz.Consulta.Modules;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Orram.Sefaz.Consulta.Modules
{
    public class SefazComunication : ISefazOperation
    {
        public string NFeConsultaProtocolo(string xml, int cUF, X509Certificate2 certificado, int tpEmiss = 1)
        {
      

            string Retorno = string.Empty;
            try
            {
                if (certificado == null)
                {

                    throw new SefazException("Certificado Inexistente");
                }
                else if (NFeCertificadoDigital.GetExpirationDate(certificado))
                {
                    throw new SefazException("Certificado Expirado não e possível comunicação com o Sefaz");
                }
                else
                {
               
                    SefazWebRequest request = new SefazWebRequest();


                    string am = "https://nfe.sefaz.am.gov.br/services2/services/NfeConsulta4";
                    string ba = "https://nfe.sefaz.ba.gov.br/webservices/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx";
                    string ce = "https://www.svc.fazenda.gov.br/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx";
                    string go = "https://nfe.sefaz.go.gov.br/nfe/services/NFeConsultaProtocolo4?wsdl";
                    string mg = "https://nfe.fazenda.mg.gov.br/nfe2/services/NFeConsultaProtocolo4?wsdl";
                    string mt = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta4?wsdl";
                    string ms = "https://nfe.sefaz.ms.gov.br/ws/NFeConsultaProtocolo4";
                    string pe = "https://nfe.sefaz.pe.gov.br/nfe-service/services/NFeConsultaProtocolo4?wsdl";
                    string pr = "https://nfe.sefa.pr.gov.br/nfe/NFeConsultaProtocolo4?wsdl";
                    string rs = "https://nfe.sefazrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx";
                    string sp = "https://nfe.fazenda.sp.gov.br/ws/nfeconsultaprotocolo4.asmx";

                    string svrs = "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx";
                    string svan = "https://www.sefazvirtual.fazenda.gov.br/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx";
                    string svc_rs = "https://nfe.svrs.rs.gov.br/ws/NfeConsulta/NfeConsulta4.asmx";
                    string svc_an = "https://www.svc.fazenda.gov.br/NFeConsultaProtocolo4/NFeConsultaProtocolo4.asmx";

                    if (tpEmiss == 1)
                    {
                        #region "UF AUTORIZADORAS"
                        switch (cUF)
                        {
                            case (int)Estados.AM:

                                Retorno = request.RequestWebService(am, xml, certificado);
                                break;
                            case (int)Estados.SE:
                                Retorno = request.RequestWebService(svc_an, xml, certificado);
                                break;
                            case (int)Estados.BA:
                                Retorno = request.RequestWebService(ba, xml, certificado);
                                break;
                            case (int)Estados.CE:
                                Retorno = request.RequestWebService(ce, xml, certificado);
                                break;
                            case (int)Estados.GO:
                                Retorno = request.RequestWebService(go, xml, certificado);
                                break;
                            case (int)Estados.MG:
                                Retorno = request.RequestWebService(mg, xml, certificado);
                                break;
                            case (int)Estados.MT:
                                Retorno = request.RequestWebService(mt, xml, certificado);
                                break;
                            case (int)Estados.MS:
                                Retorno = request.RequestWebService(ms, xml, certificado);
                                break;
                            case (int)Estados.PE:
                                Retorno = request.RequestWebService(pe, xml, certificado, "http://www.portalfiscal.inf.br/nfe/wsdl/NFeConsultaProtocolo4/nfeConsultaNF");
                                break;
                            case (int)Estados.PR:
                                Retorno = request.RequestWebService(pr, xml, certificado);
                                break;
                            case (int)Estados.RS:
                                Retorno = request.RequestWebService(rs, xml, certificado);
                                break;
                            case (int)Estados.SP:
                                Retorno = request.RequestWebService(sp, xml, certificado);
                                break;
                            case (int)Estados.SC:
                                Retorno = request.RequestWebService(svc_rs, xml, certificado);
                                break;

                        }
                        #endregion

                        #region "SVRS - SEFAZ VIRTUAL RIO GRANDE DO SUL = AC, AL, AP, CE, DF, ES, PA, PB, PI, RJ, RN, RO, RR, SC, SE, TO "

                        if (cUF == (int)Estados.AC ||
                            cUF == (int)Estados.AL ||
                            cUF == (int)Estados.AP ||
                            cUF == (int)Estados.DF ||
                            cUF == (int)Estados.PA ||
                            cUF == (int)Estados.ES ||
                            cUF == (int)Estados.PB ||
                            cUF == (int)Estados.PI ||
                            cUF == (int)Estados.RN ||
                            cUF == (int)Estados.RO ||
                            cUF == (int)Estados.RR ||
                            cUF == (int)Estados.RJ ||
                            cUF == (int)Estados.TO)
                        {
                            Retorno = request.RequestWebService(svrs, xml, certificado);

                        }


                        #endregion

                        #region "SEFAZ VIRTUAL AMBIENTE NACIONAL = MA"

                        if (cUF == (int)Estados.MA)
                        {

                            Retorno = request.RequestWebService(svan, xml, certificado);

                        }


                        #endregion
                    }
                    else if (tpEmiss == 6 || tpEmiss == 3)
                    {
                        Retorno = request.RequestWebService(svc_an, xml, certificado);
                    }
                    else if(tpEmiss == 7)
                    {
                        Retorno = request.RequestWebService(svc_rs, xml, certificado);
                    }
                   


                }
            }
            catch (Exception ex)
            {
                throw new SefazException("Erro Retorno Consulta Cadastro: " + ex.Message);

            }
            return Retorno;
        }
    }
}
