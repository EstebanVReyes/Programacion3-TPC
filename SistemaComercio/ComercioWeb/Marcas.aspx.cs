using System;
using Negocio;

namespace ComercioWeb
{
    public partial class Marcas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMarcas();
            }
        }

        private void CargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            gvMarcas.DataSource = negocio.Listar();
            gvMarcas.DataBind();
        }

        protected void btnNuevaMarca_Click(object sender, EventArgs e)
        {
            Response.Redirect("MarcasABM.aspx");
        }
    }
}
