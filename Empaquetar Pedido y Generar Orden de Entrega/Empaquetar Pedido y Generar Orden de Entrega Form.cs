using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GrupoC.Empaquetar_Pedido_y_Generar_Orden_de_Entrega
{
    public partial class Empaquetar_Pedido_y_Generar_Orden_de_Entrega_Form : Form
    {
        private readonly Empaquetar_Pedido_y_Gernerar_Orden_de_Entrega_MODELO  modelo = new();
   
        public Empaquetar_Pedido_y_Generar_Orden_de_Entrega_Form()
        {
            InitializeComponent();
        }
    }
}
