using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;
using SHomies.Core.Operaciones.Validaciones;

namespace SHomies.Core.Operaciones
{
    public class PagoFichadora : BaseOperacion.Operacion
    {
        public Persona.PersonaNatural Fichadora { get; set; }

        public PagoFichadora(IConexion iConexion)
            : base(iConexion)
        {
            this.Fichadora = new Persona.PersonaNatural(this.Conexion);
            this.Concepto = new Concepto(this.Conexion) { Id = 7 };
        }
        protected override void EjecutaOperacion()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_movimiento";
                this.Conexion.QuerySQL = "pagofichadora";

                this.Conexion.SetValorParametroInput("i_idfichador", this.Fichadora.Id);
                this.Conexion.SetValorParametroInput("i_fechaproceso", this.Auditoria.FechaSistema);

                this.Conexion.ExecuteProcedure();

                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void EjecutaValidacionesPre()
        {
            try
            {
                base.EjecutaValidacionesPre();
                new ValidaIdFichadora(this.Fichadora).Ejecutar();
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
