using System;
using System.Collections.Generic;
using System.Linq;
using Alm = GrupoC.Almacenes;

namespace GrupoC.IngresoStock
{
    public class IngresoStockModelo
    {
        // Depósito que se usa si no se eligió ninguno en el menú principal.
        private const string DepositoPorDefecto = "D01";

        #region Datos que ingresa el operario

        public string NumeroOrden { get; set; } = string.Empty;
        public ItemOrdenRecepcion? SkuSeleccionado { get; set; }
        public string Cantidad { get; set; } = string.Empty;
        public string Pasillo { get; set; } = string.Empty;
        public string Fila { get; set; } = string.Empty;
        public string Nivel { get; set; } = string.Empty;
        public AsignacionUbicacion? UbicacionSeleccionada { get; set; }

        #endregion

        #region Datos que muestra la pantalla

        public string ClienteOrigen
        {
            get { return ordenActual == null ? string.Empty : ordenActual.ClienteOrigen; }
        }

        public List<ItemOrdenRecepcion> Items
        {
            get { return ordenActual == null ? new List<ItemOrdenRecepcion>() : ordenActual.Items; }
        }

        public List<AsignacionUbicacion> Ubicaciones { get; } = new List<AsignacionUbicacion>();

        // Si se cierra la pantalla con ubicaciones sin confirmar, se pierden
        public bool HayUbicacionesSinConfirmar
        {
            get { return Ubicaciones.Count > 0; }
        }

        // Se cargan con las posiciones del depósito del usuario (paso 1 del caso de uso).
        public List<string> Pasillos { get; } = new List<string>();
        public List<string> Filas { get; } = new List<string>();
        public List<string> Niveles { get; } = new List<string>();

        #endregion

        #region Datos de los almacenes

        // Depósito, legajo y usuario del usuario logueado.
        private readonly string codigoDeposito;
        private readonly int? legajoUsuario;
        private readonly string usuario = string.Empty;

        // Al.2 Mercaderías: el stock que ya está guardado en las posiciones del depósito.
        private readonly List<Mercaderia> mercaderias = new List<Mercaderia>();

        // Registros de cada ingreso confirmado (usuario, fecha y hora, orden). Cada uno se registra
        // también como movimiento de ingreso en los movimientos de mercadería.
        private readonly List<RegistroAuditoria> historialAuditoria = new List<RegistroAuditoria>();

        // Relación entre las clases de la pantalla y los datos de los almacenes (no salen del modelo).
        private readonly Dictionary<string, string> pasilloAlmacen = new Dictionary<string, string>();
        private readonly Dictionary<string, string> filaAlmacen = new Dictionary<string, string>();
        private readonly Dictionary<string, string> nivelAlmacen = new Dictionary<string, string>();
        private readonly Dictionary<ItemOrdenRecepcion, bool> itemEsPallet = new Dictionary<ItemOrdenRecepcion, bool>();
        private Alm.OrdenRecepcionEntidad? ordenAlmacen;

        #endregion

        private OrdenRecepcion? ordenActual;

