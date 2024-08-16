using System;

namespace Orram.Sefaz.Consulta.Exceptions
{
    public class SefazException : Exception
    {

        public SefazException(string mensagem)
            : base(mensagem) { }

    }
}
