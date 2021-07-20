using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SistemaAsistencias.Logica;
using System.Windows.Forms;
using System.Data;
using SistemaAsistencias.Presentacion;

namespace SistemaAsistencias.Datos
{
    public class Dcargos
    {
        public bool insertar_Cargo(Lcargos parametros)
        {
            // Protegiendo codigo
            try
            {
                // Abrimos la conexion
                CONEXIONMAESTRA.abrir();
                // Proceso para trabajar con base de datos
                SqlCommand cmd = new SqlCommand("insertar_Cargo", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                // Pasamos los campos a SQL Server
                cmd.Parameters.AddWithValue("@Cargo", parametros.Cargo);
                cmd.Parameters.AddWithValue("@SueldoPorHora", parametros.SueldoPorhora);

                // ejecutamos el proceso
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            // Cerramos la conexion
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }

        public bool editar_Cargo(Lcargos parametros)
        {
            // Protegiendo codigo
            try
            {
                // Abrimos la conexion
                CONEXIONMAESTRA.abrir();
                // Proceso para trabajar con base de datos
                SqlCommand cmd = new SqlCommand("editar_Cargo", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                // Pasamos los campos a SQL Server
                cmd.Parameters.AddWithValue("@id", parametros.Id_cargo);
                cmd.Parameters.AddWithValue("@Cargo", parametros.Cargo);
                cmd.Parameters.AddWithValue("@Sueldo", parametros.SueldoPorhora);

                // ejecutamos el proceso
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            // Cerramos la conexion
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }

        public void buscarCargos(ref DataTable dt, string buscador)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscarCargos", CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@buscador", buscador);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }

    }
}
