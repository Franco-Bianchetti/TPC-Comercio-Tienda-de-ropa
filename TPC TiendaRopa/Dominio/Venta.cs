using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio
{
    public class Venta
    {
        public int Id { get; set; }

        public string TipoFactura { get; set; }

        public string NumeroComprobante { get; set; }

        public DateTime Fecha { get; set; }

        public Cliente Cliente { get; set; }

        public Usuario Vendedor { get; set; }

        public decimal Total { get; set; }

        public string FormaPago { get; set; }

        public string CAE { get; set; }

        public DateTime? VencimientoCAE { get; set; }

        public List<DetalleVenta> Detalles { get; set; }
            = new List<DetalleVenta>();

        public decimal CalcularTotal()
        {
            return Detalles.Sum(
                d => d.Subtotal);
        }
    }
}