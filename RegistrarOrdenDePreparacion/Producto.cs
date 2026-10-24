using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.RegistrarOrdenDePreparacion
{
    public class Producto
    {
        public string Sku { get; }
        public string Nombre { get; }

        public Producto(string sku, string nombre)
        {
            Sku = sku;
            Nombre = nombre;
        }

        // Texto que se muestra en el combo "Productos a retirar"
        public override string ToString()
        {
            return Nombre;
        }
    }
}
