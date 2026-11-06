using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GrupoC.Almacenes
{
    static class ProveedorAlmacen
    {
        public static List<ProveedorEntidad> Proveedor { get; private set; }

        static ProveedorAlmacen()
        {
            if (File.Exists(@"Datos\Proveedor.json"))
            {
                string contenido = File.ReadAllText(@"Datos\Proveedor.json");
                Proveedor = JsonSerializer.Deserialize<List<ProveedorEntidad>>(contenido) ?? new List<ProveedorEntidad>();
            }
            else // por si el archivo no existe
            {
                throw new ApplicationException($"No encuentro el archivo Proveedor.json");
            }
        }

        public static void Guardar()
        {
            string contenido = JsonSerializer.Serialize(Proveedor, new JsonSerializerOptions {WriteIndented = true });
            File.WriteAllText(@"Datos\Proveedor.json", contenido);
        }
    }
}
