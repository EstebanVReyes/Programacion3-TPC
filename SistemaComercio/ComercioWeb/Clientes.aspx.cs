using System;
using System.Collections.Generic;
using Dominio;
using Negocio; // 1. Agregamos el namespace para acceder a ClienteNegocio

namespace ComercioWeb
{
    public partial class Clientes : System.Web.UI.Page
    {
        // ELIMINADO: private static List<Cliente> listaClientes = new List<Cliente>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();
            }
        }

        private void CargarClientes()
        {
            
            ClienteNegocio negocio = new ClienteNegocio();

            try
            {
               
                gvClientes.DataSource = negocio.Listar();
                gvClientes.DataBind();
            }
            catch (Exception ex)
            {
                

                Session.Add("error", ex.ToString());
            }
        }
    }
}
