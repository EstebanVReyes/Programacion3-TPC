using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
   
        public class Factura
        {
            public int Id { get; set; }
            public Pedido Pedido { get; set; } 
            public string TipoFactura { get; set; } 
            public string CUIT_DNI { get; set; }
            public string NombreRazonSocial { get; set; }
            public DateTime FechaEmision { get; set; }
            public decimal TotalFacturado { get; set; }
        
    }
}
