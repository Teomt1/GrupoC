using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GrupoC.Almacenes
{
    static class OrdenTrasladoAlmacen
    {
        public static List<OrdenTrasladoEntidad> OrdenTraslado { get; private set; }

        static OrdenTrasladoAlmacen()
        {
            if (File.Exists(@"Datos\OrdenTraslado.json"))
            {
                string contenido = File.ReadAllText(@"Datos\OrdenTraslado.json");
                OrdenTraslado = JsonSerializer.Deserialize<List<OrdenTrasladoEntidad>>(contenido) ?? new List<OrdenTrasladoEntidad>();
            }
            else // por si el archivo no existe
            {
                throw new ApplicationException($"No encuentro el archivo OrdenTraslado.json");
            }
        }

        public static void Guardar()
        {
            string contenido = JsonSerializer.Serialize(OrdenTraslado, new JsonSerializerOptions {WriteIndented = true });
            File.WriteAllText(@"Datos\OrdenTraslado.json", contenido);
        }
    }
}
