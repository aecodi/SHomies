using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SHomies.Conexion
{
    public interface IConexion
    {
        string NombrePaquete { get; set; }
        string QuerySQL { get; set; }
        void SetValorParametroInput(string iName, object iValue);
        void BeginTransaction();
        void Commit();
        void RoolBack();
        bool ExecuteProcedure();
        bool ExecuteProcedure(out DataSet datos);
        void ExecuteQuery();
        void ValidaRespuesta(string iKeyCodigo, string iKeyMensaje);
        T GetValue<T>(string iNombreParametro);        
    }
}
