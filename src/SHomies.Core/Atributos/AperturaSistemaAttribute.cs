using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SHomies.Core.Atributos
{
    public class AperturaSistemaAttribute : Attribute
    {
        public void ValidarAperturaDelSistema()
        {
            try
            {
                SHomies.Conexion.IConexion conexion =
                    new SHomies.Conexion.Oracle.ConexionOracle("DATA SOURCE=SHomies;USER ID=SISTEMA;PASSWORD=shomies2012");

                Core.Sistema.AuditoriaSistema sistema =
                    new Sistema.AuditoriaSistema(conexion);

                sistema.ValidaAperturaSistema();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
