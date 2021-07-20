﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SistemaAsistencias.Logica;
using SistemaAsistencias.Datos;

namespace SistemaAsistencias.Presentacion
{
    public partial class Personal : UserControl
    {
        public Personal()
        {
            InitializeComponent();
        }

        int Idcargo = 0;
        int desde = 1;
        int hasta = 10;
        int contador;
        int Idpersonal;
        private int items_por_pagina = 10;
        string Estado;
        int totalPaginas;

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            localizarDtvCargos();
            PanelCargos.Visible = false;
            PanelPaginado.Visible = false;
            PanelRegistros.Visible = true;
            PanelRegistros.Dock = DockStyle.Fill;
            btnGuardarPersonal.Visible = true;
            btnGuardarCambiosPersonal.Visible = false;
            Limpiar();
        }

        private void localizarDtvCargos()
        {
            datalistadoCargos.Location = new Point(txtSueldoHora.Location.X, txtSueldoHora.Location.Y);
            datalistadoCargos.Size = new Size(469,141);
            datalistadoCargos.Visible = true;
            lblSueldo.Visible = false;
            PanelbtnGuardarPersonal.Visible = false;
        }

        private void Limpiar()
        {
            txtNombres.Clear();
            txtIdentificacion.Clear();
            txtCargo.Clear();
            txtSueldoHora.Clear();
            BuscarCargos();
        }

