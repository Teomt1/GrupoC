namespace GrupoC.EntregaATransportista
{
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
}
