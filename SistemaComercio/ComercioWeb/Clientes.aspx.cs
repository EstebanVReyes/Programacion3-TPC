using System;
using System.Collections.Generic;
using Dominio;

namespace ComercioWeb
{
    public partial class Clientes : System.Web.UI.Page
    {
        private static List<Cliente> listaClientes = new List<Cliente>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();
            }
        }
        
        private void CargarClientes()
        {
            gvClientes.DataSource = listaClientes;
            gvClientes.DataBind();
        }
        
    }

}
