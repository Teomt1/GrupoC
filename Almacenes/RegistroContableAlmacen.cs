using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GrupoC.Almacenes
{
    static class RegistroContableAlmacen
    {
        public static List<RegistroContableEntidad> RegistroContable { get; private set; }

        static RegistroContableAlmacen()
        {
            if (File.Exists(@"Datos\RegistroContable.json"))
            {
                string contenido = File.ReadAllText(@"Datos\RegistroContable.json");
                RegistroContable = JsonSerializer.Deserialize<List<RegistroContableEntidad>>(contenido) ?? new List<RegistroContableEntidad>();
            }
            else // por si el archivo no existe
            {
                throw new ApplicationException($"No encuentro el archivo RegistroContable.json");
            }
        }

        public static void Guardar()
        {
            string contenido = JsonSerializer.Serialize(RegistroContable, new JsonSerializerOptions {WriteIndented = true });
            File.WriteAllText(@"Datos\RegistroContable.json", contenido);
        }
    }
}
