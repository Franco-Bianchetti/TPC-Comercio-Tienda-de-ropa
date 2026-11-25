using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Negocio
{
    public class ProductoNegocio
    {
        private const string SELECT_PRODUCTO = @"
           SELECT
        P.IdProducto,
        P.Codigo,
        P.Nombre,
        P.Talle,
        P.Color,
        P.StockActual,
        P.StockMinimo,
        P.PorcentajeGanancia,
        P.Activo,

        M.IdMarca,
        M.Nombre AS Marca,

        T.IdTipo,
        T.Nombre AS Tipo,

        PR.IdProveedor,
        PR.RazonSocial AS Proveedor,

        ISNULL(UI.PrecioCompra, 0) AS PrecioCompra

            FROM Productos P

            INNER JOIN Marcas M
                ON P.IdMarca = M.IdMarca

            INNER JOIN Tipos T
                ON P.IdTipo = T.IdTipo

            INNER JOIN Proveedores PR
                ON P.IdProveedor = PR.IdProveedor

            OUTER APPLY
            (
                SELECT TOP 1
                    PrecioCompra
                FROM IngresosStock
                WHERE IdProducto = P.IdProducto
                ORDER BY Fecha DESC, IdIngreso DESC
                       ) UI
                ";

        private Producto leerProducto(AccesoDatos datos)
        {
            Producto aux = new Producto();

            aux.Id = (int)datos.Lector["IdProducto"];
            aux.Codigo = datos.Lector["Codigo"].ToString();
            aux.Nombre = datos.Lector["Nombre"].ToString();
            aux.Talle = datos.Lector["Talle"].ToString();
            aux.Color = datos.Lector["Color"].ToString();
            aux.StockActual = (int)datos.Lector["StockActual"];
            aux.StockMinimo = (int)datos.Lector["StockMinimo"];
            aux.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
            aux.Activo = (bool)datos.Lector["Activo"];

            aux.Marca = new Marca();
            aux.Marca.Id = (int)datos.Lector["IdMarca"];
            aux.Marca.Nombre = datos.Lector["Marca"].ToString();

            aux.Tipo = new Tipo();
            aux.Tipo.Id = (int)datos.Lector["IdTipo"];
            aux.Tipo.Nombre = datos.Lector["Tipo"].ToString();

            aux.Proveedores.Add(   new Proveedor{   
                Id = (int)datos.Lector["IdProveedor"],
                RazonSocial = datos.Lector["Proveedor"].ToString()
                 });
            decimal precioCompra =   (decimal)datos.Lector["PrecioCompra"];

            if (precioCompra > 0)
            {
                aux.IngresosStock.Add(new IngresoStock
                    {
                        Fecha = DateTime.Now,
                        Cantidad = 0,
                        PrecioCompra = precioCompra
                    });
            }
            return aux;
        }


      

        public List<Producto> listar(bool incluirInactivos = false)
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = SELECT_PRODUCTO;

                if (!incluirInactivos)
                    consulta += "WHERE P.Activo = 1 ";

                consulta += "ORDER BY P.IdProducto";

                datos.setConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto =
                        leerProducto(datos);

                    

                    lista.Add(producto);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public List<Producto> listarFiltrado(
            string texto,
            int idMarca,
            int idTipo,
            string talle,
            string color,
            bool stockBajo,
            bool incluirInactivos = false)
        {
            List<Producto> lista = new List<Producto>();

            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = SELECT_PRODUCTO + " WHERE 1=1 ";

                if (!incluirInactivos)
                    consulta += " AND P.Activo = 1 ";

                if (!string.IsNullOrWhiteSpace(texto))
                    consulta += @" AND (
                P.Codigo LIKE @Texto
                OR P.Nombre LIKE @Texto
            ) ";

                if (idMarca > 0)
                    consulta += " AND P.IdMarca = @IdMarca ";

                if (idTipo > 0)
                    consulta += " AND P.IdTipo = @IdTipo ";

                if (!string.IsNullOrWhiteSpace(talle))
                    consulta += " AND P.Talle LIKE @Talle ";

                if (!string.IsNullOrWhiteSpace(color))
                    consulta += " AND P.Color LIKE @Color ";

                if (stockBajo)
                    consulta += @" AND P.StockActual > 0
                           AND P.StockActual < P.StockMinimo ";

                consulta += " ORDER BY P.Nombre ";

                datos.setConsulta(consulta);

                if (!string.IsNullOrWhiteSpace(texto))
                    datos.setParametro("@Texto", "%" + texto + "%");

                if (idMarca > 0)
                    datos.setParametro("@IdMarca", idMarca);

                if (idTipo > 0)
                    datos.setParametro("@IdTipo", idTipo);

                if (!string.IsNullOrWhiteSpace(talle))
                    datos.setParametro("@Talle", "%" + talle + "%");

                if (!string.IsNullOrWhiteSpace(color))
                    datos.setParametro("@Color", "%" + color + "%");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = leerProducto(datos);
                    lista.Add(producto);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public Producto buscar(int id)
        {
            Producto aux = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(SELECT_PRODUCTO +
                    "WHERE P.IdProducto = @Id AND P.Activo = 1");
                datos.setParametro("@Id", id);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    aux = leerProducto(datos);
                }

            }
            finally
            {
                datos.cerrarConexion();
            }

            if (aux != null)
            {
                aux.Proveedores = listarProveedores(aux.Id);
            }

            return aux;
        }

        public List<Proveedor> listarProveedores(int idProducto)
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    SELECT PR.IdProveedor, PR.RazonSocial
                    FROM ProductoProveedor PP
                    INNER JOIN Proveedores PR ON PP.IdProveedor = PR.IdProveedor
                    WHERE PP.IdProducto = @Id");
                datos.setParametro("@Id", idProducto);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.Id = (int)datos.Lector["IdProveedor"];
                    aux.RazonSocial = datos.Lector["RazonSocial"].ToString();
                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<Producto> listarPorProveedor( int idProveedor)
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(
                    SELECT_PRODUCTO +
                    "WHERE P.Activo = 1 " +
                    "AND P.IdProveedor = @IdProveedor " +
                    " ORDER BY P.Nombre");

                datos.setParametro(
                    "@IdProveedor",
                    idProveedor);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = leerProducto(datos);

                    lista.Add(producto);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool existeCodigo(string codigo, int idActual)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    SELECT IdProducto
                    FROM Productos
                    WHERE Codigo = @Codigo
                    AND IdProducto <> @Id");
                datos.setParametro("@Codigo", codigo);
                datos.setParametro("@Id", idActual);

                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregar(Producto nuevo)
        {
            int idNuevo = 0;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    INSERT INTO Productos
                        (Codigo, Nombre, Talle, Color, IdMarca, IdTipo,IdProveedor, StockMinimo, PorcentajeGanancia)
                    OUTPUT INSERTED.IdProducto
                    VALUES
                        (@Codigo, @Nombre, @Talle, @Color, @IdMarca, @IdTipo, @IdProveedor, @StockMinimo, @Porcentaje)");

                datos.setParametro("@Codigo", nuevo.Codigo);
                datos.setParametro("@Nombre", nuevo.Nombre);
                datos.setParametro("@Talle", nuevo.Talle);
                datos.setParametro("@Color", nuevo.Color);
                datos.setParametro("@IdMarca", nuevo.Marca.Id);
                datos.setParametro("@IdTipo", nuevo.Tipo.Id);
                datos.setParametro("@IdProveedor",nuevo.Proveedores[0].Id);
                 datos.setParametro("@StockMinimo", nuevo.StockMinimo);
                datos.setParametro("@Porcentaje", nuevo.PorcentajeGanancia);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    idNuevo = (int)datos.Lector[0];
                }
            }
            finally
            {
                datos.cerrarConexion();
            }

            guardarProveedores(idNuevo, nuevo.Proveedores);
        }

        public void modificar(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"
                    UPDATE Productos
                    SET Codigo = @Codigo,
                        Nombre = @Nombre,
                        Talle = @Talle,
                        Color = @Color,
                        IdMarca = @IdMarca,
                        IdTipo = @IdTipo,
                        IdProveedor = @IdProveedor,
                        StockMinimo = @StockMinimo,
                        PorcentajeGanancia = @Porcentaje
                    WHERE IdProducto = @Id");

                datos.setParametro("@Codigo", producto.Codigo);
                datos.setParametro("@Nombre", producto.Nombre);
                datos.setParametro("@Talle", producto.Talle);
                datos.setParametro("@Color", producto.Color);
                datos.setParametro("@IdMarca", producto.Marca.Id);
                datos.setParametro("@IdTipo", producto.Tipo.Id);
                datos.setParametro("@IdProveedor",producto.Proveedores[0].Id);
                datos.setParametro("@StockMinimo", producto.StockMinimo);
                datos.setParametro("@Porcentaje", producto.PorcentajeGanancia);
                datos.setParametro("@Id", producto.Id);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }

            quitarProveedores(producto.Id);
            guardarProveedores(producto.Id, producto.Proveedores);
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("UPDATE Productos SET Activo = 0 WHERE IdProducto = @Id");
                datos.setParametro("@Id", id);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void reactivar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("UPDATE Productos SET Activo = 1 WHERE IdProducto = @Id");
                datos.setParametro("@Id", id);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private void quitarProveedores(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("DELETE FROM ProductoProveedor WHERE IdProducto = @Id");
                datos.setParametro("@Id", idProducto);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private void guardarProveedores(int idProducto, List<Proveedor> proveedores)
        {
            foreach (Proveedor proveedor in proveedores)
            {
                AccesoDatos datos = new AccesoDatos();

                try
                {
                    datos.setConsulta(@"
                        INSERT INTO ProductoProveedor (IdProducto, IdProveedor)
                        VALUES (@IdProducto, @IdProveedor)");
                    datos.setParametro("@IdProducto", idProducto);
                    datos.setParametro("@IdProveedor", proveedor.Id);

                    datos.ejecutarAccion();
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }
        }

        public List<Producto> listarConStock()
        {
            return listar()
                .Where(x => x.StockActual > 0)
                .ToList();
        }

    }
}