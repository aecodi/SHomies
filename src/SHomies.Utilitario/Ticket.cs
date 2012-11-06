using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;

namespace SHomies.Utilitario
{
    public enum Posicion
    {
        HORIZONTAL = 0,
        VERTICAL = 1
    }
    public enum Alineacion
    {
        Left = 0,
        Right = 1,
        Center = 2
    }
    class FormatoCabecera
    {
        public int Size { get; set; }
        public String Descripcion { get; set; }
        public Alineacion AlignEtiqueta { get; set; }
        public Alineacion AlignContenido { get; set; }
        public FormatoCabecera()
        {
            this.Size = 0;
            this.Descripcion = String.Empty;
            this.AlignEtiqueta = Alineacion.Left;
            this.AlignContenido = Alineacion.Left;
        }
    }
    public class FormatoItem
    {
        public int Size { get; set; }
        public Alineacion Align { get; set; }
        public FormatoItem(int piSize, Alineacion piAlign)
        {
            this.Size = piSize;
            this.Align = piAlign;
        }
        public FormatoItem(Alineacion piAlign)
        {
            this.Size = 0;
            this.Align = piAlign;
        }
    }
    public class Ticket
    {
        private List<FormatoCabecera> CabeceraDetalle;
        private List<FormatoCabecera> Cabecera;
        private List<Object[]> itemsDetalle;
        private List<Object> itemsCabecera;
        private List<FormatoCabecera> Totales;
        private List<Object> itemsTotales;

        private Graphics ticket;
        private int posicionLinea;
        private System.Drawing.Font letra;
        private String linea;
        private Brush myBrush;
        private int maxLentgh;

