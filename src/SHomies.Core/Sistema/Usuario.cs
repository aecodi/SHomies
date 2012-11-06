using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;
using SHomies.Utilitario;

namespace SHomies.Core.Sistema
{
    public class Usuario : Persona.PersonaNatural
    {
        public string UserName { get; set; }

        public Usuario()
        {

        }
        public Usuario(IConexion iConexion)
            : base(iConexion)
        {

        }

        public void GetDatos()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_usuario";
                this.Conexion.QuerySQL = "GetDatos";
                this.Conexion.SetValorParametroInput("i_username", this.UserName.ToUpper());
                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                this.Clave = this.Conexion.GetValue<string>("o_clave");
                this.Estado = Funcion.ConvertToBoolean(this.Conexion.GetValue<int>("o_estado"));
                this.Id = this.Conexion.GetValue<int>("o_idtrabajador");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Clave { get; set; }

        public bool Estado { get; set; }
    }
}
