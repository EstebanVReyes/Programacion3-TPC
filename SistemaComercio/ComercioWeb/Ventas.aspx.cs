using System;
using System.Collections.Generic;
using Dominio;

namespace ComercioWeb
{
    public partial class Ventas : System.Web.UI.Page
    {
        private static List<Venta> pedidos = new List<Venta>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarVentas();
            }
        }

       
        private void CargarVentas()
        {
            gvVentas.DataSource = pedidos;
            gvVentas.DataBind();
        }

     
    }
}