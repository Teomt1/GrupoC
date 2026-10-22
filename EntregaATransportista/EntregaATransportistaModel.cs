using System.Text.RegularExpressions;

namespace GrupoC.EntregaATransportista
{
    public class OrdenADespachar
    {
        public OrdenEntrega OrdenEntrega { get; set; } = null!;
        public OrdenPreparacion OrdenPreparacion { get; set; } = null!;
    }

    /// <summary>
    /// Lógica del caso de uso "Entregar mercadería al transportista".
    /// El transportista se identifica solo por su DNI; el nombre lo toma el sistema de la orden de preparación.
    /// </summary>
    public class EntregaATransportistaModel
    {
        public const string MensajeDniInvalido = "Ingrese un DNI válido";
        public const string MensajeNoAutorizado = "El transportista no está autorizado a retirar mercadería en este depósito";
        public const string MensajeSinOrdenes = "El transportista no tiene órdenes listas para retirar";

        /// <summary>Nombre del transportista autorizado, o null si no se encontró ninguno.</summary>
        public string? TransportistaNombre { get; private set; }
        public int TransportistaDni { get; private set; }
        public List<OrdenADespachar> Ordenes { get; } = new();
        public RemitoEntrega? Remito { get; private set; }

        private Usuario Usuario => Datos.UsuarioActual;

        /// <summary>
        /// Pasos 3 a 5. Devuelve el mensaje de error, o null si encontró órdenes para el transportista.
        /// Si está autorizado pero no tiene órdenes pendientes, el nombre queda cargado igual.
        /// </summary>
        public string? Buscar(string dniTexto)
        {
            Reiniciar();
            dniTexto = dniTexto.Trim();

            if (!Regex.IsMatch(dniTexto, @"^\d{8}$"))
                return MensajeDniInvalido;

            int dni = int.Parse(dniTexto);
            if (dni < 10000000)
                return MensajeDniInvalido;

            // Autorizado: el DNI figura en alguna orden de preparación del depósito.
            var ordenesPreparacion = Datos.OrdenesPreparacion
                .Where(op => op.CodigoDeposito == Usuario.CodigoDeposito && op.TransportistaDni == dni)
                .OrderBy(op => op.Numero)
                .ToList();

            if (ordenesPreparacion.Count == 0)
                return MensajeNoAutorizado;

            TransportistaNombre = ordenesPreparacion[0].TransportistaNombre;
            TransportistaDni = dni;

            foreach (var op in ordenesPreparacion)
            {
                var oe = Datos.OrdenesEntrega.FirstOrDefault(oe => oe.NumeroOrdenPreparacion == op.Numero
                                                                && oe.CodigoDeposito == Usuario.CodigoDeposito
                                                                && oe.Estado == EstadoOrdenEntrega.Pendiente);
                if (oe != null)
                    Ordenes.Add(new OrdenADespachar { OrdenEntrega = oe, OrdenPreparacion = op });
            }

            if (Ordenes.Count == 0)
                return MensajeSinOrdenes;

            // Se muestra el nombre registrado en las órdenes que se van a entregar.
            TransportistaNombre = Ordenes[0].OrdenPreparacion.TransportistaNombre;
            return null;
        }

        /// <summary>
        /// Paso 8. Registra el remito en estado Emitido y su detalle.
        /// </summary>
        public RemitoEntrega GenerarRemito()
        {
            if (TransportistaNombre == null || Ordenes.Count == 0)
                throw new InvalidOperationException("No hay órdenes para generar el remito.");
            if (Remito != null)
                throw new InvalidOperationException("El remito ya fue generado.");

            int numero = Datos.RemitosEntrega.Count == 0 ? 1 : Datos.RemitosEntrega.Max(r => r.Numero) + 1;

            Remito = new RemitoEntrega
            {
                Numero = numero,
                FechaHora = DateTime.Now,
                CodigoDeposito = Usuario.CodigoDeposito,
                TransportistaNombre = TransportistaNombre,
                TransportistaDni = TransportistaDni,
                CodigoUsuario = Usuario.Codigo,
                Estado = EstadoRemito.Emitido,
            };
            Datos.RemitosEntrega.Add(Remito);

            foreach (var orden in Ordenes)
            {
                Datos.DetallesRemitoEntrega.Add(new DetalleRemitoEntrega
                {
                    NumeroRemito = numero,
                    NumeroOrdenPreparacion = orden.OrdenPreparacion.Numero,
                    CantidadPaquetes = orden.OrdenEntrega.CantidadPaquetes,
                });
            }

            return Remito;
        }

        /// <summary>
        /// Pasos 14 a 16. Registra el cumplido, despacha las órdenes y el egreso de mercadería.
        /// </summary>
        public void ConfirmarDespacho()
        {
            if (Remito == null || Remito.Estado != EstadoRemito.Emitido)
                throw new InvalidOperationException("No hay un remito emitido para confirmar.");

            var ahora = DateTime.Now;

            Remito.Estado = EstadoRemito.Firmado;
            Remito.FechaHoraCumplido = ahora;
            Remito.CodigoUsuarioCumplido = Usuario.Codigo;

            foreach (var orden in Ordenes)
            {
                orden.OrdenPreparacion.Estado = EstadoOrdenPreparacion.Cumplida;
                orden.OrdenPreparacion.NumeroRemito = Remito.Numero;

                orden.OrdenEntrega.Estado = EstadoOrdenEntrega.Despachada;
                orden.OrdenEntrega.FechaHoraDespacho = ahora;

                Datos.MovimientosMercaderia.Add(new MovimientoMercaderia
                {
                    CodigoCliente = orden.OrdenPreparacion.CodigoCliente,
                    NumeroOrdenPreparacion = orden.OrdenPreparacion.Numero,
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
            Ordenes.Clear();
            Remito = null;
        }
    }
}
