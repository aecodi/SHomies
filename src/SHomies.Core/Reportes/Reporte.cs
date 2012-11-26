using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;
using System.Data;
using SHomies.Utilitario;

namespace SHomies.Core.Reportes
{
    public class Reporte : Conexion.EntidadConexion
    {
        public Reporte() { }
        public Reporte(IConexion iConexion)
        {
            this.Conexion = iConexion;
        }

        public DataSet ReporteDelCierre(DateTime fechaCierre)
        {
            DataSet data = new DataSet("DetalleCierre");
            try
            {
                this.Conexion.NombrePaquete = "administra_reportes";
                this.Conexion.QuerySQL = "reporte_detallado_cierre";
                this.Conexion.SetValorParametroInput("i_fechareporte", fechaCierre);
                this.Conexion.ExecuteProcedure(out data);
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");
                if (Funcion.IsDataSetEmpty(data))        
                    Funcion.EjecutaExepcionShomies("No existen datos para este dia del cierre");
                if (Funcion.IsDataTableEmpty(data.Tables[0]))
                    Funcion.EjecutaExepcionShomies("No existen datos para este dia del cierre");
            }
            catch (Exception)
            {
                throw;
            }
            return data;
        }
        public DataSet ReporteVentaProductosPorDia(DateTime fechaVenta)
        {
            DataSet data = new DataSet("DetalleVenta");
            try
            {
                this.Conexion.NombrePaquete = "administra_reportes";
                this.Conexion.QuerySQL = "productos_vendidos_por_dia";
                this.Conexion.SetValorParametroInput("i_fechareporte", fechaVenta);
                this.Conexion.ExecuteProcedure(out data);
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");
                if (Funcion.IsDataSetEmpty(data))
                    Funcion.EjecutaExepcionShomies("No existen datos para este dia.");
                if (Funcion.IsDataTableEmpty(data.Tables[0]))
                    Funcion.EjecutaExepcionShomies("No existen datos para este dia.");
            }
            catch (Exception)
            {
                throw;
            }
            return data;
        }
        public DataSet ReportePagoFichadoraEntreFechas(DateTime fechaInicio,
                                                       DateTime fechaFin)
        {
            DataSet data = new DataSet("fichajeporfecha");
            try
            {
                this.Conexion.NombrePaquete = "administra_reportes";
                this.Conexion.QuerySQL = "reporte_fichaje_entre_fechas";
                this.Conexion.SetValorParametroInput("i_fechainicio", fechaInicio);
                this.Conexion.SetValorParametroInput("i_fechafinal", fechaFin);
                this.Conexion.ExecuteProcedure(out data);
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");
                if (Funcion.IsDataSetEmpty(data))
                    Funcion.EjecutaExepcionShomies("No existen datos para este dia.");
                if (Funcion.IsDataTableEmpty(data.Tables[0]))
                    Funcion.EjecutaExepcionShomies("No existen datos para este dia.");
            }
            catch (Exception)
            {
                throw;
            }
            return data;
        }
        public DataSet ReportePagoFichadoraPorFichadora(DateTime fechaInicio,
                                                        DateTime fechaFin,
                                                        int idFichadora)
        {
            DataSet data = new DataSet("fichajeporfecha");
            try
            {
                this.Conexion.NombrePaquete = "administra_reportes";
                this.Conexion.QuerySQL = "reporte_chica_entre_fechas";
                this.Conexion.SetValorParametroInput("i_fechainicio", fechaInicio);
                this.Conexion.SetValorParametroInput("i_fechafinal", fechaFin);
                this.Conexion.SetValorParametroInput("i_chica", idFichadora);
                this.Conexion.ExecuteProcedure(out data);
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");
                if (Funcion.IsDataSetEmpty(data))
                    Funcion.EjecutaExepcionShomies("No existen datos para este dia.");
                if (Funcion.IsDataTableEmpty(data.Tables[0]))
                    Funcion.EjecutaExepcionShomies("No existen datos para este dia.");
            }
            catch (Exception)
            {
                throw;
            }
            return data;
        }
        public DataSet ReportePagoFichadorasEntreFechas(DateTime fechaInicio,
                                                        DateTime fechaFin)
        {
            DataSet data = new DataSet("PagoFichadora");
            try
            {
                this.Conexion.NombrePaquete = "administra_reportes";
                this.Conexion.QuerySQL = "pago_fichaje_entre_fechas";
                this.Conexion.SetValorParametroInput("i_fechainicio", fechaInicio);
                this.Conexion.SetValorParametroInput("i_fechafinal", fechaFin);
                this.Conexion.ExecuteProcedure(out data);
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");
                if (Funcion.IsDataSetEmpty(data))
                    Funcion.EjecutaExepcionShomies("No existen datos para este dia.");
                if (Funcion.IsDataTableEmpty(data.Tables[0]))
                    Funcion.EjecutaExepcionShomies("No existen datos para este dia.");
            }
            catch (Exception)
            {
                throw;
            }
            return data;
        }
    }
}
