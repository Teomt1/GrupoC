using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.RegistrarOrdenDePreparacion
{
    public class Deposito
    {
        public string Nombre { get; }
        public List<StockProducto> Stocks { get; }

        public Deposito(string nombre, List<StockProducto> stocks)
        {
            Nombre = nombre;
            Stocks = stocks;
        }

        public int ObtenerStock(Producto producto)
        {
            StockProducto? stock = Stocks.Find(s => s.Producto == producto);
            return stock == null ? 0 : stock.Cantidad;
        }

        public void DescontarStock(Producto producto, int cantidad)
        {
            StockProducto? stock = Stocks.Find(s => s.Producto == producto);
            stock?.Descontar(cantidad);
        }

        public List<Producto> ObtenerProductosConStock()
        {
            return Stocks
                .Where(s => s.Cantidad > 0)
                .Select(s => s.Producto)
                .ToList();
        }

        // Texto que se muestra en el combo "Depósito"
        public override string ToString()
        {
            return Nombre;
        }
    }
}
