using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Conexion;
using SHomies.Utilitario;

namespace SHomies.Core.Operaciones.Validaciones
{
    public class ValidaAperturaSistema : IValidacion
    {
        private Sistema.AuditoriaSistema sistema;

        public ValidaAperturaSistema(Core.Sistema.AuditoriaSistema iSistema)
        {
            this.sistema = iSistema;
        }

        public void Ejecutar()
        {
            try
            {
                Funcion.ValidaValorNulo(this.sistema, "ValidaAperturaSistema/sistema");
                sistema.ValidaAperturaSistema();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
