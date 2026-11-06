using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.Almacenes
{
    internal class ItemRecepcion
    {
        public string SKU_Producto { get; set; }

        public bool EsPallet { get; set; }

        public int CantidadRemito { get; set; }

        public int CantidadContada { get; set; }

        public string PosicionPasillo { get; set; }

        public string PosicionFila { get; set; }

        public string PosicionNivel { get; set; }

    }
}
