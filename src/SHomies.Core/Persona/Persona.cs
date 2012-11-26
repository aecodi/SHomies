using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Core.Interfaces;

namespace SHomies.Core.Persona
{
    public class Persona : Conexion.EntidadConexion, IPersona
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }

        public Persona()
        {

        }
        public Persona(SHomies.Conexion.IConexion iConexion)
        {
            this.Conexion = iConexion;
        }
        public virtual void Nuevo()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_persona";
                this.Conexion.QuerySQL = "nuevo";

                this.Conexion.SetValorParametroInput("i_nombrecompleto", this.NombreCompleto);
                this.Conexion.SetValorParametroInput("i_tipodocumento", this.TipoDocumento.Codigo);
                this.Conexion.SetValorParametroInput("i_nrodocumento", this.NumeroDocumento);

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                this.Id = this.Conexion.GetValue<int>("o_id");
            }
            catch (Exception)
            {                
                throw;
            }
        }
        public virtual void UpdateName()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_persona";
                this.Conexion.QuerySQL = "updatename";
                
                this.Conexion.SetValorParametroInput("i_nombrecompleto", this.NombreCompleto);
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
