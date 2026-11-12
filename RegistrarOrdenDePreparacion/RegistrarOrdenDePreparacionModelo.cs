#nullable enable
using System;
using System.Collections.Generic;
using System.Text;
using Alm = GrupoC.Almacenes;

namespace GrupoC.RegistrarOrdenDePreparacion;

internal class RegistrarOrdenDePreparacionModelo
{
    // Código del cliente logueado (paso 1 del caso de uso): se toma uno de los clientes con contrato vigente.
    private readonly string codigoCliente = "";

    // Relación entre las clases de la pantalla y los datos de los almacenes (no salen del modelo).
    private readonly Dictionary<Deposito, string> codigoDeposito = new();

    public string NombreCliente { get; }
    public List<Deposito> Depositos { get; }
    public Deposito? DepositoSeleccionado { get; private set; }
    public ModalidadEmpaquetado? Modalidad { get; private set; }
    public List<ItemOrden> ItemsOrden { get; } = new();

    public DateTime FechaRetiro { get; set; } = DateTime.Today;
    public string NombreDestinatario { get; set; } = "";
    public string DireccionDestino { get; set; } = "";
    public string NombreTransportista { get; set; } = "";
    public int? DniTransportista { get; set; }
    public string EmpresaTransportista { get; set; } = "";

    public RegistrarOrdenDePreparacionModelo()
    {
        Depositos = new List<Deposito>();
        NombreCliente = "";

        // Cliente logueado: uno de los clientes con contrato vigente y productos dados de alta.
        List<Alm.ClienteEntidad> clientes = Alm.ClienteAlmacen.Cliente
            .Where(c => c.VigenteContratos && c.Productos.Count > 0)
            .ToList();
        if (clientes.Count == 0)
        {
            return;
        }

        Alm.ClienteEntidad cliente = clientes[Random.Shared.Next(clientes.Count)];
        codigoCliente = cliente.Codigo;
        NombreCliente = cliente.RazonSocial;

        // Productos del cliente: se transforman una sola vez para que cada SKU sea un único objeto Producto.
        var productos = new Dictionary<string, Producto>();
        foreach (Alm.ProductoEntidad p in Alm.ProductoAlmacen.Producto)
        {
            if (cliente.Productos.Contains(p.Sku))
            {
                productos[p.Sku] = new Producto
                {
                    Sku = p.Sku,
                    Nombre = p.Descripcion,
                    StockMinimoContratado = p.StockMinimoContratado,
                    UnidadesPorPalet = p.UnidadesPorPallet
                };
            }
        }

        // Órdenes de preparación pendientes del cliente: su mercadería sigue en las posiciones
        // pero ya está comprometida, así que se descuenta del stock disponible.
        List<Alm.OrdenPreparacionEntidad> ordenesPendientes = Alm.OrdenPreparacionAlmacen.OrdenPreparacion
            .Where(o => o.CodigoCliente == codigoCliente && o.Estado == Alm.EstadoOrdenPreparacionEnum.Pendiente)
            .ToList();

        // Depósitos donde el cliente ingresó mercadería (precondición del caso de uso).
        // El stock de cada producto es la suma de las posiciones del depósito que lo guardan.
        foreach (Alm.DepositoEntidad d in Alm.DepositoAlmacen.Deposito)
        {
            var stock = new List<StockProducto>();
            foreach (Alm.Posicion p in d.Posiciones)
            {
                if (p.Sku == null || !productos.ContainsKey(p.Sku))
                {
                    continue;
                }

                StockProducto? stockProducto = stock.Find(s => s.Producto.Sku == p.Sku);
                if (stockProducto == null)
                {
                    stockProducto = new StockProducto { Producto = productos[p.Sku] };
                    stock.Add(stockProducto);
                }
                stockProducto.CantidadPalets += p.CantidadPalets;
                stockProducto.UnidadesSueltas += p.CantidadSueltos;
            }

            if (stock.Count == 0)
            {
                continue;
            }

            foreach (Alm.OrdenPreparacionEntidad orden in ordenesPendientes.Where(o => o.CodigoDeposito == d.Codigo))
            {
                ModalidadEmpaquetado modalidad = orden.TipoPedido == Alm.TipoPedidoEnum.PalletCerrado
                    ? ModalidadEmpaquetado.PaletCerrado
                    : ModalidadEmpaquetado.Fullfilment;

                foreach (Alm.RenglonOrdenPreparacion renglon in orden.Renglones)
                {
                    StockProducto? stockProducto = stock.Find(s => s.Producto.Sku == renglon.SKU_Producto);
                    if (stockProducto == null)
                    {
                        continue;
                    }

                    // En el almacén la cantidad del renglón está siempre en unidades.
                    int cantidad = renglon.Cantidad;
                    if (modalidad == ModalidadEmpaquetado.PaletCerrado && stockProducto.Producto.UnidadesPorPalet > 0)
                    {
                        cantidad = renglon.Cantidad / stockProducto.Producto.UnidadesPorPalet;
                    }
                    DescontarStock(stockProducto, modalidad, cantidad);
                }
            }

            var deposito = new Deposito { Nombre = d.Nombre, Stock = stock };
            Depositos.Add(deposito);
            codigoDeposito[deposito] = d.Codigo;
        }
    }

