using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SHomies.Entity.Persona
{
    [Serializable]
    public class TipoDocumento
    {
        public string Codigo { get; set; }
        public int Longitud { get; set; }
        public string TipoDato { get; set; }
        public string TipoPersona { get; set; }
        public TipoDocumento() { }
    }
}
