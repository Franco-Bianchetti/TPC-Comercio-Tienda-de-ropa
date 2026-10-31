using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Dominio
{
    public class IngresoStock
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioCompra { get; set; }
    }
}