namespace GrupoC.EntregaATransportista
{
    public enum EstadoOrdenPreparacion { Pendiente, Preparada, Cumplida }

    public enum EstadoOrdenEntrega { Pendiente, Despachada }

    public enum EstadoRemito { Emitido, Firmado, Anulado }

    public class Usuario
    {
        public string Codigo { get; set; } = "";
        public string CodigoDeposito { get; set; } = "";
    }

    public class OrdenPreparacion
    {
        public int Numero { get; set; }
        public string CodigoCliente { get; set; } = "";
        public string CodigoDeposito { get; set; } = "";
        public string TransportistaNombre { get; set; } = "";
        public int TransportistaDni { get; set; }
        public EstadoOrdenPreparacion Estado { get; set; }
        public int? NumeroRemito { get; set; }
    }

    public class OrdenEntrega
    {
        public int Numero { get; set; }
        public int NumeroOrdenPreparacion { get; set; }
        public string CodigoDeposito { get; set; } = "";
        public int CantidadPaquetes { get; set; }
        public EstadoOrdenEntrega Estado { get; set; }
        public DateTime? FechaHoraDespacho { get; set; }
    }

    public class RemitoEntrega
    {
        public int Numero { get; set; }
        public DateTime FechaHora { get; set; }
        public string CodigoDeposito { get; set; } = "";
        public string TransportistaNombre { get; set; } = "";
        public int TransportistaDni { get; set; }
        public string CodigoUsuario { get; set; } = "";
        public EstadoRemito Estado { get; set; }
        public DateTime? FechaHoraCumplido { get; set; }
        public string? CodigoUsuarioCumplido { get; set; }
    }

    public class DetalleRemitoEntrega
    {
        public int NumeroRemito { get; set; }
        public int NumeroOrdenPreparacion { get; set; }
        public int CantidadPaquetes { get; set; }
    }

    public class MovimientoMercaderia
    {
        public string Tipo { get; set; } = "Egreso";
        public string CodigoCliente { get; set; } = "";
        public int NumeroOrdenPreparacion { get; set; }
        public int NumeroRemito { get; set; }
        public DateTime Fecha { get; set; }
    }

    /// <summary>
    /// Tablas en memoria con los datos de prueba de la planilla
    /// "Prueba - Entregar mercadería al transportista".
    /// Son estáticas para que los cambios se mantengan mientras la aplicación está abierta.
    /// </summary>
    public static class Datos
    {
        public static Usuario UsuarioActual { get; } = new Usuario { Codigo = "OPD01", CodigoDeposito = "DEP01" };

        public static List<OrdenPreparacion> OrdenesPreparacion { get; } = new()
        {
            // Martínez, Luis: autorizado con dos órdenes listas para retirar en DEP01.
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

        public static List<OrdenEntrega> OrdenesEntrega { get; } = new()
        {
            new OrdenEntrega { Numero = 3001, NumeroOrdenPreparacion = 2001, CodigoDeposito = "DEP01", CantidadPaquetes = 3, Estado = EstadoOrdenEntrega.Pendiente },
            new OrdenEntrega { Numero = 3002, NumeroOrdenPreparacion = 2002, CodigoDeposito = "DEP01", CantidadPaquetes = 1, Estado = EstadoOrdenEntrega.Pendiente },
            new OrdenEntrega { Numero = 3003, NumeroOrdenPreparacion = 2003, CodigoDeposito = "DEP01", CantidadPaquetes = 4, Estado = EstadoOrdenEntrega.Despachada, FechaHoraDespacho = new DateTime(2026, 10, 1, 10, 30, 0) },
            new OrdenEntrega { Numero = 3005, NumeroOrdenPreparacion = 2005, CodigoDeposito = "DEP01", CantidadPaquetes = 2, Estado = EstadoOrdenEntrega.Pendiente },
            new OrdenEntrega { Numero = 3006, NumeroOrdenPreparacion = 2006, CodigoDeposito = "DEP02", CantidadPaquetes = 4, Estado = EstadoOrdenEntrega.Pendiente },
        };

        public static List<RemitoEntrega> RemitosEntrega { get; } = new()
        {
            new RemitoEntrega { Numero = 5000, FechaHora = new DateTime(2026, 10, 1, 10, 15, 0), CodigoDeposito = "DEP01", TransportistaNombre = "Sosa, Ana", TransportistaDni = 28555111, CodigoUsuario = "OPD01", Estado = EstadoRemito.Firmado, FechaHoraCumplido = new DateTime(2026, 10, 1, 10, 30, 0), CodigoUsuarioCumplido = "OPD01" },
        };

        public static List<DetalleRemitoEntrega> DetallesRemitoEntrega { get; } = new()
        {
            new DetalleRemitoEntrega { NumeroRemito = 5000, NumeroOrdenPreparacion = 2003, CantidadPaquetes = 4 },
        };

        public static List<MovimientoMercaderia> MovimientosMercaderia { get; } = new()
        {
            new MovimientoMercaderia { CodigoCliente = "CLI002", NumeroOrdenPreparacion = 2003, NumeroRemito = 5000, Fecha = new DateTime(2026, 10, 1) },
        };
    }
}
