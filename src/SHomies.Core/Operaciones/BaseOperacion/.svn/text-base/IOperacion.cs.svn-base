using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHomies.Core.Operaciones.Validaciones;

namespace SHomies.Core.Operaciones.BaseOperacion
{
    public interface IOperacion
    {
        int NroComprobante { get; set; }
        decimal Monto { get; set; }
        Sistema.AuditoriaSistema Auditoria { get; set; }
        Persona.Persona Cliente { get; set; }
        Concepto Concepto { get; set; }

        void Procesar();
    }
}
