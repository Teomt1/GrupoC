#nullable enable
using Alm = GrupoC.Almacenes;

namespace GrupoC.CumplirOrdenSeleccion;

internal class CumplirOrdenSeleccionModelo
{
    // Depósito que se usa si no se eligió ninguno en el menú principal.
    private const string DepositoPorDefecto = "D05";

    // Depósito asignado al usuario logueado (paso 2 del caso de uso): el elegido en el menú principal.
    private readonly string CodigoDeposito = string.IsNullOrEmpty(Alm.DepositoAlmacen.DepositoActual)
        ? DepositoPorDefecto
        : Alm.DepositoAlmacen.DepositoActual;

    // Legajo del usuario logueado: se toma uno de los empleados al abrir la pantalla.
    private readonly int legajoUsuario;

    // Relación entre las clases de la pantalla y los datos de los almacenes (no salen del modelo).
    private readonly Dictionary<OrdenSeleccion, Alm.OrdenSeleccionEntidad> ordenAlmacen = new();
    private readonly Dictionary<ItemInventario, Alm.Posicion> posicionAlmacen = new();

    public string UsuarioActual { get; }
    public string DepositoActual { get; }
    public List<OrdenSeleccion> OrdenesSeleccion { get; }
    public List<ItemInventario> Inventario { get; }
    public OrdenSeleccion? OrdenActual { get; private set; }

    public DateTime Fecha => DateTime.Today;

    public CumplirOrdenSeleccionModelo()
    {
        // Usuario identificado en el sistema: operario de Almacenamiento.
        UsuarioActual = "";
        List<Alm.EmpleadoEntidad> empleados = Alm.EmpleadoAlmacen.Empleado;
        if (empleados.Count > 0)
        {
            Alm.EmpleadoEntidad empleado = empleados[Random.Shared.Next(empleados.Count)];
            legajoUsuario = empleado.Legajo;
            UsuarioActual = empleado.Usuario;
        }
        DepositoActual = CodigoDeposito;

        // Descripción de cada SKU y unidades por pallet.
        var productos = new Dictionary<string, Alm.ProductoEntidad>();
        foreach (Alm.ProductoEntidad p in Alm.ProductoAlmacen.Producto)
        {
            productos[p.Sku] = p;
        }

        // Órdenes de selección con sus renglones. El renglón del almacén no trae el cliente:
        // se toma de la orden de preparación incluida en la selección que pide ese SKU,
        // o si no, del cliente que tiene dado de alta ese SKU.
        OrdenesSeleccion = new List<OrdenSeleccion>();
        foreach (Alm.OrdenSeleccionEntidad o in Alm.OrdenSeleccionAlmacen.OrdenSeleccion)
        {
            var orden = new OrdenSeleccion
            {
                Numero = o.Numero,
                Deposito = o.CodigoDeposito,
                Estado = o.Estado == Alm.EstadoOrdenSeleccionEnum.Cumplida
                    ? EstadoOrdenSeleccion.Cumplida
                    : EstadoOrdenSeleccion.Pendiente,
                FechaHoraCumplida = o.FechaHoraCumplida,
                UsuarioCumplida = empleados.Find(e => e.Legajo == o.LegajoUsuarioCumplida)?.Usuario,
                Renglones = o.Renglones.Select(r => new RenglonOrdenSeleccion
                {
                    Cliente = BuscarCliente(o.Numero, r.SKU_Producto),
                    Posicion = $"{r.PosicionPasillo}-{r.PosicionFila}-{r.PosicionNivel}",
                    Sku = r.SKU_Producto,
                    Descripcion = productos.ContainsKey(r.SKU_Producto) ? productos[r.SKU_Producto].Descripcion : "",
                    Cantidad = r.Cantidad
                }).ToList()
            };
            OrdenesSeleccion.Add(orden);
            ordenAlmacen[orden] = o;
        }

        // Inventario del depósito del usuario: una fila por posición que guarda mercadería,
        // con la cantidad en unidades (pallets completos más unidades sueltas).
        Inventario = new List<ItemInventario>();
        Alm.DepositoEntidad? deposito = Alm.DepositoAlmacen.Deposito.Find(d => d.Codigo == CodigoDeposito);
        if (deposito != null)
        {
            foreach (Alm.Posicion p in deposito.Posiciones)
            {
                if (p.Sku == null)
                {
                    continue;
                }

                int unidadesPorPallet = productos.ContainsKey(p.Sku) ? productos[p.Sku].UnidadesPorPallet : 0;
                var item = new ItemInventario
                {
                    Cliente = BuscarClienteDelSku(p.Sku),
                    Sku = p.Sku,
                    Posicion = $"{p.Pasillo}-{p.Fila}-{p.Nivel}",
                    Cantidad = p.CantidadPalets * unidadesPorPallet + p.CantidadSueltos
                };
                Inventario.Add(item);
                posicionAlmacen[item] = p;
            }
        }
    }

