namespace GrupoC.IngresoStock
{
    public class AsignacionUbicacion
    {
        public ItemOrdenRecepcion Item { get; }
        public int Cantidad { get; private set; }
        public string Pasillo { get; }
        public string Fila { get; }
        public string Nivel { get; }

        public string Sku => Item.Sku;
        public string Posicion => $"{Pasillo} - {Fila} - {Nivel}";

        public AsignacionUbicacion(ItemOrdenRecepcion item, int cantidad, string pasillo, string fila, string nivel)
        {
            Item = item;
            Cantidad = cantidad;
            Pasillo = pasillo;
            Fila = fila;
            Nivel = nivel;
        }

        public bool EstaEn(ItemOrdenRecepcion item, string pasillo, string fila, string nivel)
        {
            return Item == item && Pasillo == pasillo && Fila == fila && Nivel == nivel;
        }

        public void SumarCantidad(int cantidad)
        {
            Cantidad += cantidad;
        }
    }
}