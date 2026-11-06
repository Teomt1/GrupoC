using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.Almacenes
{
    internal class OrdenEntregaEntidad
    {
        public int Numero { get; set; }

        public int NumeroOrdenPreparacion { get; set; }

        public TipoPedidoEnum TipoPedidoPreparacion { get; set; }

        public string TransportistaNombrePreparacion { get; set; }

        public int TransportistaDniPreparacion { get; set; }

        public DateTime FechaHora { get; set; }

        public int LegajoEmpleado { get; set; }

        public int CantidadPaquetes { get; set; }

        public EstadoOrdenEntregaEnum Estado { get; set; }

        public DateTime? FechaHoraDespacho { get; set; }

    }
}
