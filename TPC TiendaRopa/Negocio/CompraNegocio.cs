using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class CompraNegocio
    {
        public List<Compra> listar(
        int idProveedor = 0,
        string numeroComprobante = "",
        DateTime? fechaDesde = null,
        DateTime? fechaHasta = null)
            {
                List<Compra> lista = new List<Compra>();

                AccesoDatos datos = new AccesoDatos();

                try
                {
                    string consulta = @"
                SELECT
                    C.IdCompra,
                    C.FechaCompra,
                    C.TipoComprobante,
                    C.NumeroComprobante,
                    C.Total,

                    U.IdUsuario,
                    U.Nombre,
                    U.Apellido,

                    P.IdProveedor,
                    P.RazonSocial

                FROM Compras C

                INNER JOIN Usuarios U
                    ON C.IdUsuario = U.IdUsuario

                INNER JOIN Proveedores P
                    ON C.IdProveedor = P.IdProveedor

                WHERE 1 = 1 ";

                    if (idProveedor > 0)
                        consulta += " AND C.IdProveedor = @IdProveedor ";

                    if (!string.IsNullOrWhiteSpace(numeroComprobante))
                        consulta += " AND C.NumeroComprobante LIKE @Comprobante ";

                    if (fechaDesde.HasValue)
                        consulta += " AND CAST(C.FechaCompra AS DATE) >= @FechaDesde ";

                    if (fechaHasta.HasValue)
                        consulta += " AND CAST(C.FechaCompra AS DATE) <= @FechaHasta ";

                    consulta += " ORDER BY C.FechaCompra DESC ";

                    datos.setConsulta(consulta);

                    if (idProveedor > 0)
                        datos.setParametro(
                            "@IdProveedor",
                            idProveedor);

                    if (!string.IsNullOrWhiteSpace(numeroComprobante))
                        datos.setParametro(
                            "@Comprobante",
                            "%" + numeroComprobante + "%");

                    if (fechaDesde.HasValue)
                        datos.setParametro(
                            "@FechaDesde",
                            fechaDesde.Value.Date);

                    if (fechaHasta.HasValue)
                        datos.setParametro(
                            "@FechaHasta",
                            fechaHasta.Value.Date);

                    datos.ejecutarLectura();

                    while (datos.Lector.Read())
                    {
                        Compra compra = new Compra();

                        compra.Id =
                            (int)datos.Lector["IdCompra"];

                        compra.Fecha =
                            (DateTime)datos.Lector["FechaCompra"];

                        compra.TipoComprobante =
                            datos.Lector["TipoComprobante"].ToString();

                        compra.NumeroComprobante =
                            datos.Lector["NumeroComprobante"].ToString();

                        compra.Total =
                            (decimal)datos.Lector["Total"];

                        compra.Usuario =
                            new Usuario();

                        compra.Usuario.Id =
                            (int)datos.Lector["IdUsuario"];

                        compra.Usuario.Nombre =
                            datos.Lector["Nombre"].ToString();

                        compra.Usuario.Apellido =
                            datos.Lector["Apellido"].ToString();

                        compra.Proveedor =
                            new Proveedor();

                        compra.Proveedor.Id =
                            (int)datos.Lector["IdProveedor"];

                        compra.Proveedor.RazonSocial =
                            datos.Lector["RazonSocial"].ToString();

                        lista.Add(compra);
                    }

                    return lista;
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }


        public Compra obtenerCompra(int idCompra)
        {
            Compra compra = null;

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    SELECT
                        C.IdCompra,
                        C.FechaCompra,
                        C.TipoComprobante,
                        C.NumeroComprobante,
                        C.Total,

                        U.IdUsuario,
                        U.Nombre,
                        U.Apellido,

                        P.IdProveedor,
                        P.RazonSocial

                    FROM Compras C

                    INNER JOIN Usuarios U
                        ON C.IdUsuario = U.IdUsuario

                    INNER JOIN Proveedores P
                        ON C.IdProveedor = P.IdProveedor

                    WHERE C.IdCompra = @Id");

                datos.setParametro("@Id", idCompra);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    compra = new Compra();

                    compra.Id =
                        (int)datos.Lector["IdCompra"];

                    compra.Fecha =
                        (DateTime)datos.Lector["FechaCompra"];

                    compra.TipoComprobante =
                        datos.Lector["TipoComprobante"].ToString();

                    compra.NumeroComprobante =
                        datos.Lector["NumeroComprobante"].ToString();

                    compra.Total =
                        (decimal)datos.Lector["Total"];

                    compra.Usuario =
                        new Usuario();

                    compra.Usuario.Id =
                        (int)datos.Lector["IdUsuario"];

                    compra.Usuario.Nombre =
                        datos.Lector["Nombre"].ToString();

                    compra.Usuario.Apellido =
                        datos.Lector["Apellido"].ToString();

                    compra.Proveedor =
                        new Proveedor();

                    compra.Proveedor.Id =
                        (int)datos.Lector["IdProveedor"];

                    compra.Proveedor.RazonSocial =
                        datos.Lector["RazonSocial"].ToString();
                }

                return compra;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<DetalleCompra> obtenerDetalle(int idCompra)
        {
            List<DetalleCompra> lista =
                new List<DetalleCompra>();

            AccesoDatos datos =
                new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    SELECT
                        DC.Cantidad,
                        DC.PrecioCompra,
                        DC.Subtotal,

                        P.IdProducto,
                        P.Nombre

                    FROM DetalleCompras DC

                    INNER JOIN Productos P
                        ON DC.IdProducto = P.IdProducto

                    WHERE DC.IdCompra = @IdCompra");

                datos.setParametro(
                    "@IdCompra",
                    idCompra);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleCompra detalle =
                        new DetalleCompra();

                    detalle.Producto =
                        new Producto();

                    detalle.Producto.Id =
                        (int)datos.Lector["IdProducto"];

                    detalle.Producto.Nombre =
                        datos.Lector["Nombre"].ToString();

                    detalle.Cantidad =
                        (int)datos.Lector["Cantidad"];

                    detalle.PrecioCompra =
                        (decimal)datos.Lector["PrecioCompra"];

                    lista.Add(detalle);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void guardar(Compra compra)
        {
            int idCompra = 0;

            AccesoDatos datos =
                new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    INSERT INTO Compras
                    (
                        IdUsuario,
                        IdProveedor,
                        TipoComprobante,
                        NumeroComprobante,
                        Total
                    )
                    OUTPUT INSERTED.IdCompra
                    VALUES
                    (
                        @Usuario,
                        @Proveedor,
                        @TipoComprobante,
                        @NumeroComprobante,
                        @Total
                    )");

                datos.setParametro(
                    "@Usuario",
                    compra.Usuario.Id);

                datos.setParametro(
                    "@Proveedor",
                    compra.Proveedor.Id);

                datos.setParametro(
                    "@TipoComprobante",
                    compra.TipoComprobante);

                datos.setParametro(
                    "@NumeroComprobante",
                    compra.NumeroComprobante);

                datos.setParametro(
                    "@Total",
                    compra.Total);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    idCompra = (int)datos.Lector[0];
            }
            finally
            {
                datos.cerrarConexion();
            }

            foreach (DetalleCompra detalle
                in compra.Detalles)
            {
                guardarDetalle(
                    idCompra,
                    detalle);

                registrarIngresoStock(
                    detalle);

                actualizarStock(
                    detalle);
            }
        }

        private void guardarDetalle(
            int idCompra,
            DetalleCompra detalle)
        {
            AccesoDatos datos =
                new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    INSERT INTO DetalleCompras
                    (
                        IdCompra,
                        IdProducto,
                        Cantidad,
                        PrecioCompra,
                        Subtotal
                    )
                    VALUES
                    (
                        @Compra,
                        @Producto,
                        @Cantidad,
                        @Precio,
                        @Subtotal
                    )");

                datos.setParametro(
                    "@Compra",
                    idCompra);

                datos.setParametro(
                    "@Producto",
                    detalle.Producto.Id);

                datos.setParametro(
                    "@Cantidad",
                    detalle.Cantidad);

                datos.setParametro(
                    "@Precio",
                    detalle.PrecioCompra);

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

        private void registrarIngresoStock(
            DetalleCompra detalle)
        {
            AccesoDatos datos =
                new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    INSERT INTO IngresosStock
                    (
                        IdProducto,
                        Cantidad,
                        PrecioCompra
                    )
                    VALUES
                    (
                        @Producto,
                        @Cantidad,
                        @Precio
                    )");

                datos.setParametro(
                    "@Producto",
                    detalle.Producto.Id);

                datos.setParametro(
                    "@Cantidad",
                    detalle.Cantidad);

                datos.setParametro(
                    "@Precio",
                    detalle.PrecioCompra);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private void actualizarStock(
            DetalleCompra detalle)
        {
            AccesoDatos datos =
                new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    UPDATE Productos
                    SET StockActual =
                        StockActual + @Cantidad
                    WHERE IdProducto = @Producto");

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
    }
}