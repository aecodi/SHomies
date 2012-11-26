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
using SHomies.Utilitario;

namespace SHomies.Tienda.Venta
{
    /// <summary>
    /// Interaction logic for Nueva.xaml
    /// </summary>
    public partial class Nueva : Window
    {
        private Conexion.IConexion conexion;
        private Entity.Almacen.Producto productoVender;
        private EEstadoFormulario estadoFormulario;
        private Core.Venta.Orden orden;
        private List<Core.Planilla.Trabajador> listaChicas;
        private List<Clases.DetalleFichajeViewModel> detalleFichaje;

        public Nueva()
        {
            InitializeComponent();
        }

        public Nueva(Core.Sistema.AuditoriaSistema iAuditoria, Conexion.IConexion iConexion)
        {
            this.conexion = iConexion;
            this.orden = new Core.Venta.Orden(this.conexion)
            {
                AuditoriaSistema = iAuditoria,
                Mozo = new Core.Sistema.Usuario(this.conexion)
                {
                    Id = 3
                },
                Mesa = new Core.Venta.Mesa(this.conexion)
                {
                    Id = 999
                },
                Cliente = new Core.Persona.Cliente(this.conexion)
                {
                    Id = 5
                },
                Estado = new Core.Venta.EstadoVenta
                {
                    Codigo = "P"
                },
                TipoPago = Core.Venta.TipoPago.EFECTIVO,
                TipoComprobante = Core.Venta.TipoComprobante.BOLETA
            };
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.estadoFormulario = EEstadoFormulario.Load;
            try
            {
                this.txbTituloDelFormulario.Text = "Nueva Orden";

                List<Entity.Almacen.Categoria> listaCategorias = new Core.Almacen.Categoria(this.conexion)
                                                                 .ListaPorEstado(true);

                if (listaCategorias.Count > 0)
                {
                    this.dtgCategorias.ItemsSource = listaCategorias;
                }
                else
                {
                    throw new Exception("No existen categorias configuradas");
                }

                this.GetNextOrden();

                detalleFichaje =
                    new List<Clases.DetalleFichajeViewModel>();

                listaChicas =
                    new Core.Planilla.Trabajador(this.conexion)
                    .ListaPorCargo(new Core.Planilla.Cargo() { Id = 3 });

                listaChicas = listaChicas.Where(x => x.Estado).ToList();

                this.dtgFichadoras.Items.Clear();

                this.SeteaFichadoras();

                this.txtNroOrden.Focus();
                this.lblAnulada1.Visibility = System.Windows.Visibility.Hidden;
                this.lblAnulada2.Visibility = System.Windows.Visibility.Hidden;
                this.dtgDetalleVenta.Items.Clear();

                this.estadoFormulario = EEstadoFormulario.EndLoad;
            }
            catch (Exception ex)
            {
                this.estadoFormulario = EEstadoFormulario.ErrorLoad;
                MessageBox.Show(ex.Message);
            }
            Clases.FuncionFormulario.ValidaCargaFormulario(estadoFormulario, this);
        }

        private void SeteaFichadoras()
        {
            this.detalleFichaje = new List<Clases.DetalleFichajeViewModel>();
            foreach (Core.Planilla.Trabajador item in listaChicas)
            {
                detalleFichaje.Add(
                    new Clases.DetalleFichajeViewModel(this.conexion)
                    {
                        Fichadora = item,
                        NombreFichadora = item.Nombres,
                        MontoFichaje = Funcion.FormatoDecimal(0)
                    }
                    );
            }
        }

        private void LlenaChicas()
        {
            this.SeteaFichadoras();
            this.dtgFichadoras.ItemsSource = detalleFichaje.OrderBy(x => x.NombreFichadora);
        }

