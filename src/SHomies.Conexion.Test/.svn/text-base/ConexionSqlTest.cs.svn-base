using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data;

namespace SHomies.Conexion.Test
{
    [TestFixture]
    public class ConexionSqlTest
    {
        [Test]
        [ExpectedException(typeof(Exception), ExpectedMessage = "Cadena de conexión no especificada")]
        public void Instanciar_CASO_1_CUANDO_cadena_de_conexion_vacia_ENTONCES_error_ingrese_cadena_de_conexion()
        {
            SQL.ConexionSQL sqlConexion = new SQL.ConexionSQL(string.Empty);
        }
        [Test]
        public void AddParametroIn_CASO_1_CUANDO_agrego_parametro_al_procedimiento_entero_con_valor_5_ENTONCES_parametro_1_con_valor_5_tipo_de_dato_int()
        {
            SQL.ConexionSQL sqlConexion = new SQL.ConexionSQL(@"Data Source=OPPDEV08\Shomies;Initial Catalog=SHomies.Relax;User ID=sa;Password=123456;Pooling=False");
            sqlConexion.SetValorParametroInput("valor", 5);
            Assert.AreEqual(5, sqlConexion.GetValue<int>("valor"));
        }
        [Test]
        public void AddParametroIn_CASO_2_CUANDO_agrego_parametro_al_procedimiento_cadena_con_valor_X_ENTONCES_parametro_1_con_valor_X_tipo_de_dato_string()
        {
            SQL.ConexionSQL sqlConexion = new SQL.ConexionSQL(@"Data Source=OPPDEV08\Shomies;Initial Catalog=SHomies.Relax;User ID=sa;Password=123456;Pooling=False");
            sqlConexion.SetValorParametroInput("valor", "X");
            Assert.AreEqual("X", sqlConexion.GetValue<string>("valor"));
        }
    }
}
