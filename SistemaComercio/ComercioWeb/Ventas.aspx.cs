using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

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
            List<Venta> lista = negocio.listar();
            Session["listaVentas"] = lista;
            gvVentas.DataSource = lista;
            gvVentas.DataBind();
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Venta> lista = (List<Venta>)Session["listaVentas"];
            string filtro = txtFiltro.Text.ToUpper();

            List<Venta> listaFiltrada = lista.FindAll(x =>
                x.Cliente.Nombre.ToUpper().Contains(filtro) ||
                x.Cliente.Apellido.ToUpper().Contains(filtro) ||
                x.NumeroFactura.ToUpper().Contains(filtro)
            );

            gvVentas.DataSource = listaFiltrada;
            gvVentas.DataBind();
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Venta> lista = (List<Venta>)Session["listaVentas"];

            if (ddlEstado.SelectedValue == "")
            {
                gvVentas.DataSource = lista;
            }
            else
            {
                List<Venta> listaFiltrada = lista.FindAll(x =>
                    x.Estado == ddlEstado.SelectedValue
                );
                gvVentas.DataSource = listaFiltrada;
            }

            gvVentas.DataBind();
        }
    }
}
