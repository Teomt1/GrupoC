#nullable enable
using Alm = GrupoC.Almacenes;

namespace GrupoC.EntregaATransportista;

internal class EntregaATransportistaModelo
{
    // Depósito del usuario si no se eligió uno en la pantalla principal.
    private const string DepositoPorDefecto = "D01";

    // Legajo del usuario logueado (el elegido en la pantalla principal, o uno de los empleados).
    private readonly int legajoUsuario;

    // Relación entre las clases de la pantalla y los datos de los almacenes (no salen del modelo).
    private readonly Dictionary<OrdenPreparacion, Alm.OrdenPreparacionEntidad> ordenPreparacionAlmacen = new();
    private readonly Dictionary<OrdenEntrega, Alm.OrdenEntregaEntidad> ordenEntregaAlmacen = new();
    private readonly Dictionary<RemitoEntrega, Alm.RemitoEntregaEntidad> remitoAlmacen = new();

    // Usuario identificado en el sistema y su depósito.
    public string CodigoUsuario { get; }
    public string CodigoDeposito { get; }

    public List<OrdenPreparacion> OrdenesPreparacion { get; }
    public List<OrdenEntrega> OrdenesEntrega { get; }
    public List<RemitoEntrega> RemitosEntrega { get; }
    public List<DetalleRemitoEntrega> DetallesRemitoEntrega { get; } = new();
    public List<MovimientoMercaderia> MovimientosMercaderia { get; } = new();

    // Entrega en curso.
    public string? TransportistaNombre { get; private set; }
    public int? TransportistaDni { get; private set; }
    public List<OrdenEntrega> OrdenesAEntregar { get; } = new();
    public RemitoEntrega? Remito { get; private set; }
    public bool RemitoFirmado { get; set; }

    public bool PuedeBuscar => Remito == null;
    public bool PuedeGenerarRemito => Remito == null && OrdenesAEntregar.Count > 0;
    public bool PuedeMarcarRemitoFirmado => Remito != null;
    public bool PuedeConfirmarDespacho => Remito != null && RemitoFirmado;

    public EntregaATransportistaModelo()
    {
        // Usuario identificado en el sistema: operario de Despacho.
        CodigoUsuario = "";
        List<Alm.EmpleadoEntidad> empleados = Alm.EmpleadoAlmacen.Empleado;
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
            CodigoUsuario = empleado.Usuario;
        }

        // Depósito del usuario (paso 4 del caso de uso).
        CodigoDeposito = string.IsNullOrEmpty(Alm.DepositoAlmacen.DepositoActual)
            ? DepositoPorDefecto
            : Alm.DepositoAlmacen.DepositoActual;

        List<Alm.RemitoEntregaEntidad> remitos = Alm.RemitoEntregaAlmacen.RemitoEntrega;

        // Órdenes de preparación con el transportista autorizado por el cliente.
        // El número de remito es el del remito firmado en el que se despachó la orden.
        OrdenesPreparacion = new List<OrdenPreparacion>();
        foreach (Alm.OrdenPreparacionEntidad op in Alm.OrdenPreparacionAlmacen.OrdenPreparacion)
        {
            var orden = new OrdenPreparacion
            {
                Numero = op.Numero,
                CodigoCliente = op.CodigoCliente ?? "",
                CodigoDeposito = op.CodigoDeposito ?? "",
                TransportistaNombre = op.TransportistaNombre ?? "",
                TransportistaDni = op.TransportistaDni,
                Estado = ConvertirEstado(op.Estado),
                NumeroRemito = remitos
                    .Find(r => r.Estado == Alm.EstadoRemitoEnum.Firmado
                               && r.Detalles != null
                               && r.Detalles.Any(d => d.NumeroOrdenPreparacion == op.Numero))
                    ?.Numero
            };
            OrdenesPreparacion.Add(orden);
            ordenPreparacionAlmacen[orden] = op;
        }

