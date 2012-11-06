using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Entity.Almacen;

namespace SHomies.Entity.Interfaces
{
    interface IProducto
    {
        int Id { get; set; }
        string Descripcion { get; set; }
        Categoria Categoria { get; set; }
        bool Estado { get; set; }
        decimal PrecioVenta { get; set; }
    }
}
