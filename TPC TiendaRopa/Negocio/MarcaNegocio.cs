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
                datos.setConsulta(@"
                    SELECT
                        IdMarca,
                        Nombre
                    FROM Marcas
                    WHERE Activo = 1
                    ORDER BY IdMarca");

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
                datos.setConsulta(@"
                    SELECT
                        IdMarca,
                        Nombre
                    FROM Marcas
                    WHERE IdMarca = @Id
                    AND Activo = 1");

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
                datos.setConsulta(@"
            SELECT IdMarca
            FROM Marcas
            WHERE UPPER(LTRIM(RTRIM(Nombre))) =
                  UPPER(LTRIM(RTRIM(@Nombre)))
            AND IdMarca <> @Id");

                datos.setParametro("@Nombre", nombre);
                datos.setParametro("@Id", idActual);

                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool existeInactiva(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    SELECT IdMarca
                    FROM Marcas
                    WHERE UPPER(LTRIM(RTRIM(Nombre))) =
                          UPPER(LTRIM(RTRIM(@Nombre)))
                    AND Activo = 0");

                datos.setParametro("@Nombre", nombre);

                datos.ejecutarLectura();

                return datos.Lector.Read();
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
                datos.setConsulta(@"
                    INSERT INTO Marcas
                    (
                        Nombre,
                        Activo
                    )
                    VALUES
                    (
                        @Nombre,
                        1
                    )");

                datos.setParametro(
                    "@Nombre",
                    nueva.Nombre);

                datos.ejecutarAccion();
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
                datos.setConsulta(@"
                    UPDATE Marcas
                    SET Nombre = @Nombre
                    WHERE IdMarca = @Id");

                datos.setParametro("@Nombre", marca.Nombre);
                datos.setParametro("@Id", marca.Id);

                datos.ejecutarAccion();
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
                datos.setConsulta(@"
                    UPDATE Marcas
                    SET Activo = 0
                    WHERE IdMarca = @Id");

                datos.setParametro("@Id", id);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public bool existeActiva(string nombre, int idActual)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
            SELECT IdMarca
            FROM Marcas
            WHERE UPPER(LTRIM(RTRIM(Nombre))) =
                  UPPER(LTRIM(RTRIM(@Nombre)))
            AND Activo = 1
            AND IdMarca <> @Id");

                datos.setParametro("@Nombre", nombre);
                datos.setParametro("@Id", idActual);

                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}