using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;
using SHomies.Core.Almacen;

namespace SHomies.Core.Fimors.Services
{
    public class ProductoService : Abstracts.AProducto
    {
        public decimal PrecioDePago { get; set; }
        public Service Service { get; set; }

        public ProductoService(IConexion iConexion)
            : base(iConexion)
        {
            this.Service = new Service(iConexion);
        }
    }
}
