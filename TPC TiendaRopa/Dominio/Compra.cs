using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio {
    public class Compra {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public Usuario Usuario { get; set; }

        public Proveedor Proveedor { get; set; }

        public decimal Total { get; set; }

        public List<DetalleCompra> Detalles { get; set; }
            = new List<DetalleCompra>();

        public decimal CalcularTotal() {
            return Detalles.Sum(
                d => d.Subtotal);
        }
    }
}