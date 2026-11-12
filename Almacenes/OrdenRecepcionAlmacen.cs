using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GrupoC.Almacenes
{
    static class OrdenRecepcionAlmacen
    {
        public static List<OrdenRecepcionEntidad> OrdenRecepcion { get; private set; }

        static OrdenRecepcionAlmacen()
        {
            if (File.Exists(@"Datos\OrdenRecepcion.json"))
            {
                string contenido = File.ReadAllText(@"Datos\OrdenRecepcion.json");
                OrdenRecepcion = JsonSerializer.Deserialize<List<OrdenRecepcionEntidad>>(contenido) ?? new List<OrdenRecepcionEntidad>();
            }
            else // por si el archivo no existe
            {
                throw new ApplicationException($"No encuentro el archivo OrdenRecepcion.json");
            }
        }

        public static void Guardar()
        {
            string contenido = JsonSerializer.Serialize(OrdenRecepcion, new JsonSerializerOptions {WriteIndented = true });
            File.WriteAllText(@"Datos\OrdenRecepcion.json", contenido);
        }
    }
}