    public bool RequiereDatosEtiquetado => Modalidad == ModalidadEmpaquetado.Fullfilment;

    public DateTime FechaMinimaRetiro => DateTime.Today;

    public string UnidadDeCantidad => Modalidad == ModalidadEmpaquetado.PaletCerrado ? "palets" : "unidades";

    public List<Producto> ProductosDisponibles =>
        DepositoSeleccionado?.Stock.Select(s => s.Producto).ToList() ?? new List<Producto>();

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

    public string? SeleccionarModalidad(ModalidadEmpaquetado? modalidad)
    {
        if (modalidad == Modalidad)
        {
            return null;
        }

        Modalidad = modalidad;

        if (ItemsOrden.Count == 0)
        {
            return null;
        }

        ItemsOrden.Clear();
        return "Se cambió la modalidad de empaquetado, la orden actual ha sido reiniciada";
    }

    public bool EsCaracterValidoParaNombre(char caracter)
    {
        return char.IsLetter(caracter) || caracter == ' ';
    }

    public string QuitarCaracteresInvalidosDeNombre(string nombre)
    {
        return new string(nombre.Where(EsCaracterValidoParaNombre).ToArray());
    }

    public string? AgregarProducto(Producto? producto, int? cantidad)
    {
        if (Modalidad == null)
        {
            return "Error: seleccione una modalidad de empaquetado antes de agregar productos";
        }

        if (DepositoSeleccionado == null || producto == null || cantidad == null || cantidad <= 0)
        {
            return "Error: seleccione un producto e ingrese una cantidad mayor a cero";
        }

        StockProducto? stock = DepositoSeleccionado.Stock.Find(s => s.Producto == producto);
        ItemOrden? item = ItemsOrden.Find(i => i.Producto == producto);
        int cantidadYaEnOrden = item?.Cantidad ?? 0;
        int cantidadDisponible = Modalidad == ModalidadEmpaquetado.PaletCerrado
            ? stock?.CantidadPalets ?? 0
            : stock?.CantidadUnidades ?? 0;

        if (cantidadDisponible < cantidadYaEnOrden + cantidad.Value)
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

        // El DNI debe tener 8 dígitos.
        if (DniTransportista < 10000000 || DniTransportista > 99999999)
        {
            return "El DNI del transportista debe tener un formato de documento válido";
        }

        if (ItemsOrden.Count == 0)
        {
            return "Error: la orden de preparación debe contener al menos 1 producto";
        }

        return null;
    }

