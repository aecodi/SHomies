using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SHomies.Utilitario;
using SHomies.Core.Interfaces;

namespace SHomies.Core.Almacen
{
    public class Producto : Abstracts.AProducto
    {
        public decimal PrecioVenta { get; set; }
        public bool StockLimitado { get; set; }
        public decimal Stock { get; set; }
        public Unidad Unidad { get; set; }
        public string Imagen { get; set; }
        public decimal Comision { get; set; }

        public Producto()
        {

        }
        public Producto(SHomies.Conexion.IConexion iConexion)
            : base(iConexion)
        {
            this.Categoria = new Categoria(this.Conexion);
            this.Unidad = new Unidad(this.Conexion);
        }

        public bool Registrar()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_productos";
                this.Conexion.QuerySQL = "registrar";

                this.Conexion.SetValorParametroInput("i_Descripcion", this.Descripcion);
                this.Conexion.SetValorParametroInput("i_Categoria", this.Categoria.Id);
                this.Conexion.SetValorParametroInput("i_PrecioVenta", this.PrecioVenta);
                this.Conexion.SetValorParametroInput("i_Comision", this.Comision);
                this.Conexion.SetValorParametroInput("i_Stock", this.Stock);
                this.Conexion.SetValorParametroInput("i_Unidad", this.Unidad.Id);
                this.Conexion.SetValorParametroInput("i_StockLimitado", this.StockLimitado);
                this.Conexion.SetValorParametroInput("i_Estado", this.Estado);
                this.Conexion.SetValorParametroInput("i_Imagen", this.Imagen);

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
            return true;
        }

        public bool Modificar()
        {
            try
            {
                this.Conexion.NombrePaquete = "administra_productos";
                this.Conexion.QuerySQL = "modificar";

                this.Conexion.SetValorParametroInput("i_Id", this.Id);
                this.Conexion.SetValorParametroInput("i_Descripcion", this.Descripcion);
                this.Conexion.SetValorParametroInput("i_Categoria", this.Categoria.Id);
                this.Conexion.SetValorParametroInput("i_PrecioVenta", this.PrecioVenta);
                this.Conexion.SetValorParametroInput("i_Comision", this.Comision);
                this.Conexion.SetValorParametroInput("i_Stock", this.Stock);
                this.Conexion.SetValorParametroInput("i_Unidad", this.Unidad.Id);
                this.Conexion.SetValorParametroInput("i_StockLimitado", this.StockLimitado);
                this.Conexion.SetValorParametroInput("i_Estado", this.Estado);
                this.Conexion.SetValorParametroInput("i_Imagen", this.Imagen);

                this.Conexion.ExecuteProcedure();
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");
            }
            catch (Exception)
            {

                throw;
            }
            return true;
        }