        public IngresoStockModelo()
        {
            // Usuario logueado: el empleado elegido en el menú principal (si no hay, uno de los habilitados).
            List<Alm.EmpleadoEntidad> empleados = Alm.EmpleadoAlmacen.Empleado;
            Alm.EmpleadoEntidad? empleado = int.TryParse(Alm.EmpleadoAlmacen.EmpleadoActual, out int legajoElegido)
                ? empleados.Find(e => e.Legajo == legajoElegido)
                : null;
            if (empleado == null && empleados.Count > 0)
            {
                empleado = empleados[Random.Shared.Next(empleados.Count)];
            }
            if (empleado != null)
            {
                legajoUsuario = empleado.Legajo;
                usuario = empleado.Usuario ?? string.Empty;
            }

            codigoDeposito = string.IsNullOrEmpty(Alm.DepositoAlmacen.DepositoActual)
                ? DepositoPorDefecto
                : Alm.DepositoAlmacen.DepositoActual;

            // Paso 1: desplegables de Pasillo, Fila y Nivel con las posiciones del depósito,
            // y el stock que ya está guardado en ellas.
            Alm.DepositoEntidad? deposito = BuscarDeposito();
            if (deposito == null)
            {
                return;
            }

            foreach (Alm.Posicion p in deposito.Posiciones)
            {
                AgregarCoordenada(Pasillos, pasilloAlmacen, "Pasillo", p.Pasillo);
                AgregarCoordenada(Filas, filaAlmacen, "Fila", p.Fila);
                AgregarCoordenada(Niveles, nivelAlmacen, "Nivel", p.Nivel);

                if (p.Sku != null)
                {
                    Alm.ProductoEntidad? producto = Alm.ProductoAlmacen.Producto.Find(x => x.Sku == p.Sku);
                    int unidadesPorPallet = producto == null ? 0 : producto.UnidadesPorPallet;
                    mercaderias.Add(new Mercaderia(p.Sku, "Pasillo " + p.Pasillo, "Fila " + p.Fila, "Nivel " + p.Nivel,
                        p.CantidadPalets * unidadesPorPallet + p.CantidadSueltos));
                }
            }

            Pasillos.Sort();
            Filas.Sort();
            Niveles.Sort();
        }

        #region Pasos 3 a 6: buscar la orden

        // Devuelve el error a mostrar, o null si se encontró la orden.
        public string? BuscarOrden()
        {
            // Paso 4
            if (!int.TryParse(NumeroOrden.Trim(), out int numero) || numero <= 0)
            {
                NumeroOrden = string.Empty;
                return "El número de orden debe ser un valor numérico";
            }

            // Paso 5: la orden tiene que ser del depósito del usuario y estar pendiente de ubicación.
            Alm.OrdenRecepcionEntidad? entidad = Alm.OrdenRecepcionAlmacen.OrdenRecepcion.Find(o =>
                o.NumeroOrdRecep == numero
                && o.CodigoDeposito == codigoDeposito
                && o.Estado == Alm.EstadoOrdenRecepcionEnum.PendienteDeAlmacenamiento);

            if (entidad == null)
            {
                Restablecer();
                return "No se encontró ninguna orden pendiente de ubicación con ese número";
            }

            // Paso 6
            Restablecer();
            ordenAlmacen = entidad;
            ordenActual = TransformarOrden(entidad);
            NumeroOrden = numero.ToString();
            return null;
        }

        private OrdenRecepcion TransformarOrden(Alm.OrdenRecepcionEntidad entidad)
        {
            Alm.ClienteEntidad? cliente = Alm.ClienteAlmacen.Cliente.Find(c => c.Codigo == entidad.CodigoCliente);
            string clienteOrigen = cliente == null ? entidad.CodigoCliente : cliente.RazonSocial;

            itemEsPallet.Clear();
            var items = new List<ItemOrdenRecepcion>();
            foreach (Alm.ItemRecepcion i in entidad.Items)
            {
                // Lo que se ubica es lo que se contó en la recepción. Si vino en pallets cerrados,
                // la cantidad está en pallets y se aclara en la descripción.
                Alm.ProductoEntidad? producto = Alm.ProductoAlmacen.Producto.Find(p => p.Sku == i.SKU_Producto);
                string descripcion = producto == null ? string.Empty : producto.Descripcion;
                if (i.EsPallet && producto != null)
                {
                    descripcion += $" (pallet x {producto.UnidadesPorPallet} u.)";
                }

                var item = new ItemOrdenRecepcion(i.SKU_Producto, descripcion, i.CantidadContada);
                items.Add(item);
                itemEsPallet[item] = i.EsPallet;
            }

            return new OrdenRecepcion(entidad.NumeroOrdRecep, clienteOrigen, EstadoOrdenRecepcion.PendienteDeUbicacion, items);
        }

        #endregion

        #region Pasos 9 a 11: agregar y quitar ubicaciones

