using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace SHomies.Tienda.Reportes
{
    public partial class ViewReport : Form
    {
        private ReportClass reporte;
        public ViewReport()
        {
            InitializeComponent();
        }
        public ViewReport(ReportClass iReporte)
        {            
            InitializeComponent();
            reporte = iReporte;
        }

        private void ViewReport_Load(object sender, EventArgs e)
        {
            this.viewReportes.ReportSource = this.reporte;
        }
    }
}
