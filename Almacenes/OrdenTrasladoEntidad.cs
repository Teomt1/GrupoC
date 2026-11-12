using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.Almacenes
{
    internal class OrdenTrasladoEntidad
    {
        public int Numero { get; set; }

        public string SKU_Producto { get; set; }

        public int Cantidad { get; set; }

        public string? CodigoDepositoOrigen { get; set; }

        public string CodigoDepositoDestino { get; set; }

        public EstadoOrdenTrasladoEnum Estado { get; set; }

    }
}
