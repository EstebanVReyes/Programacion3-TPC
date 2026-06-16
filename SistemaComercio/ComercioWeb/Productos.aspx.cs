using System;
using System.Collections.Generic;
using Dominio;
using Negocio;

namespace ComercioWeb
{
    public partial class Productos : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        private void CargarProductos()
        {
          
            ProductoNegocio negocio = new ProductoNegocio();

            try
            {
               
                gvProductos.DataSource = negocio.Listar();
                gvProductos.DataBind();
            }
            catch (Exception ex)
            {
               

               
                Session.Add("error", ex.ToString());
               
            }
        }
    }
}