    // Paso 2: toma la orden del depósito del usuario en estado Pendiente con el número más bajo.
    // Devuelve el mensaje a mostrar, o null si encontró una orden.
    public string? BuscarOrdenPendiente()
    {
        OrdenActual = OrdenesSeleccion
            .Where(o => o.Deposito == DepositoActual && o.Estado == EstadoOrdenSeleccion.Pendiente)
            .OrderBy(o => o.Numero)
            .FirstOrDefault();

        // Excepción 2.1
        if (OrdenActual == null)
        {
            return "No hay órdenes de selección pendientes";
        }

        return null;
    }

    public bool HayOrdenPendiente => OrdenActual != null;

    // Paso 3
    public string NumeroOrdenActual => OrdenActual?.Numero.ToString() ?? "-";

    // Paso 4: renglones de la orden ordenados por posición (pasillo, fila y nivel) de menor a mayor.
    public List<RenglonOrdenSeleccion> RenglonesOrdenados =>
        OrdenActual?.Renglones
            .OrderBy(r => ClaveDePosicion(r.Posicion).Pasillo)
            .ThenBy(r => ClaveDePosicion(r.Posicion).Fila)
            .ThenBy(r => ClaveDePosicion(r.Posicion).Nivel)
            .ToList()
        ?? new List<RenglonOrdenSeleccion>();

    // Paso 7
    public string PreguntaConfirmacion =>
        $"¿Confirma que entregó a Preparación toda la mercadería de la orden de selección N° {OrdenActual?.Numero}?";

    // Pasos 9 a 11. Devuelve el mensaje a mostrar.
    public string CumplirOrden()
    {
        if (OrdenActual == null)
        {
            return "No hay una orden de selección para confirmar";
        }

        // Paso 9: se actualiza la orden en la pantalla y en el almacén.
        OrdenActual.Estado = EstadoOrdenSeleccion.Cumplida;
        OrdenActual.FechaHoraCumplida = DateTime.Now;
        OrdenActual.UsuarioCumplida = UsuarioActual;

        Alm.OrdenSeleccionEntidad entidad = ordenAlmacen[OrdenActual];
        entidad.Estado = Alm.EstadoOrdenSeleccionEnum.Cumplida;
        entidad.FechaHoraCumplida = OrdenActual.FechaHoraCumplida;
        entidad.LegajoUsuarioCumplida = legajoUsuario;

        // Las órdenes de preparación de esta selección ya tienen su mercadería en Preparación.
        foreach (Alm.OrdenPreparacionEntidad op in Alm.OrdenPreparacionAlmacen.OrdenPreparacion
                     .Where(op => op.NumeroOrdenSeleccion == OrdenActual.Numero))
        {
            op.EstadoOrdenSeleccion = Alm.EstadoOrdenSeleccionEnum.Cumplida;
            if (op.Estado == Alm.EstadoOrdenPreparacionEnum.Pendiente)
            {
                op.Estado = Alm.EstadoOrdenPreparacionEnum.EnPreparacion;
            }
        }

        // Paso 10: resta del inventario cada renglón (clave: cliente, SKU y posición).
        foreach (var renglon in OrdenActual.Renglones)
        {
            var stock = Inventario.FirstOrDefault(i => i.Cliente == renglon.Cliente
                                                    && i.Sku == renglon.Sku
                                                    && i.Posicion == renglon.Posicion);
            if (stock != null)
            {
                stock.Cantidad -= renglon.Cantidad;
                DescontarDePosicion(posicionAlmacen[stock], renglon.Cantidad);
            }
        }

        // Paso 11
        return $"Orden de selección N° {OrdenActual.Numero} cumplida";
    }

