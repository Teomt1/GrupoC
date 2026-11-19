#nullable enable
namespace GrupoC.RecepcionMercaderia;

internal class ItemRecepcion
{
    public Producto Producto { get; }

    // true: las cantidades están expresadas en pallets cerrados; false: en ítems sueltos.
    public bool EsPallet { get; }
    public int CantidadContada { get; }
    public int CantidadRemito { get; }

    // null si no se contaron unidades.
    public Posicion? Posicion { get; }

    public ItemRecepcion(Producto producto, bool esPallet, int cantidadContada, int cantidadRemito, Posicion? posicion)
    {
        Producto = producto;
        EsPallet = esPallet;
        CantidadContada = cantidadContada;
        CantidadRemito = cantidadRemito;
        Posicion = posicion;
    }

    public string Codigo => EsPallet ? Producto.CodigoPallet ?? Producto.Sku : Producto.Sku;

    public string Descripcion => Producto.Descripcion;

    // Columna "Tipo": las cantidades de un ítem en pallets están en pallets; las de uno suelto, en unidades.
    public string Tipo => EsPallet ? $"Pallets x {Producto.UnidadesPorPallet} u." : "Unidades sueltas";

    // Negativa: faltante respecto del remito. Positiva: excedente.
    public int Diferencia => CantidadContada - CantidadRemito;
}
