using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace RiotTanks_1._1            //investigar sqlite
{
    internal class conexionbd
    {
        //string cadena = "Data Source=B6-603-2527\\SQLEXPRESS01;Initial Catalog=db_jugadores;Integrated Security=True"; //prueba
        string cadena = "Data Source=.;Initial Catalog=db_jugadores;Integrated Security=True"; 
        public SqlConnection conectarbd = new SqlConnection();
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        string mensaje = "";
        public conexionbd()
        {
            conectarbd.ConnectionString = cadena;
        }

        public void abrir()
        {
            try
            {
                connection = new SqlConnection(cadena);
                connection.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void cerrar()
        {
            conectarbd.Close();
        }
        public List<Player> ListarPuntuaciones()
        {
            List<Player> Jugadores = new List<Player>();
            try
            {
                abrir();
                string query = "select * from puntuaciones order by puntuacion desc";
                command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Player jugador = new Player();
                    jugador.Nombre = reader.GetString(0);
                    jugador.Puntuacion = reader.GetInt32(1);
                    Jugadores.Add(jugador);
                }
                reader.Close();
                connection.Close();
            }
            catch (SqlException e)
            {
                mensaje = e.Message;
            }
            return Jugadores;
        }
        public int GuardarJugador(string nombre, int puntuacion)
        {
            int index = -1;
            try
            {
                abrir();
                string query = $"insert into puntuaciones values('{nombre}', {puntuacion})";
                command = new SqlCommand(query, connection);
                index = command.ExecuteNonQuery();
                connection.Close();
                return index;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            connection.Close();
            return index;
        }
    }
}
