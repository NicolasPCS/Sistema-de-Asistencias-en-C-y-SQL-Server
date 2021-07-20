using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SistemaAsistencias.Datos
{
    class CONEXIONMAESTRA
    {
        // Conexion a la base de datos
        public static string conexion = @"Data source=LAPTOP-DAHCP60S\SQLEXPRESS; Initial Catalog=SistemaAsistencias; Integrated Security=true";
        // public static string conexion = Convert.ToString(Logica.Desencryptacion.checkServer());
        public static SqlConnection conectar = new SqlConnection(conexion);
        // Si la conexion se encuentra cerrada la abrimos
        public static void abrir()
        {
            if (conectar.State == ConnectionState.Closed)
            {
                conectar.Open();
            }
        }
        // Para cerrar la conexion
        public static void cerrar()
        {
            if(conectar.State == ConnectionState.Open)
            {
                conectar.Close();
            }
        }
    }
}
