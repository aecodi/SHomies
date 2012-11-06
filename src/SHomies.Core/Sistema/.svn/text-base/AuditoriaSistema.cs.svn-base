using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;

namespace SHomies.Core.Sistema
{
    public class AuditoriaSistema : Conexion.EntidadConexion
    {
        public bool EsAperturado { get; set; }
        public DateTime FechaSistema { get; set; }
        public Usuario Usuario { get; set; }
        public List<DetalleCierre> DetalleCierre { get; set; }

        public AuditoriaSistema()
        {
            this.InicializaVariables();
        }
        public AuditoriaSistema(IConexion iConexion)
        {
            this.Conexion = iConexion;
            this.InicializaVariables();
        }
        private void InicializaVariables() {
            this.Usuario = new Usuario(this.Conexion);
            this.DetalleCierre = new List<DetalleCierre>();
        }
        public void GetUltimaFechaSistema()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_sistema";
                this.Conexion.QuerySQL = "getultimafechasistema";

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                this.EsAperturado = this.Conexion.GetValue<int>("o_estado") == 1;
                this.FechaSistema = this.Conexion.GetValue<DateTime>("o_ultimafechasistema");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ValidaAperturaSistema()
        {
            try
            {
                this.GetUltimaFechaSistema();
                if (!this.EsAperturado)
                    throw new Utilitario.ExepcionSHomies("Sistema no aperturado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Aperturar()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_sistema";
                this.Conexion.QuerySQL = "aperturar";
                this.Conexion.SetValorParametroInput("i_fechaapertura", this.FechaSistema);
                this.Conexion.SetValorParametroInput("i_usuario", this.Usuario.UserName);

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Cerrar()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_sistema";
                this.Conexion.QuerySQL = "cerrar";
                this.Conexion.SetValorParametroInput("i_fechaapertura", this.FechaSistema);
                this.Conexion.SetValorParametroInput("i_usuario", this.Usuario.UserName);

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                this.EsAperturado = false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void GetDetalle()
        {
            try
            {
                this.DetalleCierre = new DetalleCierre(this.Conexion) { Fecha = FechaSistema }.GetDetalleCierre();
            }
            catch (Exception)
            {
                throw;
            };
        }
    }
}
