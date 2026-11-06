using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GrupoC.Almacenes
{
    static class ProductoAlmacen
    {
        public static List<ProductoEntidad> Producto { get; private set; }

        static ProductoAlmacen()
        {
            if (File.Exists(@"Datos\Producto.json"))
            {
                string contenido = File.ReadAllText(@"Datos\Producto.json");
                Producto = JsonSerializer.Deserialize<List<ProductoEntidad>>(contenido) ?? new List<ProductoEntidad>();
            }
            else // por si el archivo no existe
            {
                throw new ApplicationException($"No encuentro el archivo Producto.json");
            }
        }

        public static void Guardar()
        {
            string contenido = JsonSerializer.Serialize(Producto, new JsonSerializerOptions {WriteIndented = true });
            File.WriteAllText(@"DatosDatos\Producto.json", contenido);
        }
    }
}
