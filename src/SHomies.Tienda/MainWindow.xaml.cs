using System;
using System.Collections.Generic;
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

namespace SHomies.Tienda
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SHomies.Conexion.IConexion conexion;
        private Core.Sistema.AuditoriaSistema auditoria;

        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(Core.Sistema.AuditoriaSistema iAuditoria, SHomies.Conexion.IConexion iConexion)
        {
            this.auditoria = iAuditoria;
            this.conexion = iConexion;

            InitializeComponent();
        }
        private void btnCategoria_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Inventario.AdministraCategoria formularioCategoria = new Inventario.AdministraCategoria(this.conexion);
                formularioCategoria.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Error cargando formulario");
            }
        }

        private void btnNuevoProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Inventario.AdministraProducto formularioCategoria = new Inventario.AdministraProducto(this.conexion);
                formularioCategoria.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Error cargando formulario");
            }
        }

        private void btnService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Service.Nueva formularioCategoria = new Service.Nueva(this.conexion);
                formularioCategoria.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Error cargando formulario");
            }
        }

        private void btnNuevaOrden_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                auditoria.ValidaAperturaSistema();
                Venta.Nueva nuevaOrden = new Venta.Nueva(auditoria, this.conexion);
                nuevaOrden.ShowDialog();
            }
            catch (Utilitario.ExepcionSHomies sx)
            {
                MessageBox.Show(sx.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Error cargando formulario");
            }

        }

        private void btnApertura_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sistema.Aperturar aperturar = new Sistema.Aperturar(auditoria, this.conexion);
                aperturar.ShowDialog();
            }
            catch (Utilitario.ExepcionSHomies sx)
            {
                MessageBox.Show(sx.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Error cargando formulario");
            }
        }

        private void btnCierraSistema_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sistema.Cerrar cierreSistema = new Sistema.Cerrar(auditoria, this.conexion);
                cierreSistema.ShowDialog();
            }
            catch (Utilitario.ExepcionSHomies sx)
            {
                MessageBox.Show(sx.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Error cargando formulario");
            }
        }

        private void btnVentaFichadora_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.auditoria.GetUltimaFechaSistema();
                Venta.VentaFichadora ventaFichadora = new Venta.VentaFichadora(auditoria, this.conexion);
                ventaFichadora.ShowDialog();
            }
            catch (Utilitario.ExepcionSHomies sx)
            {
                MessageBox.Show(sx.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Error cargando formulario");
            }
        }

        private void btnReporte_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.auditoria.GetUltimaFechaSistema();

                Reportes.ReportesDelDia reportes = new Reportes.ReportesDelDia(auditoria, this.conexion);
                reportes.ShowDialog();
            }
            catch (Utilitario.ExepcionSHomies sx)
            {
                MessageBox.Show(sx.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Error cargando formulario");
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.txbTituloDelFormulario.Text = "Opciones del Menu";
        }

        private void btnFichadora_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mantenimiento.AdministraFichadora fichadora =
                    new Mantenimiento.AdministraFichadora(this.conexion);
                fichadora.ShowDialog();
            }
            catch (Utilitario.ExepcionSHomies sx)
            {
                MessageBox.Show(sx.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Error cargando formulario");
            }
        }

        private void btnMultas_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                auditoria.ValidaAperturaSistema();
                Venta.Multa nuevaMulta = new Venta.Multa(auditoria, this.conexion);
                nuevaMulta.ShowDialog();
            }
            catch (Utilitario.ExepcionSHomies sx)
            {
                MessageBox.Show(sx.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Error cargando formulario");
            }
        }



    }
}