    // Pasos 15 y 16: registra la orden de preparación en estado Pendiente y deja el modelo listo para otra.
    // Devuelve los mensajes a mostrar, en orden.
    public List<string> RegistrarOrden()
    {
        var mensajes = new List<string>();

        foreach (ItemOrden item in ItemsOrden)
        {
            StockProducto stock = DepositoSeleccionado!.Stock.Find(s => s.Producto == item.Producto)!;
            DescontarStock(stock, Modalidad!.Value, item.Cantidad);

            // Paso 15.2.1.
            if (item.Producto.StockMinimoContratado != null && stock.CantidadUnidades < item.Producto.StockMinimoContratado)
            {
                mensajes.Add($"Advertencia: El producto {item.Producto.Nombre} ha quedado por debajo del límite establecido, por favor generar un nuevo ingreso de este");
            }
        }

        // Se registra la orden de preparación en el almacén. La mercadería queda comprometida por la orden
        // (las posiciones del depósito no cambian hasta que se prepare).
        List<Alm.OrdenPreparacionEntidad> ordenes = Alm.OrdenPreparacionAlmacen.OrdenPreparacion;
        bool esPaletCerrado = Modalidad == ModalidadEmpaquetado.PaletCerrado;
        string deposito = codigoDeposito[DepositoSeleccionado!];

        var ordenPreparacion = new Alm.OrdenPreparacionEntidad
        {
            Numero = ordenes.Count == 0 ? 1 : ordenes.Max(o => o.Numero) + 1,
            CodigoCliente = codigoCliente,
            CodigoDeposito = deposito,
            // En el almacén la cantidad del renglón va siempre en unidades.
            Renglones = ItemsOrden.Select(i => new Alm.RenglonOrdenPreparacion
            {
                SKU_Producto = i.Producto.Sku,
                Cantidad = esPaletCerrado ? i.Cantidad * i.Producto.UnidadesPorPalet : i.Cantidad
            }).ToList(),
            FechaRetiro = FechaRetiro.Date,
            TipoPedido = esPaletCerrado ? Alm.TipoPedidoEnum.PalletCerrado : Alm.TipoPedidoEnum.Fulfillment,
            NombreDestinatario = RequiereDatosEtiquetado ? NombreDestinatario.Trim() : null,
            DireccionDestino = RequiereDatosEtiquetado ? DireccionDestino.Trim() : null,
            TransportistaNombre = NombreTransportista.Trim(),
            TransportistaDni = DniTransportista!.Value,
            TransportistaEmpresa = EmpresaTransportista.Trim(),
            Estado = Alm.EstadoOrdenPreparacionEnum.Pendiente,
            NumeroOrdenSeleccion = null,
            EstadoOrdenSeleccion = Alm.EstadoOrdenSeleccionEnum.Pendiente,
            LegajoEmpleadoTomador = 0,
            FechaPreparada = null,
            LegajoEmpleadoPreparación = 0
        };
        ordenes.Add(ordenPreparacion);

        // Se genera la orden de selección de esta orden de preparación, con las posiciones
        // de las que hay que sacar la mercadería.
        GenerarOrdenSeleccion(ordenPreparacion);

        mensajes.Add("Orden de preparación guardada con éxito");

        DepositoSeleccionado = null;
        Modalidad = null;
        ItemsOrden.Clear();
        FechaRetiro = DateTime.Today;
        NombreDestinatario = "";
        DireccionDestino = "";
        NombreTransportista = "";
        DniTransportista = null;
        EmpresaTransportista = "";

        return mensajes;
    }

