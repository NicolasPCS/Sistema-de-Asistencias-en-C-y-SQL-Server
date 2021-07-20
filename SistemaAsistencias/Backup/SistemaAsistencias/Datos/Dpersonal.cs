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
    public class Dpersonal
    {
        public bool InsertarPersonal(Lpersonal parametros)
        {
            // Protegiendo codigo
            try
            {
                // Abrimos la conexion
                CONEXIONMAESTRA.abrir();
                // Proceso para trabajar con base de datos
                SqlCommand cmd = new SqlCommand("InsertarPersonal",CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                // Pasamos los campos a SQL Server
                cmd.Parameters.AddWithValue("@Nombres", parametros.Nombres);
                cmd.Parameters.AddWithValue("@Identificacion", parametros.Identificacion);
                cmd.Parameters.AddWithValue("@Pais", parametros.Pais);
                cmd.Parameters.AddWithValue("@Id_cargo", parametros.Id_cargo);
                cmd.Parameters.AddWithValue("@SueldoPorHora", parametros.SueldoPorHora);

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

        public bool editarPersonal(Lpersonal parametros)
        {
            // Protegiendo codigo
            try
            {
                // Abrimos la conexion
                CONEXIONMAESTRA.abrir();
                // Proceso para trabajar con base de datos
                SqlCommand cmd = new SqlCommand("editarPersonal", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                // Pasamos los campos a SQL Server
                cmd.Parameters.AddWithValue("@Id_personal", parametros.Id_personal);
                cmd.Parameters.AddWithValue("@Nombres", parametros.Nombres);
                cmd.Parameters.AddWithValue("@Identificacion", parametros.Identificacion);
                cmd.Parameters.AddWithValue("@Pais", parametros.Pais);
                cmd.Parameters.AddWithValue("@Id_cargo", parametros.Id_cargo);
                cmd.Parameters.AddWithValue("@Sueldo_por_hora", parametros.SueldoPorHora);

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

        public bool eliminarPersonal(Lpersonal parametros)
        {
            // Protegiendo codigo
            try
            {
                // Abrimos la conexion
                CONEXIONMAESTRA.abrir();
                // Proceso para trabajar con base de datos
                SqlCommand cmd = new SqlCommand("eliminarPersonal", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                // Pasamos los campos a SQL Server
                cmd.Parameters.AddWithValue("@Idpersonal", parametros.Id_personal);

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

        public void mostrarPersonal(ref DataTable dt, int desde, int hasta)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarPersonal",CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Desde", desde);
                da.SelectCommand.Parameters.AddWithValue("@Hasta", hasta);
                // Pasamos toda la informacion de da a dt
                da.Fill(dt);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            } finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }

        public void buscarPersonal(ref DataTable dt, int desde, int hasta, string buscador)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscarPersonal", CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Desde", desde);
                da.SelectCommand.Parameters.AddWithValue("@Hasta", hasta);
                da.SelectCommand.Parameters.AddWithValue("@buscador", buscador);
                // Pasamos toda la informacion de da a dt
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

        public bool restaurar_personal(Lpersonal parametros)
        {
            // Protegiendo codigo
            try
            {
                // Abrimos la conexion
                CONEXIONMAESTRA.abrir();
                // Proceso para trabajar con base de datos
                SqlCommand cmd = new SqlCommand("restaurar_personal", CONEXIONMAESTRA.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                // Pasamos los campos a SQL Server
                cmd.Parameters.AddWithValue("@Idpersonal", parametros.Id_personal);

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

        public void ContarPersonal(ref int contador)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlCommand cmd = new SqlCommand("select Count(Id_personal) from Personal",CONEXIONMAESTRA.conectar);
                contador = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception)
            {
                contador = 0;
            } finally
            {
                CONEXIONMAESTRA.cerrar();
            }
        }

        public static implicit operator Dpersonal(Personal v)
        {
            throw new NotImplementedException();
        }

        public void BuscarPersonalIdentidad(ref DataTable dt, string buscador)
        {
            try
            {
                CONEXIONMAESTRA.abrir();
                SqlDataAdapter da = new SqlDataAdapter("BuscarPersonalIdentidad", CONEXIONMAESTRA.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Buscador", buscador);
                // Pasamos toda la informacion de da a dt
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
