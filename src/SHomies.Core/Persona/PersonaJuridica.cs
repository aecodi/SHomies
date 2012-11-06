using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;

namespace SHomies.Core.Persona
{
    public class PersonaJuridica : Persona
    {
        public PersonaJuridica(IConexion iConexion)
            : base(iConexion)
        {

        }
    }
}
