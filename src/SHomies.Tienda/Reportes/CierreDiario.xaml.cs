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
    /// Interaction logic for CierreDiario.xaml
    /// </summary>
    public partial class CierreDiario : Window
    {
        public List<Model.CierreDiario> detalleCierre;
        public CierreDiario()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //this.detalleCierre = new Model.CierreDiario().GetDatosCierre();
                this.dgrCierre.Items.Clear();
                this.dgrCierre.ItemsSource = this.detalleCierre;
                this.txtTotal.Text = this.detalleCierre.Sum(x => decimal.Parse(x.Monto)).ToString("#,###,###,##0.00");
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
                ticket.AddCabecera("Reporte", Alineacion.Left, Alineacion.Left, 10, "Cierre Diario");
                ticket.AddCebeceraDetalle("Concepto", Alineacion.Left, Alineacion.Left, 26);
                ticket.AddCebeceraDetalle("Importe", Alineacion.Right, Alineacion.Right, 9);

                foreach (SHomies.UI.Ventas.Model.CierreDiario cierre in this.detalleCierre)
                {
                    ticket.AddItemsDetails(cierre.Concepto, cierre.Monto);
                }
                ticket.AddTotal("Liquidez", Alineacion.Right, Alineacion.Right, 25, this.txtTotal.Text);
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
