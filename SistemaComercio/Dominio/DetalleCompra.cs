using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    namespace Dominio
    {
        public class DetalleCompra
        {
            public int Id { get; set; }
            public Articulo Articulo { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioCosto { get; set; }
        }
    }
}
