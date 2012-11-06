using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Collections;
using SHomies.Utilitario;

namespace SHomies.Conexion.Oracle
{
    public class ConexionOracle : AConexion
    {
        private OracleConnection sqlConexion;
        private OracleCommand comandoConexion;
        private OracleTransaction sqlTransaccion;

        public ConexionOracle(string iCadenaConexion)
        {
            try
            {
                if (string.IsNullOrEmpty(iCadenaConexion))
                    throw new Exception("Cadena de conexión no especificada");
                this.sqlConexion = new OracleConnection(iCadenaConexion);
                this.comandoConexion = new OracleCommand();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void SetValorParametroInput(string iName, object iValue)
        {
            try
            {
                if (this.parametrosEntrada == null)
                    this.parametrosEntrada = new Hashtable();

                parametrosEntrada.Add(iName.ToUpper(), iValue);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void BeginTransaction()
        {
            try
            {
                this.OpenConexion();
                this.comandoConexion = this.sqlConexion.CreateCommand();
                this.comandoConexion.Connection = this.sqlConexion;
                if (this.sqlTransaccion != null)
                {
                    this.sqlTransaccion.Dispose();
                }
                this.sqlTransaccion = this.sqlConexion.BeginTransaction(IsolationLevel.ReadCommitted);
                this.existeTransaccion = true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public override void Commit()
        {
            try
            {
                if (this.existeTransaccion)
                {
                    this.existeTransaccion = false;
                    this.sqlTransaccion.Commit();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public override void RoolBack()
        {
            try
            {
                if (this.existeTransaccion)
                {
                    this.existeTransaccion = false;
                    this.sqlTransaccion.Rollback();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override bool ExecuteProcedure()
        {
            try
            {
                this.CreaComandoConexion(CommandType.StoredProcedure);
                this.comandoConexion.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.LimpiaParametros();
            }

            return true;
        }
        public override bool ExecuteProcedure(out DataSet datos)
        {
            datos = new DataSet();
            try
            {
                this.CreaComandoConexion(CommandType.StoredProcedure);

                OracleDataAdapter dataAdapter = new OracleDataAdapter(this.comandoConexion);
                dataAdapter.Fill(datos);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.LimpiaParametros();
            }

            return true;
        }
        public override void ExecuteQuery()
        {
            throw new NotImplementedException();
        }

        public override T GetValue<T>(string iNombreParametro)
        {
            object value = string.Empty;
            OracleParameter parametro = new OracleParameter();
            foreach (OracleParameter oracleParametro in this.comandoConexion.Parameters)
            {
                parametro = oracleParametro.ParameterName == iNombreParametro.ToUpper() ? oracleParametro : null;
                if (parametro != null)
                    break;
            }
            try
            {
                if (parametro != null)
                {
                    if (!(parametro.Value.GetType() == typeof(DBNull)))
                        value = Funcion.ConvertTo<T>(parametro.Value);
                    else
                        value = Funcion.ConvertTo<T>(null);
                }
                else
                {
                    throw new Exception("No se encontro parametro " + iNombreParametro);
                }

            }
            catch (Exception)
            {
                throw;
            }
            return (T)value;
        }

        protected override bool OpenConexion()
        {
            try
            {
                if (this.sqlConexion == null)
                {
                    throw new Exception("Objeto de conexión no valido");
                }
                else
                {
                    if (this.sqlConexion.State == ConnectionState.Closed)
                    {
                        this.sqlConexion.Open();
                    }
                }

                if (this.sqlConexion.State == ConnectionState.Closed)
                    throw new Exception("Conexión no se puede abrir");

                this.comandoConexion.Connection = this.sqlConexion;
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        protected override void GetParametrosProcedimiento()
        {
            OracleCommandBuilder.DeriveParameters(this.comandoConexion);
        }

        protected override void CreaComandoConexion(CommandType tipoComando)
        {
            if (this.OpenConexion())
            {
                if (string.IsNullOrEmpty(this.NombrePaquete))
                    throw new Exception("Nombre del Paquete no Valido");
                if (string.IsNullOrEmpty(this.QuerySQL))
                    throw new Exception("Nombre del procedimiento no Valido");
                if (this.comandoConexion == null)
                    this.comandoConexion = new OracleCommand();

                this.comandoConexion.CommandText = string.Concat(this.NombrePaquete, ".", this.QuerySQL);
                this.comandoConexion.CommandType = CommandType.StoredProcedure;
                this.comandoConexion.CommandTimeout = 20;
                if (this.comandoConexion.Connection == null)
                    this.comandoConexion.Connection = sqlConexion;

                OracleCommandBuilder.DeriveParameters(this.comandoConexion);

                foreach (OracleParameter parametroEntrada in this.comandoConexion.Parameters)
                {
                    if (parametroEntrada.Direction == ParameterDirection.Input)
                    {
                        object value = this.parametrosEntrada[parametroEntrada.ParameterName.ToUpper()];

                        if (value != null)
                            if (value.GetType() == typeof(bool))
                                value = (bool)value ? 1 : 0;


                        parametroEntrada.Value = value;
                    }
                }
            }
            else
            {
                throw new Exception("Conexión no se puede abrir");
            }
        }
    }

}
