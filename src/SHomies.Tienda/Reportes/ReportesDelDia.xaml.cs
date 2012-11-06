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
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

namespace SHomies.Tienda.Reportes
{
    /// <summary>
    /// Interaction logic for ReportesDelDia.xaml
    /// </summary>
    public partial class ReportesDelDia : Window
    {
        private Core.Sistema.AuditoriaSistema auditoria;
        private Conexion.IConexion conexion;
        private EEstadoFormulario estadoFormulario;
        private EOpcionReporteDiario tipoReporte;

        public ReportesDelDia()
        {
            InitializeComponent();
        }

        public ReportesDelDia(Core.Sistema.AuditoriaSistema iAuditoria, Conexion.IConexion iConexion)
        {

            this.auditoria = iAuditoria;
            this.conexion = iConexion;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            estadoFormulario = EEstadoFormulario.Load;
            try
            {
                this.txbTituloDelFormulario.Text = "Reportes Diarios";
                this.cboFichadora.ItemsSource = new Core.Planilla.Trabajador(this.conexion)
                .ListaPorCargo(new Core.Planilla.Cargo() { Id = 3 });
                estadoFormulario = EEstadoFormulario.EndLoad;
            }
            catch (Utilitario.ExepcionSHomies es)
            {
                this.estadoFormulario = EEstadoFormulario.ErrorLoad;
                MessageBox.Show(es.Message);
            }
            catch (Exception ex)
            {
                this.estadoFormulario = EEstadoFormulario.ErrorLoad;
                MessageBox.Show(ex.Message);
            }

            Clases.FuncionFormulario.ValidaCargaFormulario(estadoFormulario, this);
        }
        private void Procesar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReportClass reporte = new ReportClass();

                DataSet data = new DataSet("Reporte");
                DataTable cabecera = new DataTable("Cabecera");
                cabecera.Columns.Add("Titulo");
                DataRow dataCabecera = cabecera.NewRow();
                Core.Reportes.Reporte datos = new Core.Reportes.Reporte(this.conexion);
                DateTime fechaInicio = Funcion.ConvertTo<DateTime>(this.dtpInicio.Text, new DateTime(1, 1, 1));
                DateTime fechaFin = Funcion.ConvertTo<DateTime>(this.dtpFin.Text, new DateTime(1, 1, 1));

                if (fechaInicio.ToShortDateString() == new DateTime(1, 1, 1).ToShortDateString())
                    Funcion.EjecutaExepcionShomies(string.Concat("Ingrese fecha inicio del reporte"));
                
                switch (this.tipoReporte)
                {
                    case EOpcionReporteDiario.REPORTE_CIERRE:
                        data = datos.ReporteDelCierre(fechaInicio);
                        data.Tables[0].TableName = "DetalleCierre";
                        Funcion.SetValueToRow(dataCabecera, "Titulo", "REPORTE DEL CIERRE DIARIO");
                        reporte = new ReporteCierreDiario();
                        break;
                    case EOpcionReporteDiario.REPORTE_VENTA:
                        data = datos.ReporteVentaProductosPorDia(fechaInicio);
                        data.Tables[0].TableName = "VentasDiarias";
                        Funcion.SetValueToRow(dataCabecera, "Titulo", "REPORTE VENTAS DEL DIA");
                        reporte = new ReporteVentasDelDia();
                        break;
                    case EOpcionReporteDiario.REPORTE_PAGO_FICHADORA:
                        if (fechaFin.ToShortDateString() == new DateTime(1, 1, 1).ToShortDateString())
                            Funcion.EjecutaExepcionShomies("Ingrese fecha fin del reporte");
                        data = datos.ReportePagoFichadoraEntreFechas(fechaInicio, fechaFin);
                        data.Tables[0].TableName = "Fichaje";
                        Funcion.SetValueToRow(dataCabecera, "Titulo", "REPORTE PAGO FICHADORA");
                        reporte = new ReporteFichajePorFecha();
                        break;
                    case EOpcionReporteDiario.REPORTE_FICHAJE:
                        if (fechaFin.ToShortDateString() == new DateTime(1, 1, 1).ToShortDateString())
                            Funcion.EjecutaExepcionShomies("Ingrese fecha fin del reporte");
                        data = datos.ReportePagoFichadoraEntreFechas(fechaInicio, fechaFin);
                        data.Tables[0].TableName = "Fichaje";
                        Funcion.SetValueToRow(dataCabecera, "Titulo", "REPORTE PAGO FICHADORA");
                        reporte = new ReporteFichajePorFecha();
                        break;
                    case EOpcionReporteDiario.REPORTE_POR_FICHADORA:
                        if (fechaFin.ToShortDateString() == new DateTime(1, 1, 1).ToShortDateString())
                            Funcion.EjecutaExepcionShomies("Ingrese fecha fin del reporte");
                        int idFichadora = Funcion.ConvertTo<int>(this.cboFichadora.SelectedValue, 0);
                        if (idFichadora == 0)
                            Funcion.EjecutaExepcionShomies("Selecciones fichadora");

                        data = datos.ReportePagoFichadoraPorFichadora(fechaInicio, fechaFin, idFichadora);
                        data.Tables[0].TableName = "Fichaje";
                        Funcion.SetValueToRow(dataCabecera, "Titulo", "REPORTE PAGO FICHADORA");
                        reporte = new ReporteFichajePorFecha();
                        break;
                    default:
                        Funcion.EjecutaExepcionShomies("No se ha seleccionado una opción.");
                        break;
                }
                cabecera.Rows.Add(dataCabecera);
                data.Tables.Add(cabecera);
                reporte.SetDataSource(data);
                ViewReport formulario = new ViewReport(reporte);
                formulario.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                formulario.ShowDialog();

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

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void rbtCierre_Checked(object sender, RoutedEventArgs e)
        {
            this.habilitaFiltros(EOpcionReporteDiario.REPORTE_CIERRE, false);
        }

        private void rbtVentaDiaria_Checked(object sender, RoutedEventArgs e)
        {
            this.habilitaFiltros(EOpcionReporteDiario.REPORTE_VENTA, false);
        }

        private void rbtPagoFichadora_Checked(object sender, RoutedEventArgs e)
        {
            tipoReporte = this.rbtPagoFichadora.IsChecked.Value ? EOpcionReporteDiario.REPORTE_PAGO_FICHADORA : 0;
        }

        private void rbtDetallePagoFichadora_Checked(object sender, RoutedEventArgs e)
        {
            this.habilitaFiltros(EOpcionReporteDiario.REPORTE_FICHAJE, true);
            this.cboFichadora.IsEnabled = false;
        }

        private void rbtFichadora_Checked(object sender, RoutedEventArgs e)
        {
            this.habilitaFiltros(EOpcionReporteDiario.REPORTE_POR_FICHADORA, true);
        }

        private void habilitaFiltros(EOpcionReporteDiario iEstadoReporte, bool estado)
        {
            tipoReporte = iEstadoReporte;
            this.cboFichadora.IsEnabled = estado;
            this.dtpFin.IsEnabled = estado;
        }


    }
}
