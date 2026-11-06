using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GrupoC.Almacenes
{
    static class RemitoEntregaAlmacen
    {
        public static List<RemitoEntregaEntidad> RemitoEntrega { get; private set; }

        static RemitoEntregaAlmacen()
        {
            if (File.Exists(@"Datos\RemitoEntrega.json"))
            {
                string contenido = File.ReadAllText(@"Datos\RemitoEntrega.json");
                RemitoEntrega = JsonSerializer.Deserialize<List<RemitoEntregaEntidad>>(contenido) ?? new List<RemitoEntregaEntidad>();
            }
            else // por si el archivo no existe
            {
                throw new ApplicationException($"No encuentro el archivo RemitoEntrega.json");
            }
        }

        public static void Guardar()
        {
            string contenido = JsonSerializer.Serialize(RemitoEntrega, new JsonSerializerOptions {WriteIndented = true });
            File.WriteAllText(@"Datos\RemitoEntrega.json", contenido);
        }
    }
}
