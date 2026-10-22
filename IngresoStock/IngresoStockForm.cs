#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GrupoC.IngresoStock
{
    /*
     * PROTOTIPO - CU: Ingreso a stock
     * ----------------------------------------------------------------------------
     * Toda la funcionalidad está en este archivo. El Designer NO se modifica:
     * los eventos que el Designer no tiene vinculados (Buscar, Agregar Ubicación,
     * Confirmar, Cancelar, etc.) se vinculan por código en el constructor.
     *
     * Los datos son de prueba y viven en memoria (no hay base de datos):
     *  - AL.15 Órdenes de Recepción: 1001, 1002 y 1003 están "Pendiente de Ubicación",
     *    1004 está "Cumplida" y 1005 está "En Recepción".
     *  - AL.3 Mercaderías y el historial de auditoría arrancan vacíos y se completan
     *    al confirmar ingresos.
     *
     * Cómo probar cada flujo del caso de uso:
     *  - Camino feliz.....: orden 1002 -> Licuadora 30 en Pasillo 1/Fila 1/Nivel 1 y
     *                       Pava 45 en Pasillo 2/Fila 1/Nivel 2 -> Confirmar Ingreso.
     *  - Fraccionar.......: orden 1001 -> Yerba 70 en una posición y 50 en otra.
     *  - Excepción 4......: buscar "abc", "0" o "-3".
     *  - Excepción 5......: buscar 9999 (no existe), 1004 (Cumplida) o 1005 (En Recepción).
     *                       También: confirmar la 1002 y volver a buscarla.
     *  - Excepción 8......: Agregar Ubicación sin elegir Nivel o sin cantidad.
     *  - Excepción 10.....: orden 1002 -> 31 Licuadoras (se recepcionaron 30).
     *  - Excepción 11.....: Quitar seleccionada sin elegir una fila de la grilla inferior.
     *  - Excepción 12.....: Cancelar con asignaciones cargadas.
     *
     * Atajos: Enter en "N° Orden Recepción" busca; Enter en "Cantidad" agrega la
     * ubicación; Supr en la grilla inferior quita la fila seleccionada; click en una
     * fila de la grilla principal selecciona ese SKU en el desplegable.
     */
    public partial class IngresoStockForm : Form
    {
        #region Datos de prueba del prototipo (en memoria)

        private const string EstadoPendienteUbicacion = "Pendiente de Ubicación";
        private const string EstadoCumplida = "Cumplida";
        private const string EstadoEnRecepcion = "En Recepción";
        private const string EstadoDisponibleEnGuarda = "Disponible en Guarda";

        // Precondición: operario autenticado con un depósito activo asignado
        private const string UsuarioOperario = "operario.almacen";
        private const string DepositoActivo = "Depósito Central (Barracas)";

        // Son "static" para que se conserven mientras la aplicación esté abierta.

        // AL.15 Órdenes de Recepción
        private static readonly List<OrdenRecepcion> ordenesRecepcion = new List<OrdenRecepcion>
        {
            new OrdenRecepcion(1001, "Distribuidora Andina S.A. / Mendoza", EstadoPendienteUbicacion,
                new ItemOrdenRecepcion("ALM-YER-001", "Yerba mate 1 kg", 120),
                new ItemOrdenRecepcion("ALM-ACE-002", "Aceite de girasol 900 ml", 80),
                new ItemOrdenRecepcion("ALM-ARR-003", "Arroz largo fino 1 kg", 200)),

            new OrdenRecepcion(1002, "Electro Hogar S.A. / Rosario", EstadoPendienteUbicacion,
                new ItemOrdenRecepcion("ELE-LIC-010", "Licuadora 600 W", 30),
                new ItemOrdenRecepcion("ELE-PAV-011", "Pava eléctrica 1,7 L", 45)),

            new OrdenRecepcion(1003, "Textil Patagonia S.A. / Trelew", EstadoPendienteUbicacion,
                new ItemOrdenRecepcion("TEX-REM-020", "Remera de algodón talle M", 300),
                new ItemOrdenRecepcion("TEX-BUZ-021", "Buzo de frisa talle L", 150),
                new ItemOrdenRecepcion("TEX-MED-022", "Medias pack x3", 500)),

            new OrdenRecepcion(1004, "Distribuidora Andina S.A. / Mendoza", EstadoCumplida,
                new ItemOrdenRecepcion("ALM-FID-004", "Fideos secos 500 g", 150)),

            new OrdenRecepcion(1005, "Electro Hogar S.A. / Rosario", EstadoEnRecepcion,
                new ItemOrdenRecepcion("ELE-VEN-012", "Ventilador de pie 20\"", 25)),
        };

        // AL.3 Mercaderías (existencias por SKU y posición en rack)
        private static readonly List<ExistenciaMercaderia> mercaderias = new List<ExistenciaMercaderia>();

        // Historial de auditoría del inventario
        private static readonly List<EventoAuditoria> historialAuditoria = new List<EventoAuditoria>();

        #endregion

        #region Estado de la pantalla

        private OrdenRecepcion? ordenActual;                                       // orden buscada
        private readonly List<LineaUbicacion> lineas = new List<LineaUbicacion>(); // SKUs de la orden y su saldo pendiente
        private readonly List<AsignacionTemporal> asignaciones = new List<AsignacionTemporal>(); // grilla inferior (sin confirmar)

        #endregion

        #region Inicialización

        public IngresoStockForm()
        {
            InitializeComponent();
            VincularEventos();
            LimpiarPantalla();   // Paso 1: la pantalla abre vacía, lista para ingresar el N° de orden
        }

        /// <summary>
        /// Vincula por código los eventos que el Designer no tiene (así no se toca el Designer).
        /// Se hace "-=" antes de "+=" para que, si más adelante alguien vincula el mismo
        /// evento desde el Designer (doble click sobre el control), no se ejecute dos veces.
        /// button1 ("Quitar seleccionada") y listView1 ya están vinculados en el Designer.
        /// </summary>
        private void VincularEventos()
        {
            btnBuscar.Click -= btnBuscar_Click;
            btnBuscar.Click += btnBuscar_Click;
            btnAgregarUbicacion.Click -= btnAgregarUbicacion_Click;
            btnAgregarUbicacion.Click += btnAgregarUbicacion_Click;
            btnConfirmar.Click -= btnConfirmar_Click;
            btnConfirmar.Click += btnConfirmar_Click;
            btnCancelar.Click -= btnCancelar_Click;
            btnCancelar.Click += btnCancelar_Click;

            listViewSkus.SelectedIndexChanged -= listViewSkus_SelectedIndexChanged;
            listViewSkus.SelectedIndexChanged += listViewSkus_SelectedIndexChanged;
            listView1.KeyDown -= listView1_KeyDown;
            listView1.KeyDown += listView1_KeyDown;

            txtNroOrden.KeyDown -= txtNroOrden_KeyDown;
            txtNroOrden.KeyDown += txtNroOrden_KeyDown;
            txtCantidadUbicar.KeyDown -= txtCantidadUbicar_KeyDown;
            txtCantidadUbicar.KeyDown += txtCantidadUbicar_KeyDown;

            FormClosing -= IngresoStockForm_FormClosing;
            FormClosing += IngresoStockForm_FormClosing;

            // Los desplegables solo permiten elegir de la lista (no cambia su apariencia)
            foreach (var combo in new[] { cmbSkuSeleccionado, cmbPasilloX, cmbFilaY, cmbNivelZ })
            {
                combo.KeyPress += ComboSinEscritura_KeyPress;
                combo.KeyDown += ComboSinEscritura_KeyDown;
            }
        }

        /// <summary>Deja la pantalla vacía, lista para buscar una orden (vuelve al paso 2).</summary>
        private void LimpiarPantalla()
        {
            ordenActual = null;
            lineas.Clear();
            asignaciones.Clear();

            txtNroOrden.Clear();
            txtClienteOrigen.Clear();

            RefrescarGrillaSkus();
            RefrescarGrillaAsignaciones();

            cmbSkuSeleccionado.Items.Clear();
            cmbSkuSeleccionado.Text = string.Empty;
            LimpiarCamposUbicacion();

            txtNroOrden.Focus();
        }

        /// <summary>Limpia coordenadas y cantidad (no toca el SKU seleccionado).</summary>
        private void LimpiarCamposUbicacion()
        {
            foreach (var combo in new[] { cmbPasilloX, cmbFilaY, cmbNivelZ })
            {
                combo.SelectedIndex = -1;
                combo.Text = string.Empty;
            }
            txtCantidadUbicar.Clear();
        }

        #endregion

        #region Pasos 2 a 6: buscar la orden de recepción

        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            // Paso 4: el N° de orden debe ser un entero positivo
            if (!int.TryParse(txtNroOrden.Text.Trim(), out int numeroOrden) || numeroOrden <= 0)
            {
                // Excepción 4: se limpia el campo y se vuelve al paso 2
                MostrarError("El número de orden debe ser un valor numérico",
                             "Error - N° de orden inválido");
                txtNroOrden.Clear();
                txtNroOrden.Focus();
                return;
            }

            // Si hay ubicaciones cargadas sin confirmar, se avisa antes de descartarlas
            if (asignaciones.Count > 0)
            {
                var respuesta = MessageBox.Show(this,
                    $"La orden N° {ordenActual?.Numero} tiene {asignaciones.Count} ubicación(es) cargada(s) sin confirmar.\n\n" +
                    "Si busca otra orden se descartarán. ¿Desea continuar?",
                    "Descartar ubicaciones sin confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (respuesta != DialogResult.Yes)
                {
                    txtNroOrden.Text = ordenActual?.Numero.ToString() ?? string.Empty;
                    return;
                }
            }

            // Paso 5: buscar en AL.15 Órdenes de Recepción
            var orden = ordenesRecepcion.FirstOrDefault(o => o.Numero == numeroOrden);

            if (orden == null || orden.Estado != EstadoPendienteUbicacion)
            {
                // Excepción 5: se limpia la pantalla y se vuelve al paso 2
                string detalle = orden == null
                    ? $"No existe la orden N° {numeroOrden}."
                    : $"La orden N° {numeroOrden} se encuentra en estado \"{orden.Estado}\".";

                MostrarError("No se encontró ninguna orden pendiente de ubicación con ese número\n\n" + detalle,
                             "Orden de recepción no encontrada");
                LimpiarPantalla();
                return;
            }

            // Paso 6
            CargarOrden(orden);
        }

        /// <summary>Paso 6: muestra el Cliente / Origen y los ítems de la orden.</summary>
        private void CargarOrden(OrdenRecepcion orden)
        {
            ordenActual = orden;
            lineas.Clear();
            asignaciones.Clear();

            // Se trabaja sobre una copia: hasta confirmar no se modifica la "base de datos"
            foreach (var item in orden.Items)
                lineas.Add(new LineaUbicacion(item.Sku, item.Descripcion, item.CantidadRecepcionada));

            txtNroOrden.Text = orden.Numero.ToString();
            txtClienteOrigen.Text = orden.ClienteOrigen;

            RefrescarGrillaSkus();
            RefrescarGrillaAsignaciones();

            cmbSkuSeleccionado.Items.Clear();
            foreach (var linea in lineas)
                cmbSkuSeleccionado.Items.Add(linea);
            cmbSkuSeleccionado.SelectedIndex = -1;
            cmbSkuSeleccionado.Text = string.Empty;

            LimpiarCamposUbicacion();
            cmbSkuSeleccionado.Focus();
        }

        #endregion

        #region Pasos 7 a 11: asignar ubicaciones

        private void btnAgregarUbicacion_Click(object? sender, EventArgs e)
        {
            if (ordenActual == null)
            {
                MostrarError("Primero debe buscar una Orden de Recepción pendiente de ubicación.",
                             "Error - No hay una orden cargada");
                txtNroOrden.Focus();
                return;
            }

            // Paso 7: SKU a ubicar
            if (cmbSkuSeleccionado.SelectedItem is not LineaUbicacion linea)
            {
                MostrarError("Debe seleccionar el SKU que desea ubicar.",
                             "Error - SKU no seleccionado");
                cmbSkuSeleccionado.Focus();
                return;
            }

            // Paso 8: coordenadas X, Y, Z y cantidad
            var faltantes = new List<string>();
            Control? primerFaltante = null;

            void RegistrarFaltante(string campo, Control control)
            {
                faltantes.Add("• " + campo);
                if (primerFaltante == null)
                    primerFaltante = control;
            }

            if (cmbPasilloX.SelectedIndex < 0)
                RegistrarFaltante("Pasillo/Rack (X)", cmbPasilloX);
            if (cmbFilaY.SelectedIndex < 0)
                RegistrarFaltante("Fila (Y)", cmbFilaY);
            if (cmbNivelZ.SelectedIndex < 0)
                RegistrarFaltante("Nivel/Altura (Z)", cmbNivelZ);

            bool cantidadValida = int.TryParse(txtCantidadUbicar.Text.Trim(), out int cantidad) && cantidad > 0;
            if (!cantidadValida)
                RegistrarFaltante("Cantidad (número entero mayor a 0)", txtCantidadUbicar);

            if (faltantes.Count > 0)
            {
                // Excepción 8: se vuelve al paso 8
                MostrarError("Debe seleccionar Pasillo, Fila, Nivel e ingresar una cantidad válida\n\n" +
                             "Falta completar:\n" + string.Join("\n", faltantes),
                             "Error - Datos de ubicación incompletos");
                primerFaltante?.Focus();
                return;
            }

            // Paso 10: la cantidad no puede superar el saldo pendiente del SKU
            if (cantidad > linea.CantidadPendiente)
            {
                // Excepción 10: se limpia la cantidad y se vuelve al paso 8
                MostrarError("La cantidad a ubicar no puede ser mayor a la cantidad pendiente recepcionada\n\n" +
                             $"SKU: {linea.Sku} - {linea.Descripcion}\n" +
                             $"Cantidad ingresada: {cantidad}\n" +
                             $"Cantidad pendiente de ubicar: {linea.CantidadPendiente}",
                             "Error - Cantidad mayor al saldo pendiente");
                txtCantidadUbicar.Clear();
                txtCantidadUbicar.Focus();
                return;
            }

            string pasillo = cmbPasilloX.GetItemText(cmbPasilloX.SelectedItem);
            string fila = cmbFilaY.GetItemText(cmbFilaY.SelectedItem);
            string nivel = cmbNivelZ.GetItemText(cmbNivelZ.SelectedItem);

            // Si ya había una asignación del mismo SKU en la misma posición, se suma la cantidad
            var asignacionExistente = asignaciones.FirstOrDefault(a =>
                a.Linea == linea && a.Pasillo == pasillo && a.Fila == fila && a.Nivel == nivel);

            if (asignacionExistente != null)
                asignacionExistente.Cantidad += cantidad;
            else
                asignaciones.Add(new AsignacionTemporal(linea, cantidad, pasillo, fila, nivel));

            // Paso 10: se actualiza la Cantidad a ubicar (saldo) y la grilla de asignaciones
            linea.CantidadPendiente -= cantidad;
            RefrescarGrillaSkus();
            RefrescarGrillaAsignaciones();

            // Paso 11: listo para seguir con otro SKU o fraccionar el mismo en otra posición
            LimpiarCamposUbicacion();
            if (linea.CantidadPendiente == 0)
            {
                cmbSkuSeleccionado.SelectedIndex = -1;
                cmbSkuSeleccionado.Text = string.Empty;
                cmbSkuSeleccionado.Focus();
            }
            else
            {
                cmbPasilloX.Focus();
            }
        }

        // Paso 11 (opcional): "Quitar seleccionada" - vinculado desde el Designer
        private void button1_Click(object? sender, EventArgs e)
        {
            var seleccionadas = listView1.SelectedItems
                .Cast<ListViewItem>()
                .Select(fila => fila.Tag)
                .OfType<AsignacionTemporal>()
                .ToList();

            if (seleccionadas.Count == 0)
            {
                // Excepción 11: se vuelve al paso 11
                MessageBox.Show(this, "Debe seleccionar una posición de la lista para quitar",
                                "Aviso - Ninguna posición seleccionada",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listView1.Focus();
                return;
            }

            // Se restablecen los saldos en la grilla principal
            foreach (var asignacion in seleccionadas)
            {
                asignacion.Linea.CantidadPendiente += asignacion.Cantidad;
                asignaciones.Remove(asignacion);
            }

            RefrescarGrillaSkus();
            RefrescarGrillaAsignaciones();
        }

        // Click en una fila de la grilla principal -> selecciona ese SKU en el desplegable
        private void listViewSkus_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (listViewSkus.SelectedItems.Count == 0)
                return;

            if (listViewSkus.SelectedItems[0].Tag is LineaUbicacion linea)
                cmbSkuSeleccionado.SelectedItem = linea;
        }

        // Supr en la grilla inferior equivale a "Quitar seleccionada"
        private void listView1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                button1.PerformClick();
            }
        }

        #endregion

        #region Pasos 12 a 16: confirmar el ingreso

        private void btnConfirmar_Click(object? sender, EventArgs e)
        {
            if (ordenActual == null)
            {
                MostrarError("Primero debe buscar una Orden de Recepción pendiente de ubicación.",
                             "Error - No hay una orden cargada");
                txtNroOrden.Focus();
                return;
            }

            if (asignaciones.Count == 0)
            {
                MostrarError("Debe agregar al menos una ubicación antes de confirmar el ingreso.",
                             "Error - Sin ubicaciones asignadas");
                cmbSkuSeleccionado.Focus();
                return;
            }

            // La orden pasa a "Cumplida", así que toda la mercadería recepcionada tiene que quedar ubicada
            var lineasConSaldo = lineas.Where(l => l.CantidadPendiente > 0).ToList();
            if (lineasConSaldo.Count > 0)
            {
                MostrarError("No se puede confirmar el ingreso: quedan unidades de la orden sin ubicar.\n\n" +
                             string.Join("\n", lineasConSaldo.Select(l =>
                                 $"• {l.Sku} - {l.Descripcion}: {l.CantidadPendiente} pendiente(s)")),
                             "Error - Quedan unidades sin ubicar");
                cmbSkuSeleccionado.SelectedItem = lineasConSaldo[0];
                cmbPasilloX.Focus();
                return;
            }

            var orden = ordenActual;
            var fechaHora = DateTime.Now;
            int totalUnidades = asignaciones.Sum(a => a.Cantidad);

            // Paso 13: AL.3 Mercaderías -> se incrementa el stock en cada coordenada (X, Y, Z)
            foreach (var asignacion in asignaciones)
                RegistrarExistencia(asignacion, orden.ClienteOrigen);

            // Paso 14: AL.15 Órdenes de Recepción -> la orden pasa a "Cumplida"
            orden.Estado = EstadoCumplida;

            // Paso 15: historial de auditoría del inventario (usuario, fecha y hora)
            historialAuditoria.Add(new EventoAuditoria(
                fechaHora, UsuarioOperario, DepositoActivo, orden.Numero,
                $"Ingreso a stock de {totalUnidades} unidad(es) en {asignaciones.Count} ubicación(es)"));

            // Paso 16: mensaje de confirmación
            string detalleUbicaciones = string.Join("\n", asignaciones.Select(a =>
                $"• {a.Linea.Sku}: {a.Cantidad} u. en {a.Pasillo} / {a.Fila} / {a.Nivel}"));

            MessageBox.Show(this,
                "Mercadería ingresada a stock con éxito\n\n" +
                $"Orden de Recepción N° {orden.Numero}: {orden.Estado}\n" +
                $"Cliente / Origen: {orden.ClienteOrigen}\n" +
                $"Unidades ingresadas: {totalUnidades} (estado \"{EstadoDisponibleEnGuarda}\")\n\n" +
                "Ubicaciones:\n" + detalleUbicaciones + "\n\n" +
                $"Registrado por: {UsuarioOperario} - {DepositoActivo} - {fechaHora:dd/MM/yyyy HH:mm}",
                "Ingreso a stock confirmado",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Queda lista para ingresar otra orden
            LimpiarPantalla();
        }

        /// <summary>Suma la cantidad a la existencia de ese SKU en esa posición (o la crea).</summary>
        private static void RegistrarExistencia(AsignacionTemporal asignacion, string cliente)
        {
            var existencia = mercaderias.FirstOrDefault(m =>
                m.Sku == asignacion.Linea.Sku &&
                m.Pasillo == asignacion.Pasillo &&
                m.Fila == asignacion.Fila &&
                m.Nivel == asignacion.Nivel &&
                m.Estado == EstadoDisponibleEnGuarda);

            if (existencia != null)
            {
                existencia.Cantidad += asignacion.Cantidad;
            }
            else
            {
                mercaderias.Add(new ExistenciaMercaderia(
                    asignacion.Linea.Sku, asignacion.Linea.Descripcion, cliente,
                    asignacion.Pasillo, asignacion.Fila, asignacion.Nivel,
                    asignacion.Cantidad, EstadoDisponibleEnGuarda));
            }
        }

        #endregion

        #region Cancelar / salir de la pantalla

        // Excepción 12: se descartan las asignaciones temporales y se restablece el formulario
        // sin modificar la base de datos.
        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            if (asignaciones.Count > 0)
            {
                var respuesta = MessageBox.Show(this,
                    "¿Desea cancelar el ingreso a stock?\n\n" +
                    $"Se descartarán las {asignaciones.Count} ubicación(es) cargada(s) y no se modificará la base de datos.",
                    "Cancelar ingreso a stock",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (respuesta != DialogResult.Yes)
                    return;
            }

            LimpiarPantalla();
        }

        // Si se cierra la ventana con ubicaciones sin confirmar, se pide confirmación
        private void IngresoStockForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing || asignaciones.Count == 0)
                return;

            var respuesta = MessageBox.Show(this,
                "Hay ubicaciones cargadas sin confirmar.\n\n¿Desea salir de todos modos? Se descartarán sin modificar la base de datos.",
                "Salir de Ingreso a Stock",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (respuesta != DialogResult.Yes)
                e.Cancel = true;
        }

        #endregion

        #region Grillas

        /// <summary>Grilla principal: SKU, Descripción, Cantidad Recepcionada, Cantidad a ubicar (saldo).</summary>
        private void RefrescarGrillaSkus()
        {
            listViewSkus.BeginUpdate();
            listViewSkus.Items.Clear();

            foreach (var linea in lineas)
            {
                var fila = new ListViewItem(linea.Sku);
                fila.SubItems.Add(linea.Descripcion);
                fila.SubItems.Add(linea.CantidadRecepcionada.ToString());
                fila.SubItems.Add(linea.CantidadPendiente.ToString());
                fila.Tag = linea;
                listViewSkus.Items.Add(fila);
            }

            listViewSkus.EndUpdate();
        }

        /// <summary>Grilla inferior (asignaciones temporales): SKU, Cantidad, Posición.</summary>
        private void RefrescarGrillaAsignaciones()
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();

            foreach (var asignacion in asignaciones)
            {
                var fila = new ListViewItem(asignacion.Linea.Sku);
                fila.SubItems.Add(asignacion.Cantidad.ToString());
                fila.SubItems.Add(asignacion.Posicion);
                fila.Tag = asignacion;
                listView1.Items.Add(fila);
            }

            listView1.EndUpdate();
        }

        /// <summary>Ej.: "Pasillo 1", "Fila 2", "Nivel 3" -> "X1 - Y2 - Z3".</summary>
        private static string FormatearPosicion(string pasillo, string fila, string nivel)
        {
            return $"{Coordenada("X", pasillo)} - {Coordenada("Y", fila)} - {Coordenada("Z", nivel)}";
        }

        private static string Coordenada(string eje, string valor)
        {
            // Si el texto termina en número ("Pasillo 1") se abrevia como "X1"; si no, se deja completo
            string ultimaPalabra = valor.Trim().Split(' ').Last();
            return int.TryParse(ultimaPalabra, out _) ? eje + ultimaPalabra : valor;
        }

        #endregion

        #region Métodos auxiliares

        private void MostrarError(string mensaje, string titulo)
        {
            MessageBox.Show(this, mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Enter en "N° Orden Recepción" equivale a presionar "Buscar"
        private void txtNroOrden_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnBuscar.PerformClick();
            }
        }

        // Enter en "Cantidad" equivale a presionar "Agregar Ubicación"
        private void txtCantidadUbicar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnAgregarUbicacion.PerformClick();
            }
        }

        // Los desplegables solo permiten elegir de la lista: se bloquea la escritura manual
        private void ComboSinEscritura_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Escape)
                e.Handled = true;
        }

        private void ComboSinEscritura_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                e.Handled = true;
        }

        #endregion

        #region Métodos referenciados por el Designer (NO BORRAR)

        // listView1.SelectedIndexChanged está vinculado en el Designer; si se borra, no compila.
        private void listView1_SelectedIndexChanged(object? sender, EventArgs e)
        {
        }

        // button1_Click ("Quitar seleccionada") también lo vincula el Designer: está en la región de los pasos 7 a 11.

        #endregion

        #region Clases del prototipo (modelo en memoria)

        /// <summary>Registro de AL.15 Órdenes de Recepción.</summary>
        private sealed class OrdenRecepcion
        {
            public int Numero { get; }
            public string ClienteOrigen { get; }
            public string Estado { get; set; }
            public List<ItemOrdenRecepcion> Items { get; }

            public OrdenRecepcion(int numero, string clienteOrigen, string estado, params ItemOrdenRecepcion[] items)
            {
                Numero = numero;
                ClienteOrigen = clienteOrigen;
                Estado = estado;
                Items = items.ToList();
            }
        }

        private sealed class ItemOrdenRecepcion
        {
            public string Sku { get; }
            public string Descripcion { get; }
            public int CantidadRecepcionada { get; }

            public ItemOrdenRecepcion(string sku, string descripcion, int cantidadRecepcionada)
            {
                Sku = sku;
                Descripcion = descripcion;
                CantidadRecepcionada = cantidadRecepcionada;
            }
        }

        /// <summary>Fila de la grilla principal, con el saldo que falta ubicar.</summary>
        private sealed class LineaUbicacion
        {
            public string Sku { get; }
            public string Descripcion { get; }
            public int CantidadRecepcionada { get; }
            public int CantidadPendiente { get; set; }

            public LineaUbicacion(string sku, string descripcion, int cantidadRecepcionada)
            {
                Sku = sku;
                Descripcion = descripcion;
                CantidadRecepcionada = cantidadRecepcionada;
                CantidadPendiente = cantidadRecepcionada;
            }

            public override string ToString() => $"{Sku} - {Descripcion}";   // texto del desplegable
        }

        /// <summary>Fila de la grilla inferior (asignación temporal, todavía sin confirmar).</summary>
        private sealed class AsignacionTemporal
        {
            public LineaUbicacion Linea { get; }
            public int Cantidad { get; set; }
            public string Pasillo { get; }
            public string Fila { get; }
            public string Nivel { get; }
            public string Posicion => FormatearPosicion(Pasillo, Fila, Nivel);

            public AsignacionTemporal(LineaUbicacion linea, int cantidad, string pasillo, string fila, string nivel)
            {
                Linea = linea;
                Cantidad = cantidad;
                Pasillo = pasillo;
                Fila = fila;
                Nivel = nivel;
            }
        }

        /// <summary>Registro de AL.3 Mercaderías.</summary>
        private sealed class ExistenciaMercaderia
        {
            public string Sku { get; }
            public string Descripcion { get; }
            public string Cliente { get; }
            public string Pasillo { get; }
            public string Fila { get; }
            public string Nivel { get; }
            public int Cantidad { get; set; }
            public string Estado { get; }

            public ExistenciaMercaderia(string sku, string descripcion, string cliente,
                                        string pasillo, string fila, string nivel,
                                        int cantidad, string estado)
            {
                Sku = sku;
                Descripcion = descripcion;
                Cliente = cliente;
                Pasillo = pasillo;
                Fila = fila;
                Nivel = nivel;
                Cantidad = cantidad;
                Estado = estado;
            }
        }

        /// <summary>Evento del historial de auditoría del inventario.</summary>
        private sealed class EventoAuditoria
        {
            public DateTime FechaHora { get; }
            public string Usuario { get; }
            public string Deposito { get; }
            public int NumeroOrden { get; }
            public string Detalle { get; }

            public EventoAuditoria(DateTime fechaHora, string usuario, string deposito, int numeroOrden, string detalle)
            {
                FechaHora = fechaHora;
                Usuario = usuario;
                Deposito = deposito;
                NumeroOrden = numeroOrden;
                Detalle = detalle;
            }
        }

        #endregion
    }
}