using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class DniInvalidoException : Exception
    {
        static string mensajeBase = "DNI invalido.";

        public DniInvalidoException()
            : this(mensajeBase)
        { }

        public DniInvalidoException(Exception e)
            : base(mensajeBase, e)
        { }

        public DniInvalidoException(string message)
            : base(message)
        {
            
        }

        public DniInvalidoException(string message, Exception e):base (mensajeBase + message, e)
        {
        }
    }
}
