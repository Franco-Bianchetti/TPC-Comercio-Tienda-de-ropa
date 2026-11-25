using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class ProveedorNegocio
    {
        public List<Proveedor> listar()
        {
            List<Proveedor> lista = new List<Proveedor>();

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    SELECT
                        IdProveedor,
                        RazonSocial,
                        Cuit,
                        Telefono,
                        Email
                    FROM Proveedores
                    WHERE Activo = 1
                    ORDER BY IdProveedor");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();

                    aux.Id = (int)datos.Lector["IdProveedor"];
                    aux.RazonSocial = datos.Lector["RazonSocial"].ToString();
                    aux.Cuit = datos.Lector["Cuit"].ToString();
                    aux.Telefono = datos.Lector["Telefono"].ToString();
                    aux.Email = datos.Lector["Email"].ToString();

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public Proveedor buscar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    SELECT
                        IdProveedor,
                        RazonSocial,
                        Cuit,
                        Telefono,
                        Email
                    FROM Proveedores
                    WHERE IdProveedor = @Id
                    AND Activo = 1");

                datos.setParametro("@Id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();

                    aux.Id = (int)datos.Lector["IdProveedor"];
                    aux.RazonSocial = datos.Lector["RazonSocial"].ToString();
                    aux.Cuit = datos.Lector["Cuit"] != DBNull.Value ? datos.Lector["Cuit"].ToString() : "";
                    aux.Telefono = datos.Lector["Telefono"] != DBNull.Value ? datos.Lector["Telefono"].ToString() : "";
                    aux.Email = datos.Lector["Email"] != DBNull.Value ? datos.Lector["Email"].ToString() : "";

                    return aux;
                }

                return null;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool existeCuit(string cuit, int idActual)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    SELECT IdProveedor
                    FROM Proveedores
                    WHERE Cuit = @Cuit
                    AND Activo = 1
                    AND IdProveedor <> @Id");

                datos.setParametro("@Cuit", cuit);
                datos.setParametro("@Id", idActual);
                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregar(Proveedor nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    INSERT INTO Proveedores
                    (
                        RazonSocial,
                        Cuit,
                        Telefono,
                        Email,
                        Activo
                    )
                    VALUES
                    (
                        @RazonSocial,
                        @Cuit,
                        @Telefono,
                        @Email,
                        1
                    )");

                datos.setParametro("@RazonSocial", nuevo.RazonSocial);
                datos.setParametro("@Cuit", nuevo.Cuit);
                datos.setParametro("@Telefono", (object)nuevo.Telefono ?? DBNull.Value);
                datos.setParametro("@Email", (object)nuevo.Email ?? DBNull.Value);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Proveedor proveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    UPDATE Proveedores
                    SET RazonSocial = @RazonSocial,
                        Cuit = @Cuit,
                        Telefono = @Telefono,
                        Email = @Email
                    WHERE IdProveedor = @Id");

                datos.setParametro("@RazonSocial", proveedor.RazonSocial);
                datos.setParametro("@Cuit", proveedor.Cuit);
                datos.setParametro("@Telefono", (object)proveedor.Telefono ?? DBNull.Value);
                datos.setParametro("@Email", (object)proveedor.Email ?? DBNull.Value);
                datos.setParametro("@Id", proveedor.Id);

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
                    UPDATE Proveedores
                    SET Activo = 0
                    WHERE IdProveedor = @Id");

                datos.setParametro("@Id", id);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}