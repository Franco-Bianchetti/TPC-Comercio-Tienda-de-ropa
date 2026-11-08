using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class TipoNegocio
    {
        public List<Tipo> listar()
        {
            List<Tipo> lista = new List<Tipo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(
                    "SELECT IdTipo, Nombre FROM Tipos WHERE Activo = 1 ORDER BY IdTipo");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Tipo aux = new Tipo();

                    aux.Id = (int)datos.Lector["IdTipo"];
                    aux.Nombre = datos.Lector["Nombre"].ToString();

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

        public Tipo buscar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(
                    "SELECT IdTipo, Nombre FROM Tipos WHERE IdTipo = @Id AND Activo = 1");

                datos.setParametro("@Id", id);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Tipo aux = new Tipo();

                    aux.Id = (int)datos.Lector["IdTipo"];
                    aux.Nombre = datos.Lector["Nombre"].ToString();

                    return aux;
                }

                return null;
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

        public bool existe(string nombre, int idActual)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(
                    "SELECT IdTipo FROM Tipos WHERE Nombre = @Nombre AND IdTipo <> @Id");

                datos.setParametro("@Nombre", nombre);
                datos.setParametro("@Id", idActual);

                datos.ejecutarLectura();

                return datos.Lector.Read();
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

        public void agregar(Tipo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(
                    "INSERT INTO Tipos (Nombre) VALUES (@Nombre)");

                datos.setParametro("@Nombre", nuevo.Nombre);

                datos.ejecutarAccion();
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

        public void modificar(Tipo tipo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(
                    "UPDATE Tipos SET Nombre = @Nombre WHERE IdTipo = @Id");

                datos.setParametro("@Nombre", tipo.Nombre);
                datos.setParametro("@Id", tipo.Id);

                datos.ejecutarAccion();
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

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(
                    "UPDATE Tipos SET Activo = 0 WHERE IdTipo = @Id");

                datos.setParametro("@Id", id);

                datos.ejecutarAccion();
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
