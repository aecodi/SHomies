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
    /// Interaction logic for VentaDiaria.xaml
    /// </summary>
    public partial class VentaDiaria : Window
    {
        public List<Model.VentaDiaria> listaVentaDiaria;
        public VentaDiaria()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //this.listaVentaDiaria = new Model.VentaDiaria().GetDatosVentaDiaria();
                this.dgrVentas.Items.Clear();
                this.dgrVentas.ItemsSource = this.listaVentaDiaria;
                this.txtTotal.Text = this.listaVentaDiaria.Sum(x => decimal.Parse(x.Total)).ToString("#,###,###,##0.00");
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
                ticket.AddCabecera("Reporte", Alineacion.Left, Alineacion.Left, 10, "Ventas Diarias");
                ticket.AddCebeceraDetalle("Cant.", Alineacion.Left, Alineacion.Left, 6);
                ticket.AddCebeceraDetalle("Descripción", Alineacion.Left, Alineacion.Left, 20);
                ticket.AddCebeceraDetalle("Importe", Alineacion.Right, Alineacion.Right, 9);

                foreach (SHomies.UI.Ventas.Model.VentaDiaria detalle in this.listaVentaDiaria)
                {
                    ticket.AddItemsDetails(detalle.Cantidad, detalle.Producto, Funcion.FormatoDecimal(Funcion.ConvertTo<decimal>(detalle.Total)));
                }
                ticket.AddTotal("Total Venta", Alineacion.Right, Alineacion.Right, 25, this.txtTotal.Text);

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
