using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GrupoC.Almacenes
{
    static class MovimientoMercaderiaAlmacen
    {
        public static List<MovimientoMercaderiaEntidad> MovimientoMercaderia { get; private set; }

        static MovimientoMercaderiaAlmacen()
        {
            if (File.Exists(@"Datos\MovimientoMercaderia.json"))
            {
                string contenido = File.ReadAllText(@"Datos\MovimientoMercaderia.json");
                MovimientoMercaderia = JsonSerializer.Deserialize<List<MovimientoMercaderiaEntidad>>(contenido) ?? new List<MovimientoMercaderiaEntidad>();
            }
            else // por si el archivo no existe
            {
                throw new ApplicationException($"No encuentro el archivo MovimientoMercaderia.json");
            }
        }

        public static void Guardar()
        {
            string contenido = JsonSerializer.Serialize(MovimientoMercaderia, new JsonSerializerOptions {WriteIndented = true });
            File.WriteAllText(@"Datos\MovimientoMercaderia.json", contenido);
        }
    }
}
