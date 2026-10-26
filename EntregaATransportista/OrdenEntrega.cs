namespace GrupoC.EntregaATransportista
{
    public class OrdenEntrega
    {
        public int NumeroOrdenPreparacion { get; set; }
        public string CodigoDeposito { get; set; } = "";
        public int CantidadPaquetes { get; set; }
        public EstadoOrdenEntrega Estado { get; set; }
        public DateTime? FechaHoraDespacho { get; set; }
    }
}
