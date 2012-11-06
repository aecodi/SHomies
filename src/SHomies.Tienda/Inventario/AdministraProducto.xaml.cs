using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using SHomies.Core.Almacen;
using SHomies.Utilitario;
using System.IO;

namespace SHomies.Tienda.Inventario
{
    /// <summary>
    /// Interaction logic for AdministraProducto.xaml
    /// </summary>
    public partial class AdministraProducto : Window
    {
        EEstadoFormulario estadoFormulario;
        ETipoProceso tipoProceso;
        Core.Almacen.Producto producto;
        SHomies.Conexion.IConexion conexion;

        public AdministraProducto(SHomies.Conexion.IConexion iConexion)
        {
            InitializeComponent();
            producto = new Core.Almacen.Producto(iConexion);
            conexion = iConexion;
        }
        public AdministraProducto()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.estadoFormulario = EEstadoFormulario.Load;
            try
            {
                this.txbTituloDelFormulario.Text = "Administra Productos";

                this.cboCategoria.ItemsSource =
                    new Core.Almacen.Categoria(this.conexion)
                    .ListaPorEstado(true);

                this.cboUnidad.ItemsSource =
                    new Core.Almacen.Unidad(this.conexion)
                    .Listar();

                this.ListarProductos();
            }
            catch (Exception)
            {
                throw;
            }
            this.estadoFormulario = EEstadoFormulario.EndLoad;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtBuscaDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter ||
                    e.Key == Key.Back ||
                    e.Key == Key.Subtract)
                    this.BuscaProducto();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtBuscaDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Back ||
                    e.Key == Key.Subtract)
                    this.BuscaProducto();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void rbtActivo_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.rbtActivo.IsChecked.Value && this.estadoFormulario == EEstadoFormulario.EndLoad)
                {
                    this.ListarProductos();
                    this.LimpiaRegistro();
                }
                else
                {
                    this.dtgProductos.ItemsSource = null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void rbtInactivo_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.rbtInactivo.IsChecked.Value && this.estadoFormulario == EEstadoFormulario.EndLoad)
                {
                    this.ListarProductos();
                    this.LimpiaRegistro();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.tipoProceso = ETipoProceso.Nuevo;
                this.grbRegistrar.IsEnabled = true;
                this.btnGrabar.IsEnabled = true;
                this.btnCancelar.IsEnabled = true;
                this.btnModificar.IsEnabled = false;
                this.LimpiaRegistro();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.grbRegistrar.IsEnabled = false;
                this.btnGrabar.IsEnabled = false;
                this.btnCancelar.IsEnabled = false;
                LimpiaRegistro();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LimpiaRegistro()
        {
            this.txbId.Text = "No Generado";
            this.txtDescripcion.Text = string.Empty;
            this.txtPrecioVenta.Text = "0";
            this.txtStock.Text = "0";
            this.chkStockLimitado.IsChecked = true;
            this.chkEstado.IsChecked = false;
            this.cboCategoria.SelectedIndex = -1;
            this.cboUnidad.SelectedIndex = -1;
            this.imgProducto.Source = null;
            this.producto.Imagen = string.Empty;
        }

        private void btnGrabar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (this.tipoProceso)
                {
                    case ETipoProceso.Nuevo:
                        this.Registrar();
                        break;
                    case ETipoProceso.Modificar:
                        this.Modificar();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void chkStockLimitado_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.estadoFormulario == EEstadoFormulario.EndLoad)
                {
                    this.txtStock.IsEnabled = chkStockLimitado.IsChecked.Value;
                    this.txtStock.Text = "0";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnImagen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BitmapDecoder bitmap;
                Microsoft.Win32.OpenFileDialog directorio =
                    new Microsoft.Win32.OpenFileDialog();

                directorio.CheckFileExists = true;
                directorio.ValidateNames = true;
                directorio.Filter = "Imagenes png(*.png)|*png";

                if (directorio.ShowDialog() == true)
                {
                    using (Stream data = directorio.OpenFile())
                    {
                        bitmap = BitmapDecoder.Create(data,
                                BitmapCreateOptions.PreservePixelFormat,
                                BitmapCacheOption.OnLoad);
                        int width = bitmap.Frames[0].PixelWidth;
                        int height = bitmap.Frames[0].PixelHeight;

                        //if (width != 64 ||
                        //    height != 64)
                        //{
                        //    throw new Utilitario.ExepcionSHomies("Imagen debe ser de 64x64 pixeles");
                        //}
                        imgProducto.Source = bitmap.Frames[0];
                        this.producto.Imagen = Convert.ToBase64String(Funcion.ConvertStreamToBytes(directorio.OpenFile()));
                    }
                }
                else
                {
                    imgProducto.Source = null;
                }
            }
            catch (Utilitario.ExepcionSHomies es)
            {
                MessageBox.Show(es.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dtgProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.grbRegistrar.IsEnabled = false;

                if (this.estadoFormulario == EEstadoFormulario.EndLoad)
                {
                    if (this.dtgProductos.Items.Count > 0)
                    {
                        if (this.dtgProductos.SelectedCells[0].Item != null)
                        {
                            Clases.ProductoViewModel productoSeleccionado =
                                (Clases.ProductoViewModel)this.dtgProductos.SelectedCells[0].Item;

                            if (productoSeleccionado != null)
                            {
                                this.txbId.Text = productoSeleccionado.Id.ToString();
                                this.cboCategoria.SelectedValue = productoSeleccionado.Categoria.Id;
                                this.txtDescripcion.Text = productoSeleccionado.Descripcion;
                                this.txtPrecioVenta.Text = Funcion.FormatoDecimal(productoSeleccionado.PrecioVenta);
                                this.txbComision.Text = Funcion.FormatoDecimal(productoSeleccionado.Comision);
                                this.cboUnidad.SelectedValue = productoSeleccionado.Unidad.Id;
                                this.txtStock.Text = Funcion.FormatoDecimal(productoSeleccionado.Stock);
                                this.chkStockLimitado.IsChecked = productoSeleccionado.StockLimitado;
                                this.chkEstado.IsChecked = productoSeleccionado.Estado;                                
                                if (!string.IsNullOrEmpty(productoSeleccionado.Imagen))
                                {
                                    BitmapDecoder bitmap;

                                    using (Stream data = new MemoryStream(Convert.FromBase64String(productoSeleccionado.Imagen)))
                                    {
                                        bitmap = BitmapDecoder.Create(data,
                                                BitmapCreateOptions.PreservePixelFormat,
                                                BitmapCacheOption.OnLoad);
                                        int width = bitmap.Frames[0].PixelWidth;
                                        int height = bitmap.Frames[0].PixelHeight;

                                        //if (width != 64 ||
                                        //    height != 64)
                                        //{
                                        //    throw new Utilitario.ExepcionSHomies("Imagen debe ser de 64x64 pixeles");
                                        //}
                                        imgProducto.Source = bitmap.Frames[0];
                                        this.producto.Imagen = productoSeleccionado.Imagen;
                                    }
                                }
                                else
                                {
                                    imgProducto.Source = null;
                                    this.producto.Imagen = string.Empty;
                                }
                                this.grbRegistrar.IsEnabled = false;
                                this.btnGrabar.IsEnabled = false;
                                this.btnCancelar.IsEnabled = false;
                                this.btnModificar.IsEnabled = true;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.tipoProceso = ETipoProceso.Modificar;
                this.grbRegistrar.IsEnabled = true;
                this.btnGrabar.IsEnabled = true;
                this.btnCancelar.IsEnabled = true;
                this.btnModificar.IsEnabled = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void rbtTodos_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.estadoFormulario == EEstadoFormulario.EndLoad)
                {
                    this.ListarProductos();
                    this.LimpiaRegistro();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Registrar()
        {
            try
            {
                if (this.producto == null)
                    this.producto = new Producto(this.conexion);

                this.producto.Descripcion = this.txtDescripcion.Text;
                this.producto.Estado = this.chkEstado.IsChecked.Value;
                this.producto.StockLimitado = this.chkStockLimitado.IsChecked.Value;
                this.producto.PrecioVenta = Funcion.ConvertTo<decimal>(this.txtPrecioVenta.Text, 0.00);
                this.producto.Stock = Funcion.ConvertTo<decimal>(this.txtStock.Text, 0.00);
                this.producto.Categoria = new Categoria();
                this.producto.Unidad = new Unidad(this.conexion);
                this.producto.Categoria.Id = Funcion.ConvertTo<int>(this.cboCategoria.SelectedValue, 0);
                this.producto.Unidad.Id = Funcion.ConvertTo<int>(this.cboUnidad.SelectedValue, 0);
                this.producto.Comision = Funcion.ConvertTo<decimal>(this.txbComision.Text, 0.00);

                if (this.producto.Categoria.Id <= 0)
                    throw new ExepcionSHomies("Seleccione categoria del producto.");
                if (string.IsNullOrEmpty(this.producto.Descripcion))
                    throw new ExepcionSHomies("Ingrese descripción para el producto.");                
                if (this.producto.PrecioVenta <= 0)
                    throw new ExepcionSHomies("Precio de venta debe ser mayor a cero.");                
                if (this.producto.Unidad.Id <= 0)
                    throw new ExepcionSHomies("Seleccione unidad minima del producto.");
                if (this.producto.StockLimitado && this.producto.Stock <= 0)
                    throw new ExepcionSHomies("Stock debe ser mayor a cero.");

                this.producto.Conexion.BeginTransaction();
                if (this.producto.Registrar())
                {
                    this.txbId.Text = this.producto.Id.ToString();
                    this.grbRegistrar.IsEnabled = false;
                    this.btnGrabar.IsEnabled = false;
                    this.btnCancelar.IsEnabled = false;
                    this.btnModificar.IsEnabled = true;

                    this.producto.Conexion.Commit();

                    MessageBox.Show("Producto guardado con exito");

                    this.ListarProductos();
                }
            }
            catch (ExepcionSHomies sh)
            {
                MessageBox.Show(sh.Message);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.producto.Conexion.RoolBack();
            }
        }
        private void Modificar()
        {
            try
            {
                if (this.producto == null)
                    this.producto = new Producto(this.conexion);

                this.producto.Id = Funcion.ConvertTo<int>(this.txbId.Text, 0);
                this.producto.Descripcion = this.txtDescripcion.Text;
                this.producto.Estado = this.chkEstado.IsChecked.Value;
                this.producto.StockLimitado = this.chkStockLimitado.IsChecked.Value;
                this.producto.PrecioVenta = Funcion.ConvertTo<decimal>(this.txtPrecioVenta.Text, 0.00);
                this.producto.Stock = Funcion.ConvertTo<decimal>(this.txtStock.Text, 0.00);
                this.producto.Categoria = new Categoria();
                this.producto.Unidad = new Unidad(this.conexion);
                this.producto.Categoria.Id = Funcion.ConvertTo<int>(this.cboCategoria.SelectedValue, 0);
                this.producto.Unidad.Id = Funcion.ConvertTo<int>(this.cboUnidad.SelectedValue, 0);
                this.producto.Comision = Funcion.ConvertTo<decimal>(this.txbComision.Text, 0.00);

                if (this.producto.Id <= 0)
                    throw new ExepcionSHomies("Codigo del producto no valido.");
                if (this.producto.Categoria.Id <= 0)
                    throw new ExepcionSHomies("Seleccione categoria del producto.");
                if (string.IsNullOrEmpty(this.producto.Descripcion))
                    throw new ExepcionSHomies("Ingrese descripción para el producto.");                
                if (this.producto.PrecioVenta <= 0)
                    throw new ExepcionSHomies("Precio de venta debe ser mayor a cero.");
                if (this.producto.Unidad.Id <= 0)
                    throw new ExepcionSHomies("Seleccione unidad minima del producto.");
                if (this.producto.StockLimitado && this.producto.Stock <= 0)
                    throw new ExepcionSHomies("Stock debe ser mayor a cero.");

                this.producto.Conexion.BeginTransaction();
                if (this.producto.Modificar())
                {
                    this.txbId.Text = this.producto.Id.ToString();                   

                    this.producto.Conexion.Commit();

                    MessageBox.Show("Producto modificado con exito");

                    this.ListarProductos();

                    this.btnModificar.IsEnabled = true;
                }
            }
            catch (ExepcionSHomies sh)
            {
                MessageBox.Show(sh.Message);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.producto.Conexion.RoolBack();
            }
        }

        private void ListarProductos()
        {
            try
            {
                this.btnModificar.IsEnabled = false;
                this.btnCancelar.IsEnabled = false;
                this.btnGrabar.IsEnabled = false;

                if (!this.rbtTodos.IsChecked.Value)
                {
                    bool estado = this.rbtActivo.IsChecked.Value;
                    new Clases.ProductoViewModel(this.conexion).ListarPorEstado(ref this.dtgProductos, estado);
                }
                else
                {
                    new Clases.ProductoViewModel(this.conexion).Listar(ref this.dtgProductos);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void BuscaProducto()
        {
            try
            {
                string descripcion = this.txtBuscaDescripcion.Text;

                if (!string.IsNullOrEmpty(descripcion))
                {
                    if (!this.rbtTodos.IsChecked.Value)
                    {
                        bool estado = this.rbtActivo.IsChecked.Value;

                        new Clases.ProductoViewModel(this.conexion).BuscarPorDescripcionYEstado(ref this.dtgProductos, estado, descripcion);
                    }
                    else
                    {
                        new Clases.ProductoViewModel(this.conexion).BuscarPorDescripcion(ref this.dtgProductos, descripcion);
                    }
                }
                else
                {
                    this.ListarProductos();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
