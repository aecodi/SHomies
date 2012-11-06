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
using SHomies.Core.Almacen;
using SHomies.Conexion;
using SHomies.Utilitario;
using System.IO;

namespace SHomies.Tienda.Inventario
{
    /// <summary>
    /// Interaction logic for AdministraCategoria.xaml
    /// </summary>
    public partial class AdministraCategoria : Window
    {
        EEstadoFormulario estadoFormulario;
        ETipoProceso tipoProceso;

        Categoria categoria;
        List<SHomies.Entity.Almacen.Categoria> listaCategorias;

        public AdministraCategoria(SHomies.Conexion.IConexion iConexion)
        {
            InitializeComponent();
            this.categoria = new Categoria(iConexion);
        }
        public AdministraCategoria()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            estadoFormulario = EEstadoFormulario.Load;
            this.txbTituloDelFormulario.Text = "Administra Categorias";

            try
            {
                this.listaCategorias = this.categoria.ListaPorEstado(true);
                if (this.listaCategorias != null)
                {
                    this.dtgCategorias.ItemsSource = this.listaCategorias;
                }
            }
            catch (Exception)
            {
                throw;
            }
            estadoFormulario = EEstadoFormulario.EndLoad;
        }

        private void rbtActivo_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.estadoFormulario == EEstadoFormulario.EndLoad)
                {

                    if (this.rbtActivo.IsChecked.Value)
                    {
                        this.listaCategorias = this.categoria.ListaPorEstado(true);
                        this.LimpiarRegistro();
                        this.LlenaCategorias();
                    }
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
                if (this.estadoFormulario == EEstadoFormulario.EndLoad)
                {
                    if (this.rbtInactivo.IsChecked.Value)
                    {
                        this.listaCategorias = this.categoria.ListaPorEstado(false);
                        this.LimpiarRegistro();
                        this.LlenaCategorias();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LlenaCategorias()
        {
            this.estadoFormulario = EEstadoFormulario.Load;

            this.dtgCategorias.ItemsSource = null;
            if (this.listaCategorias != null)
                if (this.listaCategorias.Count > 0)
                    this.dtgCategorias.ItemsSource = this.listaCategorias;

            this.estadoFormulario = EEstadoFormulario.EndLoad;
        }

        private void LimpiarRegistro()
        {
            this.txbId.Text = "No generado";
            this.txtDescripcion.Text = string.Empty;
            this.chkEstado.IsChecked = false;
            this.imgCategoria.Source = null;
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.grbRegistrar.IsEnabled = true;
                this.LimpiarRegistro();
                this.btnCancelar.IsEnabled = true;
                this.btnGrabar.IsEnabled = true;
                this.imgCategoria.Source = null;
                this.tipoProceso = ETipoProceso.Nuevo;
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
                this.LimpiarRegistro();
                this.btnCancelar.IsEnabled = false;
                this.btnGrabar.IsEnabled = false;
                this.imgCategoria.Source = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dtgCategorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.grbRegistrar.IsEnabled = false;

                if (this.estadoFormulario == EEstadoFormulario.EndLoad)
                {
                    if (this.dtgCategorias.Items.Count > 0)
                    {
                        if (this.dtgCategorias.SelectedCells[0].Item != null)
                        {
                            SHomies.Entity.Almacen.Categoria categoria =
                                (SHomies.Entity.Almacen.Categoria)this.dtgCategorias.SelectedCells[0].Item;

                            if (categoria != null)
                            {
                                this.txbId.Text = categoria.Id.ToString();
                                this.txtDescripcion.Text = categoria.Descripcion;
                                this.chkEstado.IsChecked = categoria.Estado;
                                this.btnModificar.IsEnabled = true;
                                if (!string.IsNullOrEmpty(categoria.Imagen))
                                {
                                    BitmapDecoder bitmap;

                                    using (Stream data = new MemoryStream(Convert.FromBase64String(categoria.Imagen)))
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
                                        imgCategoria.Source = bitmap.Frames[0];
                                        this.categoria.Imagen = categoria.Imagen;
                                    }
                                }
                                else
                                {
                                    imgCategoria.Source = null;
                                    this.categoria.Imagen = string.Empty;
                                }
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
                if (ExpresionRegular.IsNumeric(this.txbId.Text.Trim()))
                {
                    this.grbRegistrar.IsEnabled = true;
                    this.btnGrabar.IsEnabled = true;
                    this.btnCancelar.IsEnabled = true;
                    this.tipoProceso = ETipoProceso.Modificar;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnGrabar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                switch (this.tipoProceso)
                {
                    case ETipoProceso.Nuevo: this.Registrar();
                        break;
                    case ETipoProceso.Modificar: this.Modificar();
                        break;
                    default:
                        this.grbRegistrar.IsEnabled = false;
                        this.LimpiarRegistro();
                        throw new Exception("Operación no se puede realizar");
                }
                this.btnCancelar.IsEnabled = false;
                this.btnGrabar.IsEnabled = false;
                this.grbRegistrar.IsEnabled = false;
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

        private void Registrar()
        {
            try
            {
                this.categoria.Conexion.BeginTransaction();

                this.categoria.Descripcion = this.txtDescripcion.Text;
                this.categoria.Estado = this.chkEstado.IsChecked.Value;

                if (string.IsNullOrEmpty(this.categoria.Descripcion))
                    throw new Utilitario.ExepcionSHomies("Ingrese descripción de la categoria");

                this.categoria.Registrar();

                this.txbId.Text = this.categoria.Id.ToString();

                if ((this.rbtActivo.IsChecked == this.categoria.Estado) ||
                    (this.rbtInactivo.IsChecked == !this.categoria.Estado))
                    this.listaCategorias.Add(
                        new Entity.Almacen.Categoria
                        {
                            Id = this.categoria.Id,
                            Descripcion = this.categoria.Descripcion,
                            Estado = this.categoria.Estado,
                            Imagen = this.categoria.Imagen
                        });

                this.LlenaCategorias();

                this.categoria.Conexion.Commit();

                MessageBox.Show("Proceso completado con exito");
            }
            catch (Utilitario.ExepcionSHomies es)
            {
                MessageBox.Show(es.Message);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.categoria.Conexion.RoolBack();
            }
        }

        private void Modificar()
        {
            try
            {
                this.categoria.Conexion.BeginTransaction();

                this.categoria.Id = Funcion.ConvertTo<int>(this.txbId.Text);
                this.categoria.Descripcion = this.txtDescripcion.Text;
                this.categoria.Estado = this.chkEstado.IsChecked.Value;

                if (string.IsNullOrEmpty(this.categoria.Descripcion))
                    throw new Utilitario.ExepcionSHomies("Ingrese descripción de la categoria");

                this.categoria.Modificar();

                this.txbId.Text = this.categoria.Id.ToString();
                Entity.Almacen.Categoria categoriaModificar = this.listaCategorias.Find(x => x.Id == this.categoria.Id);

                if (categoriaModificar != null)
                {
                    this.listaCategorias.Find(x => x.Id == this.categoria.Id).Descripcion = this.categoria.Descripcion;
                    this.listaCategorias.Find(x => x.Id == this.categoria.Id).Estado = this.categoria.Estado;
                    this.listaCategorias.Find(x => x.Id == this.categoria.Id).Imagen = this.categoria.Imagen;

                    this.listaCategorias = this.listaCategorias.Where(x => x.Estado == this.rbtActivo.IsChecked.Value).ToList();

                    if ((this.rbtActivo.IsChecked.Value != this.categoria.Estado) &&
                        this.rbtActivo.IsChecked.Value)
                        this.listaCategorias.Remove(
                            this.listaCategorias.Find(x => x.Id == this.categoria.Id));

                    if ((this.rbtInactivo.IsChecked.Value == this.categoria.Estado) &&
                        this.rbtInactivo.IsChecked.Value)
                        this.listaCategorias.Remove(
                            this.listaCategorias.Find(x => x.Id == this.categoria.Id));
                }
                else
                {
                    this.listaCategorias.Add(
                        new Entity.Almacen.Categoria
                        {
                            Id = this.categoria.Id,
                            Descripcion = this.categoria.Descripcion,
                            Estado = this.categoria.Estado,
                            Imagen = this.categoria.Imagen
                        });
                }


                this.LlenaCategorias();

                this.categoria.Conexion.Commit();

                MessageBox.Show("Proceso completado con exito");
            }
            catch (Utilitario.ExepcionSHomies es)
            {
                MessageBox.Show(es.Message);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.categoria.Conexion.RoolBack();
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.BuscarPorDescripcionYEstado();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void BuscarPorDescripcionYEstado()
        {
            try
            {
                this.categoria.Descripcion = this.txtBuscaDescripcion.Text;
                this.categoria.Estado = this.rbtActivo.IsChecked.Value;

                if (string.IsNullOrEmpty(this.categoria.Descripcion))
                    this.listaCategorias = this.categoria.ListaPorEstado(this.categoria.Estado);
                else
                    this.listaCategorias = this.categoria.BuscarPorDescripcionYEstado();

                this.LlenaCategorias();
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

        private void txtBuscaDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter ||
                    e.Key == Key.Back ||
                    e.Key == Key.Subtract)
                    BuscarPorDescripcionYEstado();
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
                    BuscarPorDescripcionYEstado();
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
                        imgCategoria.Source = bitmap.Frames[0];
                        this.categoria.Imagen = Convert.ToBase64String(Funcion.ConvertStreamToBytes(directorio.OpenFile()));
                    }
                }
                else
                {
                    imgCategoria.Source = null;
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

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
