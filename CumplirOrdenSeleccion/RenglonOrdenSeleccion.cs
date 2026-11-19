namespace GrupoC.CumplirOrdenSeleccion;

internal class RenglonOrdenSeleccion
{
    public required string Cliente { get; set; }
    public required string Posicion { get; set; }
    public required string Sku { get; set; }
    public required string Descripcion { get; set; }

    // true: se bajan pallets cerrados; false: unidades sueltas (abriendo pallets si hace falta).
    public bool EsPallet { get; set; }
    public int UnidadesPorPallet { get; set; }

    // Siempre en unidades.
    public int Cantidad { get; set; }

    // Columna "Tipo".
    public string Tipo => EsPallet ? $"Pallets x {UnidadesPorPallet} u." : "Unidades sueltas";

    // Columna "Cantidad": pallets a bajar o unidades sueltas.
    public int CantidadABajar => EsPallet && UnidadesPorPallet > 0 ? Cantidad / UnidadesPorPallet : Cantidad;
}
