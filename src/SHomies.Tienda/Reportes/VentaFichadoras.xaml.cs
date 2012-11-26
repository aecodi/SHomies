using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SHomies.Utilitario;

namespace SHomies.UI.Ventas.Reportes
{
    /// <summary>
    /// Interaction logic for VentaFichadoras.xaml
    /// </summary>
    public partial class VentaFichadoras : Window
    {
        public List<Model.VentaFichadoras> ventasFichadoras;
        public VentaFichadoras()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //this.detalleFichaje = new Model.VentaFichadoras().GetDatosFichaje();
                this.dgrVentaFichadora.Items.Clear();
                this.dgrVentaFichadora.ItemsSource = this.ventasFichadoras;
                this.txtTotal.Text = this.ventasFichadoras.Sum(x => decimal.Parse(x.MontoPago)).ToString("#,###,###,##0.00");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Ticket ticket = new Ticket();
                ticket.Title = "EL RELAX";
                ticket.AddCabecera("Reporte", Alineacion.Left, Alineacion.Left, 10, "Venta Fichaje");
                ticket.AddCebeceraDetalle("Fichadora", Alineacion.Left, Alineacion.Left, 12);
                ticket.AddCebeceraDetalle("Fichaje", Alineacion.Right, Alineacion.Right, 7);
                ticket.AddCebeceraDetalle("Multa", Alineacion.Right, Alineacion.Right, 7);
                ticket.AddCebeceraDetalle("Pago", Alineacion.Right, Alineacion.Right, 8);

                foreach (var fichaje in this.ventasFichadoras)
                {
                    var mfichaje = Funcion.FormatoDecimal(decimal.Parse(fichaje.MontoFichaje));
                    var mMulta = Funcion.FormatoDecimal(decimal.Parse(fichaje.MontoMulta));
                    var mPago = Funcion.FormatoDecimal(decimal.Parse(fichaje.MontoPago));
                    ticket.AddItemsDetails(fichaje.NombreFichadora, mfichaje, mMulta, mPago);
                }
                ticket.AddTotal("TOTAL", Alineacion.Right, Alineacion.Right, 25, this.txtTotal.Text);
                ticket.itemsPie.Add("Los esperamos");
                String impresora = System.Configuration.ConfigurationSettings.AppSettings["Impresora"].ToString();

                ticket.Imprimir(impresora);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