        private void btnGuardarPersonal_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombres.Text))
            {
                if (!string.IsNullOrEmpty(txtIdentificacion.Text))
                {
                    if (!string.IsNullOrEmpty(cbxPais.Text))
                    {
                        if (Idcargo > 0)
                        {
                            if (!string.IsNullOrEmpty(txtSueldoHora.Text))
                            {
                                Insertar_Personal();
                            }
                        }
                    }
                }
            }
        }

        private void MostrarPersonal()
        {
            DataTable dt = new DataTable();
            Dpersonal funcion = new Dpersonal();
            funcion.mostrarPersonal(ref dt,desde,hasta);
            datalistadoPersonal.DataSource = dt;
            DiseñaDtvPersonal();
        }

        private void DiseñaDtvPersonal()
        {
            Bases.DiseñoDtv(ref datalistadoPersonal);
            Bases.DiseñoDtvEliminar(ref datalistadoPersonal);
            PanelPaginado.Visible = true;
            datalistadoPersonal.Columns[2].Visible = false;
            datalistadoPersonal.Columns[7].Visible = false;
        }

        private void Insertar_Personal()
        {
            Lpersonal parametros = new Lpersonal();
            Dpersonal funcion = new Dpersonal();
            parametros.Nombres = txtNombres.Text;
            parametros.Identificacion = txtIdentificacion.Text;
            parametros.Pais = cbxPais.Text;
            parametros.Id_cargo = Idcargo;
            parametros.SueldoPorHora = Convert.ToDouble(txtSueldoHora.Text);
            if (funcion.InsertarPersonal(parametros) == true)
            {
                ReiniciarPaginado();
                MostrarPersonal();
                PanelRegistros.Visible = false;
            }
        }

        private void InsertarCargos()
        {
            if (!string.IsNullOrEmpty(txtCargoG.Text))
            {
                if (!string.IsNullOrEmpty(txtsueldoG.Text))
                {
                    Lcargos parametros = new Lcargos();
                    Dcargos funcion = new Dcargos();
                    parametros.Cargo = txtCargoG.Text;
                    parametros.SueldoPorhora = Convert.ToDouble(txtsueldoG.Text);
                    if (funcion.insertar_Cargo(parametros) == true)
                    {
                        txtCargo.Clear();
                        BuscarCargos();
                        PanelCargos.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Agregue el sueldo", "Falta el sueldo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } else
            {
                MessageBox.Show("Agregue el cargo", "Falta el cargo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            
        }

        private void BuscarCargos()
        {
            DataTable dt = new DataTable();
            Dcargos funcion = new Dcargos();
            funcion.buscarCargos(ref dt, txtCargo.Text);
            datalistadoCargos.DataSource = dt;
            // Haciendo el llamado al data grid view para que se vean los cargos
            Bases.DiseñoDtv(ref datalistadoCargos);
            datalistadoCargos.Columns[1].Visible = false;
            datalistadoCargos.Columns[3].Visible = false;
            datalistadoCargos.Visible = true;
        }

        private void txtCargo_TextChanged(object sender, EventArgs e)
        {
            BuscarCargos();
        }

        private void btnAgregarCargo_Click(object sender, EventArgs e)
        {
            PanelCargos.Visible = true;
            PanelCargos.Dock = DockStyle.Fill;
            PanelCargos.BringToFront();
            btnGuardarC.Visible = true;
            btnGuardarCambiosC.Visible = false;
            txtCargoG.Clear();
            txtsueldoG.Clear();
        }

        private void btnGuardarC_Click(object sender, EventArgs e)
        {
            InsertarCargos();
        }

        private void txtsueldoG_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtsueldoG, e);
        }

        private void txtSueldoHora_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSueldoHora_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldoHora, e);
        }

        private void datalistadoCargos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void datalistadoCargos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datalistadoCargos.Columns["EditarCargos"].Index)
            {
                ObtenerCargosEditar();
            }
            if (e.ColumnIndex == datalistadoCargos.Columns["Cargo"].Index)
            {
                obtenerDatosCargos();
            }
        }

        private void obtenerDatosCargos()
        {
            Idcargo = Convert.ToInt32(datalistadoCargos.SelectedCells[1].Value);
            txtCargo.Text = datalistadoCargos.SelectedCells[2].Value.ToString();
            txtSueldoHora.Text = datalistadoCargos.SelectedCells[3].Value.ToString();
            datalistadoCargos.Visible = false;
            PanelbtnGuardarPersonal.Visible = true;
            lblSueldo.Visible = true;
        }

        private void ObtenerCargosEditar()
        {
            Idcargo = Convert.ToInt32(datalistadoCargos.SelectedCells[1].Value);
            txtCargoG.Text = datalistadoCargos.SelectedCells[2].Value.ToString();
            txtsueldoG.Text = datalistadoCargos.SelectedCells[3].Value.ToString();
            btnGuardarC.Visible = false;
            btnGuardarCambiosC.Visible = true;
            txtCargoG.Focus();
            txtCargoG.SelectAll();
            PanelCargos.Visible = true;
            PanelCargos.Dock = DockStyle.Fill;
            PanelCargos.BringToFront();
        }

        private void btnVolverCargos_Click(object sender, EventArgs e)
        {
            PanelCargos.Visible = false;
        }

        private void btnVolverPersonal_Click(object sender, EventArgs e)
        {
            PanelRegistros.Visible = false;
            PanelPaginado.Visible = true;
        }

        private void btnGuardarCambiosC_Click(object sender, EventArgs e)
        {
            editarCargos();
        }

        private void editarCargos()
        {
            Lcargos parametros = new Lcargos();
            Dcargos funcion = new Dcargos();
            parametros.Id_cargo = Idcargo;
            parametros.Cargo = txtCargoG.Text;
            parametros.SueldoPorhora = Convert.ToDouble(txtsueldoG.Text);
            if (funcion.editar_Cargo(parametros) == true)
            {
                txtCargo.Clear();
                BuscarCargos();
                PanelCargos.Visible = false;
            }
        }

        private void Personal_Load(object sender, EventArgs e)
        {
            // validarPermisos();
            ReiniciarPaginado();
            MostrarPersonal();
        }

        //// Obteniedo el ID del usuario actual
        //private int IdusuarioThis;

        //private void validarPermisos()
        //{
        //    DataTable dt = new DataTable();
        //    Dpermisos funcion = new Dpermisos();
        //    Lpermisos parametros = new Lpermisos();

        //    parametros.IdUsuario = IdusuarioThis;
        //    funcion.mostrar_Permisos(ref dt, parametros);
        //    btnAgregar.Visible = true;
        //    // MessageBox.Show("Mensaje " + IdusuarioThis, "Mensaje", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        //    foreach (DataRow rowPermisos in dt.Rows)
        //    {
        //        string Modulo = Convert.ToString(rowPermisos["Modulo"]);
        //        // MessageBox.Show("Mensaje" + Modulo, "Mensaje", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        //        if (Modulo == "Usuarios")
        //        {
        //            datalistadoPersonal.Columns[0].Visible = false;
        //            datalistadoPersonal.Columns[1].Visible = false;
        //            // MessageBox.Show("Mensaje", "Mensaje", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        //            btnAgregar.Visible = false;
        //        }
        //    }
        //}

        //// Metodo que recibe el id del usuario actual
        //// Recibe un parametro que se envia desde el menu principal
        //public void ObtenerIdUsuarioPersonal(int idPara)
        //{
        //    IdusuarioThis = idPara;
        //}

        private void ReiniciarPaginado()
        {
            desde = 1;
            hasta = 10;
            Contar();

            if (contador > hasta)
            {

                btn_Sig.Visible = true;
                btn_atras.Visible = false;
                btn_Ultima.Visible = true;
                btn_Primera.Visible = true;
            }
            else
            {

                btn_Sig.Visible = false;
                btn_atras.Visible = false;
                btn_Ultima.Visible = false;
                btn_Primera.Visible = false;
            }
            Paginar();
        }

        private void datalistadoPersonal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datalistadoPersonal.Columns["Eliminar"].Index)
            {
                DialogResult result = MessageBox.Show("¿Solo se cambiara el estado para que no pueda acceder, desea continuar?", "Eliminando registros", MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    EliminarPersonal();
                }
            }
            if (e.ColumnIndex == datalistadoPersonal.Columns["Editar"].Index)
            {
                ObtenerDatos();
            }
        }

        private void ObtenerDatos()
        {
            Idpersonal = Convert.ToInt32(datalistadoPersonal.SelectedCells[2].Value);
            Estado = datalistadoPersonal.SelectedCells[8].Value.ToString();
            if (Estado == "ELIMINADO")
            {
                restaurar_personal();
            } 
            else
            {
                localizarDtvCargos();
                txtNombres.Text = datalistadoPersonal.SelectedCells[3].Value.ToString();
                txtIdentificacion.Text = datalistadoPersonal.SelectedCells[4].Value.ToString();
                cbxPais.Text = datalistadoPersonal.SelectedCells[10].Value.ToString();
                txtCargo.Text = datalistadoPersonal.SelectedCells[6].Value.ToString();
                Idcargo = Convert.ToInt32(datalistadoPersonal.SelectedCells[7].Value);
                txtSueldoHora.Text = datalistadoPersonal.SelectedCells[5].Value.ToString();
                PanelPaginado.Visible = false;
                PanelRegistros.Visible = true;
                PanelRegistros.Dock = DockStyle.Fill;
                datalistadoCargos.Visible = false;
                lblSueldo.Visible = true;
                PanelbtnGuardarPersonal.Visible = true;
                btnGuardarPersonal.Visible = false;
                btnGuardarCambiosPersonal.Visible = true;
                PanelCargos.Visible = false;
            }
        }

        private void restaurar_personal()
        {
            DialogResult result = MessageBox.Show("Este personal se elimino, ¿Desea restaurarlo?", "Restauración de registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                HabilitarPersonal();
            }
        }

        private void HabilitarPersonal()
        {
            Lpersonal parametros = new Lpersonal();
            Dpersonal funcion = new Dpersonal();
            parametros.Id_personal = Idpersonal;
            if (funcion.restaurar_personal(parametros) == true)
            {
                MostrarPersonal();
            }
        }

        private void EliminarPersonal()
        {
            Idpersonal = Convert.ToInt32(datalistadoPersonal.SelectedCells[2].Value);
            Lpersonal parametros = new Lpersonal();
            Dpersonal funcion = new Dpersonal();
            parametros.Id_personal = Idpersonal;
            if (funcion.eliminarPersonal(parametros) == true)
            {
                MostrarPersonal();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DiseñaDtvPersonal();
            timer1.Stop();
        }

        private void btnGuardarCambiosPersonal_Click(object sender, EventArgs e)
        {
            EditarPersonal();
        }

        private void EditarPersonal()
        {
            Lpersonal parametros = new Lpersonal();
            Dpersonal funcion = new Dpersonal();
            parametros.Id_personal = Idpersonal;
            parametros.Nombres = txtNombres.Text;
            parametros.Identificacion = txtIdentificacion.Text;
            parametros.Pais = cbxPais.Text;
            parametros.Id_cargo = Idcargo;
            parametros.SueldoPorHora = Convert.ToDouble(txtSueldoHora.Text);
            if (funcion.editarPersonal(parametros) == true)
            {
                MostrarPersonal();
                PanelRegistros.Visible = false;
            }
        }

        private void btn_Sig_Click(object sender, EventArgs e)
        {
            desde += 10;
            hasta += 10;
            MostrarPersonal();
            Contar();
            if (contador > hasta)
            {
                btn_Sig.Visible = true;
                btn_atras.Visible = true;
            }
            else
            {
                btn_Sig.Visible = false;
                btn_atras.Visible = true;
            }
            Paginar();
        }

        private void Paginar()
        {
            try
            {
                lbl_Pagina.Text = (hasta / items_por_pagina).ToString();
                lbl_totalPaginas.Text = Math.Ceiling(Convert.ToSingle(contador) / items_por_pagina).ToString();
                totalPaginas = Convert.ToInt32(lbl_totalPaginas.Text);
            }
            catch (Exception)
            {

            }
        }

        private void Contar()
        {
            Dpersonal funcion = new Dpersonal();
            funcion.ContarPersonal(ref contador);
        }

        private void btn_atras_Click(object sender, EventArgs e)
        {
            desde -= 10;
            hasta -= 10;
            MostrarPersonal();
            Contar();
            if (contador > hasta)
            {
                btn_Sig.Visible = true;
                btn_atras.Visible = true;
            }
            else
            {
                btn_Sig.Visible = false;
                btn_atras.Visible = true;
            }
            if (desde == 1)
            {
                ReiniciarPaginado();
            }
            Paginar();
        }

        private void btn_Ultima_Click(object sender, EventArgs e)
        {
            hasta = totalPaginas * items_por_pagina;
            desde = hasta - 9;
            MostrarPersonal();
            Contar();
            if (contador > hasta)
            {
                btn_Sig.Visible = true;
                btn_atras.Visible = true;
            }
            else
            {
                btn_Sig.Visible = false;
                btn_atras.Visible = true;
            }
            Paginar();
        }

        private void btn_Primera_Click(object sender, EventArgs e)
        {
            ReiniciarPaginado();
            MostrarPersonal();
        }

        private void txtBuscador_TextChanged(object sender, EventArgs e)
        {
            BuscarPersonal();
        }

        private void BuscarPersonal()
        {
            DataTable dt = new DataTable();
            Dpersonal funcion = new Dpersonal();
            funcion.buscarPersonal(ref dt, desde, hasta,txtBuscador.Text);
            datalistadoPersonal.DataSource = dt;
            DiseñaDtvPersonal();
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            ReiniciarPaginado();
            MostrarPersonal();
        }

        private void PanelRegistros_Paint(object sender, PaintEventArgs e)
        {

        }

        private void datalistadoPersonal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
