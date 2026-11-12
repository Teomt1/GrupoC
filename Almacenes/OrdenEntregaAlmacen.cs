using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GrupoC.Almacenes
{
    static class OrdenEntregaAlmacen
    {
        public static List<OrdenEntregaEntidad> OrdenEntrega { get; private set; }

        static OrdenEntregaAlmacen()
        {
            if (File.Exists(@"Datos\OrdenEntrega.json"))
            {
                string contenido = File.ReadAllText(@"Datos\OrdenEntrega.json");
                OrdenEntrega = JsonSerializer.Deserialize<List<OrdenEntregaEntidad>>(contenido) ?? new List<OrdenEntregaEntidad>();
            }
            else // por si el archivo no existe
            {
                throw new ApplicationException($"No encuentro el archivo OrdenEntrega.json");
            }
        }

        public static void Guardar()
        {
            string contenido = JsonSerializer.Serialize(OrdenEntrega, new JsonSerializerOptions {WriteIndented = true });
            File.WriteAllText(@"Datos\OrdenEntrega.json", contenido);
        }
    }
}
