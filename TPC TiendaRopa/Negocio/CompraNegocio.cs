using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio {
    public class CompraNegocio {

        public List<Compra> listar(int idProveedor = 0)
        {
            List<Compra> lista = new List<Compra>();

            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"
            SELECT
                C.IdCompra,
                C.FechaCompra,
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
        ";

                if (idProveedor > 0)
                {
                    consulta += " WHERE C.IdProveedor = @IdProveedor ";
                }

                consulta += " ORDER BY C.FechaCompra DESC ";

                datos.setConsulta(consulta);

                if (idProveedor > 0)
                {
                    datos.setParametro("@IdProveedor", idProveedor);
                }

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Compra compra = new Compra();

                    compra.Id = (int)datos.Lector["IdCompra"];

                    compra.Fecha = (DateTime)datos.Lector["FechaCompra"];

                    compra.Total = (decimal)datos.Lector["Total"];

                    compra.Usuario = new Usuario();

                    compra.Usuario.Id = (int)datos.Lector["IdUsuario"];

                    compra.Usuario.Nombre =
                        datos.Lector["Nombre"].ToString();

                    compra.Usuario.Apellido =
                        datos.Lector["Apellido"].ToString();

                    compra.Proveedor = new Proveedor();

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


        public void guardar(
            Compra compra) {
            int idCompra = 0;

            AccesoDatos datos =
                new AccesoDatos();

            try {
                datos.setConsulta(@"
                    INSERT INTO Compras
                    (
                        IdUsuario,
                        IdProveedor,
                        Total
                    )
                    OUTPUT INSERTED.IdCompra
                    VALUES
                    (
                        @Usuario,
                        @Proveedor,
                        @Total
                    )");

                datos.setParametro(
                    "@Usuario",
                    compra.Usuario.Id);

                datos.setParametro(
                    "@Proveedor",
                    compra.Proveedor.Id);

                datos.setParametro(
                    "@Total",
                    compra.Total);

                datos.ejecutarLectura();

                if (datos.Lector.Read()) {
                    idCompra =
                        (int)datos.Lector[0];
                }
            }
            finally {
                datos.cerrarConexion();
            }

            foreach (
                DetalleCompra detalle
                in compra.Detalles) {
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
            DetalleCompra detalle) {
            AccesoDatos datos =
                new AccesoDatos();

            try {
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
            finally {
                datos.cerrarConexion();
            }
        }

        private void registrarIngresoStock(
            DetalleCompra detalle) {
            AccesoDatos datos =
                new AccesoDatos();

            try {
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
            finally {
                datos.cerrarConexion();
            }
        }

        private void actualizarStock(
            DetalleCompra detalle) {
            AccesoDatos datos =
                new AccesoDatos();

            try {
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
            finally {
                datos.cerrarConexion();
            }
        }
    }
}