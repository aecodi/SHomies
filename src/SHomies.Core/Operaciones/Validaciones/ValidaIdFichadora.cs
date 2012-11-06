using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Utilitario;

namespace SHomies.Core.Operaciones.Validaciones
{
    public class ValidaIdFichadora : IValidacion
    {
        private Persona.PersonaNatural fichadora;
        public ValidaIdFichadora(Persona.PersonaNatural iFichadora)
        {
            this.fichadora = iFichadora;
        }
        public void Ejecutar()
        {
            try
            {
                Funcion.ValidaValorNulo(this.fichadora, "ValidaIdFichadora/fichadora");

                if (this.fichadora.Id == 0)
                    Funcion.EjecutaExepcionShomies("Ingrese fichadora");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
