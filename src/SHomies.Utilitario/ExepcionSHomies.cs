using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SHomies.Utilitario
{
    public class ExepcionSHomies : Exception
    {
        public ExepcionSHomies(string iMensaje) : base(iMensaje) { }
    }
}
