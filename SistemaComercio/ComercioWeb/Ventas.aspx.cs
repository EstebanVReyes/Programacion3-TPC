using Dominio;
using negocio;
using Negocio;
using System;
using System.Collections.Generic;

namespace ComercioWeb
{
    public partial class Ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Seguridad.esAdmin(Session["usuario"]) || Seguridad.esVendedor(Session["usuario"]) || Seguridad.esCajero(Session["usuario"])))
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                CargarVentas();
            }
        }

        private void CargarVentas()
        {
            VentaNegocio negocio = new VentaNegocio();


            gvVentas.DataSource = negocio.listar();
            gvVentas.DataBind();
        }





    }
}