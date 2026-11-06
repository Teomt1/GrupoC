using GrupoC;
using GrupoC.Almacenes;

internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

        //La app se cierra, giardamos los almacenes
        ProductoAlmacen.Guardar();
        ClienteAlmacen.Guardar();
        ContratosAlmacen.Guardar();
        DepositoAlmacen.Guardar();
        EmpleadoAlmacen.Guardar();
        MovimientoMercaderiaAlmacen.Guardar();
        OrdenEntregaAlmacen.Guardar();
        OrdenPreparacionAlmacen.Guardar();
        OrdenRecepcionAlmacen.Guardar();
        OrdenSeleccionAlmacen.Guardar();
        OrdenTrasladoAlmacen.Guardar();
        ProveedorAlmacen.Guardar();
        RegistroContableAlmacen.Guardar();
        RemitoEntregaAlmacen.Guardar();
    }
    }