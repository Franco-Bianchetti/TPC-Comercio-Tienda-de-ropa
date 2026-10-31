using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio
{
    public class Venta
    {
        public int Id { get; set; }

        public string NumeroFactura { get; set; }

        public DateTime Fecha { get; set; }

        public Cliente Cliente { get; set; }

        public Usuario Vendedor { get; set; }

        public List<DetalleVenta> Detalles { get; set; }
            = new List<DetalleVenta>();

        public decimal CalcularTotal()
        {
            return Detalles.Sum(d =>
                d.CalcularSubtotal());
        }
    }
}