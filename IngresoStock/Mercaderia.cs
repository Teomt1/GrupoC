namespace GrupoC.IngresoStock
{
    // Existencia de un SKU en una posición física del depósito (AL.3 Mercaderías)
    public class Mercaderia
    {
        public string Sku { get; }
        public string Pasillo { get; }
        public string Fila { get; }
        public string Nivel { get; }
        public int Cantidad { get; private set; }
        public EstadoMercaderia Estado { get; }

        public string Posicion => $"{Pasillo} - {Fila} - {Nivel}";

        public Mercaderia(string sku, string pasillo, string fila, string nivel, int cantidad)
        {
            Sku = sku;
            Pasillo = pasillo;
            Fila = fila;
            Nivel = nivel;
            Cantidad = cantidad;
            Estado = EstadoMercaderia.DisponibleEnGuarda;
        }

        public bool EstaEn(string sku, string pasillo, string fila, string nivel)
        {
            return Sku == sku && Pasillo == pasillo && Fila == fila && Nivel == nivel;
        }

        public void IncrementarStock(int cantidad)
        {
            Cantidad += cantidad;
        }
    }
}