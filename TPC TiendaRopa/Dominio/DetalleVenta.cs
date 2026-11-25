namespace Dominio
{
    public class DetalleVenta
    {
        public int Id { get; set; }

        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal
        {
            get
            {
                return Cantidad * PrecioUnitario;
            }
        }

        public string NombreProducto
        {
            get
            {
                return Producto.Nombre;
            }
        }
        public decimal CalcularSubtotal()
        {
            return Cantidad * PrecioUnitario;
        }

    }
}