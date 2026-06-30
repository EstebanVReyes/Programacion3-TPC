using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public static class Seguridad
    {
        public static bool sesionActiva(object userCheck)
        {
            Usuario usuario = userCheck != null ? (Usuario)userCheck : null;
            if (usuario != null && usuario.ID != 0)
                return true;
            else
                return false;
        }

        public static bool esAdmin(object userCheck)
        {
            Usuario usuario = userCheck != null ? (Usuario)userCheck : null;
            if (usuario != null && usuario.TipoUsuario == "Administrador")
                return true;
            else
                return false;
        }

        public static bool esVendedor(object userCheck)
        {
            Usuario usuario = userCheck != null ? (Usuario)userCheck : null;
            if (usuario != null && usuario.TipoUsuario == "Vendedor")
                return true;
            else
                return false;
        }

        public static bool esCajero(object userCheck)
        {
            Usuario usuario = userCheck != null ? (Usuario)userCheck : null;
            if (usuario != null && usuario.TipoUsuario == "Cajero")
                return true;
            else
                return false;
        }

        public static bool esDeposito(object userCheck)
        {
            Usuario usuario = userCheck != null ? (Usuario)userCheck : null;
            if (usuario != null && usuario.TipoUsuario == "Deposito")
                return true;
            else
                return false;
        }
    }
}
