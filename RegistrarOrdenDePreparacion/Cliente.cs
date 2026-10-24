using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.RegistrarOrdenDePreparacion
{
    public class Cliente
    {
        public string Nombre { get; }
        public List<StockMinimo> StocksMinimos { get; }

        public Cliente(string nombre, List<StockMinimo> stocksMinimos)
        {
            Nombre = nombre;
            StocksMinimos = stocksMinimos;
        }

        public bool TieneStockMinimoContratado()
        {
            return StocksMinimos.Count > 0;
        }

        public StockMinimo? BuscarStockMinimo(Producto producto)
        {
            return StocksMinimos.Find(m => m.Producto == producto);
        }
    }
}
