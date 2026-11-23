using Dominio;
using System;

namespace Negocio
{
    public class EmpresaNegocio
    {
        public Empresa obtener()
        {
            Empresa empresa =
                null;

            AccesoDatos datos =
                new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    SELECT TOP 1

                        IdEmpresa,
                        RazonSocial,
                        CUIT,
                        IngresosBrutos,
                        Direccion,
                        CondicionIVA,
                        PuntoVenta,
                        FechaInicioActividad,
                        Ciudad

                    FROM Empresa");

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    empresa =
                        new Empresa();

                    empresa.IdEmpresa =
                        (int)datos.Lector["IdEmpresa"];

                    empresa.RazonSocial =
                        datos.Lector["RazonSocial"]
                            .ToString();

                    empresa.CUIT =
                        datos.Lector["CUIT"]
                            .ToString();

                    empresa.IngresosBrutos =
                        datos.Lector["IngresosBrutos"]
                            .ToString();

                    empresa.Direccion =
                        datos.Lector["Direccion"]
                            .ToString();

                    empresa.CondicionIVA =
                        datos.Lector["CondicionIVA"]
                            .ToString();

                    empresa.PuntoVenta =
                        datos.Lector["PuntoVenta"]
                            .ToString();

                    empresa.FechaInicioActividad =
                        (DateTime)
                        datos.Lector["FechaInicioActividad"];

                    empresa.Ciudad =
                        datos.Lector["Ciudad"]
                            .ToString();
                }

                return empresa;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}