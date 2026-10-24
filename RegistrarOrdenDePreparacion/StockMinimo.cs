using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.RegistrarOrdenDePreparacion
{
    public class StockMinimo
    {
        public Producto Producto { get; }
        public int CantidadMinima { get; }

        public StockMinimo(Producto producto, int cantidadMinima)
        {
            Producto = producto;
            CantidadMinima = cantidadMinima;
        }
    }
}
