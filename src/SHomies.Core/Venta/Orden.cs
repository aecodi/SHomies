using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;

namespace SHomies.Core.Venta
{
    public class Orden : Conexion.EntidadConexion
    {
        public int Id { get; set; }
        public Persona.Cliente Cliente { get; set; }
        public Sistema.AuditoriaSistema AuditoriaSistema { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal TotalComision { get; set; }
        public TipoPago TipoPago { get; set; }
        public TipoComprobante TipoComprobante { get; set; }
        public Mesa Mesa { get; set; }
        public Sistema.Usuario Mozo { get; set; }
        public List<DetalleOrden> DetalleProducto { get; set; }
        public List<Fichaje> Fichadoras { get; set; }
        public EstadoVenta Estado { get; set; }
        public DateTime FechaProceso { get; set; }

        public Orden(IConexion iConexion)
        {
            this.Conexion = iConexion;
            this.Cliente = new Persona.Cliente(iConexion);
            this.AuditoriaSistema = new Sistema.AuditoriaSistema(iConexion);
            this.Mesa = new Mesa(this.Conexion);
            this.Mozo = new Sistema.Usuario(this.Conexion);
            this.DetalleProducto = new List<DetalleOrden>();
            this.Fichadoras = new List<Fichaje>();
            this.Estado = new EstadoVenta();

            this.TotalVenta = 0;
            this.TotalComision = 0;
            this.TipoPago = Venta.TipoPago.EFECTIVO;
            this.TipoComprobante = Venta.TipoComprobante.BOLETA;
        }

        public Orden()
        {

        }

        public void GetNumeroOrden()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_orden";
                this.Conexion.QuerySQL = "getnumero";

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                this.Id = this.Conexion.GetValue<int>("o_id");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Registrar()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_orden";
                this.Conexion.QuerySQL = "registrar";

                this.Conexion.SetValorParametroInput("i_id", this.Id);
                this.Conexion.SetValorParametroInput("i_idcliente", this.Cliente.Id);
                this.Conexion.SetValorParametroInput("i_idmesa", this.Mesa.Id);
                this.Conexion.SetValorParametroInput("i_idmozo", this.Mozo.Id);
                this.Conexion.SetValorParametroInput("i_idusuario", this.AuditoriaSistema.Usuario.UserName);
                this.Conexion.SetValorParametroInput("i_fechaproceso", DateTime.Now);
                this.Conexion.SetValorParametroInput("i_fechasistema", this.AuditoriaSistema.FechaSistema);
                this.Conexion.SetValorParametroInput("i_tipopago", (int)this.TipoPago);
                this.Conexion.SetValorParametroInput("i_tipocomprobante", (int)this.TipoComprobante);
                this.Conexion.SetValorParametroInput("i_estado", this.Estado.Codigo);

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RegistraDetalle()
        {
            try
            {
                foreach (DetalleOrden detalle in this.DetalleProducto)
                {
                    detalle.Registrar();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RegistraFichaje()
        {
            try
            {
                foreach (Fichaje fichadora in this.Fichadoras)
                {
                    fichadora.Registrar();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void GetDatos()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_orden";
                this.Conexion.QuerySQL = "getdatos";

                this.Conexion.SetValorParametroInput("i_idorden", this.Id);

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                this.Cliente.Id = this.Conexion.GetValue<int>("o_idcliente");
                this.Mesa.Id = this.Conexion.GetValue<int>("o_idmesa");
                this.Mozo.Id = this.Conexion.GetValue<int>("o_idmozo");
                this.AuditoriaSistema.Usuario.UserName = this.Conexion.GetValue<string>("o_idusuario");
                this.FechaProceso = this.Conexion.GetValue<DateTime>("o_fechaproceso");
                this.AuditoriaSistema.FechaSistema = this.Conexion.GetValue<DateTime>("o_fechasistema");
                this.TipoPago = (TipoPago)this.Conexion.GetValue<int>("o_tipopago");
                this.TipoComprobante = (TipoComprobante)this.Conexion.GetValue<int>("o_tipocomprobante");
                this.Estado = new EstadoVenta
                {
                    Codigo = this.Conexion.GetValue<string>("o_estado")
                };

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void GetDatosOrden()
        {
            try
            {
                this.GetDatos();
                this.Fichadoras = new Fichaje(this.Conexion).GetFichajePorNroOrden(this);
                this.DetalleProducto = new DetalleOrden(this.Conexion).GetDetallePorNroOrden(this);
            }
            catch (Exception)
            {                
                throw;
            }
        }


        public void Anular()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_orden";
                this.Conexion.QuerySQL = "anular";

                this.Conexion.SetValorParametroInput("i_idorden", this.Id);

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
