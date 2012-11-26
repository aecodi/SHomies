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
using SHomies.Conexion;
using SHomies.Core.Planilla;
using SHomies.Utilitario;

namespace SHomies.Tienda.Venta
{
    /// <summary>
    /// Lógica de interacción para Multa.xaml
    /// </summary>
    public partial class Multa : Window
    {
        private Conexion.IConexion conexion;
        private Core.Sistema.AuditoriaSistema auditoria;
        private EEstadoFormulario estadoFormulario;
        private Trabajador fichadoraSeleccionada;
        private List<Core.Planilla.Trabajador> listaChicas;

        public Multa()
        {
            InitializeComponent();
        }

        public Multa(Core.Sistema.AuditoriaSistema iAuditoria, Conexion.IConexion iConexion)
        {
            this.conexion = iConexion;
            this.auditoria = iAuditoria;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.estadoFormulario = EEstadoFormulario.Load;
            try
            {
                listaChicas = new Core.Planilla.Trabajador(this.conexion).ListaPorCargo(new Core.Planilla.Cargo() { Id = 3 }).Where(x => x.Estado).ToList();
                this.dtgFichadoras.Items.Clear();
                this.dtgFichadoras.ItemsSource = listaChicas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Clases.FuncionFormulario.ValidaCargaFormulario(estadoFormulario, this);
        }

        #region Eventos

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFichadora.Text.Trim()))
                    MessageBox.Show("Seleccionar fichadora");
                else if (string.IsNullOrEmpty(txtMontoMulta.Text.Trim()))
                    MessageBox.Show("Ingresar monto de multa");
                else
                {
                    var monto = Convert.ToDecimal(txtMontoMulta.Text);
                    if (monto <= 0)
                        MessageBox.Show("Monto debe ser mayor a cero");
                    else
                        GrabarMulta(monto);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("ERROR : Monto debe tener un valor numérico");
            }
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.BuscaChica(((TextBox)sender).Text);
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
                this.BuscaChica(((TextBox)sender).Text);
        }

        private void dtgFichadoras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                fichadoraSeleccionada = (Trabajador)this.dtgFichadoras.SelectedItem;
                if (fichadoraSeleccionada != null && fichadoraSeleccionada.Id != 0)
                    this.txtFichadora.Text = fichadoraSeleccionada.Nombres;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        #endregion

        #region Metodos

        private void BuscaChica(string iNombreChica)
        {
            if (!string.IsNullOrEmpty(iNombreChica))
            {
                List<Trabajador> busqueda = this.listaChicas.Where(x => x.Nombres.ToUpper().Contains(iNombreChica.ToUpper())).ToList();
                LlenaListaFichadoras(busqueda);
            }
            else
            {
                LlenaListaFichadoras(listaChicas);
            }
        }

        private void LlenaListaFichadoras(List<Trabajador> iData)
        {
            this.dtgFichadoras.ItemsSource = null;
            this.dtgFichadoras.ItemsSource = iData;
        }

        private void GrabarMulta(decimal monto)
        {
            try
            {
                var multaFichadora = new Core.Operaciones.Multa(this.conexion)
                    {
                        Fichadora = new Core.Persona.PersonaNatural
                        {
                            Id = this.fichadoraSeleccionada.Id
                        },
                        Monto = monto,
                        Fecha = this.auditoria.FechaSistema
                    };
                multaFichadora.Registrar();
                MessageBox.Show("Multa registrada correctamente");
                this.Close();
            }
            catch (Utilitario.ExepcionSHomies es)
            {
                MessageBox.Show(es.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}