    // Registra la orden de selección "Pendiente" de la orden de preparación y las asocia.
    // Cada SKU se toma de las posiciones del depósito que lo guardan, de la primera a la última
    // (pasillo, fila y nivel), descontando lo que ya piden otras órdenes de selección pendientes.
    private void GenerarOrdenSeleccion(Alm.OrdenPreparacionEntidad ordenPreparacion)
    {
        List<Alm.OrdenSeleccionEntidad> ordenesSeleccion = Alm.OrdenSeleccionAlmacen.OrdenSeleccion;
        Alm.DepositoEntidad? deposito = Alm.DepositoAlmacen.Deposito.Find(d => d.Codigo == ordenPreparacion.CodigoDeposito);

        List<Alm.Posicion> posiciones = deposito == null
            ? new List<Alm.Posicion>()
            : deposito.Posiciones
                .OrderBy(p => p.Pasillo)
                .ThenBy(p => int.TryParse(p.Fila, out int f) ? f : 0)
                .ThenBy(p => int.TryParse(p.Nivel, out int n) ? n : 0)
                .ToList();

        // Lo que ya está pedido en cada posición por órdenes de selección pendientes del depósito.
        List<Alm.RenglonOrdenSeleccion> comprometidos = ordenesSeleccion
            .Where(o => o.CodigoDeposito == ordenPreparacion.CodigoDeposito && o.Estado == Alm.EstadoOrdenSeleccionEnum.Pendiente)
            .SelectMany(o => o.Renglones)
            .ToList();

        var renglones = new List<Alm.RenglonOrdenSeleccion>();
        foreach (Alm.RenglonOrdenPreparacion renglon in ordenPreparacion.Renglones)
        {
            Alm.ProductoEntidad? producto = Alm.ProductoAlmacen.Producto.Find(p => p.Sku == renglon.SKU_Producto);
            int unidadesPorPallet = producto?.UnidadesPorPallet ?? 0;
            int faltante = renglon.Cantidad;

            foreach (Alm.Posicion p in posiciones.Where(p => p.Sku == renglon.SKU_Producto))
            {
                if (faltante <= 0)
                {
                    break;
                }

                int pedido = comprometidos
                    .Concat(renglones)
                    .Where(r => r.SKU_Producto == p.Sku && r.PosicionPasillo == p.Pasillo
                                && r.PosicionFila == p.Fila && r.PosicionNivel == p.Nivel)
                    .Sum(r => r.Cantidad);
                int disponible = p.CantidadPalets * unidadesPorPallet + p.CantidadSueltos - pedido;
                if (disponible <= 0)
                {
                    continue;
                }

                int cantidad = Math.Min(disponible, faltante);
                renglones.Add(new Alm.RenglonOrdenSeleccion
                {
                    SKU_Producto = renglon.SKU_Producto,
                    PosicionPasillo = p.Pasillo,
                    PosicionFila = p.Fila,
                    PosicionNivel = p.Nivel,
                    Cantidad = cantidad
                });
                faltante -= cantidad;
            }
        }

        int numero = ordenesSeleccion.Count == 0 ? 1 : ordenesSeleccion.Max(o => o.Numero) + 1;
        ordenesSeleccion.Add(new Alm.OrdenSeleccionEntidad
        {
            Numero = numero,
            CodigoDeposito = ordenPreparacion.CodigoDeposito,
            Renglones = renglones,
            Estado = Alm.EstadoOrdenSeleccionEnum.Pendiente,
            FechaHoraCumplida = null,
            LegajoUsuarioCumplida = 0
        });

        ordenPreparacion.NumeroOrdenSeleccion = numero;
        ordenPreparacion.EstadoOrdenSeleccion = Alm.EstadoOrdenSeleccionEnum.Pendiente;
    }

    // Descuenta del stock la cantidad pedida: en palets para Palet Cerrado, en unidades para Fullfilment
    // (primero las unidades sueltas y, si no alcanzan, se abren los palets necesarios).
    private void DescontarStock(StockProducto stock, ModalidadEmpaquetado modalidad, int cantidad)
    {
        int unidadesPorPalet = stock.Producto.UnidadesPorPalet;

        if (modalidad == ModalidadEmpaquetado.PaletCerrado)
        {
            stock.CantidadPalets -= cantidad;
        }
        else if (stock.UnidadesSueltas >= cantidad)
        {
            stock.UnidadesSueltas -= cantidad;
        }
        else if (unidadesPorPalet > 0)
        {
            int unidadesFaltantes = cantidad - stock.UnidadesSueltas;
            int paletsAAbrir = (unidadesFaltantes + unidadesPorPalet - 1) / unidadesPorPalet;
            stock.CantidadPalets -= paletsAAbrir;
            stock.UnidadesSueltas = paletsAAbrir * unidadesPorPalet - unidadesFaltantes;
        }
        else
        {
            stock.UnidadesSueltas = 0;
        }

        // Por si los datos de los almacenes comprometen más de lo que hay.
        if (stock.CantidadPalets < 0)
        {
            stock.CantidadPalets = 0;
            stock.UnidadesSueltas = 0;
        }
    }
}