        private void btnNumber_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.productoVender != null)
                {
                    Button Number = (Button)sender;

                    this.txbCantidad.Text = this.txbCantidad.Text.ToString() == "0" ?
                                            Number.Content.ToString() :
                                            String.Concat(this.txbCantidad.Text, Number.Content.ToString());

                    int cantidad = Convert.ToInt32(this.txbCantidad.Text);
                    this.txbTotal.Text = Funcion.FormatoDecimal(cantidad * this.productoVender.PrecioVenta);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void btnCleanCantidad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button Number = (Button)sender;

                this.txbCantidad.Text = "0";
                this.txbTotal.Text = Funcion.FormatoDecimal(0);
            }
            catch (Exception ex)
            {

            }
        }

        private void dtgCategorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.estadoFormulario == EEstadoFormulario.EndLoad)
                {
                    if (this.dtgCategorias.Items.Count > 0 && this.dtgCategorias.SelectedIndex >= 0)
                    {
                        if (this.dtgCategorias.SelectedCells[0].Item != null)
                        {
                            Entity.Almacen.Categoria categoriaSeleccionada =
                                (Entity.Almacen.Categoria)this.dtgCategorias.SelectedCells[0].Item;

                            if (categoriaSeleccionada != null)
                            {
                                this.dtgProductos.ItemsSource = new Core.Almacen.Producto(this.conexion)
                                                    .ListaPorCategoria(categoriaSeleccionada);

                                this.txbNameProduct.Text = string.Empty;
                                this.txbPrecio.Text = Funcion.FormatoDecimal(0);
                                this.txbTotal.Text = Funcion.FormatoDecimal(0);
                                this.txbCantidad.Text = "0";
                                this.txbComision.Text = Funcion.FormatoDecimal(0);
                                this.grdIngresaDetalle.IsEnabled = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtgProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.estadoFormulario == EEstadoFormulario.EndLoad)
                {
                    if (this.dtgProductos.Items.Count > 0 && this.dtgProductos.SelectedIndex >= 0)
                    {
                        if (this.dtgProductos.SelectedCells[0].Item != null)
                        {
                            productoVender =
                                (Entity.Almacen.Producto)this.dtgProductos.SelectedCells[0].Item;

                            if (productoVender != null)
                            {
                                this.txbNameProduct.Text = productoVender.Descripcion;
                                this.txbPrecio.Text = Funcion.FormatoDecimal(productoVender.PrecioVenta);
                                this.txbTotal.Text = Funcion.FormatoDecimal(0);
                                this.txbCantidad.Text = "0";
                                this.txbComision.Text = Funcion.FormatoDecimal(productoVender.Comision);
                                this.grdIngresaDetalle.IsEnabled = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNueva_Click(object sender, RoutedEventArgs e)
        {
            this.NuevaOrden();
        }
        private void NuevaOrden()
        {
            try
            {
                this.GetNextOrden();

                this.LlenaChicas();

                this.orden.DetalleProducto.Clear();
                this.orden.Fichadoras.Clear();

                this.LimpiaDatos();

                this.brdProducto.IsEnabled = true;
                this.grdIngresaDetalle.IsEnabled = false;


                this.dtgCategorias.IsEnabled = true;
                this.dtgProductos.IsEnabled = true;
                this.txtBuscar.IsEnabled = true;
                this.dtgFichadoras.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LimpiaDatos()
        {
            try
            {
                this.txbNameProduct.Text = string.Empty;
                this.txbPrecio.Text = Funcion.FormatoDecimal(0);
                this.txbTotal.Text = Funcion.FormatoDecimal(0);
                this.txbCantidad.Text = "0";
                this.txbComision.Text = Funcion.FormatoDecimal(0);

                this.dtgDetalleVenta.ItemsSource = null;
                this.dtgFichadoras.ItemsSource = null;
                this.dtgProductos.ItemsSource = null;
                this.dtgCategorias.SelectedIndex = -1;


                this.lblAnulada1.Visibility = System.Windows.Visibility.Hidden;
                this.lblAnulada2.Visibility = System.Windows.Visibility.Hidden;
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.productoVender == null || this.productoVender.Id == 0)
                    throw new Utilitario.ExepcionSHomies("Selecciones un producto para la venta.");

                Core.Venta.DetalleOrden productoAgregar = new Core.Venta.DetalleOrden(this.conexion);
                productoAgregar.Cantidad = Funcion.ConvertTo<int>(this.txbCantidad.Text, 0);
                productoAgregar.Total = this.productoVender.PrecioVenta * productoAgregar.Cantidad;
                productoAgregar.Producto = new Core.Almacen.Producto(this.conexion)
                {
                    Id = this.productoVender.Id,
                    Descripcion = this.productoVender.Descripcion,
                    PrecioVenta = this.productoVender.PrecioVenta,
                    Imagen = this.productoVender.Imagen,
                    Comision = this.productoVender.Comision
                };
                productoAgregar.Orden = this.orden;

                if (productoAgregar.Cantidad == 0)
                    throw new Utilitario.ExepcionSHomies("No se ha especificado la cantidad a vender del producto.");

                Core.Venta.DetalleOrden producto = this.orden.DetalleProducto.Find(x => x.Producto.Id == productoAgregar.Producto.Id);
                if (producto != null)
                    this.orden.DetalleProducto.Remove(producto);

                this.orden.DetalleProducto.Add(productoAgregar);

                this.DetalleOrden();
                this.DistribulleComision();
            }
            catch (Utilitario.ExepcionSHomies sx)
            {
                MessageBox.Show(sx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DetalleOrden()
        {
            try
            {
                List<Clases.DetalleOrdenViewModel> detalle =
                    new List<Clases.DetalleOrdenViewModel>();

                foreach (Core.Venta.DetalleOrden item in this.orden.DetalleProducto)
                {
                    detalle.Add(
                        new Clases.DetalleOrdenViewModel(this.conexion)
                        {
                            DescripcionProducto = item.Producto.Descripcion,
                            PrecioProducto = Funcion.FormatoDecimal(item.Producto.PrecioVenta),
                            Cantidad = item.Cantidad,
                            Imagen = item.Producto.Imagen
                        }
                        );
                }

                this.dtgDetalleVenta.ItemsSource = detalle.OrderBy(x => x.DescripcionProducto);
                this.txbTotalVenta.Text = Funcion.FormatoDecimal(this.orden.DetalleProducto.Sum(x => x.Total));
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GetNextOrden()
        {
            this.orden.GetNumeroOrden();
            this.txbNroOrden.Text = this.orden.Id.ToString();
        }

        private void btnGrabar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new Core.Operaciones.Validaciones.ValidaAperturaSistema(new Core.Sistema.AuditoriaSistema(this.conexion)).Ejecutar();

                if (this.orden.Id == 0)
                    throw new Utilitario.ExepcionSHomies("Número de la orden no valido.");

                if (this.orden.DetalleProducto.Count == 0)
                    throw new Utilitario.ExepcionSHomies("No se ha ingresado detalle de la orden.");

                this.orden.Conexion.BeginTransaction();

                this.GetFichadoras();

                this.orden.Registrar();
                this.orden.RegistraDetalle();
                this.orden.RegistraFichaje();

                //MessageBox.Show("Orden guardada, se procedera a imprimir ...");

                this.grdIngresaDetalle.IsEnabled = false;
                this.brdProducto.IsEnabled = false;
                this.btnAnular.IsEnabled = true;
                this.chkBuscar.IsEnabled = false;
                this.txtBuscar.IsEnabled = false;
                this.dtgCategorias.IsEnabled = false;
                this.dtgProductos.IsEnabled = false;
                this.dtgFichadoras.IsEnabled = false;

                this.orden.Conexion.Commit();

                this.ImprimirBoleto();
            }
            catch (Utilitario.ExepcionSHomies sx)
            {
                MessageBox.Show(sx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.orden.Conexion.RoolBack();
            }
        }

        private void GetFichadoras()
        {
            try
            {
                this.dtgFichadoras.ItemsSource = this.detalleFichaje.Where(x => x.IsAddToOrden).ToList();
                foreach (Clases.DetalleFichajeViewModel fichadora in this.detalleFichaje.Where(x => x.IsAddToOrden).ToList())
                {
                    this.orden.Fichadoras.Add(
                        new Core.Venta.Fichaje
                        {
                            Conexion = this.conexion,
                            Orden = this.orden,
                            Fichadora = fichadora.Fichadora,
                            Monto = Funcion.ConvertTo<decimal>(fichadora.MontoFichaje, 0)
                        });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ImprimirBoleto()
        {
            if (this.orden.DetalleProducto.Count > 0)
            {
                Ticket ticket = new Ticket();
                ticket.Title = "EL RELAX";
                ticket.AddCabecera("TICKET", Alineacion.Left, Alineacion.Left, 10, this.orden.Id);
                ticket.AddCabecera("CAJERO", Alineacion.Left, Alineacion.Left, 10, this.orden.AuditoriaSistema.Usuario.UserName);
                ticket.AddCabecera("FECHA", Alineacion.Left, Alineacion.Left, 10, DateTime.Now);
                ticket.AddCebeceraDetalle("Cant.", Alineacion.Left, Alineacion.Left, 6);
                ticket.AddCebeceraDetalle("Descripción", Alineacion.Left, Alineacion.Left, 20);
                ticket.AddCebeceraDetalle("Importe", Alineacion.Right, Alineacion.Right, 9);

                foreach (Core.Venta.DetalleOrden detalle in this.orden.DetalleProducto)
                {
                    ticket.AddItemsDetails(detalle.Cantidad, detalle.Producto.Descripcion, Funcion.FormatoDecimal(detalle.Total));
                }
                ticket.AddTotal("Total Venta", Alineacion.Right, Alineacion.Right, 25, Funcion.FormatoDecimal(this.orden.DetalleProducto.Sum(o => o.Total)));

                foreach (Core.Venta.Fichaje fichaje in this.orden.Fichadoras)
                {
                    ticket.AddTotal(fichaje.Fichadora.Nombres, Alineacion.Right, Alineacion.Right, 25, Funcion.FormatoDecimal(fichaje.Monto));
                }
                ticket.itemsPie.Add("Los esperamos");
                String impresora = System.Configuration.ConfigurationSettings.AppSettings["Impresora"].ToString();

                ticket.Imprimir(impresora);
            }
        }

        private void btnReimprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.ImprimirBoleto();
            }
            catch (Utilitario.ExepcionSHomies sx)
            {
                MessageBox.Show(sx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAnular_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.orden.Estado.Codigo != "A")
                {
                    this.orden.Conexion.BeginTransaction();
                    this.orden.Anular();
                    new Core.Venta.DetalleOrden(this.orden.Conexion)
                    {
                        Orden = this.orden
                    }.Anular();
                    new Core.Venta.Fichaje(this.orden.Conexion)
                    {
                        Orden = this.orden
                    }.Anular();

                    MessageBox.Show(string.Concat("Ticket anulado con exito. Nro Ticket = ", this.orden.Id));

                    this.orden.Conexion.Commit();

                    this.btnAnular.IsEnabled = false;
                    this.lblAnulada1.Visibility = System.Windows.Visibility.Visible;
                    this.lblAnulada2.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    MessageBox.Show(string.Concat("Ticket ya se encuentra anulado. Nro Ticket = ", this.orden.Id));
                }
            }
            catch (Utilitario.ExepcionSHomies sx)
            {
                MessageBox.Show(sx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.orden.Conexion.RoolBack();
            }
        }

        private void txtNroOrden_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    this.BuscarOrden();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BuscarOrden()
        {
            try
            {
                LimpiaDatos();

                Core.Venta.Orden ordenBuscar =
                        new Core.Venta.Orden(this.conexion)
                        {
                            Id = Utilitario.Funcion.ConvertTo<int>(this.txtNroOrden.Text.Trim(), 0)
                        };

                if (ordenBuscar.Id != 0)
                {
                    ordenBuscar.GetDatosOrden();


                    this.orden = new Core.Venta.Orden(this.conexion);
                    this.orden = ordenBuscar;

                    this.DetalleOrden();
                    this.LlenaFichadorasOrden();

                    this.grdIngresaDetalle.IsEnabled = false;
                    this.brdProducto.IsEnabled = false;
                    this.dtgCategorias.IsEnabled = false;
                    this.dtgProductos.IsEnabled = false;
                    this.dtgFichadoras.IsEnabled = false;

                    if (this.orden.Estado.Codigo == "A")
                    {
                        this.lblAnulada1.Visibility = System.Windows.Visibility.Visible;
                        this.lblAnulada2.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        this.btnAnular.IsEnabled = true;
                    }
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
        private void LlenaFichadorasOrden()
        {
            SeteaFichadoras();

            foreach (Core.Venta.Fichaje fichadora in this.orden.Fichadoras)
            {
                this.detalleFichaje.Find(x => x.Fichadora.Id == fichadora.Fichadora.Id).IsAddToOrden = true;
                this.detalleFichaje.Find(x => x.Fichadora.Id == fichadora.Fichadora.Id).MontoFichaje = Funcion.FormatoDecimal(fichadora.Monto);
            }
            this.detalleFichaje = this.detalleFichaje.Where(x => x.IsAddToOrden).ToList();
            this.DistribulleComision();
        }
        private void DistribulleComision()
        {
            decimal totalComision = this.orden.DetalleProducto.Sum(x => x.Producto.Comision * x.Cantidad);
            decimal nroChicas = this.detalleFichaje.Where(x => x.IsAddToOrden).Count();

            decimal montoPorChica = totalComision / (nroChicas > 0 ? nroChicas : 1);

            foreach (Clases.DetalleFichajeViewModel ficha in this.detalleFichaje)
            {
                ficha.MontoFichaje = ficha.IsAddToOrden ? montoPorChica.ToString("##0.00") : "0.00";
            };
            this.detalleFichaje = this.detalleFichaje
                                      .OrderByDescending(x => x.IsAddToOrden)
                                      .ToList();

            LlenaListaFichadoras(this.detalleFichaje);

            this.txbTotalComision.Text = Funcion.FormatoDecimal(totalComision);
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.BuscarOrden();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                this.BuscaChica(((TextBox)sender).Text);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Back)
                    this.BuscaChica(((TextBox)sender).Text);
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void BuscaChica(string iNombreChica)
        {
            if (!string.IsNullOrEmpty(iNombreChica))
            {
                List<Clases.DetalleFichajeViewModel> busqueda = this.detalleFichaje.Where(x => x.NombreFichadora.ToUpper().Contains(iNombreChica.ToUpper()) && !x.IsAddToOrden).ToList();
                List<Clases.DetalleFichajeViewModel> newFichaje = this.detalleFichaje.Where(x => x.IsAddToOrden).ToList();
                newFichaje.AddRange(busqueda);
                newFichaje = newFichaje.OrderByDescending(x => x.IsAddToOrden)
                                       .ToList();

                LlenaListaFichadoras(newFichaje);
            }
            else
            {
                LlenaListaFichadoras(this.detalleFichaje);
            }
        }
        private void IsAddFichadora_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                int? indice = this.dtgFichadoras.SelectedIndex;
                if (indice != null && indice >= 0)
                {
                    Clases.DetalleFichajeViewModel fichaje = (Clases.DetalleFichajeViewModel)this.dtgFichadoras.SelectedItem;
                    Clases.DetalleFichajeViewModel datosFichaje = this.detalleFichaje.Find(x => x.NombreFichadora == fichaje.NombreFichadora);

                    if (datosFichaje.IsAddToOrden != ((CheckBox)sender).IsChecked.Value)
                    {
                        datosFichaje.IsAddToOrden = ((CheckBox)sender).IsChecked.Value;

                        DistribulleComision();

                        BuscaChica(this.txtBuscar.Text);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void LlenaListaFichadoras(List<Clases.DetalleFichajeViewModel> iData)
        {
            this.dtgFichadoras.ItemsSource = null;
            this.dtgFichadoras.ItemsSource = iData;
        }
    }
}
