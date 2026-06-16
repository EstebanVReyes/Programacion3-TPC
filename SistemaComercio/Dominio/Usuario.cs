using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string TipoUsuario { get; set; }
        public  bool Estado { get; set; }
      
        public override string ToString()
        {
            return Username;
        }
    }
}