using System;
using System.Collections.Generic;
using Dominio;

namespace ComercioWeb
{
    public partial class Productos : System.Web.UI.Page
    {
        private static List<Articulo> articulos = new List<Articulo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

  

     
        private void CargarProductos()
        {
            gvProductos.DataSource = articulos;
            gvProductos.DataBind();
        }

       
    }
}