using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public enum TipoPerfil { Vendedor, Administrador }

    public class Marca
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class Tipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }

    public class Proveedor
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string Cuit { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }

    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public TipoPerfil Perfil { get; set; }
    }

    public class IngresoStock
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioCompra { get; set; }
    }

    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Talle { get; set; }
        public string Color { get; set; }
        public Marca Marca { get; set; }
        public Tipo Tipo { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public decimal PorcentajeGanancia { get; set; }
        public List<Proveedor> Proveedores { get; set; } = new List<Proveedor>();
        public List<IngresoStock> IngresosStock { get; set; } = new List<IngresoStock>();

        public bool TieneStockBajo()
        {
            return StockActual <= StockMinimo;
        }

        public decimal ObtenerPrecioVenta()
        {
            IngresoStock ultimo = IngresosStock.OrderByDescending(i => i.Fecha).FirstOrDefault();
            if (ultimo == null) return 0;
            return ultimo.PrecioCompra * (1 + PorcentajeGanancia / 100);
        }
    }

    public class DetalleCompra
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioCompra { get; set; }
    }

    public class Compra
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public Proveedor Proveedor { get; set; }
        public List<DetalleCompra> Detalles { get; set; } = new List<DetalleCompra>();

        public decimal CalcularTotal()
        {
            return Detalles.Sum(d => d.Cantidad * d.PrecioCompra);
        }
    }

    public class DetalleVenta
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public decimal CalcularSubtotal()
        {
            return Cantidad * PrecioUnitario;
        }
    }

    public class Venta
    {
        public int Id { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Vendedor { get; set; }
        public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();

        public decimal CalcularTotal()
        {
            return Detalles.Sum(d => d.CalcularSubtotal());
        }
    }
}
