using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoC.RegistrarOrdenDePreparacion
{
    public class OrdenDePreparacion
    {
        public Cliente Cliente { get; }
        public Deposito? Deposito { get; set; }
        public ModalidadEmpaquetado Modalidad { get; set; }
        public List<ItemOrden> Items { get; }
        public DateTime FechaRetiro { get; set; }
        public string NombreDestinatario { get; set; }
        public string DireccionDestino { get; set; }
        public Transportista? Transportista { get; set; }
        public string Estado { get; set; }

        public OrdenDePreparacion(Cliente cliente)
        {
            Cliente = cliente;
            Items = new List<ItemOrden>();
            NombreDestinatario = string.Empty;
            DireccionDestino = string.Empty;
            Estado = "Pendiente";
        }

        public int ObtenerCantidad(Producto producto)
        {
            ItemOrden? item = Items.Find(i => i.Producto == producto);
            return item == null ? 0 : item.Cantidad;
        }

        // Si el producto ya está en la orden, se acumula la cantidad
        public void AgregarProducto(Producto producto, int cantidad)
        {
            ItemOrden? existente = Items.Find(i => i.Producto == producto);
            if (existente == null)
            {
                Items.Add(new ItemOrden(producto, cantidad));
            }
            else
            {
                existente.SumarCantidad(cantidad);
            }
        }
        public void VaciarProductos()
        {
            Items.Clear();
        }
    }
}

