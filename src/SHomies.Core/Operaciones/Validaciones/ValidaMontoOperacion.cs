using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SHomies.Core.Operaciones.Validaciones
{
    public class ValidaMontoOperacion : IValidacion
    {
        private decimal montoOperacion;

        public ValidaMontoOperacion(decimal iMontoOperacion)
        {
            this.montoOperacion = iMontoOperacion;
        }
        public void Ejecutar()
        {
            try
            {
                if (this.montoOperacion == 0)
                    throw new Utilitario.ExepcionSHomies("Monto de la operación debe ser mayor a cero");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
