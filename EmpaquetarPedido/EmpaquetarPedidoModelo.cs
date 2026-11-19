#nullable enable
using Alm = GrupoC.Almacenes;

namespace GrupoC.EmpaquetarPedido;

internal class EmpaquetarPedidoModelo
{
    // Depósito del usuario si no se eligió uno en la pantalla principal.
    private const string DepositoPorDefecto = "D01";

    // Legajo del usuario logueado (el elegido en la pantalla principal, o uno de los empleados).
    private readonly int legajoUsuario;

    // Código del depósito del usuario tal como está en el almacén (por ejemplo "D01").
    private readonly string codigoDeposito;

    // Empleados del almacén, para pasar de legajo a usuario.
    private readonly List<Alm.EmpleadoEntidad> empleados;

    // Relación entre las clases de la pantalla y los datos de los almacenes (no salen del modelo).
    private readonly Dictionary<OrdenPreparacion, Alm.OrdenPreparacionEntidad> ordenAlmacen = new();

    // Operario identificado en el sistema y su depósito asignado (precondición del caso de uso).
    public string Usuario { get; }
    public int Deposito { get; }

    public OrdenPreparacion? OrdenActual { get; private set; }

    // Cantidad total de paquetes que informa el operario al terminar de empaquetar (null: todavía no la ingresó).
    public int? CantidadPaquetes { get; private set; }

    // La cantidad de paquetes se ingresa una sola vez por orden, y recién con ella se puede confirmar.
    public bool PuedeIngresarPaquetes => OrdenActual != null && CantidadPaquetes == null;
    public bool PuedeConfirmar => OrdenActual != null && CantidadPaquetes != null;

    private readonly List<OrdenPreparacion> ordenesPreparacion;

    public EmpaquetarPedidoModelo()
    {
        // Usuario identificado en el sistema: operario de Preparación.
        Usuario = "";
        empleados = Alm.EmpleadoAlmacen.Empleado;
        Alm.EmpleadoEntidad? empleado = int.TryParse(Alm.EmpleadoAlmacen.EmpleadoActual, out int legajoElegido)
            ? empleados.Find(e => e.Legajo == legajoElegido)
            : null;
        if (empleado == null && empleados.Count > 0)
        {
            empleado = empleados[Random.Shared.Next(empleados.Count)];
        }
        if (empleado != null)
        {
            legajoUsuario = empleado.Legajo;
            Usuario = empleado.Usuario;
        }

        // Depósito del usuario. En la pantalla es un número: "D03" pasa a ser 3.
        codigoDeposito = string.IsNullOrEmpty(Alm.DepositoAlmacen.DepositoActual)
            ? DepositoPorDefecto
            : Alm.DepositoAlmacen.DepositoActual;
        Deposito = NumeroDeDeposito(codigoDeposito);

        // Órdenes de preparación con sus renglones. Solo se cargan las "En preparación",
        // que son las únicas que la pantalla puede tomar para empaquetar.
        ordenesPreparacion = new List<OrdenPreparacion>();
        foreach (Alm.OrdenPreparacionEntidad op in Alm.OrdenPreparacionAlmacen.OrdenPreparacion)
        {
            if (op.Estado != Alm.EstadoOrdenPreparacionEnum.EnPreparacion)
            {
                continue;
            }

            var orden = new OrdenPreparacion
            {
                Numero = op.Numero,
                Deposito = NumeroDeDeposito(op.CodigoDeposito),
                TipoPedido = op.TipoPedido == Alm.TipoPedidoEnum.Fulfillment
                    ? TipoPedido.Fulfillment
                    : TipoPedido.PaleCerrado,
                Transportista = op.TransportistaNombre ?? "",
                OrdenSeleccionCumplida = op.EstadoOrdenSeleccion == Alm.EstadoOrdenSeleccionEnum.Cumplida,
                Estado = EstadoOrdenPreparacion.EnPreparacion,
                TomadaPor = BuscarUsuario(op.LegajoEmpleadoTomador),
                // En el almacén la cantidad está en unidades; en la pantalla va en pallets si el pedido
                // es a palé cerrado y en unidades si es fulfillment (lo indica la columna del tipo de pedido).
                Renglones = (op.Renglones ?? new List<Alm.RenglonOrdenPreparacion>())
                    .Select(r => new RenglonOrdenPreparacion
                    {
                        Sku = r.SKU_Producto,
                        Cantidad = op.TipoPedido == Alm.TipoPedidoEnum.PalletCerrado
                            ? CantidadEnPallets(r.SKU_Producto, r.Cantidad)
                            : r.Cantidad
                    }).ToList()
            };
            ordenesPreparacion.Add(orden);
            ordenAlmacen[orden] = op;
        }
    }

    // Paso 2: orden "En preparación" del depósito, con su orden de selección "Cumplida" y no tomada
    // por otro operario. Toma la de número más bajo y la marca como tomada con el código del usuario.
    // Devuelve null si no hay órdenes para preparar (excepción del paso 2).
    public OrdenPreparacion? TomarSiguienteOrden()
    {
        CantidadPaquetes = null;
        OrdenActual = ordenesPreparacion
            .Where(o => o.Deposito == Deposito
                        && o.Estado == EstadoOrdenPreparacion.EnPreparacion
                        && o.OrdenSeleccionCumplida
                        && (o.TomadaPor == null || o.TomadaPor == Usuario))
            .OrderBy(o => o.Numero)
            .FirstOrDefault();

        if (OrdenActual != null)
        {
            // Se marca como tomada en la pantalla y en el almacén.
            OrdenActual.TomadaPor = Usuario;
            ordenAlmacen[OrdenActual].LegajoEmpleadoTomador = legajoUsuario;
        }
        return OrdenActual;
    }

