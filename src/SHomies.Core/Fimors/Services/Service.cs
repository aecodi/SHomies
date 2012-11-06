using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;

namespace SHomies.Core.Fimors.Services
{
    public class Service : Persona.PersonaJuridica
    {
        public List<ProductoService> Productos { get; set; }

        public Service(IConexion iConexion)
            : base(iConexion)
        {

        }
    }
}
