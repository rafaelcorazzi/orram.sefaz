using Orram.Sefaz.Consulta.Domain.Utils;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace Orram.Sefaz.Consulta.Adapters
{
    public  class SefazWebRequest
    {
        public  string RequestWebService(string wsURL, string Payload, X509Certificate2 certificado, string soapAction = null)
        {
             HttpWebRequest request = (HttpWebRequest)WebRequest.Create(wsURL);
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.DefaultConnectionLimit = 9999;
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(Payload);
            if (soapAction != null)
            {
                request.ContentType = "application/soap+xml; encoding=utf-8; action=" + soapAction;

            }
            else {
                request.ContentType = "application/soap+xml; encoding='utf-8'";
            }

          
            request.ContentLength = bytes.Length;
            ServicePointManager.SecurityProtocol =  SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            request.Method = "POST";
            request.ClientCertificates.Add(certificado);
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();

                return NormalizarStrings.RemoverAcentos(responseStr);
            }
            return null;
        }
    }
}
