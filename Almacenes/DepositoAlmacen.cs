using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GrupoC.Almacenes
{
    static class DepositoAlmacen
    {
        public static List<DepositoEntidad> Deposito { get; private set; }

        static DepositoAlmacen()
        {
            if (File.Exists(@"Datos\Deposito.json"))
            {
                string contenido = File.ReadAllText(@"Datos\Deposito.json");
                Deposito = JsonSerializer.Deserialize<List<DepositoEntidad>>(contenido) ?? new List<DepositoEntidad>();
            }
            else // por si el archivo no existe
            {
                throw new ApplicationException($"No encuentro el archivo Deposito.json");
            }
        }

        public static void Guardar()
        {
            string contenido = JsonSerializer.Serialize(Deposito, new JsonSerializerOptions {WriteIndented = true });
            File.WriteAllText(@"Datos\Deposito.json", contenido);
        }
    }
}
