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
    /// El transportista se identifica solo por su DNI; el nombre lo toma el sistema de la tabla Transportistas.
    /// </summary>
    public class EntregaATransportistaModel
    {
        public const string MensajeDniInvalido = "Ingrese un DNI válido de 8 dígitos, sin puntos";

        public Transportista? Transportista { get; private set; }
        public List<OrdenADespachar> Ordenes { get; } = new();
        public RemitoEntrega? Remito { get; private set; }

        private Usuario Usuario => Datos.UsuarioActual;

        /// <summary>
        /// Pasos 4 a 7. Devuelve el mensaje de error, o null si encontró órdenes para el transportista.
        /// Si el transportista está habilitado pero no tiene órdenes, queda cargado en <see cref="Transportista"/>.
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

            var transportista = Datos.Transportistas.FirstOrDefault(t => t.Dni == dni);
            if (transportista == null)
                return $"No existe un transportista registrado con el DNI {dni}";

            if (transportista.Estado != EstadoTransportista.Habilitado)
                return $"El transportista con DNI {dni} no está habilitado para retirar mercadería";

            Transportista = transportista;

            var ordenesPreparacion = Datos.OrdenesPreparacion
                .Where(op => op.CodigoDeposito == Usuario.CodigoDeposito && op.TransportistaDni == dni)
                .OrderBy(op => op.Numero);

            foreach (var op in ordenesPreparacion)
            {
                var oe = Datos.OrdenesEntrega.FirstOrDefault(oe => oe.NumeroOrdenPreparacion == op.Numero
                                                                && oe.CodigoDeposito == Usuario.CodigoDeposito
                                                                && oe.Estado == EstadoOrdenEntrega.Pendiente);
                if (oe != null)
                    Ordenes.Add(new OrdenADespachar { OrdenEntrega = oe, OrdenPreparacion = op });
            }

            if (Ordenes.Count == 0)
                return $"El transportista {transportista.ApellidoNombre} no tiene órdenes listas para retirar en este depósito";

            return null;
        }

        /// <summary>
        /// Paso 11. Registra el remito en estado Emitido y su detalle.
        /// </summary>
        public RemitoEntrega GenerarRemito()
        {
            if (Transportista == null || Ordenes.Count == 0)
                throw new InvalidOperationException("No hay órdenes para generar el remito.");
            if (Remito != null)
                throw new InvalidOperationException("El remito ya fue generado.");

            int numero = Datos.RemitosEntrega.Count == 0 ? 1 : Datos.RemitosEntrega.Max(r => r.Numero) + 1;

            Remito = new RemitoEntrega
            {
                Numero = numero,
                FechaHora = DateTime.Now,
                CodigoDeposito = Usuario.CodigoDeposito,
                TransportistaNombre = Transportista.ApellidoNombre,
                TransportistaDni = Transportista.Dni,
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
        /// Pasos 18 a 20. Registra el cumplido, despacha las órdenes y el egreso de mercadería.
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
            Transportista = null;
            Ordenes.Clear();
            Remito = null;
        }
    }
}
