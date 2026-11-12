using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.Almacenes
{
    internal class RemitoEntregaEntidad
    {
        public int Numero { get; set; }

        public DateTime FechaHora { get; set; }

        public string CodigoDeposito { get; set; }

        public List<DetalleRemitoEntrega> Detalles { get; set; }

        public int LegajoUsuario { get; set; }

        public EstadoRemitoEnum Estado { get; set; }

        public DateTime? FechaHoraCumplido { get; set; }

        public int? LegajoCumplido { get; set; }

        public string TransportistaNombre { get; set; }

        public int TransportistaDni { get; set; }

        public int CantidadPaquetes { get; set; }

    }
}
