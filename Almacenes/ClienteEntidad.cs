using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.Almacenes
{
    internal class ClienteEntidad
    {
        public string Codigo { get; set; }

        public string RazonSocial { get; set; }

        public bool VigenteContratos { get; set; }

        public List<string> Productos { get; set; }

    }
}
