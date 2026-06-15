using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public Usuario Usuario { get; set; }
        public string Estado { get; set; }    
        public string  NumeroFactura { get; set; }
        public decimal Total { get; set; }

        public int Cantidad { get; set; }

        public List<DetallePedido> Detalles { get; set; }
    }
}