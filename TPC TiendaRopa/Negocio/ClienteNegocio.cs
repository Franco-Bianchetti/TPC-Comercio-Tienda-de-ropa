using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
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
                    WHERE Activo = 1
                    ORDER BY IdCliente");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();

                    aux.Id =
                        (int)datos.Lector["IdCliente"];

                    aux.Nombre =
                        datos.Lector["Nombre"].ToString();

                    aux.Apellido =
                        datos.Lector["Apellido"].ToString();

                    aux.Dni =
                        datos.Lector["Dni"].ToString();

                    aux.Telefono =
                        datos.Lector["Telefono"].ToString();

                    aux.Email =
                        datos.Lector["Email"].ToString();

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Cliente buscar(int id)
        {
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
                    WHERE IdCliente = @Id AND Activo = 1");

                datos.setParametro("@Id", id);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();

                    aux.Id =
                        (int)datos.Lector["IdCliente"];

                    aux.Nombre =
                        datos.Lector["Nombre"].ToString();

                    aux.Apellido =
                        datos.Lector["Apellido"].ToString();

                    aux.Dni =
                        datos.Lector["Dni"].ToString();

                    aux.Telefono =
                        datos.Lector["Telefono"].ToString();

                    aux.Email =
                        datos.Lector["Email"].ToString();

                    return aux;
                }

                return null;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool existeDni(
            string dni,
            int idActual)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    SELECT IdCliente
                    FROM Clientes
                    WHERE Dni = @Dni
                    AND IdCliente <> @Id");

                datos.setParametro("@Dni", dni);
                datos.setParametro("@Id", idActual);

                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool existeEmail(
            string email,
            int idActual)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    SELECT IdCliente
                    FROM Clientes
                    WHERE Email = @Email
                    AND IdCliente <> @Id");

                datos.setParametro("@Email", email);
                datos.setParametro("@Id", idActual);

                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregar(
            Cliente nuevo)
        {
            AccesoDatos datos =
                new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    INSERT INTO Clientes
                    (
                        Nombre,
                        Apellido,
                        Dni,
                        Telefono,
                        Email
                    )
                    VALUES
                    (
                        @Nombre,
                        @Apellido,
                        @Dni,
                        @Telefono,
                        @Email
                    )");

                datos.setParametro(
                    "@Nombre",
                    nuevo.Nombre);

                datos.setParametro(
                    "@Apellido",
                    nuevo.Apellido);

                datos.setParametro(
                    "@Dni",
                    nuevo.Dni);

                datos.setParametro(
                    "@Telefono",
                    nuevo.Telefono);

                datos.setParametro(
                    "@Email",
                    nuevo.Email);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(
            Cliente cliente)
        {
            AccesoDatos datos =
                new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    UPDATE Clientes
                    SET
                        Nombre = @Nombre,
                        Apellido = @Apellido,
                        Dni = @Dni,
                        Telefono = @Telefono,
                        Email = @Email
                    WHERE IdCliente = @Id");

                datos.setParametro(
                    "@Nombre",
                    cliente.Nombre);

                datos.setParametro(
                    "@Apellido",
                    cliente.Apellido);

                datos.setParametro(
                    "@Dni",
                    cliente.Dni);

                datos.setParametro(
                    "@Telefono",
                    cliente.Telefono);

                datos.setParametro(
                    "@Email",
                    cliente.Email);

                datos.setParametro(
                    "@Id",
                    cliente.Id);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(
            int id)
        {
            AccesoDatos datos =
                new AccesoDatos();

            try
            {
                datos.setConsulta(
                    "UPDATE Clientes SET Activo = 0 WHERE IdCliente = @Id");

                datos.setParametro(
                    "@Id",
                    id);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}