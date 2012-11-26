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
using SHomies.Core.Planilla;

namespace SHomies.Tienda.Mantenimiento
{
    /// <summary>
    /// Interaction logic for AdministraFichadora.xaml
    /// </summary>
    public partial class AdministraFichadora : Window
    {
        private Conexion.IConexion conexion;
        private EEstadoFormulario estadoFormulario;
        private Trabajador fichadoraSeleccionada;
        private List<Trabajador> fichadoras;
        private List<Trabajador> listaFichadoras;
        public AdministraFichadora()
        {
            InitializeComponent();
        }
        public AdministraFichadora(Conexion.IConexion iConexion)
        {
            this.conexion = iConexion;
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.estadoFormulario = EEstadoFormulario.Load;
            try
            {
                this.dtgFichadoras.Items.Clear();
                this.CargarFichadoras();
                this.llenaFichadoras();
            }
            catch (Exception ex)
            {
                this.estadoFormulario = EEstadoFormulario.ErrorLoad;
                MessageBox.Show(ex.Message);
            }
            Clases.FuncionFormulario.ValidaCargaFormulario(estadoFormulario, this);
        }

        private void CargarFichadoras()
        {
            this.listaFichadoras = new Core.Planilla.Trabajador(this.conexion)
                .ListaPorCargo(new Core.Planilla.Cargo() { Id = 3 });
            this.fichadoras = this.listaFichadoras;
        }

        private void llenaFichadoras()
        {
            this.dtgFichadoras.ItemsSource = this.fichadoras;
        }
        private void IsActivo_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Trabajador chica = (Trabajador)this.dtgFichadoras.SelectedItem;
                if (chica != null)
                    if (chica.Id != 0)
                    {
                        chica.Estado = ((CheckBox)sender).IsChecked.Value;
                        chica.Conexion = this.conexion;
                        chica.ChangeEstado();
                    }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                this.fichadoras = this.listaFichadoras.Where(x => x.Nombres.ToUpper().StartsWith(this.txtBuscar.Text.ToUpper())).ToList();
                llenaFichadoras();
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

        private void btnNueva_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.txtFichadora.Text = string.Empty;
                this.txtFichadora.IsReadOnly = false;
                this.estadoFormulario = EEstadoFormulario.Nuevo;
                this.txtFichadora.Focus();
                this.btnGrabar.IsEnabled = true;
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
                if (string.IsNullOrEmpty(this.txtFichadora.Text.Trim()))
                {
                    throw new Exception("Ingrese nombre de fichadora");
                }

                switch (this.estadoFormulario)
                {
                    case EEstadoFormulario.Nuevo:
                        this.NuevaFichadora();
                        this.CargarFichadoras();
                        break;
                    case EEstadoFormulario.Modificar:
                        this.ModificarFichadora();
                        break;
                    default:
                        break;
                }
                this.dtgFichadoras.ItemsSource = null;
                this.llenaFichadoras();
                this.btnGrabar.IsEnabled = false;
                this.estadoFormulario = EEstadoFormulario.EndLoad;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ModificarFichadora()
        {
            try
            {
                this.conexion.BeginTransaction();
                fichadoraSeleccionada.Conexion = this.conexion;
                fichadoraSeleccionada.Nombres = this.txtFichadora.Text;
                fichadoraSeleccionada.NombreCompleto = this.txtFichadora.Text;
                fichadoraSeleccionada.UpdateName();
                this.listaFichadoras.Find(x => x.Id == fichadoraSeleccionada.Id).Nombres = fichadoraSeleccionada.Nombres;
                this.txtFichadora.IsReadOnly = true;
                this.conexion.Commit();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.conexion.RoolBack();
            }
        }

        private void NuevaFichadora()
        {
            try
            {
                this.conexion.BeginTransaction();
                Trabajador fichadora = new Trabajador(this.conexion)
                {
                    ApellidoMaterno = this.txtFichadora.Text,
                    ApellidoPaterno = this.txtFichadora.Text,
                    Cargo = new Cargo { Id = 3 },
                    FechaIngreso = DateTime.Now,
                    FechaNacimiento = DateTime.Now,
                    Nombres = this.txtFichadora.Text,
                    NombreCompleto = this.txtFichadora.Text,
                    TipoDocumento = new Core.Persona.TipoDocumento
                    {
                        Codigo = "DNI"
                    },
                    NumeroDocumento = "00000001",
                    Sexo = 'F'
                };

                fichadora.Nuevo();

                this.txtFichadora.IsReadOnly = true;
                this.conexion.Commit();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.conexion.RoolBack();
            }
        }

        private void dtgFichadoras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                fichadoraSeleccionada = (Trabajador)this.dtgFichadoras.SelectedItem;
                if (fichadoraSeleccionada != null)
                {
                    if (fichadoraSeleccionada.Id != 0)
                    {
                        this.txtFichadora.Text = fichadoraSeleccionada.Nombres;
                        this.btnEditar.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.txtFichadora.IsReadOnly = false;
                this.estadoFormulario = EEstadoFormulario.Modificar;
                this.txtFichadora.Focus();
                this.btnGrabar.IsEnabled = true;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
