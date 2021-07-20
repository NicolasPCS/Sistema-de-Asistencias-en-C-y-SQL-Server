using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SistemaAsistencias.Presentacion.AsistenteInstalacion
{
    public partial class ElecccionServidor : Form
    {
        public ElecccionServidor()
        {
            InitializeComponent();
        }

        private void BtnPrincipal_Click(object sender, EventArgs e)
        {
            // destruye el formulario actual
            Dispose();
            // nuevo formulario para instalar la base de datos
            UsuarioPrincipal frm = new UsuarioPrincipal();
            // Login frm = new Login();
            frm.ShowDialog();
        }

        private void BtnRemoto_Click(object sender, EventArgs e)
        {
            Dispose();
            ConexionRemota frm = new ConexionRemota();
            frm.ShowDialog();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
