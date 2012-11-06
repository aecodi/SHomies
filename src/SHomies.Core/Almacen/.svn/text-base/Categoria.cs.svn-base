using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SHomies.Utilitario;

namespace SHomies.Core.Almacen
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public string Imagen { get; set; }

        public SHomies.Conexion.IConexion Conexion;

        public Categoria() { }
        public Categoria(SHomies.Conexion.IConexion iConexion)
        {
            this.Conexion = iConexion;
        }
        public List<Entity.Almacen.Categoria> ListaPorEstado(bool iEstado)
        {
            List<Entity.Almacen.Categoria> categorias = new List<Entity.Almacen.Categoria>();
            try
            {
                DataSet datos = new DataSet();

                this.Conexion.NombrePaquete = "administra_categorias";
                this.Conexion.QuerySQL = "lista_categorias_x_estado";
                this.Conexion.SetValorParametroInput("i_Estado", iEstado);
                this.Conexion.ExecuteProcedure(out datos);

                categorias = Funcion.ConvertToList<Entity.Almacen.Categoria>(datos.Tables[0]);
            }
            catch (Exception)
            {
                throw;
            }

            return categorias;
        }
        public List<Entity.Almacen.Categoria> Listar()
        {
            List<Entity.Almacen.Categoria> categorias = new List<Entity.Almacen.Categoria>();
            try
            {
                DataSet datos = new DataSet();

                this.Conexion.NombrePaquete = "administra_categorias";
                this.Conexion.QuerySQL = "listar";
                this.Conexion.ExecuteProcedure(out datos);

                categorias = Funcion.ConvertToList<Entity.Almacen.Categoria>(datos.Tables[0]);
            }
            catch (Exception)
            {
                throw;
            }

            return categorias;
        }
        public void Registrar()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_categorias";
                this.Conexion.QuerySQL = "registrar";

                this.Conexion.SetValorParametroInput("i_Descripcion", this.Descripcion);
                this.Conexion.SetValorParametroInput("i_Estado", this.Estado);
                this.Conexion.SetValorParametroInput("i_imagen", this.Imagen);

                this.Conexion.ExecuteProcedure();

                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                this.Id = this.Conexion.GetValue<int>("o_Id");

                if (this.Id == 0)
                {
                    throw new Utilitario.ExepcionSHomies("No se puede obtener codigo de la categoria");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Modificar()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_categorias";
                this.Conexion.QuerySQL = "modificar";

                this.Conexion.SetValorParametroInput("i_Id", this.Id);
                this.Conexion.SetValorParametroInput("i_Descripcion", this.Descripcion);
                this.Conexion.SetValorParametroInput("i_Estado", this.Estado);
                this.Conexion.SetValorParametroInput("i_Imagen", this.Imagen);

                this.Conexion.ExecuteProcedure();

                this.Conexion.ValidaRespuesta("o_Codigo", "o_Mensaje");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Entity.Almacen.Categoria> BuscarPorDescripcionYEstado()
        {
            List<Entity.Almacen.Categoria> categorias = new List<Entity.Almacen.Categoria>();
            try
            {
                DataSet datos = new DataSet();

                this.Conexion.NombrePaquete = "administra_categorias";
                this.Conexion.QuerySQL = "buscar_x_descripcion_y_estado";
                this.Conexion.SetValorParametroInput("i_Descripcion", this.Descripcion);
                this.Conexion.SetValorParametroInput("i_Estado", this.Estado);
                this.Conexion.ExecuteProcedure(out datos);

                categorias = Funcion.ConvertToList<Entity.Almacen.Categoria>(datos.Tables[0]);
            }
            catch (Exception)
            {
                throw;
            }

            return categorias;
        }
    }
}
