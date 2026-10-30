using System;
using System.Collections.Generic;
using Dominio;

namespace Tienda_Ropa
{
    public class ClienteNegocio
    {
        public List<Cliente> listar()
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    SELECT
                        IdCliente,
                        Nombre,
                        Apellido,
                        Dni,
                        Telefono,
                        Email
                    FROM Clientes
                    ORDER BY IdCliente");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();

                    aux.Id = (int)datos.Lector["IdCliente"];
                    aux.Nombre = datos.Lector["Nombre"].ToString();
                    aux.Apellido = datos.Lector["Apellido"].ToString();
                    aux.Dni = datos.Lector["Dni"].ToString();
                    aux.Telefono = datos.Lector["Telefono"].ToString();
                    aux.Email = datos.Lector["Email"].ToString();

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
