using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;
using SHomies.Utilitario;
using SHomies.Core.Operaciones;

namespace SHomies.Tienda.Clases
{
    public class DetalleCierreViewModel : Core.Sistema.DetalleCierre
    {
        public string DescripcionConcepto { get; set; }
        public string MontoCierre { get; set; }

        public DetalleCierreViewModel()
        {

        }
        public DetalleCierreViewModel(IConexion iConexion)
        {
            this.Conexion = iConexion;
        }

        public List<DetalleCierreViewModel> GetDetalleDelCierre(Core.Sistema.AuditoriaSistema iAuditoriaSistema)
        {
            List<DetalleCierreViewModel> detalleCierre = new List<DetalleCierreViewModel>();
            try
            {
                iAuditoriaSistema.GetDetalle();

                foreach (Core.Sistema.DetalleCierre detalle in iAuditoriaSistema.DetalleCierre)
                {
                    detalleCierre.Add(
                        new DetalleCierreViewModel()
                        {
                            Concepto = detalle.Concepto,
                            Fecha = detalle.Fecha,
                            Id = detalle.Id,
                            Monto = detalle.Monto,
                            DescripcionConcepto = detalle.Concepto.Descripcion,
                            MontoCierre = Funcion.FormatoDecimal(detalle.Concepto.Tipo == (int)TipoConcepto.EGRESO ? detalle.Monto * -1 : detalle.Monto)
                        });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return detalleCierre;
        }
        
    }
}
