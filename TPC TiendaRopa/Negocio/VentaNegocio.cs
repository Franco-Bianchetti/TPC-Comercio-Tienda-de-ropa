using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class VentaNegocio
    {
        public string ObtenerProximoNumeroComprobante()
        {
            AccesoDatos datos =
                new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    SELECT
                        ISNULL(MAX(IdVenta),0) + 1
                    FROM Ventas");

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int numero =
                        Convert.ToInt32(
                            datos.Lector[0]);

                    return "0001-" +
                        numero.ToString("00000000");
                }

                return "0001-00000001";
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Venta> listar()
        {
            List<Venta> lista =
                new List<Venta>();

            AccesoDatos datos =
                new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    SELECT

                        V.IdVenta,
                        V.NumeroComprobante,
                        V.TipoFactura,
                        V.FormaPago,
                        V.FechaVenta,
                        V.Total,
                        V.CAE,
                        V.VencimientoCAE,

                        C.IdCliente,
                        C.Nombre,
                        C.Apellido,

                        U.IdUsuario,
                        U.Nombre AS NombreUsuario,
                        U.Apellido AS ApellidoUsuario

                    FROM Ventas V

                    INNER JOIN Clientes C
                        ON V.IdCliente = C.IdCliente

                    INNER JOIN Usuarios U
                        ON V.IdUsuario = U.IdUsuario

                    ORDER BY
                        V.FechaVenta DESC");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta venta =
                        new Venta();

                    venta.Id =
                        (int)datos.Lector["IdVenta"];

                    venta.NumeroComprobante =
                        datos.Lector["NumeroComprobante"]
                            .ToString();

                    venta.TipoFactura =
                        datos.Lector["TipoFactura"]
                            .ToString();

                    venta.FormaPago =
                        datos.Lector["FormaPago"]
                            .ToString();

                    venta.CAE =
                        datos.Lector["CAE"]
                            .ToString();

                    if (!(datos.Lector["VencimientoCAE"]
                        is DBNull))
                    {
                        venta.VencimientoCAE =
                            (DateTime)
                            datos.Lector["VencimientoCAE"];
                    }

                    venta.Fecha =
                        (DateTime)datos.Lector["FechaVenta"];

                    venta.Total =
                        (decimal)datos.Lector["Total"];

                    venta.Cliente =
                        new Cliente();

                    venta.Cliente.Id =
                        (int)datos.Lector["IdCliente"];

                    venta.Cliente.Nombre =
                        datos.Lector["Nombre"]
                            .ToString();

                    venta.Cliente.Apellido =
                        datos.Lector["Apellido"]
                            .ToString();

                    venta.Vendedor =
                        new Usuario();

                    venta.Vendedor.Id =
                        (int)datos.Lector["IdUsuario"];

                    venta.Vendedor.Nombre =
                        datos.Lector["NombreUsuario"]
                            .ToString();

                    venta.Vendedor.Apellido =
                        datos.Lector["ApellidoUsuario"]
                            .ToString();

                    lista.Add(venta);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void guardar(Venta venta)
        {
            int idVenta = 0;

            AccesoDatos datos =
                new AccesoDatos();

            try
            {
                datos.setConsulta(@"
            INSERT INTO Ventas
            (
                NumeroComprobante,
                TipoFactura,
                FormaPago,
                FechaVenta,
                IdCliente,
                IdUsuario,
                Total,
                CAE,
                VencimientoCAE
            )

            OUTPUT INSERTED.IdVenta

            VALUES
            (
                @NumeroComprobante,
                @TipoFactura,
                @FormaPago,
                @FechaVenta,
                @Cliente,
                @Usuario,
                @Total,
                @CAE,
                @VencimientoCAE
            )");

                datos.setParametro(
                    "@NumeroComprobante",
                    venta.NumeroComprobante);

                datos.setParametro(
                    "@TipoFactura",
                    venta.TipoFactura);

                datos.setParametro(
                    "@FormaPago",
                    venta.FormaPago);

                datos.setParametro(
                    "@FechaVenta",
                    venta.Fecha);

                datos.setParametro(
                    "@Cliente",
                    venta.Cliente.Id);

                datos.setParametro(
                    "@Usuario",
                    venta.Vendedor.Id);

                datos.setParametro(
                    "@Total",
                    venta.Total);

                datos.setParametro(
                    "@CAE",
                    venta.CAE);

                datos.setParametro(
                    "@VencimientoCAE",
                    venta.VencimientoCAE);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    idVenta =
                        (int)datos.Lector[0];
                }
            }
            finally
            {
                datos.cerrarConexion();
            }

            foreach (DetalleVenta detalle
                in venta.Detalles)
            {
                guardarDetalle(
                    idVenta,
                    detalle);

                descontarStock(
                    detalle);
            }
        }

        private void guardarDetalle(
            int idVenta,
            DetalleVenta detalle)
        {
            AccesoDatos datos =
                new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    INSERT INTO DetalleVentas
                    (
                        IdVenta,
                        IdProducto,
                        Cantidad,
                        PrecioUnitario,
                        Subtotal
                    )
                    VALUES
                    (
                        @Venta,
                        @Producto,
                        @Cantidad,
                        @Precio,
                        @Subtotal
                    )");

                datos.setParametro(
                    "@Venta",
                    idVenta);

                datos.setParametro(
                    "@Producto",
                    detalle.Producto.Id);

                datos.setParametro(
                    "@Cantidad",
                    detalle.Cantidad);

                datos.setParametro(
                    "@Precio",
                    detalle.PrecioUnitario);

                datos.setParametro(
                    "@Subtotal",
                    detalle.Subtotal);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public Producto obtenerProducto(int id)
        {
            ProductoNegocio negocio =
                new ProductoNegocio();

            return negocio.buscar(id);
        }

        private void descontarStock(
            DetalleVenta detalle)
        {
            AccesoDatos datos =
                new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    UPDATE Productos
                    SET StockActual =
                        StockActual - @Cantidad
                    WHERE IdProducto =
                        @Producto");

                datos.setParametro(
                    "@Cantidad",
                    detalle.Cantidad);

                datos.setParametro(
                    "@Producto",
                    detalle.Producto.Id);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Venta obtenerVenta(int idVenta)
        {
            Venta venta = null;

            AccesoDatos datos =
                new AccesoDatos();

            try
            {
                datos.setConsulta(@"
            SELECT

                V.IdVenta,
                V.NumeroComprobante,
                V.TipoFactura,
                V.FormaPago,
                V.FechaVenta,
                V.Total,
                V.CAE,
                V.VencimientoCAE,

                C.IdCliente,
                C.Nombre,
                C.Apellido,

                U.IdUsuario,
                U.Nombre AS NombreUsuario,
                U.Apellido AS ApellidoUsuario

            FROM Ventas V

            INNER JOIN Clientes C
                ON V.IdCliente = C.IdCliente

            INNER JOIN Usuarios U
                ON V.IdUsuario = U.IdUsuario

            WHERE
                V.IdVenta = @Id");

                datos.setParametro(
                    "@Id",
                    idVenta);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    venta = new Venta();

                    venta.Id =
                        (int)datos.Lector["IdVenta"];

                    venta.NumeroComprobante =
                        datos.Lector["NumeroComprobante"]
                            .ToString();

                    venta.TipoFactura =
                        datos.Lector["TipoFactura"]
                            .ToString();

                    venta.FormaPago =
                        datos.Lector["FormaPago"]
                            .ToString();

                    venta.CAE =
                        datos.Lector["CAE"]
                            .ToString();

                    if (!(datos.Lector["VencimientoCAE"]
                        is DBNull))
                    {
                        venta.VencimientoCAE =
                            (DateTime)
                            datos.Lector["VencimientoCAE"];
                    }

                    venta.Fecha =
                        (DateTime)
                        datos.Lector["FechaVenta"];

                    venta.Total =
                        (decimal)
                        datos.Lector["Total"];

                    venta.Cliente =
                        new Cliente();

                    venta.Cliente.Id =
                        (int)
                        datos.Lector["IdCliente"];

                    venta.Cliente.Nombre =
                        datos.Lector["Nombre"]
                            .ToString();

                    venta.Cliente.Apellido =
                        datos.Lector["Apellido"]
                            .ToString();

                    venta.Vendedor =
                        new Usuario();

                    venta.Vendedor.Id =
                        (int)
                        datos.Lector["IdUsuario"];

                    venta.Vendedor.Nombre =
                        datos.Lector["NombreUsuario"]
                            .ToString();

                    venta.Vendedor.Apellido =
                        datos.Lector["ApellidoUsuario"]
                            .ToString();
                }

                return venta;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<DetalleVenta> obtenerDetalle(
    int idVenta)
        {
            List<DetalleVenta> lista =
                new List<DetalleVenta>();

            AccesoDatos datos =
                new AccesoDatos();

            try
            {
                datos.setConsulta(@"
            SELECT

                DV.IdDetalleVenta,
                DV.Cantidad,
                DV.PrecioUnitario,
                DV.Subtotal,

                P.IdProducto,
                P.Nombre

            FROM DetalleVentas DV

            INNER JOIN Productos P
                ON DV.IdProducto =
                   P.IdProducto

            WHERE
                DV.IdVenta = @Id");

                datos.setParametro(
                    "@Id",
                    idVenta);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleVenta detalle =
                        new DetalleVenta();

                    detalle.Id =
                        (int)datos.Lector["IdDetalleVenta"];

                    detalle.Producto =
                        new Producto();

                    detalle.Producto.Id =
                        (int)datos.Lector["IdProducto"];

                    detalle.Producto.Nombre =
                        datos.Lector["Nombre"]
                            .ToString();

                    detalle.Cantidad =
                        (int)datos.Lector["Cantidad"];

                    detalle.PrecioUnitario =
                        (decimal)datos.Lector["PrecioUnitario"];

                    lista.Add(detalle);
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