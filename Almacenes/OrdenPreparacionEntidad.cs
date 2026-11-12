using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.Almacenes
{
    internal class OrdenPreparacionEntidad
    {
        public int Numero { get; set; }

        public string CodigoCliente { get; set; }

        public string CodigoDeposito { get; set; }

        public List<RenglonOrdenPreparacion> Renglones { get; set; }

        public DateTime FechaRetiro { get; set; }

        public TipoPedidoEnum TipoPedido { get; set; }

        public string? NombreDestinatario { get; set; }

        public string? DireccionDestino { get; set; }

        public string TransportistaNombre { get; set; }

        public int TransportistaDni { get; set; }

        public string TransportistaEmpresa { get; set; }

        public EstadoOrdenPreparacionEnum Estado { get; set; }

        public int? NumeroOrdenSeleccion { get; set; }

        public EstadoOrdenSeleccionEnum EstadoOrdenSeleccion { get; set; }

        public int LegajoEmpleadoTomador { get; set; }

        public DateTime? FechaPreparada { get; set; }

        public int LegajoEmpleadoPreparación { get; set; }

    }
}
