#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GrupoC.RegistrarOrdenDePreparacion
{
    public partial class RegistrarOrdenDePreparacionForm : Form
    {
        private readonly RegistrarOrdenDePreparacionModelo modelo;

        public RegistrarOrdenDePreparacionForm()
        {
            InitializeComponent();

            modelo = new RegistrarOrdenDePreparacionModelo(
                this,
                labtemp,
                combodepo,
                radiocerrado,
                radiofullfilment,
                labdatosetiq,
                labnombre,
                textnombre,
                labdireccion,
                textdireccion,
                comboprodretirar,
                textcantretirar,
                btnagregar,
                listordenact,
                datefecha,
                textnomtrans,
                textDNI,
                textempresatrans,
                btnconfirmar,
                btncancelar);
        }

        /* Creo que quedaron del último intento?
        no hacen nada pero si los borro me deja de funcionar el programa y no me doy cuenta de como corregirlo */
        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
