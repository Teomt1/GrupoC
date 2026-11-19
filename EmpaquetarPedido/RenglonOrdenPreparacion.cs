namespace GrupoC.EmpaquetarPedido;

internal class RenglonOrdenPreparacion
{
    public string Sku { get; set; } = "";
    // En pallets si el pedido es a palé cerrado; en unidades si es fulfillment.
    public int Cantidad { get; set; }
}
