using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GrupoC.Almacenes
{
    static class OrdenSeleccionAlmacen
    {
        public static List<OrdenSeleccionEntidad> OrdenSeleccion { get; private set; }

        static OrdenSeleccionAlmacen()
        {
            if (File.Exists(@"Datos\OrdenSeleccion.json"))
            {
                string contenido = File.ReadAllText(@"Datos\OrdenSeleccion.json");
                OrdenSeleccion = JsonSerializer.Deserialize<List<OrdenSeleccionEntidad>>(contenido) ?? new List<OrdenSeleccionEntidad>();
            }
            else // por si el archivo no existe
            {
                throw new ApplicationException($"No encuentro el archivo OrdenSeleccion.json");
            }
        }

        public static void Guardar()
        {
            string contenido = JsonSerializer.Serialize(OrdenSeleccion, new JsonSerializerOptions {WriteIndented = true });
            File.WriteAllText(@"Datos\OrdenSeleccion.json", contenido);
        }
    }
}
