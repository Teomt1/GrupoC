using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.Almacenes
{
    internal class ProductoEntidad
    {
        public string Sku { get; set; }

        public string Descripcion { get; set; }

        public string? CodigoPallet { get; set; }

        public int UnidadesPorPallet { get; set; }

        public int? StockMinimoContratado { get; set; }

    }
}
