using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;
using SHomies.Core.Operaciones;

namespace SHomies.Core.Sistema
{
    public class DetalleCierre : Conexion.EntidadConexion
    {
        public int Id { get; set; }
        public Operaciones.Concepto Concepto { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }

        public DetalleCierre()
        {
            this.Concepto = new Operaciones.Concepto();
        }
        public DetalleCierre(IConexion iConexion)
        {
            this.Conexion = iConexion;
            this.Concepto = new Operaciones.Concepto(iConexion);
        }

        public List<DetalleCierre> GetDetalleCierre()
        {
            List<DetalleCierre> detalle = new List<DetalleCierre>();
            try
            {
                List<Operaciones.Concepto> listaConceptos =
                    this.Concepto.ListarPorEstado(true);

                foreach (Operaciones.Concepto concepto in listaConceptos)
                {
                    DetalleCierre detalleCierre = new DetalleCierre(this.Conexion);
                    detalleCierre.Concepto = concepto;
                    detalleCierre.Fecha = this.Fecha;
                    detalleCierre.GetMontoConcepto();                   

                    detalle.Add(detalleCierre);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return detalle;
        }

        public void GetMontoConcepto()
        {
            try
            {
                switch (this.Concepto.Id)
                {
                    case 1:
                        this.GetTotalVenta();
                        break;
                    case 7:
                        this.GetTotalPagoFixadoras();
                        break;
                    default:
                        this.GetMonto();
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void GetMonto()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_sistema";
                this.Conexion.QuerySQL = "getmontoconcepto";

                this.Conexion.SetValorParametroInput("i_concepto", this.Concepto.Id);
                this.Conexion.SetValorParametroInput("i_fechasistema", this.Fecha);

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                this.Monto = this.Conexion.GetValue<decimal>("o_monto");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void GetTotalVenta()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_sistema";
                this.Conexion.QuerySQL = "gettotalventa";

                this.Conexion.SetValorParametroInput("i_fechasistema", this.Fecha);

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                this.Monto = this.Conexion.GetValue<decimal>("o_monto");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void GetTotalPagoFixadoras()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_sistema";
                this.Conexion.QuerySQL = "gettotalpagofixadoras";

                this.Conexion.SetValorParametroInput("i_fechasistema", this.Fecha);

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                this.Monto = this.Conexion.GetValue<decimal>("o_monto");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ActualizaMonto()
        {
            try
            {
                this.GetIdDetallePorConceptoYFecha();

                if (this.Id == 0)
                {
                    this.Registrar();
                }
                else
                {
                    this.Actualizar();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Registrar()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_sistema";
                this.Conexion.QuerySQL = "registramontoconcepto";

                this.Conexion.SetValorParametroInput("i_fechasistema", this.Fecha);
                this.Conexion.SetValorParametroInput("i_concepto", this.Concepto.Id);
                this.Conexion.SetValorParametroInput("i_monto", this.Monto);

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                this.Id = this.Conexion.GetValue<int>("o_id");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Actualizar()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_sistema";
                this.Conexion.QuerySQL = "actualizamontoconcepto";

                this.Conexion.SetValorParametroInput("i_id", this.Id);
                this.Conexion.SetValorParametroInput("i_monto", this.Monto);

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void GetIdDetallePorConceptoYFecha()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_sistema";
                this.Conexion.QuerySQL = "getiddetalleporconcepto";

                this.Conexion.SetValorParametroInput("i_fechasistema", this.Fecha);
                this.Conexion.SetValorParametroInput("i_concepto", this.Concepto.Id);

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                this.Id = this.Conexion.GetValue<int>("o_id");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
