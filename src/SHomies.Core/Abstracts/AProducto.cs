using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SHomies.Core.Abstracts
{
    public class AProducto : Conexion.EntidadConexion, Interfaces.IProducto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public Almacen.Categoria Categoria { get; set; }
        public bool Estado { get; set; }

        public AProducto()
        {

        }
        public AProducto(SHomies.Conexion.IConexion iConexion)
        {
            this.Conexion = iConexion;
        }
    }
}
