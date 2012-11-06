using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Utilitario;

namespace SHomies.Core.Operaciones.Validaciones
{
    public class ValidaIdConcepto : IValidacion
    {
        private Concepto concepto;
        public ValidaIdConcepto(Concepto iConcepto)
        {
            this.concepto = iConcepto;
        }
        public void Ejecutar()
        {
            try
            {
                Funcion.ValidaValorNulo(this.concepto, "ValidaIdConcepto/concepto");
                if (this.concepto.Id == 0)
                    Funcion.EjecutaExepcionShomies("Concepto de la operación no definido");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
