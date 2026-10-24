using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.RegistrarOrdenDePreparacion
{
    public class StockProducto
    {
        public Producto Producto { get; }
        public int Cantidad { get; private set; }

        public StockProducto(Producto producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;
        }

        public void Descontar(int cantidad)
        {
            Cantidad -= cantidad;
        }
    }
}
