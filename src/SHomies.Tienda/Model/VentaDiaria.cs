using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHomies.UI.Ventas.Model
{
  public class VentaDiaria
  {
    public string Producto { get; set; }
    public int Cantidad { get; set; }
    public string Total { get; set; }

    public List<VentaDiaria> GetDatosVentaDiaria()
    {
      return new List<VentaDiaria>{
        new VentaDiaria { Producto ="Cerveza",Cantidad=1, Total="150.00"},
        new VentaDiaria { Producto ="Wisky", Cantidad=1, Total="1050.00"}
      };
    }
  }
}