        // Devuelve el error a mostrar, o null si se agregó la ubicación.
        public string? AgregarUbicacion()
        {
            if (SkuSeleccionado == null || !Items.Contains(SkuSeleccionado))
            {
                return "Debe seleccionar un SKU de la orden de recepción";
            }

            // Paso 8
            if (!int.TryParse(Cantidad.Trim(), out int cantidad))
            {
                return "La cantidad debe ser un valor numérico";
            }

            if (cantidad <= 0)
            {
                return "La cantidad debe ser mayor a 0";
            }

            if (!Pasillos.Contains(Pasillo) || !Filas.Contains(Fila) || !Niveles.Contains(Nivel))
            {
                return "Debe seleccionar Pasillo, Fila y Nivel";
            }

            // Paso 10
            if (cantidad > SkuSeleccionado.CantidadAUbicar)
            {
                Cantidad = string.Empty;
                return "La cantidad a ubicar no puede ser mayor a la cantidad pendiente recepcionada";
            }

            // Pasillo, Fila y Nivel se eligen por separado: la combinación tiene que ser una posición del depósito.
            if (BuscarPosicion(Pasillo, Fila, Nivel) == null)
            {
                return "La ubicación seleccionada no existe en el depósito";
            }

            // Una posición no puede tener dos SKUs distintos, ni entre lo agregado ni con el stock existente
            bool ocupadaPorOtroSku =
                Ubicaciones.Any(u => u.Item.Sku != SkuSeleccionado.Sku && u.Pasillo == Pasillo && u.Fila == Fila && u.Nivel == Nivel)
                || mercaderias.Any(m => m.Sku != SkuSeleccionado.Sku && m.Pasillo == Pasillo && m.Fila == Fila && m.Nivel == Nivel);

            if (ocupadaPorOtroSku)
            {
                return "La ubicación seleccionada ya está ocupada por otro SKU";
            }

            // Cada coordenada se ubica una sola vez: al fraccionar, cada parte va a una coordenada distinta
            bool yaUbicadoEnLaPosicion = Ubicaciones.Any(u => u.Pasillo == Pasillo && u.Fila == Fila && u.Nivel == Nivel);

            if (yaUbicadoEnLaPosicion)
            {
                return "El SKU ya fue ubicado en esas coordenadas";
            }

            Ubicaciones.Add(new AsignacionUbicacion(SkuSeleccionado, cantidad, Pasillo, Fila, Nivel));
            SkuSeleccionado.CantidadAUbicar -= cantidad;
            LimpiarDatosUbicacion();
            return null;
        }

        // Devuelve el error a mostrar, o null si se quitó la ubicación.
        public string? QuitarUbicacion()
        {
            if (UbicacionSeleccionada == null)
            {
                return "Debe seleccionar una ubicación de la lista para quitar";
            }

            UbicacionSeleccionada.Item.CantidadAUbicar += UbicacionSeleccionada.Cantidad;
            Ubicaciones.Remove(UbicacionSeleccionada);
            UbicacionSeleccionada = null;
            return null;
        }

        #endregion

        #region Pasos 12 a 16: confirmar el ingreso

        // Devuelve el error a mostrar, o null si el ingreso se puede confirmar.
        public string? ValidarIngreso()
        {
            if (ordenActual == null)
            {
                return "Debe buscar una orden de recepción antes de confirmar el ingreso";
            }

            if (ordenActual.Items.Any(i => i.CantidadAUbicar > 0))
            {
                return "Debe ubicar toda la mercadería recepcionada antes de confirmar el ingreso";
            }

            return null;
        }

