namespace GrupoC.IngresoStock
{
    public class ItemOrdenRecepcion
    {
        public string Sku { get; }
        public string Descripcion { get; }

        // true: se recibió en pallets cerrados y las cantidades están en pallets; false: suelto, en unidades.
        public bool EsPallet { get; }
        public int UnidadesPorPallet { get; }

        public int CantidadRecepcionada { get; }

        // Saldo pendiente de ubicar (columna "Cantidad a ubicar" de la grilla)
        public int CantidadAUbicar { get; set; }

        // Texto de la columna "Tipo"
        public string Tipo
        {
            get { return EsPallet ? $"Pallets x {UnidadesPorPallet} u." : "Unidades sueltas"; }
        }

        public ItemOrdenRecepcion(string sku, string descripcion, bool esPallet, int unidadesPorPallet, int cantidadRecepcionada)
        {
            Sku = sku;
            Descripcion = descripcion;
            EsPallet = esPallet;
            UnidadesPorPallet = unidadesPorPallet;
            CantidadRecepcionada = cantidadRecepcionada;
            CantidadAUbicar = cantidadRecepcionada;
        }

        // Texto que se ve en el desplegable de SKU: el mismo SKU puede estar en pallets y suelto.
        public override string ToString()
        {
            return $"{Sku} - {Descripcion} ({(EsPallet ? "pallets" : "sueltas")})";
        }
    }
}
