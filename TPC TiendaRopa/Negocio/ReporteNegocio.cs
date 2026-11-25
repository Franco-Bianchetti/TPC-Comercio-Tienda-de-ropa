using System;
using System.Data;
using Dominio;

namespace Negocio
{
    public class ReporteNegocio
    {
        public DataTable obtenerRankingClientes(DateTime desde, DateTime hasta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"
                    SELECT
                        C.Dni AS DNI,
                        C.Nombre,
                        C.Apellido,
                        ISNULL(C.Email, '') AS Email,
                        ISNULL(C.Telefono, '') AS [Teléfono],
                        COUNT(DISTINCT V.IdVenta) AS [Cant. Compras],
                        ISNULL(SUM(DV.Cantidad), 0) AS [Unidades Compradas],
                        ISNULL(SUM(V.Total), 0) AS [Total Gastado]
                    FROM Ventas V
                    INNER JOIN Clientes C ON C.IdCliente = V.IdCliente
                    LEFT JOIN DetalleVentas DV ON DV.IdVenta = V.IdVenta
                    WHERE V.TipoFactura <> 'PRESUPUESTO'
                      AND CAST(V.FechaVenta AS DATE) >= @Desde
                      AND CAST(V.FechaVenta AS DATE) <= @Hasta
                    GROUP BY C.IdCliente, C.Dni, C.Nombre, C.Apellido, C.Email, C.Telefono
                    ORDER BY [Total Gastado] DESC, [Cant. Compras] DESC");

                datos.setParametro("@Desde", desde.Date);
                datos.setParametro("@Hasta", hasta.Date);
                datos.ejecutarLectura();

                DataTable dt = new DataTable();
                dt.Load(datos.Lector);
                return dt;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public DataTable obtenerRankingVendedores(DateTime desde, DateTime hasta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"
                    SELECT
                        U.Codigo AS [Código],
                        U.Nombre,
                        U.Apellido,
                        COUNT(DISTINCT V.IdVenta) AS [Cant. Ventas],
                        ISNULL(SUM(DV.Cantidad), 0) AS [Unidades Vendidas],
                        ISNULL(SUM(V.Total), 0) AS [Total Vendido]
                    FROM Ventas V
                    INNER JOIN Usuarios U ON U.IdUsuario = V.IdUsuario
                    LEFT JOIN DetalleVentas DV ON DV.IdVenta = V.IdVenta
                    WHERE V.TipoFactura <> 'PRESUPUESTO'
                      AND CAST(V.FechaVenta AS DATE) >= @Desde
                      AND CAST(V.FechaVenta AS DATE) <= @Hasta
                    GROUP BY U.IdUsuario, U.Codigo, U.Nombre, U.Apellido
                    ORDER BY [Total Vendido] DESC, [Cant. Ventas] DESC");

                datos.setParametro("@Desde", desde.Date);
                datos.setParametro("@Hasta", hasta.Date);
                datos.ejecutarLectura();

                DataTable dt = new DataTable();
                dt.Load(datos.Lector);
                return dt;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public DataTable obtenerTopProductos(DateTime desde, DateTime hasta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"
                    SELECT
                        (P.Codigo + ' | ' + P.Nombre + ' | ' + P.Talle + ' | ' + P.Color) AS Producto,
                        M.Nombre AS Marca,
                        T.Nombre AS Tipo,
                        ISNULL(SUM(DV.Cantidad), 0) AS [Unidades Vendidas],
                        ISNULL(SUM(DV.Subtotal), 0) AS [Total Facturado]
                    FROM DetalleVentas DV
                    INNER JOIN Ventas V ON V.IdVenta = DV.IdVenta
                    INNER JOIN Productos P ON P.IdProducto = DV.IdProducto
                    INNER JOIN Marcas M ON M.IdMarca = P.IdMarca
                    INNER JOIN Tipos T ON T.IdTipo = P.IdTipo
                    WHERE V.TipoFactura <> 'PRESUPUESTO'
                      AND CAST(V.FechaVenta AS DATE) >= @Desde
                      AND CAST(V.FechaVenta AS DATE) <= @Hasta
                    GROUP BY P.IdProducto, P.Codigo, P.Nombre, P.Talle, P.Color, M.Nombre, T.Nombre
                    ORDER BY [Unidades Vendidas] DESC, [Total Facturado] DESC");

                datos.setParametro("@Desde", desde.Date);
                datos.setParametro("@Hasta", hasta.Date);
                datos.ejecutarLectura();

                DataTable dt = new DataTable();
                dt.Load(datos.Lector);
                return dt;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public DataTable obtenerBalanceVentasVsCompras(DateTime desde, DateTime hasta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"
                    WITH VentasPeriodo AS (
                        SELECT
                            CAST(FechaVenta AS DATE) AS Fecha,
                            COUNT(IdVenta) AS CantVentas,
                            ISNULL(SUM(Total), 0) AS TotalVentas
                        FROM Ventas
                        WHERE TipoFactura <> 'PRESUPUESTO'
                          AND CAST(FechaVenta AS DATE) >= @Desde
                          AND CAST(FechaVenta AS DATE) <= @Hasta
                        GROUP BY CAST(FechaVenta AS DATE)
                    ),
                    ComprasPeriodo AS (
                        SELECT
                            CAST(FechaCompra AS DATE) AS Fecha,
                            COUNT(IdCompra) AS CantCompras,
                            ISNULL(SUM(Total), 0) AS TotalCompras
                        FROM Compras
                        WHERE CAST(FechaCompra AS DATE) >= @Desde
                          AND CAST(FechaCompra AS DATE) <= @Hasta
                        GROUP BY CAST(FechaCompra AS DATE)
                    ),
                    FechasUnidas AS (
                        SELECT Fecha FROM VentasPeriodo
                        UNION
                        SELECT Fecha FROM ComprasPeriodo
                    )
                    SELECT
                        F.Fecha,
                        ISNULL(VP.CantVentas, 0) AS [Cant. Ventas],
                        ISNULL(VP.TotalVentas, 0) AS [Ingresos por Ventas],
                        ISNULL(CP.CantCompras, 0) AS [Cant. Compras],
                        ISNULL(CP.TotalCompras, 0) AS [Egresos por Compras],
                        (ISNULL(VP.TotalVentas, 0) - ISNULL(CP.TotalCompras, 0)) AS [Diferencia]
                    FROM FechasUnidas F
                    LEFT JOIN VentasPeriodo VP ON VP.Fecha = F.Fecha
                    LEFT JOIN ComprasPeriodo CP ON CP.Fecha = F.Fecha
                    ORDER BY F.Fecha DESC");

                datos.setParametro("@Desde", desde.Date);
                datos.setParametro("@Hasta", hasta.Date);
                datos.ejecutarLectura();

                DataTable dt = new DataTable();
                dt.Load(datos.Lector);
                return dt;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public DataTable obtenerProductosBajoStock()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"
                    SELECT
                        (P.Codigo + ' | ' + P.Nombre + ' | ' + P.Talle + ' | ' + P.Color) AS Producto,
                        M.Nombre AS Marca,
                        PR.RazonSocial AS Proveedor,
                        P.StockActual AS [Stock Actual],
                        P.StockMinimo AS [Stock Mínimo],
                        CASE
                            WHEN (P.StockMinimo - P.StockActual) > 0 THEN (P.StockMinimo - P.StockActual)
                            ELSE 1
                        END AS [Unidades a Reponer]
                    FROM Productos P
                    INNER JOIN Marcas M ON M.IdMarca = P.IdMarca
                    INNER JOIN Proveedores PR ON PR.IdProveedor = P.IdProveedor
                    WHERE P.Activo = 1
                      AND P.StockActual <= P.StockMinimo
                    ORDER BY [Unidades a Reponer] DESC, Producto ASC");

                datos.ejecutarLectura();

                DataTable dt = new DataTable();
                dt.Load(datos.Lector);
                return dt;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public DataTable obtenerVentasPorMedioPago(DateTime desde, DateTime hasta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"
                    SELECT
                        ISNULL(V.FormaPago, 'SIN ESPECIFICAR') AS [Medio de Pago],
                        COUNT(DISTINCT V.IdVenta) AS [Cant. Ventas],
                        ISNULL(SUM(DV.Cantidad), 0) AS [Unidades Vendidas],
                        ISNULL(SUM(V.Total), 0) AS [Total Cobrado]
                    FROM Ventas V
                    LEFT JOIN DetalleVentas DV ON DV.IdVenta = V.IdVenta
                    WHERE V.TipoFactura <> 'PRESUPUESTO'
                      AND CAST(V.FechaVenta AS DATE) >= @Desde
                      AND CAST(V.FechaVenta AS DATE) <= @Hasta
                    GROUP BY V.FormaPago
                    ORDER BY [Total Cobrado] DESC, [Cant. Ventas] DESC");

                datos.setParametro("@Desde", desde.Date);
                datos.setParametro("@Hasta", hasta.Date);
                datos.ejecutarLectura();

                DataTable dt = new DataTable();
                dt.Load(datos.Lector);
                return dt;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}