using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.Almacenes
{
    internal class DepositoEntidad
    {
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public List<Posicion> Posiciones { get; set; }

    }
}
