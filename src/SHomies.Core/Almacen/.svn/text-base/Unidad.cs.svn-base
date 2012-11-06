using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SHomies.Utilitario;

namespace SHomies.Core.Almacen
{
    public class Unidad
    {
        public SHomies.Conexion.IConexion Conexion;

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Abreviatura { get; set; }

        public Unidad()
        {

        }
        public Unidad(SHomies.Conexion.IConexion iConexion)
        {
            this.Conexion = iConexion;
        }

        public List<Entity.Almacen.Unidad> Listar()
        {
            List<Entity.Almacen.Unidad> unidades =
                new List<Entity.Almacen.Unidad>();
            try
            {
                DataSet datos = new DataSet();
                this.Conexion.NombrePaquete = "administra_unidades";
                this.Conexion.QuerySQL = "listar";
                this.Conexion.ExecuteProcedure(out datos);

                unidades = Funcion.ConvertToList<Entity.Almacen.Unidad>(datos.Tables[0]);
            }
            catch (Exception)
            {
                
                throw;
            }

            return unidades;
        }
    }
}
