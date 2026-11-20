using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetalleVenta
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal
        {
            get { return Cantidad * PrecioUnitario; }
        }

        public string NombreProducto
        {
            get
            {
                if (Producto == null)
                    return "";

                return Producto.Nombre;
            }
        }

        public decimal CalcularSubtotal()
        {
            return Subtotal;
        }
    }
}