        // Órdenes de entrega. El depósito es el de su orden de preparación.
        OrdenesEntrega = new List<OrdenEntrega>();
        foreach (Alm.OrdenEntregaEntidad oe in Alm.OrdenEntregaAlmacen.OrdenEntrega)
        {
            Alm.OrdenPreparacionEntidad? op = Alm.OrdenPreparacionAlmacen.OrdenPreparacion
                .Find(o => o.Numero == oe.NumeroOrdenPreparacion);

            var orden = new OrdenEntrega
            {
                NumeroOrdenPreparacion = oe.NumeroOrdenPreparacion,
                CodigoDeposito = op?.CodigoDeposito ?? "",
                CantidadPaquetes = oe.CantidadPaquetes,
                Estado = oe.Estado == Alm.EstadoOrdenEntregaEnum.Despachada
                    ? EstadoOrdenEntrega.Despachada
                    : EstadoOrdenEntrega.Pendiente,
                FechaHoraDespacho = oe.FechaHoraDespacho
            };
            OrdenesEntrega.Add(orden);
            ordenEntregaAlmacen[orden] = oe;
        }

        // Remitos de entrega con un renglón de detalle por orden.
        RemitosEntrega = new List<RemitoEntrega>();
        foreach (Alm.RemitoEntregaEntidad r in remitos)
        {
            var remito = new RemitoEntrega
            {
                Numero = r.Numero,
                FechaHora = r.FechaHora,
                CodigoDeposito = r.CodigoDeposito ?? "",
                TransportistaNombre = r.TransportistaNombre ?? "",
                TransportistaDni = r.TransportistaDni,
                CodigoUsuario = empleados.Find(e => e.Legajo == r.LegajoUsuario)?.Usuario ?? "",
                Estado = ConvertirEstado(r.Estado),
                FechaHoraCumplido = r.FechaHoraCumplido,
                CodigoUsuarioCumplido = empleados.Find(e => e.Legajo == r.LegajoCumplido)?.Usuario
            };
            RemitosEntrega.Add(remito);
            remitoAlmacen[remito] = r;

            foreach (Alm.DetalleRemitoEntrega d in r.Detalles ?? new List<Alm.DetalleRemitoEntrega>())
            {
                Alm.OrdenEntregaEntidad? oe = Alm.OrdenEntregaAlmacen.OrdenEntrega
                    .Find(o => o.Numero == d.NumeroOrdenEntrega);

                DetallesRemitoEntrega.Add(new DetalleRemitoEntrega
                {
                    NumeroRemito = r.Numero,
                    NumeroOrdenPreparacion = d.NumeroOrdenPreparacion,
                    CantidadPaquetes = oe?.CantidadPaquetes ?? 0
                });
            }
        }

