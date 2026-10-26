namespace GrupoC.EntregaATransportista
{
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
}
