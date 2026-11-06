using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GrupoC.Almacenes
{
    static class OrdenPreparacionAlmacen
    {
        public static List<OrdenPreparacionEntidad> OrdenPreparacion { get; private set; }

        static OrdenPreparacionAlmacen()
        {
            if (File.Exists(@"Datos\OrdenPreparacion.json"))
            {
                string contenido = File.ReadAllText(@"Datos\OrdenPreparacion.json");
                OrdenPreparacion = JsonSerializer.Deserialize<List<OrdenPreparacionEntidad>>(contenido) ?? new List<OrdenPreparacionEntidad>();
            }
            else // por si el archivo no existe
            {
                throw new ApplicationException($"No encuentro el archivo OrdenPreparacion.json");
            }
        }

        public static void Guardar()
        {
            string contenido = JsonSerializer.Serialize(OrdenPreparacion, new JsonSerializerOptions {WriteIndented = true });
            File.WriteAllText(@"Datos\OrdenPreparacion.json", contenido);
        }
    }
}
