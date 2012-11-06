using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Core.Almacen;

namespace SHomies.Core.Interfaces
{
    interface IProducto
    {
        int Id { get; set; }
        string Descripcion { get; set; }
        Categoria Categoria { get; set; }
        bool Estado { get; set; }
    }
}
