using System;
using Negocio;

namespace ComercioWeb
{
    public partial class Categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
            }
        }

        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            gvCategorias.DataSource = negocio.Listar();
            gvCategorias.DataBind();
        }

        protected void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            Response.Redirect("CategoriasABM.aspx");
        }
    }
}
