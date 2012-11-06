using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Utilitario;

namespace SHomies.Core.Operaciones.Validaciones
{
    public class ValidaUsuario : IValidacion
    {
        private Sistema.Usuario usuario;
        public ValidaUsuario(Sistema.Usuario iUsuario)
        {
            this.usuario = iUsuario;
            Utilitario.Funcion.ValidaValorNulo(this.usuario, "ValidaUsuario/Usuario");
        }
        public void Ejecutar()
        {
            try
            {
                if (string.IsNullOrEmpty(this.usuario.UserName))
                    Funcion.EjecutaExepcionShomies("Codigo de usuario no valido");
                this.usuario.GetDatos();
                if (!this.usuario.Estado)
                    Funcion.EjecutaExepcionShomies(string.Concat("Usuario [", this.usuario.UserName, "] inactivo"));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
