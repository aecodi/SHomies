using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Core.Interfaces;

namespace SHomies.Core.Persona
{
    public class Persona : Conexion.EntidadConexion, IPersona
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }

        public Persona()
        {

        }
        public Persona(SHomies.Conexion.IConexion iConexion)
        {
            this.Conexion = iConexion;
        }
        public bool Registrar()
        {
            throw new NotImplementedException();
        }
    }
}
