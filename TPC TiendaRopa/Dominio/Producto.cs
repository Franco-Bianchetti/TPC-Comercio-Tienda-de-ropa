
using System.Collections.Generic;
using System.Linq;





namespace Dominio
{
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

        public List<Proveedor> Proveedores { get; set; }
            = new List<Proveedor>();

        public List<IngresoStock> IngresosStock { get; set; }
            = new List<IngresoStock>();

        public bool TieneStockBajo()
        {
            return StockActual <= StockMinimo;
        }

        public decimal ObtenerPrecioVenta()
        {
            IngresoStock ultimo =
                IngresosStock
                .OrderByDescending(x => x.Fecha)
                .FirstOrDefault();

            if (ultimo == null)
                return 0;

            return ultimo.PrecioCompra *
                   (1 + PorcentajeGanancia / 100);
        }
    }
}
