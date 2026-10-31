using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class ProductoNegocio
    {
        public List<Producto> listar()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    SELECT
                        P.IdProducto,
                        P.Nombre,
                        P.Talle,
                        P.Color,
                        P.StockActual,
                        P.StockMinimo,
                        P.PorcentajeGanancia,

                        M.IdMarca,
                        M.Nombre AS Marca,

                        T.IdTipo,
                        T.Nombre AS Tipo

                    FROM Productos P
                    INNER JOIN Marcas M
                        ON P.IdMarca = M.IdMarca
                    INNER JOIN Tipos T
                        ON P.IdTipo = T.IdTipo
                    ORDER BY P.IdProducto
                ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();

                    aux.Id =
                        (int)datos.Lector["IdProducto"];

                    aux.Nombre =
                        datos.Lector["Nombre"].ToString();

                    aux.Talle =
                        datos.Lector["Talle"].ToString();

                    aux.Color =
                        datos.Lector["Color"].ToString();

                    aux.StockActual =
                        (int)datos.Lector["StockActual"];

                    aux.StockMinimo =
                        (int)datos.Lector["StockMinimo"];

                    aux.PorcentajeGanancia =
                        (decimal)datos.Lector["PorcentajeGanancia"];

                    aux.Marca = new Marca();
                    aux.Marca.Id =
                        (int)datos.Lector["IdMarca"];
                    aux.Marca.Nombre =
                        datos.Lector["Marca"].ToString();

                    aux.Tipo = new Tipo();
                    aux.Tipo.Id =
                        (int)datos.Lector["IdTipo"];
                    aux.Tipo.Nombre =
                        datos.Lector["Tipo"].ToString();

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