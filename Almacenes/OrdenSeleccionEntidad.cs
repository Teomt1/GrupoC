using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.Almacenes
{
    internal class OrdenSeleccionEntidad
    {
        public int Numero { get; set; }

        public string CodigoDeposito { get; set; }

        public List<RenglonOrdenSeleccion> Renglones { get; set; }

        public EstadoOrdenSeleccionEnum Estado { get; set; }

        public DateTime? FechaHoraCumplida { get; set; }

        public int LegajoUsuarioCumplida { get; set; }

    }
}
