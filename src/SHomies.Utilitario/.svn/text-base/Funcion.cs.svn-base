using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.ComponentModel;
using System.IO;

namespace SHomies.Utilitario
{
    public class Funcion
    {
        public static string NombreModulo = "SHomies";
        public static DateTime FechaNula = new DateTime(1, 1, 1);
        public static List<T> ConvertToList<T>(DataTable data)
        {
            List<T> lista = new List<T>();

            foreach (DataRow item in data.Rows)
            {
                object ObjetoLista = Activator.CreateInstance<T>();
                foreach (PropertyInfo propiedadClase in ObjetoLista.GetType().GetProperties())
                {
                    SeteaValores(ObjetoLista, propiedadClase, item, data.Columns, propiedadClase.Name);
                }
                lista.Add((T)ObjetoLista);
            }
            return lista;
        }
        public static void RenameColumnsDataTable(DataTable data)
        {
            foreach (DataColumn columna in data.Columns)
                columna.ColumnName = columna.ColumnName.ToUpper();
        }
        public static void SeteaValores(object objeto, PropertyInfo propiedad, DataRow data, DataColumnCollection columnas, string nombrePropiedad)
        {
            object valor = null;

            if (EsClaseLaPropiedad(propiedad))
            {
                object objetoClase = Activator.CreateInstance(propiedad.PropertyType.UnderlyingSystemType);
                foreach (PropertyInfo propiedadClase in objetoClase.GetType().GetProperties())
                {
                    string NameProperty = string.Concat(objetoClase.GetType().Name.ToUpper(), ".", propiedadClase.Name.ToUpper());

                    if (EsClaseLaPropiedad(propiedadClase))
                    {
                        SeteaValores(objetoClase, propiedadClase, data, columnas, NameProperty);
                    }
                    else
                    {                        
                        if (columnas.Contains(NameProperty))
                            SeteaValores(objetoClase, propiedadClase, data, columnas, NameProperty);
                    }
                    
                }
                propiedad.SetValue(objeto, objetoClase, null);
            }
            else
            {
                if (columnas.Contains(nombrePropiedad.ToUpper()))
                {
                    valor = data[nombrePropiedad];
                    SetValorPropiedad(objeto, propiedad, valor);
                }
            }
        }
        public static void SetValorPropiedad(object objeto, PropertyInfo propiedad, object valor)
        {
            valor = Convert.ChangeType(valor, propiedad.PropertyType.UnderlyingSystemType);
            propiedad.SetValue(objeto, valor, null);
        }
        public static bool EsClaseLaPropiedad(PropertyInfo propiedad)
        {
            if (propiedad.PropertyType.FullName.StartsWith(NombreModulo) &&
                propiedad.PropertyType.IsClass)
                return true;
            return false;
        }

        public static object SetValorAlObjeto(DataColumnCollection columnas, object valor, string nombreObjeto, Type tipoDato)
        {
            bool SeSeteoValor = false;
            object objeto = Activator.CreateInstance(tipoDato);
            PropertyInfo[] propiedades = objeto.GetType().GetProperties();
            foreach (PropertyInfo propiedad in propiedades)
            {
                foreach (DataColumn columna in columnas)
                {
                    if (propiedad.PropertyType.FullName.StartsWith(NombreModulo) &&
                        propiedad.PropertyType.IsClass)
                    {
                        Type tipo = propiedad.PropertyType.UnderlyingSystemType;
                        propiedad.SetValue(objeto, SetValorAlObjeto(columnas, valor, string.Concat(nombreObjeto, ".", propiedad.Name.ToUpper()), tipo), null);
                        break;
                    }
                    else
                    {
                        if (columna.ColumnName.ToUpper() == string.Concat(nombreObjeto, ".", propiedad.Name.ToUpper()))
                        {
                            propiedad.SetValue(objeto, Convert.ChangeType(valor, propiedad.PropertyType.UnderlyingSystemType), null);
                            SeSeteoValor = true;
                            break;
                        }
                    }
                }
                if (SeSeteoValor)
                    break;
            }
            return objeto;
        }
        public static T ConvertTo<T>(object value, object valorPorDefecto = null)
        {
            Object result = null;
            try
            {
                if (Funcion.EsValorValido<T>(value))
                    result = value;
                else
                    result = valorPorDefecto;

                result = Convert.ChangeType(result, typeof(T));
            }
            catch (Exception)
            {
                result = valorPorDefecto;
            }

            return (T)(result);
        }
        private static Object FormateaValorNulo(Object value, Type type)
        {
            Object newValue = value;
            if (newValue == null)
            {
                switch (type.Name)
                {
                    case "Decimal":
                    case "Double":
                    case "Int32":
                    case "Int64":
                    case "long":
                    case "short":
                        newValue = 0;
                        break;
                    case "String":
                        newValue = String.Empty;
                        break;
                    default:
                        break;
                }
            }
            return newValue;
        }
        public static void SetValueToRow(DataRow fila, string columna, object valor)
        {
            try
            {
                if (!fila.Table.Columns.Contains(columna))
                    fila.Table.Columns.Add(columna);
                fila[columna] = valor;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static byte[] ConvertStreamToBytes(Stream data)
        {
            byte[] fileData = new byte[0];
            try
            {
                int longitudImage = Convert.ToInt32(data.Length);
                fileData = new byte[longitudImage + 1];
                data.Read(fileData, 0, longitudImage);
                data.Close();
            }
            catch (Exception)
            {
                fileData = null;
            }

            return fileData;
        }
        public static bool EsValorValido<T>(object valor)
        {
            bool response = false;
            try
            {
                Convert.ChangeType(valor, typeof(T));
                response = true;
            }
            catch (Exception) { }
            return response;
        }
        public static bool IsDataSetEmpty(DataSet data)
        {
            if (data.Tables.Count > 0)
                return false;

            return true;
        }
        public static bool IsDataTableEmpty(DataTable data)
        {
            if (data.Rows.Count > 0)
                return false;

            return true;
        }

        public static string FormatoDecimal(decimal value)
        {
            return value.ToString("#,###,###,##0.00");
        }
        public static void ValidaValorNulo(Object iObjeto, string iMensaje)
        {
            if (iObjeto == null)
                throw new Exception(iMensaje);
        }
        public static void EjecutaExepcionShomies(string iMensaje)
        {
            throw new ExepcionSHomies(iMensaje);
        }

        public static bool ConvertToBoolean(int iValue)
        {
            return iValue == 1;
        }
    }
}

