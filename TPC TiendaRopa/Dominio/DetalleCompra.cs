using System;

namespace Dominio {
    public class DetalleCompra {
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioCompra { get; set; }

        public decimal Subtotal {
            get {
                return Cantidad * PrecioCompra;
            }
        }

        public string NombreProducto {
            get {
                if (Producto == null)
                    return "";

                return Producto.Nombre;
            }
        }
    }
}