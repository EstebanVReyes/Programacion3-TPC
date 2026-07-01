using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
   
        public class Proveedor
        {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
    
}