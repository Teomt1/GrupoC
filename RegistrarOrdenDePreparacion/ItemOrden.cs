using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.RegistrarOrdenDePreparacion
{
    public class ItemOrden
    {
        public Producto Producto { get; }
        public int Cantidad { get; private set; }

        public ItemOrden(Producto producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;
        }

        public void SumarCantidad(int cantidad)
        {
            Cantidad += cantidad;
        }
    }
}

