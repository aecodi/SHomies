﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;
using System.Data;
using SHomies.Utilitario;

namespace SHomies.Core.Venta
{
    public class Fichaje : Conexion.EntidadConexion
    {
        public Venta.Orden Orden { get; set; }
        public decimal Monto { get; set; }
        public Planilla.Trabajador Fichadora { get; set; }
        public int Estado { get; set; }

        public Fichaje()
        {
            this.Fichadora = new Planilla.Trabajador();
            this.Orden = new Orden();
        }
        public Fichaje(IConexion iConexion)
        {
            this.Conexion = iConexion;
            this.Fichadora = new Planilla.Trabajador(iConexion);
            this.Orden = new Orden(iConexion);
        }

        public void Registrar()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_detallefichaje";
                this.Conexion.QuerySQL = "registrar";

                this.Conexion.SetValorParametroInput("i_idorden", this.Orden.Id);
                this.Conexion.SetValorParametroInput("i_idfichador", this.Fichadora.Id);
                this.Conexion.SetValorParametroInput("i_fechaproceso", this.Orden.AuditoriaSistema.FechaSistema);
                this.Conexion.SetValorParametroInput("i_monto", this.Monto);

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Fichaje> GetFichajePorFichadoraYFecha(Planilla.Trabajador iFichadora, DateTime iFecha)
        {
            List<Fichaje> fichaje = new List<Fichaje>();
            try
            {
                DataSet data = new DataSet();

                this.Conexion.NombrePaquete = "administra_detallefichaje";
                this.Conexion.QuerySQL = "getfichajeporfichadorayfecha";

                this.Conexion.SetValorParametroInput("i_idfichador", iFichadora.Id);
                this.Conexion.SetValorParametroInput("i_fechaproceso", iFecha);

                this.Conexion.ExecuteProcedure(out data);

                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                if (!Funcion.IsDataSetEmpty(data))
                    if (!Funcion.IsDataTableEmpty(data.Tables[0]))
                    {
                        data.Tables[0].Columns["idorden"].ColumnName = "Orden.Id";
                        data.Tables[0].Columns["idfichador"].ColumnName = "Trabajador.Id";
                        data.Tables[0].Columns["fechaproceso"].ColumnName = "AuditoriaSistema.FechaSistema";
                        data.Tables[0].Columns["monto"].ColumnName = "Monto";
                        data.Tables[0].Columns["estado"].ColumnName = "Estado";

                        fichaje = Funcion.ConvertToList<Fichaje>(data.Tables[0]);                        
                    }
            }
            catch (Exception)
            {
                throw;
            }
            return fichaje;
        }
        public List<Fichaje> GetFichajePorFichadoraEntreFechas(Planilla.Trabajador iFichadora, 
                                                                DateTime iFechaInicio,
                                                                DateTime iFechaFin)
        {
            List<Fichaje> fichaje = new List<Fichaje>();
            try
            {
                DataSet data = new DataSet();

                this.Conexion.NombrePaquete = "administra_detallefichaje";
                this.Conexion.QuerySQL = "getfichajeporfichadorayfechas";

                this.Conexion.SetValorParametroInput("i_idfichador", iFichadora.Id);
                this.Conexion.SetValorParametroInput("i_fechainicio", iFechaInicio);
                this.Conexion.SetValorParametroInput("i_fechafin", iFechaFin);

                this.Conexion.ExecuteProcedure(out data);

                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                if (!Funcion.IsDataSetEmpty(data))
                    if (!Funcion.IsDataTableEmpty(data.Tables[0]))
                    {
                        data.Tables[0].Columns["idorden"].ColumnName = "Orden.Id";
                        data.Tables[0].Columns["idfichador"].ColumnName = "Trabajador.Id";
                        data.Tables[0].Columns["fechaproceso"].ColumnName = "AuditoriaSistema.FechaSistema";
                        data.Tables[0].Columns["monto"].ColumnName = "Monto";
                        data.Tables[0].Columns["estado"].ColumnName = "Estado";

                        fichaje = Funcion.ConvertToList<Fichaje>(data.Tables[0]);
                    }
            }
            catch (Exception)
            {
                throw;
            }
            return fichaje;
        }
        public List<Fichaje> GetFichajePorNroOrden(Venta.Orden iOrden)
        {
            List<Fichaje> fichaje = new List<Fichaje>();
            try
            {
                DataSet data = new DataSet();

                this.Conexion.NombrePaquete = "administra_detallefichaje";
                this.Conexion.QuerySQL = "getfichajepororden";

                this.Conexion.SetValorParametroInput("i_orden", iOrden.Id);

                this.Conexion.ExecuteProcedure(out data);

                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                if (!Funcion.IsDataSetEmpty(data))
                    if (!Funcion.IsDataTableEmpty(data.Tables[0]))
                    {
                        data.Tables[0].Columns["idorden"].ColumnName = "Orden.Id";
                        data.Tables[0].Columns["idfichador"].ColumnName = "Trabajador.Id";
                        data.Tables[0].Columns["nombretrabajador"].ColumnName = "Trabajador.Nombres";
                        data.Tables[0].Columns["fechaproceso"].ColumnName = "AuditoriaSistema.FechaSistema";
                        data.Tables[0].Columns["monto"].ColumnName = "Monto";
                        data.Tables[0].Columns["estado"].ColumnName = "Estado";

                        fichaje = Funcion.ConvertToList<Fichaje>(data.Tables[0]);
                    }
            }
            catch (Exception)
            {
                throw;
            }
            return fichaje;
        }
        public void Anular()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_detallefichaje";
                this.Conexion.QuerySQL = "anular";

                this.Conexion.SetValorParametroInput("i_idorden", this.Orden.Id);

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
