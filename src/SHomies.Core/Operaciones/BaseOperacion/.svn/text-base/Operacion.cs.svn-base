using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;
using SHomies.Core.Operaciones.Validaciones;
using SHomies.Utilitario;

namespace SHomies.Core.Operaciones.BaseOperacion
{
    public abstract class Operacion : Conexion.EntidadConexion, IOperacion
    {
        public int NroComprobante { get; set; }
        public decimal Monto { get; set; }
        public Sistema.AuditoriaSistema Auditoria { get; set; }
        public Persona.Persona Cliente { get; set; }
        public Concepto Concepto { get; set; }
        
        public Operacion()
        {
            this.InicializaVariables();
        }
        public Operacion(IConexion iConexion)
        {
            this.Conexion = iConexion;
            this.InicializaVariables();
        }
        private void InicializaVariables()
        {
            this.NroComprobante = 0;
            this.Monto = 0;
            this.Auditoria = new Sistema.AuditoriaSistema(this.Conexion);
            this.Cliente = new Persona.Persona(this.Conexion);
            this.Concepto = new Concepto(this.Conexion);
        }
        public void Procesar()
        {
            try
            {
                Funcion.ValidaValorNulo(this.Conexion, "Operacion/Conexion");

                this.Conexion.BeginTransaction();

                this.EjecutaValidacionesPre();

                this.RegistraMovimiento();

                this.EjecutaOperacion();

                this.EjecutaValidacionesPost();

                this.Conexion.Commit();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.Conexion.RoolBack();
            }
        }

        protected virtual void EjecutaValidacionesPre()
        {
            try
            {
                new ValidaMontoOperacion(this.Monto).Ejecutar();
                new ValidaUsuario(this.Auditoria.Usuario).Ejecutar();
                new ValidaIdConcepto(this.Concepto).Ejecutar();
                new ValidaAperturaSistema(this.Auditoria).Ejecutar();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected virtual void EjecutaValidacionesPost()
        {
            try
            {
                new ValidaNroComprobante(this.NroComprobante).Ejecutar();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected abstract void EjecutaOperacion();

        private void RegistraMovimiento()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_movimiento";
                this.Conexion.QuerySQL = "registramovimiento";
                
                this.Conexion.SetValorParametroInput("i_monto", this.Monto);
                this.Conexion.SetValorParametroInput("i_username", this.Auditoria.Usuario.UserName);
                this.Conexion.SetValorParametroInput("i_fechasistema", this.Auditoria.FechaSistema);
                this.Conexion.SetValorParametroInput("i_concepto", this.Concepto.Id);

                this.Conexion.ExecuteProcedure();

                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                this.NroComprobante = this.Conexion.GetValue<int>("o_nrocomprobante");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