    // Resta las unidades de la posición del almacén. Si la cantidad son pallets completos y los hay,
    // se bajan pallets cerrados; si no, primero las unidades sueltas y luego se abren los pallets necesarios.
    private void DescontarDePosicion(Alm.Posicion posicion, int unidades)
    {
        Alm.ProductoEntidad? producto = Alm.ProductoAlmacen.Producto.Find(p => p.Sku == posicion.Sku);
        int unidadesPorPallet = producto?.UnidadesPorPallet ?? 0;

        if (unidadesPorPallet > 0 && unidades % unidadesPorPallet == 0
            && posicion.CantidadPalets >= unidades / unidadesPorPallet)
        {
            posicion.CantidadPalets -= unidades / unidadesPorPallet;
        }
        else if (posicion.CantidadSueltos >= unidades)
        {
            posicion.CantidadSueltos -= unidades;
        }
        else if (unidadesPorPallet > 0)
        {
            int unidadesFaltantes = unidades - posicion.CantidadSueltos;
            int palletsAAbrir = (unidadesFaltantes + unidadesPorPallet - 1) / unidadesPorPallet;
            posicion.CantidadPalets -= palletsAAbrir;
            posicion.CantidadSueltos = palletsAAbrir * unidadesPorPallet - unidadesFaltantes;
        }
        else
        {
            posicion.CantidadSueltos = 0;
        }

        // Por si en la posición había menos mercadería que la pedida.
        if (posicion.CantidadPalets < 0)
        {
            posicion.CantidadPalets = 0;
            posicion.CantidadSueltos = 0;
        }

        // Si la posición quedó vacía, se libera.
        if (posicion.CantidadPalets == 0 && posicion.CantidadSueltos == 0)
        {
            posicion.Sku = null;
        }
    }

    // Cliente del renglón: el de la orden de preparación de esta selección que pide el SKU.
    private string BuscarCliente(int numeroOrdenSeleccion, string sku)
    {
        Alm.OrdenPreparacionEntidad? orden = Alm.OrdenPreparacionAlmacen.OrdenPreparacion.Find(op =>
            op.NumeroOrdenSeleccion == numeroOrdenSeleccion && op.Renglones.Any(r => r.SKU_Producto == sku));

        return orden?.CodigoCliente ?? BuscarClienteDelSku(sku);
    }

    // Cliente que tiene dado de alta el SKU.
    private string BuscarClienteDelSku(string sku)
    {
        return Alm.ClienteAlmacen.Cliente.Find(c => c.Productos.Contains(sku))?.Codigo ?? "";
    }

    // Convierte "A-1-2" en ("A", 1, 2) para ordenar la fila y el nivel por número y no como texto.
    private (string Pasillo, int Fila, int Nivel) ClaveDePosicion(string posicion)
    {
        var partes = posicion.Split('-');
        int fila = partes.Length > 1 && int.TryParse(partes[1], out int f) ? f : 0;
        int nivel = partes.Length > 2 && int.TryParse(partes[2], out int n) ? n : 0;
        return (partes[0], fila, nivel);
    }
}
