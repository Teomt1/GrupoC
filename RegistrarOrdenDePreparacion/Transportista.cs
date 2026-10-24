using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.RegistrarOrdenDePreparacion
{
    public class Transportista
    {
        public string Nombre { get; }
        public string Dni { get; }
        public string Empresa { get; }

        public Transportista(string nombre, string dni, string empresa)
        {
            Nombre = nombre;
            Dni = dni;
            Empresa = empresa;
        }
    }
}
