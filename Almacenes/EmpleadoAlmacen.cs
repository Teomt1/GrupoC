using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GrupoC.Almacenes
{
    static class EmpleadoAlmacen
    {
        public static List<EmpleadoEntidad> Empleado { get; private set; }

        public static string EmpleadoActual {  get; set; }

        static EmpleadoAlmacen()
        {
            if (File.Exists(@"Datos\Empleado.json"))
            {
                string contenido = File.ReadAllText(@"Datos\Empleado.json");
                Empleado = JsonSerializer.Deserialize<List<EmpleadoEntidad>>(contenido) ?? new List<EmpleadoEntidad>();
            }
            else // por si el archivo no existe
            {
                throw new ApplicationException($"No encuentro el archivo Empleado.json");
            }
        }

        public static void Guardar()
        {
            string contenido = JsonSerializer.Serialize(Empleado, new JsonSerializerOptions {WriteIndented = true });
            File.WriteAllText(@"Datos\Empleado.json", contenido);
        }
    }
}
