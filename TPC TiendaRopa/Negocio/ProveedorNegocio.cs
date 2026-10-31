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
    }
}