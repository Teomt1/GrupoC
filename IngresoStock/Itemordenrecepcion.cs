namespace GrupoC.IngresoStock
{
    public class ItemOrdenRecepcion
    {
        public string Sku { get; }
        public string Descripcion { get; }
        public int CantidadRecepcionada { get; }

        public ItemOrdenRecepcion(string sku, string descripcion, int cantidadRecepcionada)
        {
            Sku = sku;
            Descripcion = descripcion;
            CantidadRecepcionada = cantidadRecepcionada;
        }

        // Texto que se muestra en cmbSkuSeleccionado
        public override string ToString()
        {
            return $"{Sku} - {Descripcion}";
        }
    }
}