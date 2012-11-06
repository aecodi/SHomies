using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using SHomies.Conexion;

namespace SHomies.Tienda.Clases
{
    public class ProductoViewModel : Entity.Almacen.Producto
    {
        public string DescripcionCategoria { get; set; }
        private IConexion conexion;
        public ProductoViewModel(IConexion iConexion)
        {
            this.conexion = iConexion;
        }
        public ProductoViewModel()
        {

        }
        public void Listar(ref DataGrid iGrillaProductos)
        {
            try
            {
                List<Entity.Almacen.Producto> listarProductos
                       = new Core.Almacen.Producto(this.conexion)
                       .Listar();

                this.SetItemsSourceGrilla(ref iGrillaProductos, listarProductos);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ListarPorEstado(ref DataGrid iGrillaProductos, bool iEstado)
        {
            try
            {
                List<Entity.Almacen.Producto> listarProductos
                      = new Core.Almacen.Producto(this.conexion)
                      .ListarPorEstado(iEstado);

                this.SetItemsSourceGrilla(ref iGrillaProductos, listarProductos);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void BuscarPorDescripcionYEstado(ref DataGrid iGrillaProductos, bool iEstado, string iDescripcion)
        {
            try
            {
                List<Entity.Almacen.Producto> listarProductos
                       = new Core.Almacen.Producto(this.conexion)
                       .BuscarPorDescripcionYEstado(iEstado, iDescripcion);

                this.SetItemsSourceGrilla(ref iGrillaProductos, listarProductos);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void BuscarPorDescripcion(ref DataGrid iGrillaProductos, string iDescripcion)
        {
            try
            {
                List<Entity.Almacen.Producto> listarProductos
                       = new Core.Almacen.Producto(this.conexion)
                       .BuscarPorDescripcion(iDescripcion);

                this.SetItemsSourceGrilla(ref iGrillaProductos, listarProductos);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void SetItemsSourceGrilla(ref DataGrid iGrillaProductos, List<Entity.Almacen.Producto> iListarProductos)
        {
            try
            {
                iGrillaProductos.ItemsSource = null;
                if (iListarProductos.Count > 0)
                {
                    var productos =
                        (from data in iListarProductos
                         select new Clases.ProductoViewModel
                         {
                             Descripcion = data.Descripcion,
                             Id = data.Id,
                             Imagen = data.Imagen,
                             Estado = data.Estado,
                             PrecioCompra = data.PrecioCompra,
                             PrecioVenta = data.PrecioVenta,
                             Stock = data.Stock,
                             StockLimitado = data.StockLimitado,
                             Unidad = data.Unidad,
                             DescripcionCategoria = data.Categoria.Descripcion,
                             Categoria = data.Categoria,
                             Comision = data.Comision
                         });
                    iGrillaProductos.ItemsSource = productos;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
