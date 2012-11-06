using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace SHomies.Conexion
{
    public abstract class AConexion : IConexion
    {
        public string NombrePaquete { get; set; }
        public string QuerySQL { get; set; }
        protected bool existeTransaccion;
        protected Hashtable parametrosEntrada;

        public abstract void BeginTransaction();
        public abstract void Commit();
        public abstract void RoolBack();

        protected abstract bool OpenConexion();

        public abstract bool ExecuteProcedure();

        public abstract bool ExecuteProcedure(out System.Data.DataSet datos);

        public abstract void ExecuteQuery();

        public abstract T GetValue<T>(string iNombreParametro);

        public abstract void SetValorParametroInput(string iName, object iValue);

        protected abstract void GetParametrosProcedimiento();

        protected abstract void CreaComandoConexion(CommandType tipoComando);

        protected void LimpiaParametros()
        {
            if (this.parametrosEntrada != null)
                this.parametrosEntrada.Clear();
        }


        public void ValidaRespuesta(string iKeyCodigo, string iKeyMensaje)
        {
            try
            {
                int codigo = this.GetValue<int>(iKeyCodigo);
                if (codigo != 0)
                {
                    if (codigo == 999)
                        throw new Exception(this.GetValue<string>(iKeyMensaje));
                    throw new Utilitario.ExepcionSHomies(this.GetValue<string>(iKeyMensaje));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
