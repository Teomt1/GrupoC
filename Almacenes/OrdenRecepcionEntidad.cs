using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.Almacenes
{
    internal class OrdenRecepcionEntidad
    {
        public int NumeroOrdRecep { get; set; }

        public string CodigoCliente { get; set; }

        public string CodigoDeposito { get; set; }

        public string TransportistaEmpresa { get; set; }

        public string Patente { get; set; }

        public string TransportistaNombreYApellido { get; set; }

        public string NumeroRemito { get; set; }

        public DateTime FechaRemito { get; set; }

        public DateTime FechaHoraRecepcion { get; set; }

        public string Observaciones { get; set; }

        public EstadoOrdenRecepcionEnum Estado { get; set; }

        public List<ItemRecepcion> Items { get; set; }

    }
}
