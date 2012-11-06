using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Utilitario;

namespace SHomies.Tienda.Clases
{
    public class DetalleFichajeViewModel : Core.Venta.Fichaje
    {
        public string NombreFichadora { get; set; }
        public string MontoFichaje { get; set; }
        public int IdOrden { get; set; }
        public DateTime FechaProceso { get; set; }
        public bool IsAddToOrden { get; set; }

        public DetalleFichajeViewModel()
        {

        }
        public DetalleFichajeViewModel(SHomies.Conexion.IConexion iConexion)
        {
            this.Conexion = iConexion;
        }

        public List<DetalleFichajeViewModel> GetFichaje(Core.Planilla.Trabajador iFichadora, 
                                                        DateTime iFechaInicio,
                                                        DateTime iFechaFin)
        {
            if (iFechaInicio == Funcion.FechaNula)
                Funcion.EjecutaExepcionShomies("Ingrese una fecha inicial de fichaje");
            if (iFechaFin == Funcion.FechaNula)
                Funcion.EjecutaExepcionShomies("Ingrese una fecha final de fichaje");
            if (iFichadora.Id == 0)
                Funcion.EjecutaExepcionShomies("Seleccione fichadora.");

            List<Core.Venta.Fichaje> fichaje =
                new Core.Venta.Fichaje(this.Conexion).GetFichajePorFichadoraEntreFechas(iFichadora, iFechaInicio, iFechaFin);

            return (from fichajes in fichaje
                    select new DetalleFichajeViewModel
                    {
                        IdOrden = fichajes.Orden.Id,
                        Orden = fichajes.Orden,
                        Monto = fichajes.Monto,
                        Estado = fichajes.Estado,
                        Fichadora = fichajes.Fichadora,
                        FechaProceso = fichajes.Orden.AuditoriaSistema.FechaSistema
                    }).ToList();
        }

    }
}
