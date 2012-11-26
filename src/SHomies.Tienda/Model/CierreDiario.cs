using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHomies.UI.Ventas.Model
{
  public class CierreDiario
  {
    public string Concepto { get; set; }
    public string Monto { get; set; }

    public List<CierreDiario> GetDatosCierre()
    {
      return new List<CierreDiario>{
        new CierreDiario { Concepto ="Pago Tarjeta", Monto="150.00"},
        new CierreDiario { Concepto ="Venta Diaria", Monto="1050.00"}
      };
    }
  }
}
