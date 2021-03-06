﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;
using System.Data;
using SHomies.Utilitario;

namespace SHomies.Core.Venta
{
    public class DetalleOrden : Conexion.EntidadConexion
    {
        public Orden Orden { get; set; }
        public Almacen.Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        public DetalleOrden()
        {

        }
        public DetalleOrden(IConexion iConexion)
        {
            this.Conexion = iConexion;
            this.Producto = new Almacen.Producto(iConexion);
            this.Cantidad = 0;
            this.Total = 0;
            this.Orden = new Orden(this.Conexion);
        }

        public void Registrar()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_detalleorden";
                this.Conexion.QuerySQL = "registrar";

                this.Conexion.SetValorParametroInput("i_idorden", this.Orden.Id);
                this.Conexion.SetValorParametroInput("i_idproducto", this.Producto.Id);
                this.Conexion.SetValorParametroInput("i_cantidad", this.Cantidad);
                this.Conexion.SetValorParametroInput("i_fechaproceso", this.Orden.AuditoriaSistema.FechaSistema);
                this.Conexion.SetValorParametroInput("i_precioventa", this.Producto.PrecioVenta);
                this.Conexion.SetValorParametroInput("i_comision", this.Producto.Comision);
                this.Conexion.SetValorParametroInput("i_total", this.Total);

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<DetalleOrden> GetDetallePorNroOrden(Venta.Orden iOrden)
        {
            List<DetalleOrden> detalleOrden = new List<DetalleOrden>();
            try
            {
                DataSet data = new DataSet();

                this.Conexion.NombrePaquete = "administra_detalleorden";
                this.Conexion.QuerySQL = "getdetallepororden";

                this.Conexion.SetValorParametroInput("i_orden", iOrden.Id);

                this.Conexion.ExecuteProcedure(out data);

                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                if (!Funcion.IsDataSetEmpty(data))
                    if (!Funcion.IsDataTableEmpty(data.Tables[0]))
                    {
                        data.Tables[0].Columns["idorden"].ColumnName = "Orden.Id";
                        data.Tables[0].Columns["idproducto"].ColumnName = "Producto.Id";
                        data.Tables[0].Columns["nombreproducto"].ColumnName = "Producto.Descripcion";
                        data.Tables[0].Columns["precioventa"].ColumnName = "Producto.PrecioVenta";
                        data.Tables[0].Columns["comision"].ColumnName = "Producto.Comision";
                        data.Tables[0].Columns["imagenproducto"].ColumnName = "Producto.Imagen";

                        detalleOrden = Funcion.ConvertToList<DetalleOrden>(data.Tables[0]);
                    }
            }
            catch (Exception)
            {
                throw;
            }
            return detalleOrden;
        }
        public void Anular()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_detalleorden";
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
