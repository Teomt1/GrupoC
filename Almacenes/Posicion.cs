using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.Almacenes
{
    internal class Posicion
    {
        public string Pasillo { get; set; }

        public string Fila { get; set; }

        public string Nivel { get; set; }

        public string? Sku { get; set; }

        public int CantidadPalets { get; set; }

        public int CantidadSueltos { get; set; }

    }
}
