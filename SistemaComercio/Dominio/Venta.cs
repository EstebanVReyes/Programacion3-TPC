using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public Usuario Usuario { get; set; }
        public Cliente Cliente { get; set; }
        public string Estado { get; set; }
        public string NumeroFactura { get; set; }
        public decimal Total { get; set; }

        public List<DetalleVenta> Detalles { get; set; }

        public Venta()
        {
            Detalles = new List<DetalleVenta>();
        }

    }
}