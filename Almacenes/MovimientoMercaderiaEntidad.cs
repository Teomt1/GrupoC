using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.Almacenes
{
    internal class MovimientoMercaderiaEntidad
    {
        public TipoMovimientoEnum Tipo { get; set; }

        public DateTime FechaHora { get; set; }

        public string CodigoCliente { get; set; }

        public int? NumeroOrdenRecepcion { get; set; }

        public int? NumeroOrdenPreparacion { get; set; }

        public int? NumeroRemitoEntrega { get; set; }

        public int? LegajoUsuario { get; set; }

    }
}
