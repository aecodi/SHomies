using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;

namespace SHomies.Core.Persona
{
    public class Cliente : Persona
    {
        public int IdCliente { get; set; }
        public Cliente()
        {

        }
        public Cliente(IConexion iConexion)
            : base(iConexion)
        {

        }
    }
}