        public List<Entity.Almacen.Producto> Listar()
        {
            List<Entity.Almacen.Producto> listaProductos =
                new List<Entity.Almacen.Producto>();
            try
            {
                DataSet datos = new DataSet();
                this.Conexion.NombrePaquete = "administra_productos";
                this.Conexion.QuerySQL = "listar";

                this.Conexion.ExecuteProcedure(out datos);
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                if (!Funcion.IsDataSetEmpty(datos))
                    if (!Funcion.IsDataTableEmpty(datos.Tables[0]))
                    {
                        datos.Tables[0].Columns["idcategoria"].ColumnName = "Categoria.Id";
                        datos.Tables[0].Columns["descripcioncategoria"].ColumnName = "Categoria.Descripcion";
                        datos.Tables[0].Columns["idunidad"].ColumnName = "Unidad.Id";
                        datos.Tables[0].Columns["descripcionunidad"].ColumnName = "Unidad.Descripcion";
                        listaProductos = Funcion.ConvertToList<Entity.Almacen.Producto>(datos.Tables[0]);
                    }
            }
            catch (Exception)
            {

                throw;
            }
            return listaProductos;
        }
        public List<Entity.Almacen.Producto> ListarPorEstado(bool iEstado)
        {
            List<Entity.Almacen.Producto> listaProductos =
                new List<Entity.Almacen.Producto>();
            try
            {
                DataSet datos = new DataSet();
                this.Conexion.NombrePaquete = "administra_productos";
                this.Conexion.QuerySQL = "listar_por_estado";
                this.Conexion.SetValorParametroInput("i_Estado", iEstado);
                this.Conexion.ExecuteProcedure(out datos);
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                if (!Funcion.IsDataSetEmpty(datos))
                    if (!Funcion.IsDataTableEmpty(datos.Tables[0]))
                    {
                        datos.Tables[0].Columns["idcategoria"].ColumnName = "Categoria.Id";
                        datos.Tables[0].Columns["descripcioncategoria"].ColumnName = "Categoria.Descripcion";
                        datos.Tables[0].Columns["idunidad"].ColumnName = "Unidad.Id";
                        datos.Tables[0].Columns["descripcionunidad"].ColumnName = "Unidad.Descripcion";
                        listaProductos = Funcion.ConvertToList<Entity.Almacen.Producto>(datos.Tables[0]);
                    }
            }
            catch (Exception)
            {

                throw;
            }
            return listaProductos;
        }
        public List<Entity.Almacen.Producto> BuscarPorDescripcion(string iDescripcion)
        {
            List<Entity.Almacen.Producto> listaProductos =
                new List<Entity.Almacen.Producto>();
            try
            {
                DataSet datos = new DataSet();
                this.Conexion.NombrePaquete = "administra_productos";
                this.Conexion.QuerySQL = "buscar_x_descripcion";
                this.Conexion.SetValorParametroInput("i_Descripcion", iDescripcion);
                this.Conexion.ExecuteProcedure(out datos);
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                if (!Funcion.IsDataSetEmpty(datos))
                    if (!Funcion.IsDataTableEmpty(datos.Tables[0]))
                    {
                        datos.Tables[0].Columns["idcategoria"].ColumnName = "Categoria.Id";
                        datos.Tables[0].Columns["descripcioncategoria"].ColumnName = "Categoria.Descripcion";
                        datos.Tables[0].Columns["idunidad"].ColumnName = "Unidad.Id";
                        datos.Tables[0].Columns["descripcionunidad"].ColumnName = "Unidad.Descripcion";
                        listaProductos = Funcion.ConvertToList<Entity.Almacen.Producto>(datos.Tables[0]);
                    }
            }
            catch (Exception)
            {

                throw;
            }
            return listaProductos;
        }
        public List<Entity.Almacen.Producto> BuscarPorDescripcionYEstado(bool iEstado, string iDescripcion)
        {
            List<Entity.Almacen.Producto> listaProductos =
               new List<Entity.Almacen.Producto>();
            try
            {
                DataSet datos = new DataSet();
                this.Conexion.NombrePaquete = "administra_productos";
                this.Conexion.QuerySQL = "buscar_x_descripcion_y_estado";
                this.Conexion.SetValorParametroInput("i_Estado", iEstado);
                this.Conexion.SetValorParametroInput("i_Descripcion", iDescripcion);

                this.Conexion.ExecuteProcedure(out datos);
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                if (!Funcion.IsDataSetEmpty(datos))
                    if (!Funcion.IsDataTableEmpty(datos.Tables[0]))
                    {
                        datos.Tables[0].Columns["idcategoria"].ColumnName = "Categoria.Id";
                        datos.Tables[0].Columns["descripcioncategoria"].ColumnName = "Categoria.Descripcion";
                        datos.Tables[0].Columns["idunidad"].ColumnName = "Unidad.Id";
                        datos.Tables[0].Columns["descripcionunidad"].ColumnName = "Unidad.Descripcion";
                        listaProductos = Funcion.ConvertToList<Entity.Almacen.Producto>(datos.Tables[0]);
                    }
            }
            catch (Exception)
            {

                throw;
            }
            return listaProductos;
        }
        public List<Entity.Almacen.Producto> ListaPorCategoria(Entity.Almacen.Categoria iCategoria)
        {
            List<Entity.Almacen.Producto> listaProductos = new List<Entity.Almacen.Producto>();
            try
            {
                DataSet datos = new DataSet();

                this.Conexion.NombrePaquete = "administra_productos";
                this.Conexion.QuerySQL = "listar_por_categoria";
                this.Conexion.SetValorParametroInput("i_Categoria", iCategoria.Id);

                this.Conexion.ExecuteProcedure(out datos);
                this.Conexion.ValidaRespuesta("o_codigo", "o_mensaje");

                if (!Funcion.IsDataSetEmpty(datos))
                    if (!Funcion.IsDataTableEmpty(datos.Tables[0]))
                    {
                        datos.Tables[0].Columns["idcategoria"].ColumnName = "Categoria.Id";
                        datos.Tables[0].Columns["descripcioncategoria"].ColumnName = "Categoria.Descripcion";
                        datos.Tables[0].Columns["idunidad"].ColumnName = "Unidad.Id";
                        datos.Tables[0].Columns["descripcionunidad"].ColumnName = "Unidad.Descripcion";
                        listaProductos = Funcion.ConvertToList<Entity.Almacen.Producto>(datos.Tables[0]);
                    }
            }
            catch (Exception)
            {
                throw;
            }
            return listaProductos;
        }


    }
}
