using SistemaAsistencias.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SistemaAsistencias.Presentacion
{
    public partial class PrePlanilla : UserControl
    {
        public PrePlanilla()
        {
            InitializeComponent();
        }

        private void PrePlanilla_Load(object sender, EventArgs e)
        {
            calcular_numero_de_semana();
        }

        private void ReporteAsistencias()
        {
            Reportes.ReportAsistencias rpt = new Reportes.ReportAsistencias();
            DataTable dt = new DataTable();
            Dasistencias funcion = new Dasistencias();
            funcion.mostrar_asistencias_diarias(ref dt, txtdesde.Value, txthasta.Value, Convert.ToInt32(lblnumerosemana.Text));
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;


            reportViewer1.Report = rpt;
            reportViewer1.RefreshReport();
        }

        private void calcular_numero_de_semana()
        {
            DateTime v2 = txthasta.Value;
            lblnumerosemana.Text = CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(v2, CalendarWeekRule.FirstDay, v2.DayOfWeek).ToString();
        }

        private void txtdesde_ValueChanged(object sender, EventArgs e)
        {
            calcular_numero_de_semana();
            ReporteAsistencias();
        }

        private void txthasta_ValueChanged(object sender, EventArgs e)
        {
            calcular_numero_de_semana();
            ReporteAsistencias();
        }

        private void Panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
