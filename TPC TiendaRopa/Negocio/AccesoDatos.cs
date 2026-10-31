using System.Data;
using System.Data.SqlClient;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
            conexion = new SqlConnection(
                "server=.\\SQLEXPRESS;database=TIENDA_ROPA_DB;integrated security=true");

            comando = new SqlCommand();
        }

        public void setConsulta(string consulta)
        {
            comando.CommandType = CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            conexion.Open();
            lector = comando.ExecuteReader();
        }

        public void setParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            conexion.Open();
            comando.ExecuteNonQuery();
        }

        public void limpiarParametros()
        {
            comando.Parameters.Clear();
        }

        public void cerrarConexion()
        {
            if (lector != null)
                lector.Close();

            if (conexion.State == ConnectionState.Open)
                conexion.Close();
        }
    }
}