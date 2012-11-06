using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Utilitario;

namespace SHomies.Core.Operaciones.Validaciones
{
   public class ValidaNroComprobante:IValidacion
    {
       private decimal nroComprobante;
       public ValidaNroComprobante(decimal iNroComprobante)
       {
           this.nroComprobante = iNroComprobante;
       }
        public void Ejecutar()
        {
            try
            {
                if (this.nroComprobante == 0)
                    Funcion.EjecutaExepcionShomies("Nro de compronte no valido");
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
