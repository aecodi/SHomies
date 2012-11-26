using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;

namespace SHomies.Core.Persona
{
    public class PersonaNatural : Persona
    {
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public char Sexo { get; set; }

        public PersonaNatural()
        {

        }
        public PersonaNatural(IConexion iConexion)
            : base(iConexion)
        {

        }
        public override void Nuevo()
        {
            try
            {
                base.Nuevo();

                this.Conexion.NombrePaquete = "administra_personanatural";
                this.Conexion.QuerySQL = "nuevo";

                this.Conexion.SetValorParametroInput("i_id", this.Id);
                this.Conexion.SetValorParametroInput("i_nombres", this.Nombres);
                this.Conexion.SetValorParametroInput("i_apellidopaterno", this.ApellidoPaterno);
                this.Conexion.SetValorParametroInput("i_apellidomaterno", this.ApellidoMaterno);
                this.Conexion.SetValorParametroInput("i_fechanacimiento", this.FechaNacimiento);
                this.Conexion.SetValorParametroInput("i_sexo", this.Sexo);;

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public override void UpdateName()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_personanatural";
                this.Conexion.QuerySQL = "updatename";

                this.Conexion.SetValorParametroInput("i_nombre", this.Nombres);
                this.Conexion.SetValorParametroInput("i_id", this.Id);

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
