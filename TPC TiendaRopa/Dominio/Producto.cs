using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio
{
    public class Producto
    {
        public int Id { get; set; }

        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public string Talle { get; set; }

        public string Color { get; set; }

        public Marca Marca { get; set; }

        public Tipo Tipo { get; set; }

        public int StockActual { get; set; }

        public int StockMinimo { get; set; }

        public decimal PorcentajeGanancia { get; set; }

        public bool Activo { get; set; }

        public List<Proveedor> Proveedores { get; set; }
            = new List<Proveedor>();

        public List<IngresoStock> IngresosStock { get; set; }
            = new List<IngresoStock>();

        public bool TieneStockBajo()
        {
            return StockActual <= StockMinimo;
        }

        public string EstadoStock
        {
            get
            {
                return TieneStockBajo()
                    ? "⚠ Stock bajo, realizar compra"
                    : "✅ Disponible";
            }
        }

        public decimal ObtenerPrecioCompra()
        {
            IngresoStock ultimo =
                IngresosStock
                .OrderByDescending(x => x.Fecha)
                .FirstOrDefault();

            if (ultimo == null)
                return 0;

            return ultimo.PrecioCompra;
        }

        public decimal ObtenerPrecioVenta()
        {
            decimal precioCompra =
                ObtenerPrecioCompra();

            if (precioCompra <= 0)
                return 0;

            return Math.Round(
                precioCompra *
                (1 + (PorcentajeGanancia / 100)),
                2);
        }

        public string PrecioCompraMostrado
        {
            get
            {
                decimal precio =
                    ObtenerPrecioCompra();

                return precio > 0
                    ? precio.ToString("C2")
                    : "-";
            }
        }

        public string PrecioVentaMostrado
        {
            get
            {
                decimal precio =
                    ObtenerPrecioVenta();

                return precio > 0
                    ? precio.ToString("C2")
                    : "-";
            }
        }
    }
}