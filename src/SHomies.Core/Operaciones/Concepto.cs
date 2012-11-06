using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;
using System.Data;
using SHomies.Utilitario;

namespace SHomies.Core.Operaciones
{
    public class Concepto : Conexion.EntidadConexion
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public int Tipo { get; set; }

        public Concepto()
        {
            this.Tipo = 0;
        }
        public Concepto(IConexion iConexion)
        {
            this.Tipo = 0;
            this.Conexion = iConexion;
        }

        public List<Concepto> ListarPorEstado(bool iEstado)
        {
            List<Concepto> conceptos = new List<Concepto>();
            try
            {
                DataSet datos = new DataSet();

                this.Conexion.NombrePaquete = "administra_conceptos";
                this.Conexion.QuerySQL = "listar_x_estado";
                this.Conexion.SetValorParametroInput("i_estado", iEstado);

                this.Conexion.ExecuteProcedure(out datos);
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                if (!Funcion.IsDataSetEmpty(datos))
                    if (!Funcion.IsDataTableEmpty(datos.Tables[0]))
                    {
                        conceptos = Funcion.ConvertToList<Concepto>(datos.Tables[0]);
                    }

            }
            catch (Exception)
            {

                throw;
            }

            return conceptos;
        }
    }
}
