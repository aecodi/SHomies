namespace SHomies.Tienda.Reportes
{
    partial class ViewReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.viewReportes = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // viewReportes
            // 
            this.viewReportes.ActiveViewIndex = -1;
            this.viewReportes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.viewReportes.Cursor = System.Windows.Forms.Cursors.Default;
            this.viewReportes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewReportes.Location = new System.Drawing.Point(0, 0);
            this.viewReportes.Name = "viewReportes";
            this.viewReportes.Size = new System.Drawing.Size(284, 262);
            this.viewReportes.TabIndex = 0;
            // 
            // ViewReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.viewReportes);
            this.Name = "ViewReport";
            this.Text = "ViewReport";
            this.Load += new System.EventHandler(this.ViewReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer viewReportes;
    }
}