        public string ConfirmarIngreso()
        {
            // Paso 13: el stock queda guardado en las posiciones del depósito.
            // Lo recibido en pallets cerrados suma pallets; lo recibido suelto suma unidades.
            foreach (AsignacionUbicacion ubicacion in Ubicaciones)
            {
                Alm.Posicion posicion = BuscarPosicion(ubicacion.Pasillo, ubicacion.Fila, ubicacion.Nivel)!;
                posicion.Sku = ubicacion.Item.Sku;
                if (itemEsPallet[ubicacion.Item])
                {
                    posicion.CantidadPalets += ubicacion.Cantidad;
                }
                else
                {
                    posicion.CantidadSueltos += ubicacion.Cantidad;
                }

                Mercaderia? mercaderia = mercaderias.FirstOrDefault(m =>
                    m.Sku == ubicacion.Item.Sku && m.Pasillo == ubicacion.Pasillo && m.Fila == ubicacion.Fila && m.Nivel == ubicacion.Nivel);

                if (mercaderia == null)
                {
                    mercaderia = new Mercaderia(ubicacion.Item.Sku, ubicacion.Pasillo, ubicacion.Fila, ubicacion.Nivel, 0);
                    mercaderias.Add(mercaderia);
                }

                Alm.ProductoEntidad? producto = Alm.ProductoAlmacen.Producto.Find(p => p.Sku == ubicacion.Item.Sku);
                int unidadesPorPallet = producto == null ? 0 : producto.UnidadesPorPallet;
                mercaderia.Cantidad += itemEsPallet[ubicacion.Item] ? ubicacion.Cantidad * unidadesPorPallet : ubicacion.Cantidad;
            }

            // Paso 14
            ordenActual!.Estado = EstadoOrdenRecepcion.Cumplida;
            ordenAlmacen!.Estado = Alm.EstadoOrdenRecepcionEnum.Cumplida;

            // Paso 15: registro con usuario, fecha y hora, que queda como movimiento de ingreso.
            var registro = new RegistroAuditoria(usuario, DateTime.Now, ordenActual.Numero);
            historialAuditoria.Add(registro);

            Alm.MovimientoMercaderiaAlmacen.MovimientoMercaderia.Add(new Alm.MovimientoMercaderiaEntidad
            {
                Tipo = Alm.TipoMovimientoEnum.Ingreso,
                FechaHora = registro.FechaHora,
                CodigoCliente = ordenAlmacen.CodigoCliente,
                NumeroOrdenRecepcion = registro.NumeroOrden,
                NumeroOrdenPreparacion = null,
                NumeroRemitoEntrega = null,
                LegajoUsuario = legajoUsuario
            });

            // Paso 16
            Restablecer();
            return "Mercadería ingresada a stock con éxito";
        }

        #endregion

        #region Excepción paso 12: cancelar

        public void Cancelar()
        {
            Restablecer();
        }

        #endregion

        private Alm.DepositoEntidad? BuscarDeposito()
        {
            return Alm.DepositoAlmacen.Deposito.Find(d => d.Codigo == codigoDeposito);
        }

        // Busca la posición del depósito que corresponde a lo elegido en los desplegables.
        private Alm.Posicion? BuscarPosicion(string pasillo, string fila, string nivel)
        {
            Alm.DepositoEntidad? deposito = BuscarDeposito();
            if (deposito == null
                || !pasilloAlmacen.ContainsKey(pasillo) || !filaAlmacen.ContainsKey(fila) || !nivelAlmacen.ContainsKey(nivel))
            {
                return null;
            }

            return deposito.Posiciones.Find(p =>
                p.Pasillo == pasilloAlmacen[pasillo] && p.Fila == filaAlmacen[fila] && p.Nivel == nivelAlmacen[nivel]);
        }

        // Agrega una coordenada al desplegable con el mismo formato de siempre ("Pasillo A", "Fila 1", "Nivel 1").
        private void AgregarCoordenada(List<string> lista, Dictionary<string, string> relacion, string nombre, string valor)
        {
            string texto = $"{nombre} {valor}";
            if (!relacion.ContainsKey(texto))
            {
                relacion[texto] = valor;
                lista.Add(texto);
            }
        }

        private void Restablecer()
        {
            ordenActual = null;
            ordenAlmacen = null;
            NumeroOrden = string.Empty;
            SkuSeleccionado = null;
            UbicacionSeleccionada = null;
            Ubicaciones.Clear();
            LimpiarDatosUbicacion();
        }

        private void LimpiarDatosUbicacion()
        {
            Cantidad = string.Empty;
            Pasillo = string.Empty;
            Fila = string.Empty;
            Nivel = string.Empty;
        }
    }
}