    // Cantidad de paquetes: solo admite números.
    public bool AdmiteSoloNumeros(char caracter)
    {
        return char.IsControl(caracter) || char.IsDigit(caracter);
    }

    // Registra la cantidad total de paquetes de la orden actual.
    // Devuelve el error a mostrar, o null si quedó registrada.
    public string? IngresarCantidadPaquetes(string texto)
    {
        if (!PuedeIngresarPaquetes)
        {
            return "La cantidad de paquetes de esta orden ya fue ingresada";
        }

        if (!int.TryParse(texto.Trim(), out int cantidad) || cantidad <= 0)
        {
            return "Ingrese una cantidad de paquetes mayor a cero";
        }

        CantidadPaquetes = cantidad;
        return null;
    }

    public string DescripcionTipoPedido(TipoPedido tipoPedido)
    {
        return tipoPedido == TipoPedido.Fulfillment ? "Fulfillment" : "Palé cerrado";
    }

    // Pasos 8 y 9: registra la orden de entrega "Pendiente" y deja la orden de preparación
    // "Preparada" con fecha, hora y usuario, liberándola. Devuelve null si no hay orden con productos
    // o si todavía no se ingresó la cantidad de paquetes.
    public OrdenEntrega? ConfirmarEmpaquetado()
    {
        if (OrdenActual == null || OrdenActual.Renglones.Count == 0 || CantidadPaquetes == null)
        {
            return null;
        }

        Alm.OrdenPreparacionEntidad op = ordenAlmacen[OrdenActual];
        List<Alm.OrdenEntregaEntidad> ordenesEntrega = Alm.OrdenEntregaAlmacen.OrdenEntrega;
        DateTime ahora = DateTime.Now;

        // Paso 8: orden de entrega en la pantalla y en el almacén.
        var ordenEntrega = new OrdenEntrega
        {
            Numero = ordenesEntrega.Count == 0 ? 1 : ordenesEntrega.Max(o => o.Numero) + 1,
            FechaHora = ahora,
            Deposito = Deposito,
            NumeroOrdenPreparacion = OrdenActual.Numero,
            Transportista = OrdenActual.Transportista,
            Usuario = Usuario,
            CantidadPaquetes = CantidadPaquetes.Value,
            Estado = EstadoOrdenEntrega.Pendiente
        };

        ordenesEntrega.Add(new Alm.OrdenEntregaEntidad
        {
            Numero = ordenEntrega.Numero,
            NumeroOrdenPreparacion = op.Numero,
            TipoPedidoPreparacion = op.TipoPedido,
            TransportistaNombrePreparacion = op.TransportistaNombre,
            TransportistaDniPreparacion = op.TransportistaDni,
            FechaHora = ahora,
            LegajoEmpleado = legajoUsuario,
            // La cantidad de paquetes es la que informó el operario; es la que después figura en el remito.
            CantidadPaquetes = ordenEntrega.CantidadPaquetes,
            Estado = Alm.EstadoOrdenEntregaEnum.Pendiente,
            FechaHoraDespacho = null
        });

        // Paso 9: la orden de preparación queda "Preparada" y liberada, en la pantalla y en el almacén.
        OrdenActual.Estado = EstadoOrdenPreparacion.Preparada;
        OrdenActual.FechaPreparada = ahora;
        OrdenActual.PreparadaPor = Usuario;
        OrdenActual.TomadaPor = null;

        op.Estado = Alm.EstadoOrdenPreparacionEnum.Preparada;
        op.FechaPreparada = ahora;
        op.LegajoEmpleadoPreparación = legajoUsuario;
        op.LegajoEmpleadoTomador = 0;

        OrdenActual = null;
        CantidadPaquetes = null;

        return ordenEntrega;
    }

    // Cierre sin confirmar: la orden sigue "En preparación" y queda liberada para otro puesto.
    public void LiberarOrdenActual()
    {
        if (OrdenActual == null)
        {
            return;
        }

        OrdenActual.TomadaPor = null;
        ordenAlmacen[OrdenActual].LegajoEmpleadoTomador = 0;
        OrdenActual = null;
        CantidadPaquetes = null;
    }

    // Usuario del empleado con ese legajo, o null si no hay legajo (0) o no existe.
    private string? BuscarUsuario(int legajo)
    {
        return legajo == 0 ? null : empleados.Find(e => e.Legajo == legajo)?.Usuario;
    }

    // Pasa unidades a pallets con las unidades por pallet del producto.
    private int CantidadEnPallets(string sku, int unidades)
    {
        int unidadesPorPallet = Alm.ProductoAlmacen.Producto.Find(p => p.Sku == sku)?.UnidadesPorPallet ?? 0;
        return unidadesPorPallet > 0 ? unidades / unidadesPorPallet : unidades;
    }

    // Convierte el código del almacén ("D03") en el número que usa la pantalla (3).
    private int NumeroDeDeposito(string? codigo)
    {
        string digitos = new string((codigo ?? "").Where(char.IsDigit).ToArray());
        return int.TryParse(digitos, out int numero) ? numero : 0;
    }
}
