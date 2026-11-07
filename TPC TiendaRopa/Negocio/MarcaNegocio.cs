using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(
                    "SELECT IdMarca, Nombre FROM Marcas WHERE Activo = 1 ORDER BY IdMarca");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["IdMarca"];
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

        public Marca buscar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(
                    "SELECT IdMarca, Nombre FROM Marcas WHERE IdMarca = @Id AND Activo = 1");
                datos.setParametro("@Id", id);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["IdMarca"];
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
                    "SELECT IdMarca FROM Marcas WHERE Nombre = @Nombre AND IdMarca <> @Id");
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

        public void agregar(Marca nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("INSERT INTO Marcas (Nombre) VALUES (@Nombre)");
                datos.setParametro("@Nombre", nueva.Nombre);

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

        public void modificar(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("UPDATE Marcas SET Nombre = @Nombre WHERE IdMarca = @Id");
                datos.setParametro("@Nombre", marca.Nombre);
                datos.setParametro("@Id", marca.Id);

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
                datos.setConsulta("UPDATE Marcas SET Activo = 0 WHERE IdMarca = @Id");
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