using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.Almacenes
{
    internal class RegistroContableEntidad
    {
        public int Numero { get; set; }

        public DateTime Fecha { get; set; }

        public string Concepto { get; set; }

        public decimal Importe { get; set; }

        public string CodigoCliente { get; set; }

        public string CUIT_Proveedor { get; set; }

    }
}
