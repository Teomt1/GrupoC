using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.Almacenes
{
    internal class ContratosEntidad
    {
        public int Número { get; set; }

        public string CodigoCliente { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public string Ubicaciondep { get; set; }

        public decimal Tarifa { get; set; }

    }
}
