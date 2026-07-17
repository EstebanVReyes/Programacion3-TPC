using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace ComercioWeb
{
    public partial class Compras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Seguridad.esAdmin(Session["usuario"]) || Seguridad.esDeposito(Session["usuario"]) || Seguridad.esCajero(Session["usuario"])))
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                CargarCompras();
            }
        }

        private void CargarCompras()
        {
            CompraNegocio negocio = new CompraNegocio();
            List<Compra> lista = negocio.listar();
            Session["listaCompras"] = lista;
            gvCompras.DataSource = lista;
            gvCompras.DataBind();
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Compra> lista = (List<Compra>)Session["listaCompras"];
            string filtro = txtFiltro.Text.ToUpper();

            List<Compra> listaFiltrada = lista.FindAll(x =>
                x.Proveedor.Nombre.ToUpper().Contains(filtro)
            );

            gvCompras.DataSource = listaFiltrada;
            gvCompras.DataBind();
        }
    }
}