        private String title;
        public String Title
        {
            get { return title; }
            set { title = value; }
        }
        private int marginTop;
        public int MarginTop
        {
            get { return marginTop; }
            set { marginTop = value; }
        }
        public List<String> itemsPie { get; set; }
        public Ticket()
        {
            this.CabeceraDetalle = new List<FormatoCabecera>();
            this.Cabecera = new List<FormatoCabecera>();
            this.Totales = new List<FormatoCabecera>();
            this.itemsDetalle = new List<Object[]>();
            this.itemsCabecera = new List<Object>();
            this.itemsTotales = new List<Object>();
            this.itemsPie = new List<string>();
            String SizeLetra = System.Configuration.ConfigurationSettings.AppSettings["SizeLetraBoleta"].ToString();

            this.letra = new System.Drawing.Font("Lucida Console", Funcion.ConvertTo<float>(SizeLetra), System.Drawing.FontStyle.Regular);
            this.myBrush = new SolidBrush(Color.Black);
            this.maxLentgh = 35;
            this.title = "SHomies S.A.";
            this.marginTop = 1;
        }
        public void AddCebeceraDetalle(String piDescripcion,
                                       Alineacion piAlignEtiqueta,
                                       Alineacion piAlignContenido,
                                       int piSize)
        {
            this.CabeceraDetalle.Add(
                new FormatoCabecera()
                {
                    AlignEtiqueta = piAlignEtiqueta,
                    AlignContenido = piAlignContenido,
                    Descripcion = piDescripcion,
                    Size = piSize
                });
        }
        public void AddCabecera(String piDescripcion,
                                Alineacion piAlignEtiqueta,
                                Alineacion piAlignContenido,
                                int piSize,
                                Object piItem)
        {
            this.Cabecera.Add(
                new FormatoCabecera()
                {
                    AlignEtiqueta = piAlignEtiqueta,
                    AlignContenido = piAlignContenido,
                    Descripcion = piDescripcion,
                    Size = piSize
                });
            this.itemsCabecera.Add(piItem);
        }
        public void AddTotal(String piDescripcion,
                             Alineacion piAlignEtiqueta,
                             Alineacion piAlignContenido,
                             int piSize,
                             Object piItem)
        {
            this.Totales.Add(
                new FormatoCabecera()
                {
                    AlignEtiqueta = piAlignEtiqueta,
                    AlignContenido = piAlignContenido,
                    Descripcion = piDescripcion,
                    Size = piSize
                });
            this.itemsTotales.Add(piItem);
        }
        public void AddItemsDetails(params Object[] piItems)
        {
            try
            {
                if (piItems.Length != this.CabeceraDetalle.Count)
                {
                    throw new Exception("Error de configuracion de boltela.");
                }
                else
                {
                    this.itemsDetalle.Add(piItems);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void DrawdTitle()
        {
            int posicion = 0;
            posicion = this.maxLentgh - this.title.Length;
            Boolean esImpar = posicion % 2 != 0 ? false : true;
            String texto = String.Concat(String.Empty.PadLeft(esImpar ? posicion / 2 : (posicion + 1) / 2, ' '), this.title);
            texto = String.Concat(texto, String.Empty.PadRight(esImpar ? posicion / 2 : (posicion - 1) / 2, ' '));
            this.linea += texto;
            DrawdLine();
        }
        private void DrawdCabecera()
        {
            this.DrawdSeparadorDoble(this.maxLentgh);
            this.linea = String.Empty;
            int contador = 0;
            foreach (FormatoCabecera cabecera in this.Cabecera)
            {
                int posicion = 0;
                switch (cabecera.AlignEtiqueta)
                {
                    case Alineacion.Left:
                        posicion = cabecera.Size - cabecera.Descripcion.Length;
                        this.linea = String.Concat(cabecera.Descripcion, String.Empty.PadRight(posicion, ' '), ":");
                        break;
                    case Alineacion.Right:
                        posicion = cabecera.Size - cabecera.Descripcion.Length;
                        this.linea = String.Concat(String.Empty.PadLeft(posicion, ' '), cabecera.Descripcion, ":");
                        break;
                    case Alineacion.Center:
                        posicion = cabecera.Size - cabecera.Descripcion.Length;
                        Boolean esImpar = posicion % 2 != 0 ? false : true;
                        String texto = String.Concat(String.Empty.PadLeft(esImpar ? posicion / 2 : (posicion + 1) / 2, ' '), cabecera.Descripcion);
                        texto = String.Concat(texto, String.Empty.PadRight(esImpar ? posicion / 2 : (posicion - 1) / 2, ' '), ":");
                        this.linea = texto;
                        break;
                    default:
                        break;
                }
                String itemCabecera = this.itemsCabecera[contador].ToString();
                switch (cabecera.AlignContenido)
                {
                    case Alineacion.Left:
                        posicion = this.maxLentgh - (cabecera.Size + 1);
                        this.linea += String.Concat(itemCabecera, String.Empty.PadRight(posicion, ' '));
                        break;
                    case Alineacion.Right:
                        posicion = this.maxLentgh - (cabecera.Size + 1);
                        this.linea += itemCabecera.PadLeft(posicion, ' ');
                        break;
                    case Alineacion.Center:
                        posicion = (this.maxLentgh - cabecera.Size) - itemCabecera.Length;
                        Boolean esImpar = posicion % 2 != 0 ? false : true;
                        String texto = String.Concat(String.Empty.PadLeft(esImpar ? posicion / 2 : (posicion + 1) / 2, ' '), itemCabecera);
                        texto = String.Concat(texto, String.Empty.PadRight(esImpar ? posicion / 2 : (posicion - 1) / 2, ' '));
                        this.linea += texto;
                        break;
                    default:
                        break;
                }
                contador++;
                this.DrawdLine();
            }
        }
        private void DrawdTotales()
        {
            this.linea = String.Empty;
            int contador = 0;
            foreach (FormatoCabecera total in this.Totales)
            {
                int posicion = 0;
                switch (total.AlignEtiqueta)
                {
                    case Alineacion.Left:
                        posicion = total.Size - total.Descripcion.Length;
                        this.linea = String.Concat(total.Descripcion, String.Empty.PadRight(posicion, ' '), ":");
                        break;
                    case Alineacion.Right:
                        posicion = total.Size - total.Descripcion.Length;
                        this.linea = String.Concat(String.Empty.PadLeft(posicion, ' '), total.Descripcion, ":");
                        break;
                    case Alineacion.Center:
                        posicion = total.Size - total.Descripcion.Length;
                        Boolean esImpar = posicion % 2 != 0 ? false : true;
                        String texto = String.Concat(String.Empty.PadLeft(esImpar ? posicion / 2 : (posicion + 1) / 2, ' '), total.Descripcion);
                        texto = String.Concat(texto, String.Empty.PadRight(esImpar ? posicion / 2 : (posicion - 1) / 2, ' '), ":");
                        this.linea = texto;
                        break;
                    default:
                        break;
                }
                String itemTotal = this.itemsTotales[contador].ToString();
                switch (total.AlignContenido)
                {
                    case Alineacion.Left:
                        posicion = this.maxLentgh - (total.Size + 1);
                        this.linea += String.Concat(itemTotal, String.Empty.PadRight(posicion, ' '));
                        break;
                    case Alineacion.Right:
                        posicion = this.maxLentgh - (total.Size + 1);
                        this.linea += itemTotal.PadLeft(posicion, ' ');
                        break;
                    case Alineacion.Center:
                        posicion = (this.maxLentgh - total.Size) - itemTotal.Length;
                        Boolean esImpar = posicion % 2 != 0 ? false : true;
                        String texto = String.Concat(String.Empty.PadLeft(esImpar ? posicion / 2 : (posicion + 1) / 2, ' '), itemTotal);
                        texto = String.Concat(texto, String.Empty.PadRight(esImpar ? posicion / 2 : (posicion - 1) / 2, ' '));
                        this.linea += texto;
                        break;
                    default:
                        break;
                }
                contador++;
                this.DrawdLine();
            }
        }
        private void DrawdCabeceraDetalle()
        {
            this.DrawdSeparadorDoble(this.maxLentgh);
            this.linea = String.Empty;
            int posicionFinal = 0;
            foreach (FormatoCabecera cabecera in this.CabeceraDetalle)
            {
                int posicion = posicionFinal;
                switch (cabecera.AlignEtiqueta)
                {
                    case Alineacion.Left:
                        posicion = cabecera.Size - cabecera.Descripcion.Length;
                        this.linea += String.Concat(cabecera.Descripcion, String.Empty.PadRight(posicion, ' '));
                        break;
                    case Alineacion.Right:
                        posicion = cabecera.Size - cabecera.Descripcion.Length;
                        this.linea += String.Concat(String.Empty.PadLeft(posicion, ' '), cabecera.Descripcion);
                        break;
                    case Alineacion.Center:
                        posicion = cabecera.Size - cabecera.Descripcion.Length;
                        Boolean esImpar = posicion % 2 != 0 ? false : true;
                        String texto = String.Concat(String.Empty.PadLeft(esImpar ? posicion / 2 : (posicion + 1) / 2, ' '), cabecera.Descripcion);
                        texto = String.Concat(texto, String.Empty.PadRight(esImpar ? posicion / 2 : (posicion - 1) / 2, ' '));
                        this.linea += texto;
                        break;
                    default:
                        break;
                }
                posicionFinal = cabecera.Size;
            }
            this.DrawdLine();
            this.DrawdSeparadorSimple(this.maxLentgh);
        }
        private void DrawdItemsDetalle()
        {
            foreach (Object[] Items in this.itemsDetalle)
            {
                int posicionFinal = 0;
                this.linea = String.Empty;
                for (int i = 0; i < Items.Length; i++)
                {
                    int posicion = 0;
                    switch (this.CabeceraDetalle[i].AlignContenido)
                    {
                        case Alineacion.Left:
                            if (Items[i].ToString().Length < this.CabeceraDetalle[i].Size)
                            {
                                posicion = this.CabeceraDetalle[i].Size - Items[i].ToString().Length;
                            }
                            else
                            {
                                Items[i] = Items[i].ToString().Substring(0, this.CabeceraDetalle[i].Size);
                                posicion = 0;
                            }
                            this.linea += String.Concat(Items[i], String.Empty.PadRight(posicion, ' '));
                            break;
                        case Alineacion.Right:
                            posicion = this.CabeceraDetalle[i].Size - Items[i].ToString().Length;
                            this.linea += String.Concat(String.Empty.PadLeft(posicion, ' '), Items[i].ToString());
                            break;
                        case Alineacion.Center:
                            posicion = this.CabeceraDetalle[i].Size - Items[i].ToString().Length;
                            Boolean esImpar = posicion % 2 != 0 ? false : true;
                            String texto = String.Concat(String.Empty.PadLeft(esImpar ? posicion / 2 : (posicion + 1) / 2, ' '), Items[i].ToString());
                            texto = String.Concat(texto, String.Empty.PadRight(esImpar ? posicion / 2 : (posicion - 1) / 2, ' '));
                            this.linea += texto;
                            break;
                        default:
                            break;
                    }
                    posicionFinal = this.CabeceraDetalle[i].Size;
                }
                DrawdLine();
            }
            DrawdSeparadorDoble(this.maxLentgh);
        }
        private void DrawdPie()
        {
            this.linea = String.Empty;
            foreach (String pie in this.itemsPie)
            {
                int posicion = this.maxLentgh - pie.Length;
                Boolean esImpar = posicion % 2 != 0 ? false : true;
                String texto = String.Concat(String.Empty.PadLeft(esImpar ? posicion / 2 : (posicion + 1) / 2, ' '), pie);
                texto = String.Concat(texto, String.Empty.PadRight(esImpar ? posicion / 2 : (posicion - 1) / 2, ' '));
                this.linea += texto;
                DrawdPie();
            }
        }
        private void DrawdLine()
        {
            this.ticket.DrawString(this.linea, this.letra, this.myBrush, 0f, this.YPosicion(), new StringFormat());
            this.posicionLinea++;
        }
        private void DrawdSeparadorDoble(int piMaxLentgh)
        {
            this.linea = String.Empty;
            for (int x = 0; x < piMaxLentgh; x++)
            {
                this.linea += "=";
            }
            this.DrawdLine();
        }
        private void DrawdSeparadorSimple(int piMaxLentgh)
        {
            this.linea = String.Empty;
            for (int x = 0; x < piMaxLentgh; x++)
            {
                this.linea += "-";
            }
            this.DrawdLine();
        }
        private void pr_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            this.ticket = e.Graphics;
            this.DrawdTitle();
            this.DrawdCabecera();
            this.DrawdCabeceraDetalle();
            this.DrawdItemsDetalle();
            this.DrawdTotales();
            //this.DrawdPie();
        }
        public bool ExisteImpresora(string impresora)
        {
            foreach (string strPrinter in PrinterSettings.InstalledPrinters)
            {
                if (impresora == strPrinter)
                {
                    return true;
                }
            }
            return false;
        }
        public void Imprimir(string impresora)
        {
            PrintDocument pr = new PrintDocument();
            pr.PrinterSettings.PrinterName = impresora;
            pr.PrintPage += new PrintPageEventHandler(this.pr_PrintPage);
            this.maxLentgh = this.CabeceraDetalle.Sum(m => m.Size);
            this.posicionLinea = this.marginTop;
            pr.Print();
        }
        private float YPosicion()
        {
            return (this.posicionLinea * this.letra.GetHeight(this.ticket));
        }
    }
}
