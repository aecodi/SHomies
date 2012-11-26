using System;
using System.Data;
using System.Linq;
using SHomies.Conexion;
using SHomies.Core.Conexion;
using System.Collections.Generic;
using SHomies.Utilitario;

namespace SHomies.Core.Operaciones
{
    public class Multa : EntidadConexion
    {
        public Persona.PersonaNatural Fichadora { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }

        public Multa(IConexion iConexion)
        {
            this.Conexion = iConexion;
            this.Fichadora = new Persona.PersonaNatural(this.Conexion);
        }

        public Multa(decimal monto, DateTime fecha)
        {
            Fecha = fecha;
            Monto = monto;
        }

        public void Registrar()
        {
            try
            {
                Conexion.NombrePaquete = "administra_multa";
                Conexion.QuerySQL = "registrar";

                Conexion.SetValorParametroInput("i_idfichador", Fichadora.Id);
                Conexion.SetValorParametroInput("i_monto", this.Monto);
                Conexion.SetValorParametroInput("i_fechasistema", this.Fecha);

                Conexion.ExecuteProcedure();

                Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

            }
            catch (Exception)
            {
                throw;
            }
        }

        public decimal GetMontoMulta(Planilla.Trabajador iFichadora, DateTime iFecha)
        {
            var multa = 0M;
            try
            {
                this.Conexion.NombrePaquete = "administra_multa";
                this.Conexion.QuerySQL = "getmultaporfichadorayfecha";

                this.Conexion.SetValorParametroInput("i_idfichador", iFichadora.Id);
                this.Conexion.SetValorParametroInput("i_fechaproceso", iFecha);

                this.Conexion.ExecuteProcedure();

                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");
                multa = this.Conexion.GetValue<int>("o_montomulta");

            }
            catch (Exception)
            {
                throw;
            }
            return multa;
        }
    }
}
