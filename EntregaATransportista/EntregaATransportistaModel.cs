namespace GrupoC.EntregaATransportista
{
    /// <summary>
    /// Lógica del caso de uso "Entregar mercadería al transportista".
    /// El transportista se identifica solo por su DNI; el nombre lo toma el sistema de la orden de preparación.
    /// </summary>
    public class EntregaATransportistaModel
    {
        // Usuario identificado en el sistema y su depósito.
        public string CodigoUsuario { get; } = "OPD01";
        public string CodigoDeposito { get; } = "DEP01";

        // Datos de prueba de la planilla "Prueba - Entregar mercadería al transportista".
        // El transportista autorizado (nombre y DNI) está registrado en cada orden de preparación.
        // 99999999 no figura en ninguna orden a propósito (excepción "no autorizado").
        public List<OrdenPreparacion> OrdenesPreparacion { get; } = new()
        {
            // Martínez, Luis: dos órdenes listas para retirar en DEP01.
            new OrdenPreparacion { Numero = 2001, CodigoCliente = "CLI001", CodigoDeposito = "DEP01", TransportistaNombre = "Martínez, Luis", TransportistaDni = 30123456, Estado = EstadoOrdenPreparacion.Preparada },
            new OrdenPreparacion { Numero = 2005, CodigoCliente = "CLI002", CodigoDeposito = "DEP01", TransportistaNombre = "Martínez, Luis", TransportistaDni = 30123456, Estado = EstadoOrdenPreparacion.Preparada },
            // Martínez, Luis en otro depósito: no debe aparecer en la grilla.
            new OrdenPreparacion { Numero = 2006, CodigoCliente = "CLI003", CodigoDeposito = "DEP02", TransportistaNombre = "Martínez, Luis", TransportistaDni = 30123456, Estado = EstadoOrdenPreparacion.Preparada },
            // Gómez, Carlos: otro transportista con una orden lista.
            new OrdenPreparacion { Numero = 2002, CodigoCliente = "CLI001", CodigoDeposito = "DEP01", TransportistaNombre = "Gómez, Carlos", TransportistaDni = 27888999, Estado = EstadoOrdenPreparacion.Preparada },
            // Sosa, Ana: autorizada, pero una orden ya fue despachada y la otra todavía no está preparada.
            new OrdenPreparacion { Numero = 2003, CodigoCliente = "CLI002", CodigoDeposito = "DEP01", TransportistaNombre = "Sosa, Ana", TransportistaDni = 28555111, Estado = EstadoOrdenPreparacion.Cumplida, NumeroRemito = 5000 },
            new OrdenPreparacion { Numero = 2004, CodigoCliente = "CLI003", CodigoDeposito = "DEP01", TransportistaNombre = "Sosa, Ana", TransportistaDni = 28555111, Estado = EstadoOrdenPreparacion.Pendiente },
        };

        public List<OrdenEntrega> OrdenesEntrega { get; } = new()
        {
            new OrdenEntrega { NumeroOrdenPreparacion = 2001, CodigoDeposito = "DEP01", CantidadPaquetes = 3, Estado = EstadoOrdenEntrega.Pendiente },
            new OrdenEntrega { NumeroOrdenPreparacion = 2002, CodigoDeposito = "DEP01", CantidadPaquetes = 1, Estado = EstadoOrdenEntrega.Pendiente },
            new OrdenEntrega { NumeroOrdenPreparacion = 2003, CodigoDeposito = "DEP01", CantidadPaquetes = 4, Estado = EstadoOrdenEntrega.Despachada, FechaHoraDespacho = new DateTime(2026, 10, 1, 10, 30, 0) },
            new OrdenEntrega { NumeroOrdenPreparacion = 2005, CodigoDeposito = "DEP01", CantidadPaquetes = 2, Estado = EstadoOrdenEntrega.Pendiente },
            new OrdenEntrega { NumeroOrdenPreparacion = 2006, CodigoDeposito = "DEP02", CantidadPaquetes = 4, Estado = EstadoOrdenEntrega.Pendiente },
        };

        public List<RemitoEntrega> RemitosEntrega { get; } = new()
        {
            new RemitoEntrega { Numero = 5000, FechaHora = new DateTime(2026, 10, 1, 10, 15, 0), CodigoDeposito = "DEP01", TransportistaNombre = "Sosa, Ana", TransportistaDni = 28555111, CodigoUsuario = "OPD01", Estado = EstadoRemito.Firmado, FechaHoraCumplido = new DateTime(2026, 10, 1, 10, 30, 0), CodigoUsuarioCumplido = "OPD01" },
        };

        public List<DetalleRemitoEntrega> DetallesRemitoEntrega { get; } = new()
        {
            new DetalleRemitoEntrega { NumeroRemito = 5000, NumeroOrdenPreparacion = 2003, CantidadPaquetes = 4 },
        };

        public List<MovimientoMercaderia> MovimientosMercaderia { get; } = new()
        {
            new MovimientoMercaderia { CodigoCliente = "CLI002", NumeroOrdenPreparacion = 2003, NumeroRemito = 5000, Fecha = new DateTime(2026, 10, 1) },
        };

        // Estado de la entrega en curso.

        /// <summary>Nombre del transportista autorizado, o null si no se encontró ninguno.</summary>
        public string? TransportistaNombre { get; private set; }
        public int TransportistaDni { get; private set; }
        public List<OrdenEntrega> OrdenesAEntregar { get; } = new();
        public RemitoEntrega? Remito { get; private set; }
        public bool RemitoFirmado { get; set; }

        public bool PuedeBuscar => Remito == null;
        public bool PuedeGenerarRemito => Remito == null && OrdenesAEntregar.Count > 0;
        public bool PuedeMarcarRemitoFirmado => Remito != null;
        public bool PuedeConfirmarDespacho => Remito != null && RemitoFirmado;

        /// <summary>
        /// Pasos 3 a 5. Devuelve el mensaje de error, o null si encontró órdenes para el transportista.
        /// Si está autorizado pero no tiene órdenes pendientes, el nombre queda cargado igual.
        /// </summary>
        public string? Buscar(int dni)
        {
            Reiniciar();

            if (dni < 10000000 || dni > 99999999)
                return "Ingrese un DNI válido";

            // Autorizado: el DNI figura en alguna orden de preparación del depósito.
            var ordenesPreparacion = OrdenesPreparacion
                .Where(op => op.CodigoDeposito == CodigoDeposito && op.TransportistaDni == dni)
                .OrderBy(op => op.Numero)
                .ToList();

            if (ordenesPreparacion.Count == 0)
                return "El transportista no está autorizado a retirar mercadería en este depósito";

            TransportistaNombre = ordenesPreparacion[0].TransportistaNombre;
            TransportistaDni = dni;

            foreach (var op in ordenesPreparacion)
            {
                var oe = OrdenesEntrega.FirstOrDefault(oe => oe.NumeroOrdenPreparacion == op.Numero
                                                          && oe.CodigoDeposito == CodigoDeposito
                                                          && oe.Estado == EstadoOrdenEntrega.Pendiente);
                if (oe != null)
                    OrdenesAEntregar.Add(oe);
            }

            if (OrdenesAEntregar.Count == 0)
                return "El transportista no tiene órdenes listas para retirar";

            // Se muestra el nombre registrado en las órdenes que se van a entregar.
            TransportistaNombre = BuscarOrdenPreparacion(OrdenesAEntregar[0]).TransportistaNombre;
            return null;
        }

        /// <summary>
        /// Paso 8. Registra el remito en estado Emitido y su detalle.
        /// </summary>
        public RemitoEntrega GenerarRemito()
        {
            if (!PuedeGenerarRemito)
                throw new InvalidOperationException("No hay órdenes para generar el remito, o el remito ya fue generado.");

            int numero = RemitosEntrega.Count == 0 ? 1 : RemitosEntrega.Max(r => r.Numero) + 1;

            Remito = new RemitoEntrega
            {
                Numero = numero,
                FechaHora = DateTime.Now,
                CodigoDeposito = CodigoDeposito,
                TransportistaNombre = TransportistaNombre!,
                TransportistaDni = TransportistaDni,
                CodigoUsuario = CodigoUsuario,
                Estado = EstadoRemito.Emitido,
            };
            RemitosEntrega.Add(Remito);

            foreach (var oe in OrdenesAEntregar)
            {
                DetallesRemitoEntrega.Add(new DetalleRemitoEntrega
                {
                    NumeroRemito = numero,
                    NumeroOrdenPreparacion = oe.NumeroOrdenPreparacion,
                    CantidadPaquetes = oe.CantidadPaquetes,
                });
            }

            return Remito;
        }

        /// <summary>
        /// Pasos 14 a 16. Registra el cumplido, despacha las órdenes y el egreso de mercadería.
        /// </summary>
        public void ConfirmarDespacho()
        {
            if (!PuedeConfirmarDespacho || Remito!.Estado != EstadoRemito.Emitido)
                throw new InvalidOperationException("El remito tiene que estar generado y firmado para confirmar el despacho.");

            var ahora = DateTime.Now;

            Remito.Estado = EstadoRemito.Firmado;
            Remito.FechaHoraCumplido = ahora;
            Remito.CodigoUsuarioCumplido = CodigoUsuario;

            foreach (var oe in OrdenesAEntregar)
            {
                var op = BuscarOrdenPreparacion(oe);
                op.Estado = EstadoOrdenPreparacion.Cumplida;
                op.NumeroRemito = Remito.Numero;

                oe.Estado = EstadoOrdenEntrega.Despachada;
                oe.FechaHoraDespacho = ahora;

                MovimientosMercaderia.Add(new MovimientoMercaderia
                {
                    CodigoCliente = op.CodigoCliente,
                    NumeroOrdenPreparacion = op.Numero,
                    NumeroRemito = Remito.Numero,
                    Fecha = ahora.Date,
                });
            }
        }

        /// <summary>
        /// Excepción "Cancelar": si el remito ya se había generado y no se confirmó, queda Anulado.
        /// Las órdenes siguen Pendiente.
        /// </summary>
        public void Cancelar()
        {
            if (Remito != null && Remito.Estado == EstadoRemito.Emitido)
                Remito.Estado = EstadoRemito.Anulado;
            Reiniciar();
        }

        public void Reiniciar()
        {
            TransportistaNombre = null;
            TransportistaDni = 0;
            OrdenesAEntregar.Clear();
            Remito = null;
            RemitoFirmado = false;
        }

        private OrdenPreparacion BuscarOrdenPreparacion(OrdenEntrega oe) =>
            OrdenesPreparacion.First(op => op.Numero == oe.NumeroOrdenPreparacion && op.CodigoDeposito == oe.CodigoDeposito);
    }
}
