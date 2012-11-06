using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;

namespace SHomies.Tienda.Clases
{
    public class DetalleOrdenViewModel : Core.Venta.DetalleOrden
    {
        public string DescripcionProducto { get; set; }
        public string PrecioProducto { get; set; }
        public string Imagen { get; set; }

        public DetalleOrdenViewModel(IConexion iConexion)
            : base(iConexion)
        {

        }
    }
}
