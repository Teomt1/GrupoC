#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.RegistrarOrdenDePreparacion;

internal class RegistrarOrdenDePreparacionModelo
{
    public string NombreCliente { get; }
    public List<Deposito> Depositos { get; }
    public Deposito? DepositoSeleccionado { get; private set; }
    public ModalidadEmpaquetado? Modalidad { get; set; }
    public List<ItemOrden> ItemsOrden { get; } = new();

    public string NombreDestinatario { get; set; } = "";
    public string DireccionDestino { get; set; } = "";
    public string NombreTransportista { get; set; } = "";
    public int? DniTransportista { get; set; }
    public string EmpresaTransportista { get; set; } = "";

    public RegistrarOrdenDePreparacionModelo()
    {
        NombreCliente = "Distribuidora Del Plata S.A.";

        var yerba = new Producto { Sku = "YER-500-001", Nombre = "Yerba mate 500 g", StockMinimoContratado = 100 };
        var cafe = new Producto { Sku = "CAF-250-002", Nombre = "Café molido 250 g" };
        var azucar = new Producto { Sku = "AZU-1000-003", Nombre = "Azúcar 1 kg", StockMinimoContratado = 50 };
        var aceite = new Producto { Sku = "ACE-900-004", Nombre = "Aceite de girasol 900 ml" };
        var harina = new Producto { Sku = "HAR-1000-005", Nombre = "Harina 000 1 kg", StockMinimoContratado = 30 };

        Depositos = new List<Deposito>
        {
            new Deposito
            {
                Nombre = "Depósito Avellaneda",
                Stock = new List<StockProducto>
                {
                    new StockProducto { Producto = yerba, Cantidad = 150 },
                    new StockProducto { Producto = cafe, Cantidad = 80 },
                    new StockProducto { Producto = azucar, Cantidad = 60 },
                    new StockProducto { Producto = aceite, Cantidad = 200 }
                }
            },
            new Deposito
            {
                Nombre = "Depósito Pilar",
                Stock = new List<StockProducto>
                {
                    new StockProducto { Producto = yerba, Cantidad = 40 },
                    new StockProducto { Producto = harina, Cantidad = 120 },
                    new StockProducto { Producto = aceite, Cantidad = 25 }
                }
            },
            new Deposito
            {
                Nombre = "Depósito Córdoba",
                Stock = new List<StockProducto>
                {
                    new StockProducto { Producto = cafe, Cantidad = 300 },
                    new StockProducto { Producto = azucar, Cantidad = 500 },
                    new StockProducto { Producto = harina, Cantidad = 35 }
                }
            }
        };
    }

    public bool RequiereDatosEtiquetado => Modalidad == ModalidadEmpaquetado.Fullfilment;

    public List<Producto> ProductosDisponibles =>
        DepositoSeleccionado?.Stock.Select(s => s.Producto).ToList() ?? new List<Producto>();

    // Devuelve el mensaje a mostrar, o null si no hay nada que avisar.
    public string? SeleccionarDeposito(Deposito? deposito)
    {
        if (deposito == DepositoSeleccionado)
        {
            return null;
        }

        bool habiaDepositoSeleccionado = DepositoSeleccionado != null;
        DepositoSeleccionado = deposito;
        ItemsOrden.Clear();

        if (!habiaDepositoSeleccionado)
        {
            return null;
        }

        return "Se cambió el depósito, la orden actual ha sido reiniciada";
    }

    // Devuelve el error a mostrar, o null si el producto se agregó a la orden.
    public string? AgregarProducto(Producto? producto, int? cantidad)
    {
        if (DepositoSeleccionado == null || producto == null || cantidad == null || cantidad <= 0)
        {
            return "Error: seleccione un producto e ingrese una cantidad mayor a cero";
        }

        StockProducto? stock = DepositoSeleccionado.Stock.Find(s => s.Producto == producto);
        ItemOrden? item = ItemsOrden.Find(i => i.Producto == producto);
        int cantidadYaEnOrden = item?.Cantidad ?? 0;

        if (stock == null || stock.Cantidad < cantidadYaEnOrden + cantidad.Value)
        {
            return "Error: Stock insuficiente";
        }

        if (item == null)
        {
            ItemsOrden.Add(new ItemOrden { Producto = producto, Cantidad = cantidad.Value });
        }
        else
        {
            item.Cantidad += cantidad.Value;
        }

        return null;
    }

    // Devuelve el error a mostrar, o null si la orden se puede confirmar.
    public string? ValidarOrden()
    {
        bool faltanDatosEtiquetado = RequiereDatosEtiquetado
            && (string.IsNullOrWhiteSpace(NombreDestinatario) || string.IsNullOrWhiteSpace(DireccionDestino));

        if (DepositoSeleccionado == null || Modalidad == null || faltanDatosEtiquetado
            || string.IsNullOrWhiteSpace(NombreTransportista)
            || DniTransportista == null
            || string.IsNullOrWhiteSpace(EmpresaTransportista))
        {
            return "Error: por favor rellene todos los campos antes de completar la orden";
        }

        if (ItemsOrden.Count == 0)
        {
            return "Error: la orden de preparación debe contener al menos 1 producto";
        }

        return null;
    }

    // Guarda la orden (descuenta el stock del depósito) y deja el modelo listo para una orden nueva.
    // Devuelve los mensajes a mostrar, en orden: las advertencias de stock mínimo y la confirmación.
    public List<string> RegistrarOrden()
    {
        var mensajes = new List<string>();

        foreach (ItemOrden item in ItemsOrden)
        {
            StockProducto stock = DepositoSeleccionado!.Stock.Find(s => s.Producto == item.Producto)!;
            stock.Cantidad -= item.Cantidad;

            if (item.Producto.StockMinimoContratado != null && stock.Cantidad < item.Producto.StockMinimoContratado)
            {
                mensajes.Add($"Advertencia: El producto {item.Producto.Nombre} ha quedado por debajo del límite establecido, por favor generar un nuevo ingreso de este");
            }
        }

        mensajes.Add("Orden de preparación guardada con éxito");

        DepositoSeleccionado = null;
        Modalidad = null;
        ItemsOrden.Clear();
        NombreDestinatario = "";
        DireccionDestino = "";
        NombreTransportista = "";
        DniTransportista = null;
        EmpresaTransportista = "";

        return mensajes;
    }
}
