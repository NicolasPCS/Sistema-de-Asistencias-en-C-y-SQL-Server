﻿using ORUSCURSO.Presentacion;
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

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            panelBienvenida.Dock = DockStyle.Fill;
            validarPermisos();
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
                if (Modulo == "PrePlanillas")
                {
                    btnConsultas.Enabled = true;
                }
                if (Modulo == "Usuarios")
                {
                    btnUsuarios.Enabled = true;
                    btnRegistro.Enabled = true;
                }
                if (Modulo == "Personal")
                {
                    btnPersonal.Enabled = true;
                }
                if (Modulo == "Respaldos")
                {
                    btnRespaldos.Enabled = true;
                    btnRestaurar.Enabled = true;
                }
            }
        }

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            // Llamando al control de usuario Personal
            panelPadre.Controls.Clear();
            Personal control = new Personal();
            control.Dock = DockStyle.Fill;
            panelPadre.Controls.Add(control);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            // limpiando panel padre
            panelPadre.Controls.Clear();
            CtlUsuarios control = new CtlUsuarios();
            control.Dock = DockStyle.Fill;
            panelPadre.Controls.Add(control);
        }
    }
}