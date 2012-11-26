using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHomies.Utilitario;

namespace SHomies.UI.Ventas.Model
{
    public class VentaFichadoras
    {
        private string montoMulta;

        public string NombreFichadora { get; set; }
        public string MontoFichaje { get; set; }

        public string MontoMulta
        {
            get { return string.IsNullOrEmpty(montoMulta) ? "0" : montoMulta; }
            set
            {
                montoMulta = string.IsNullOrEmpty(value) ? "0" : value;
            }
        }

        public string MontoPago
        {
            get { return Funcion.FormatoDecimal(Convert.ToDecimal(MontoFichaje) - (string.IsNullOrEmpty(MontoMulta) ? 0 : decimal.Parse(MontoMulta))); }
        }

        public List<VentaFichadoras> GetDatosFichaje()
        {
            return new List<VentaFichadoras>{
        new VentaFichadoras { NombreFichadora ="Perrita", MontoFichaje="150.00"},
        new VentaFichadoras { NombreFichadora ="Gatita", MontoFichaje="100.00"}
      };
        }
    }
}
