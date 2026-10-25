using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GrupoC.IngresoStock
{
    public class IngresoStockModelo
    {
        private readonly string TituloMensajes = "Pampazon S.A. - Ingreso a Stock";

        // Controles de la pantalla (los mismos nombres del Designer)
        private readonly TextBox txtNroOrden;
        private readonly Button btnBuscar;
        private readonly TextBox txtClienteOrigen;
        private readonly ListView listViewSkus;
        private readonly ComboBox cmbSkuSeleccionado;
        private readonly TextBox txtCantidadUbicar;
        private readonly ComboBox cmbPasilloX;
        private readonly ComboBox cmbFilaY;
        private readonly ComboBox cmbNivelZ;
        private readonly Button btnAgregarUbicacion;
        private readonly ListView listView1;
        private readonly Button button1;
        private readonly Button btnConfirmar;
        private readonly Button btnCancelar;

        // Datos de la pantalla
        private readonly List<OrdenRecepcion> ordenesRecepcion;
        private readonly List<Mercaderia> mercaderias;
        private readonly List<AsignacionUbicacion> asignaciones = new List<AsignacionUbicacion>();
        private OrdenRecepcion? ordenActual;

        public IngresoStockModelo(
            TextBox txtNroOrden,
            Button btnBuscar,
            TextBox txtClienteOrigen,
            ListView listViewSkus,
            ComboBox cmbSkuSeleccionado,
            TextBox txtCantidadUbicar,
            ComboBox cmbPasilloX,
            ComboBox cmbFilaY,
            ComboBox cmbNivelZ,
            Button btnAgregarUbicacion,
            ListView listView1,
            Button button1,
            Button btnConfirmar,
            Button btnCancelar)
        {
            this.txtNroOrden = txtNroOrden;
            this.btnBuscar = btnBuscar;
            this.txtClienteOrigen = txtClienteOrigen;
            this.listViewSkus = listViewSkus;
            this.cmbSkuSeleccionado = cmbSkuSeleccionado;
            this.txtCantidadUbicar = txtCantidadUbicar;
            this.cmbPasilloX = cmbPasilloX;
            this.cmbFilaY = cmbFilaY;
            this.cmbNivelZ = cmbNivelZ;
            this.btnAgregarUbicacion = btnAgregarUbicacion;
            this.listView1 = listView1;
            this.button1 = button1;
            this.btnConfirmar = btnConfirmar;
            this.btnCancelar = btnCancelar;

            ordenesRecepcion = CargarOrdenesRecepcion();
            mercaderias = CargarMercaderias();

            SuscribirEventos();
            RestablecerFormulario();
        }

        #region Datos hardcodeados (AL.15 Órdenes de Recepción)

        private List<OrdenRecepcion> CargarOrdenesRecepcion()
        {
            return new List<OrdenRecepcion>
            {
                new OrdenRecepcion(1001, "Distribuidora del Sur S.A.", EstadoOrdenRecepcion.PendienteDeUbicacion,
                    new List<ItemOrdenRecepcion>
                    {
                        new ItemOrdenRecepcion("ELE-0001", "Auriculares inalámbricos Bluetooth", 40),
                        new ItemOrdenRecepcion("ELE-0002", "Cargador USB-C 20W", 60),
                        new ItemOrdenRecepcion("HOG-0010", "Juego de sábanas 2 plazas", 25)
                    }),

                new OrdenRecepcion(1002, "Textil Pampeana S.R.L.", EstadoOrdenRecepcion.PendienteDeUbicacion,
                    new List<ItemOrdenRecepcion>
                    {
                        new ItemOrdenRecepcion("IND-0101", "Remera de algodón talle M", 80),
                        new ItemOrdenRecepcion("IND-0102", "Buzo de frisa talle L", 35)
                    }),

                new OrdenRecepcion(1003, "Librería Belgrano", EstadoOrdenRecepcion.PendienteDeUbicacion,
                    new List<ItemOrdenRecepcion>
                    {
                        new ItemOrdenRecepcion("LIB-0201", "Cuaderno A4 rayado", 150),
                        new ItemOrdenRecepcion("LIB-0202", "Resma A4 500 hojas", 50),
                        new ItemOrdenRecepcion("LIB-0203", "Caja de lapiceras azules x50", 20)
                    }),

                // Orden ya cumplida: sirve para probar la excepción del paso 5
                new OrdenRecepcion(1004, "Ferretería Los Andes", EstadoOrdenRecepcion.Cumplida,
                    new List<ItemOrdenRecepcion>
                    {
                        new ItemOrdenRecepcion("FER-0301", "Taladro percutor 650W", 10)
                    })
            };
        }

        #endregion

        #region Datos hardcodeados (AL.3 Mercaderías)

        // Stock que ya existe en el depósito. Si se ubica un SKU en una de estas
        // posiciones, se incrementa la cantidad; si no, se crea una existencia nueva.
        private List<Mercaderia> CargarMercaderias()
        {
            return new List<Mercaderia>
            {
                new Mercaderia("ELE-0001", "Pasillo 1", "Fila 1", "Nivel 1", 15),
                new Mercaderia("ELE-0002", "Pasillo 1", "Fila 2", "Nivel 1", 30),
                new Mercaderia("IND-0101", "Pasillo 2", "Fila 1", "Nivel 2", 50),
                new Mercaderia("LIB-0202", "Pasillo 3", "Fila 3", "Nivel 1", 12)
            };
        }

        #endregion

        #region Eventos

        private void SuscribirEventos()
        {
            btnBuscar.Click += BtnBuscar_Click;
            listViewSkus.SelectedIndexChanged += ListViewSkus_SelectedIndexChanged;
            btnAgregarUbicacion.Click += BtnAgregarUbicacion_Click;
            button1.Click += BtnQuitarSeleccionada_Click;
            btnConfirmar.Click += BtnConfirmar_Click;
            btnCancelar.Click += BtnCancelar_Click;
        }

        private void BtnBuscar_Click(object? sender, EventArgs e)
        {
            BuscarOrden();
        }

        private void ListViewSkus_SelectedIndexChanged(object? sender, EventArgs e)
        {
            SincronizarSkuSeleccionado();
        }

        private void BtnAgregarUbicacion_Click(object? sender, EventArgs e)
        {
            AgregarUbicacion();
        }

        private void BtnQuitarSeleccionada_Click(object? sender, EventArgs e)
        {
            QuitarSeleccionada();
        }

        private void BtnConfirmar_Click(object? sender, EventArgs e)
        {
            ConfirmarIngreso();
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            RestablecerFormulario();
        }

        #endregion

        #region Pasos 2 a 6: búsqueda de la orden

        private void BuscarOrden()
        {
            // Paso 4: el N° de orden debe ser un entero positivo
            bool esNumeroValido = int.TryParse(txtNroOrden.Text.Trim(), out int numeroOrden) && numeroOrden > 0;
            if (!esNumeroValido)
            {
                MostrarAdvertencia("El número de orden debe ser un valor numérico");
                txtNroOrden.Clear();
                txtNroOrden.Focus();
                return;
            }

            // Paso 5: la orden debe existir y estar Pendiente de Ubicación
            OrdenRecepcion? orden = ordenesRecepcion.FirstOrDefault(o =>
                o.Numero == numeroOrden && o.Estado == EstadoOrdenRecepcion.PendienteDeUbicacion);

            if (orden == null)
            {
                MostrarAdvertencia("No se encontró ninguna orden pendiente de ubicación con ese número");
                RestablecerFormulario();
                return;
            }

            // Paso 6: mostrar cliente e ítems
            ordenActual = orden;
            asignaciones.Clear();
            txtClienteOrigen.Text = orden.ClienteOrigen;
            CargarComboSkus();
            LimpiarCamposUbicacion();
            RefrescarGrillas();
        }

        private void CargarComboSkus()
        {
            cmbSkuSeleccionado.Items.Clear();
            cmbSkuSeleccionado.Text = string.Empty;

            if (ordenActual == null)
            {
                return;
            }

            foreach (ItemOrdenRecepcion item in ordenActual.Items)
            {
                cmbSkuSeleccionado.Items.Add(item);
            }

            if (cmbSkuSeleccionado.Items.Count > 0)
            {
                cmbSkuSeleccionado.SelectedIndex = 0;
            }
        }

        // Al elegir una fila en la grilla principal se selecciona ese SKU en el desplegable
        private void SincronizarSkuSeleccionado()
        {
            if (listViewSkus.SelectedItems.Count == 0)
            {
                return;
            }

            if (listViewSkus.SelectedItems[0].Tag is ItemOrdenRecepcion item)
            {
                cmbSkuSeleccionado.SelectedItem = item;
            }
        }

        #endregion

        #region Pasos 7 a 10: asignación de ubicaciones

        private void AgregarUbicacion()
        {
            if (ordenActual == null || cmbSkuSeleccionado.SelectedItem is not ItemOrdenRecepcion item)
            {
                MostrarAdvertencia("Debe buscar una orden de recepción y seleccionar un SKU");
                return;
            }

            // Paso 8: tres coordenadas seleccionadas y cantidad válida
            bool cantidadValida = int.TryParse(txtCantidadUbicar.Text.Trim(), out int cantidad) && cantidad > 0;
            bool coordenadasSeleccionadas = cmbPasilloX.SelectedIndex >= 0
                                            && cmbFilaY.SelectedIndex >= 0
                                            && cmbNivelZ.SelectedIndex >= 0;

            if (!coordenadasSeleccionadas || !cantidadValida)
            {
                MostrarAdvertencia("Debe seleccionar Pasillo, Fila, Nivel e ingresar una cantidad válida");
                return;
            }

            // Paso 10: la cantidad no puede superar el saldo pendiente del SKU
            if (cantidad > CalcularCantidadPendiente(item))
            {
                MostrarAdvertencia("La cantidad a ubicar no puede ser mayor a la cantidad pendiente recepcionada");
                txtCantidadUbicar.Clear();
                txtCantidadUbicar.Focus();
                return;
            }

            string pasillo = cmbPasilloX.Text;
            string fila = cmbFilaY.Text;
            string nivel = cmbNivelZ.Text;

            // Si ya existe la misma posición para ese SKU, se suma la cantidad
            AsignacionUbicacion? existente = asignaciones.FirstOrDefault(a => a.EstaEn(item, pasillo, fila, nivel));
            if (existente != null)
            {
                existente.SumarCantidad(cantidad);
            }
            else
            {
                asignaciones.Add(new AsignacionUbicacion(item, cantidad, pasillo, fila, nivel));
            }

            LimpiarCamposUbicacion();
            RefrescarGrillas();
        }

        private int CalcularCantidadPendiente(ItemOrdenRecepcion item)
        {
            int cantidadAsignada = asignaciones.Where(a => a.Item == item).Sum(a => a.Cantidad);
            return item.CantidadRecepcionada - cantidadAsignada;
        }

        #endregion

        #region Paso 11: quitar asignación

        private void QuitarSeleccionada()
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MostrarAdvertencia("Debe seleccionar una posición de la lista para quitar");
                return;
            }

            foreach (ListViewItem filaSeleccionada in listView1.SelectedItems)
            {
                if (filaSeleccionada.Tag is AsignacionUbicacion asignacion)
                {
                    asignaciones.Remove(asignacion);
                }
            }

            RefrescarGrillas();
        }

        #endregion

        #region Pasos 12 a 16: confirmación

        private void ConfirmarIngreso()
        {
            if (ordenActual == null)
            {
                MostrarAdvertencia("Debe buscar una orden de recepción antes de confirmar el ingreso");
                return;
            }

            if (asignaciones.Count == 0)
            {
                MostrarAdvertencia("Debe agregar al menos una ubicación antes de confirmar el ingreso");
                return;
            }

            if (ordenActual.Items.Any(i => CalcularCantidadPendiente(i) > 0))
            {
                MostrarAdvertencia("Quedan unidades sin ubicar. Debe ubicar toda la mercadería recepcionada antes de confirmar");
                return;
            }

            // Paso 13: se registran las existencias en AL.3 Mercaderías
            string resumenStock = RegistrarExistencias();

            // Paso 14: la orden pasa a Cumplida (ya no se puede volver a buscar)
            ordenActual.Estado = EstadoOrdenRecepcion.Cumplida;

            // Paso 16
            MessageBox.Show(
                "Mercadería ingresada a stock con éxito" + Environment.NewLine + Environment.NewLine +
                "Stock actualizado (Disponible en Guarda):" + Environment.NewLine + resumenStock,
                TituloMensajes, MessageBoxButtons.OK, MessageBoxIcon.Information);

            RestablecerFormulario();
        }

        // Incrementa el stock de cada posición asignada y devuelve un resumen para mostrar
        private string RegistrarExistencias()
        {
            List<string> lineasResumen = new List<string>();

            foreach (AsignacionUbicacion asignacion in asignaciones)
            {
                Mercaderia? mercaderia = mercaderias.FirstOrDefault(m =>
                    m.EstaEn(asignacion.Sku, asignacion.Pasillo, asignacion.Fila, asignacion.Nivel));

                if (mercaderia == null)
                {
                    mercaderia = new Mercaderia(asignacion.Sku, asignacion.Pasillo, asignacion.Fila, asignacion.Nivel, 0);
                    mercaderias.Add(mercaderia);
                }

                int stockAnterior = mercaderia.Cantidad;
                mercaderia.IncrementarStock(asignacion.Cantidad);

                lineasResumen.Add($"• {mercaderia.Sku} en {mercaderia.Posicion}: {stockAnterior} → {mercaderia.Cantidad} u.");
            }

            return string.Join(Environment.NewLine, lineasResumen);
        }

        #endregion

        #region Pantalla

        private void RefrescarGrillas()
        {
            listViewSkus.BeginUpdate();
            listViewSkus.Items.Clear();
            if (ordenActual != null)
            {
                foreach (ItemOrdenRecepcion item in ordenActual.Items)
                {
                    ListViewItem filaSku = new ListViewItem(new[]
                    {
                        item.Sku,
                        item.Descripcion,
                        item.CantidadRecepcionada.ToString(),
                        CalcularCantidadPendiente(item).ToString()
                    });
                    filaSku.Tag = item;
                    listViewSkus.Items.Add(filaSku);
                }
            }
            listViewSkus.EndUpdate();

            listView1.BeginUpdate();
            listView1.Items.Clear();
            foreach (AsignacionUbicacion asignacion in asignaciones)
            {
                ListViewItem filaAsignacion = new ListViewItem(new[]
                {
                    asignacion.Sku,
                    asignacion.Cantidad.ToString(),
                    asignacion.Posicion
                });
                filaAsignacion.Tag = asignacion;
                listView1.Items.Add(filaAsignacion);
            }
            listView1.EndUpdate();
        }

        private void LimpiarCamposUbicacion()
        {
            txtCantidadUbicar.Clear();
            LimpiarCombo(cmbPasilloX);
            LimpiarCombo(cmbFilaY);
            LimpiarCombo(cmbNivelZ);
        }

        private void LimpiarCombo(ComboBox combo)
        {
            combo.SelectedIndex = -1;
            combo.Text = string.Empty;
        }

        // Excepción paso 12 (Cancelar) y cierre del caso de uso
        private void RestablecerFormulario()
        {
            ordenActual = null;
            asignaciones.Clear();

            txtNroOrden.Clear();
            txtClienteOrigen.Clear();
            cmbSkuSeleccionado.Items.Clear();
            cmbSkuSeleccionado.Text = string.Empty;
            LimpiarCamposUbicacion();
            RefrescarGrillas();

            txtNroOrden.Focus();
        }

        private void MostrarAdvertencia(string mensaje)
        {
            MessageBox.Show(mensaje, TituloMensajes, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #endregion
    }
}