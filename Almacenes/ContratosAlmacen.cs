using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GrupoC.Almacenes
{
    static class ContratosAlmacen
    {
        public static List<ContratosEntidad> Contratos { get; private set; }

        static ContratosAlmacen()
        {
            if (File.Exists(@"Datos\Contratos.json"))
            {
                string contenido = File.ReadAllText(@"Datos\Contratos.json");
                Contratos = JsonSerializer.Deserialize<List<ContratosEntidad>>(contenido) ?? new List<ContratosEntidad>();
            }
            else // por si el archivo no existe
            {
                throw new ApplicationException($"No encuentro el archivo Contratos.json");
            }
        }

        public static void Guardar()
        {
            string contenido = JsonSerializer.Serialize(Contratos, new JsonSerializerOptions {WriteIndented = true });
            File.WriteAllText(@"Datos\Contratos.json", contenido);
        }
    }
}
