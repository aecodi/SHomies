using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SHomies.Core.Persona
{
    public class TipoDocumento : Entity.Persona.TipoDocumento
    {
        public List<Entity.Persona.TipoDocumento> Lista()
        {
            List<Entity.Persona.TipoDocumento> tipoDocumentos = new List<Entity.Persona.TipoDocumento>() { 
            new Entity.Persona.TipoDocumento {
                 Codigo="DNI",
                 Longitud = 8,
                 TipoDato="N",
                 TipoPersona ="N"
            },
             new Entity.Persona.TipoDocumento {
                 Codigo="RUC",
                 Longitud = 11,
                 TipoDato="N",
                 TipoPersona ="J"
            }
            };

            return tipoDocumentos;
        }
    }
}
