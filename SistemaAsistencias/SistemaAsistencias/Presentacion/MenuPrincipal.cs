using ORUSCURSO.Presentacion;
using SistemaAsistencias.Datos;
using SistemaAsistencias.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SistemaAsistencias.Presentacion
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        public int Idusuario;
        public string LoginV;

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
            TomarAsistencias frm = new TomarAsistencias();
            frm.ShowDialog();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            panelBienvenida.Dock = DockStyle.Fill;
            validarPermisos();
            // MessageBox.Show("Mensaje " + Idusuario, "Mensaje", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }

        private void validarPermisos()
        {
            DataTable dt = new DataTable();
            Dpermisos funcion = new Dpermisos();
            Lpermisos parametros = new Lpermisos();
            parametros.IdUsuario = Idusuario;
            funcion.mostrar_Permisos(ref dt, parametros);
            btnConsultas.Enabled = false;
            btnPersonal.Enabled = false;
            btnRegistro.Enabled = false;
            btnUsuarios.Enabled = false;

            btnRestaurar.Enabled = false;
            btnRespaldos.Enabled = false;

            foreach (DataRow rowPermisos in dt.Rows)
            {
                string Modulo = Convert.ToString(rowPermisos["Modulo"]);
                if (Modulo == "Planillas")
                {
                    btnConsultas.Enabled = true;
                    btnRegistro.Enabled = true;
                }
                if (Modulo == "Personal")
                {
                    btnConsultas.Enabled = true;
                    btnRegistro.Enabled = true;
                }
                if (Modulo == "Usuarios")
                {
                    btnUsuarios.Enabled = true;
                    // btnRegistro.Enabled = true;
                }
                if (Modulo == "Admin")
                {
                    btnConsultas.Enabled = true;
                    btnPersonal.Enabled = true;
                    btnRegistro.Enabled = true;
                    btnUsuarios.Enabled = true;
                }
            }
        }

        public int enviarIdUsuario()
        {
            return Idusuario;
            
        }

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            // Llamando al control de usuario Personal
            panelPadre.Controls.Clear();
            Personal control = new Personal();
            // Enviamos el id del usuario actual
            // control.ObtenerIdUsuarioPersonal(Idusuario);
            control.Dock = DockStyle.Fill;
            panelPadre.Controls.Add(control);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            // limpiando panel padre
            panelPadre.Controls.Clear();
            CtlUsuarios control = new CtlUsuarios();
            // Enviamos el id del usuario actual
            control.ObtenerIdUsuario2(Idusuario);
            // MessageBox.Show("Mensaje " + Idusuario, "Mensaje", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            control.Dock = DockStyle.Fill;
            panelPadre.Controls.Add(control);
        }

        private void btnConsultas_Click(object sender, EventArgs e)
        {
            panelPadre.Controls.Clear();
            PrePlanilla control = new PrePlanilla();
            control.Dock = DockStyle.Fill;
            panelPadre.Controls.Add(control);
        }

        private void panelPadre_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnEstaciones_Click(object sender, EventArgs e)
        {
            Dispose();
            Login frm = new Login();
            frm.ShowDialog();
        }

        private void panelBienvenida_Click(object sender, EventArgs e)
        {

        }

        private void icono_Click(object sender, EventArgs e)
        {

        }
    }
}