        // Egresos de mercadería ya registrados.
        foreach (Alm.MovimientoMercaderiaEntidad m in Alm.MovimientoMercaderiaAlmacen.MovimientoMercaderia)
        {
            if (m.Tipo != Alm.TipoMovimientoEnum.Egreso || m.NumeroOrdenPreparacion == null || m.NumeroRemitoEntrega == null)
            {
                continue;
            }

            MovimientosMercaderia.Add(new MovimientoMercaderia
            {
                CodigoCliente = m.CodigoCliente ?? "",
                NumeroOrdenPreparacion = m.NumeroOrdenPreparacion.Value,
                NumeroRemito = m.NumeroRemitoEntrega.Value,
                Fecha = m.FechaHora.Date
            });
        }
    }

    // Devuelve el error a mostrar, o null si encontró órdenes para el transportista.
    // Si está autorizado pero no tiene órdenes pendientes, el nombre queda cargado igual.
    public string? Buscar(int? dni)
    {
        Reiniciar();

        if (dni == null || dni < 10000000 || dni > 99999999)
        {
            return "Ingrese un DNI válido";
        }

        // Autorizado: el DNI figura en alguna orden de preparación del depósito.
        List<OrdenPreparacion> ordenesDelTransportista = OrdenesPreparacion
            .Where(op => op.CodigoDeposito == CodigoDeposito && op.TransportistaDni == dni)
            .OrderBy(op => op.Numero)
            .ToList();

        if (ordenesDelTransportista.Count == 0)
        {
            return "El transportista no está autorizado a retirar mercadería en este depósito";
        }

        TransportistaNombre = ordenesDelTransportista[0].TransportistaNombre;
        TransportistaDni = dni;

        foreach (OrdenPreparacion op in ordenesDelTransportista)
        {
            OrdenEntrega? oe = OrdenesEntrega.Find(oe => oe.NumeroOrdenPreparacion == op.Numero
                                                      && oe.CodigoDeposito == CodigoDeposito
                                                      && oe.Estado == EstadoOrdenEntrega.Pendiente);
            if (oe != null)
            {
                OrdenesAEntregar.Add(oe);
            }
        }

        if (OrdenesAEntregar.Count == 0)
        {
            return "El transportista no tiene órdenes listas para retirar";
        }

        // Se muestra el nombre registrado en las órdenes que se van a entregar.
        TransportistaNombre = BuscarOrdenPreparacion(OrdenesAEntregar[0]).TransportistaNombre;
        return null;
    }

    // Paso 8: registra el remito en estado Emitido y un renglón de detalle por orden.
    public RemitoEntrega GenerarRemito()
    {
        if (!PuedeGenerarRemito)
        {
            throw new InvalidOperationException("No hay órdenes para generar el remito, o el remito ya fue generado.");
        }

        int numero = RemitosEntrega.Count == 0 ? 1 : RemitosEntrega.Max(r => r.Numero) + 1;

        Remito = new RemitoEntrega
        {
            Numero = numero,
            FechaHora = DateTime.Now,
            CodigoDeposito = CodigoDeposito,
            TransportistaNombre = TransportistaNombre!,
            TransportistaDni = TransportistaDni!.Value,
            CodigoUsuario = CodigoUsuario,
            Estado = EstadoRemito.Emitido
        };
        RemitosEntrega.Add(Remito);

        foreach (OrdenEntrega oe in OrdenesAEntregar)
        {
            DetallesRemitoEntrega.Add(new DetalleRemitoEntrega
            {
                NumeroRemito = numero,
                NumeroOrdenPreparacion = oe.NumeroOrdenPreparacion,
                CantidadPaquetes = oe.CantidadPaquetes
            });
        }

        // Se registra el remito en el almacén con su detalle.
        var entidad = new Alm.RemitoEntregaEntidad
        {
            Numero = Remito.Numero,
            FechaHora = Remito.FechaHora,
            CodigoDeposito = Remito.CodigoDeposito,
            Detalles = OrdenesAEntregar.Select(oe => new Alm.DetalleRemitoEntrega
            {
                NumeroOrdenEntrega = ordenEntregaAlmacen[oe].Numero,
                NumeroOrdenPreparacion = oe.NumeroOrdenPreparacion
            }).ToList(),
            LegajoUsuario = legajoUsuario,
            Estado = Alm.EstadoRemitoEnum.Emitido,
            FechaHoraCumplido = null,
            LegajoCumplido = null,
            TransportistaNombre = Remito.TransportistaNombre,
            TransportistaDni = Remito.TransportistaDni,
            CantidadPaquetes = OrdenesAEntregar.Sum(oe => oe.CantidadPaquetes)
        };
        Alm.RemitoEntregaAlmacen.RemitoEntrega.Add(entidad);
        remitoAlmacen[Remito] = entidad;

        return Remito;
    }

    // Pasos 14 a 16: registra el cumplido, despacha las órdenes y el egreso de mercadería.
    public void ConfirmarDespacho()
    {
        if (!PuedeConfirmarDespacho || Remito!.Estado != EstadoRemito.Emitido)
        {
            throw new InvalidOperationException("El remito tiene que estar generado y firmado para confirmar el despacho.");
        }

        DateTime ahora = DateTime.Now;

        // Paso 14: cumplido del remito, en la pantalla y en el almacén.
        Remito.Estado = EstadoRemito.Firmado;
        Remito.FechaHoraCumplido = ahora;
        Remito.CodigoUsuarioCumplido = CodigoUsuario;

        Alm.RemitoEntregaEntidad remitoEntidad = remitoAlmacen[Remito];
        remitoEntidad.Estado = Alm.EstadoRemitoEnum.Firmado;
        remitoEntidad.FechaHoraCumplido = ahora;
        remitoEntidad.LegajoCumplido = legajoUsuario;

        foreach (OrdenEntrega oe in OrdenesAEntregar)
        {
            // Paso 15: orden de preparación Cumplida y orden de entrega Despachada.
            OrdenPreparacion op = BuscarOrdenPreparacion(oe);
            op.Estado = EstadoOrdenPreparacion.Cumplida;
            op.NumeroRemito = Remito.Numero;
            ordenPreparacionAlmacen[op].Estado = Alm.EstadoOrdenPreparacionEnum.Cumplida;

            oe.Estado = EstadoOrdenEntrega.Despachada;
            oe.FechaHoraDespacho = ahora;
            Alm.OrdenEntregaEntidad oeEntidad = ordenEntregaAlmacen[oe];
            oeEntidad.Estado = Alm.EstadoOrdenEntregaEnum.Despachada;
            oeEntidad.FechaHoraDespacho = ahora;

            // Paso 16: egreso en Movimientos de mercadería para la factura mensual.
            MovimientosMercaderia.Add(new MovimientoMercaderia
            {
                CodigoCliente = op.CodigoCliente,
                NumeroOrdenPreparacion = op.Numero,
                NumeroRemito = Remito.Numero,
                Fecha = ahora.Date
            });

            Alm.MovimientoMercaderiaAlmacen.MovimientoMercaderia.Add(new Alm.MovimientoMercaderiaEntidad
            {
                Tipo = Alm.TipoMovimientoEnum.Egreso,
                FechaHora = ahora,
                CodigoCliente = op.CodigoCliente,
                NumeroOrdenRecepcion = null,
                NumeroOrdenPreparacion = op.Numero,
                NumeroRemitoEntrega = Remito.Numero,
                LegajoUsuario = legajoUsuario
            });
        }
    }

    // Excepción "Cancelar": si el remito ya se había generado y no se confirmó, queda Anulado.
    // Las órdenes siguen Pendiente.
    public void Cancelar()
    {
        if (Remito != null && Remito.Estado == EstadoRemito.Emitido)
        {
            Remito.Estado = EstadoRemito.Anulado;
            remitoAlmacen[Remito].Estado = Alm.EstadoRemitoEnum.Anulado;
        }

        Reiniciar();
    }

    public void Reiniciar()
    {
        TransportistaNombre = null;
        TransportistaDni = null;
        OrdenesAEntregar.Clear();
        Remito = null;
        RemitoFirmado = false;
    }

    private OrdenPreparacion BuscarOrdenPreparacion(OrdenEntrega oe)
    {
        return OrdenesPreparacion.Find(op => op.Numero == oe.NumeroOrdenPreparacion && op.CodigoDeposito == oe.CodigoDeposito)!;
    }

    // La pantalla no distingue "En preparación": para entregar, esa orden todavía no está lista.
    private EstadoOrdenPreparacion ConvertirEstado(Alm.EstadoOrdenPreparacionEnum estado)
    {
        switch (estado)
        {
            case Alm.EstadoOrdenPreparacionEnum.Preparada:
                return EstadoOrdenPreparacion.Preparada;
            case Alm.EstadoOrdenPreparacionEnum.Cumplida:
                return EstadoOrdenPreparacion.Cumplida;
            default:
                return EstadoOrdenPreparacion.Pendiente;
        }
    }

    private EstadoRemito ConvertirEstado(Alm.EstadoRemitoEnum estado)
    {
        switch (estado)
        {
            case Alm.EstadoRemitoEnum.Firmado:
                return EstadoRemito.Firmado;
            case Alm.EstadoRemitoEnum.Anulado:
                return EstadoRemito.Anulado;
            default:
                return EstadoRemito.Emitido;
        }
    }
}
