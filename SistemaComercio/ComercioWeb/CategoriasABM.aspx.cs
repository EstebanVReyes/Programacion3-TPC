using System;
using Dominio;
using Negocio;

namespace ComercioWeb
{
    public partial class CategoriasABM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Seguridad.esAdmin(Session["usuario"])))
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;

            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                Categoria nueva = new Categoria();
                nueva.Descripcion = txtNombre.Text.Trim();

                negocio.Agregar(nueva);

                Session["MensajeExito"] = "Categoria agregada correctamente.";
                Response.Redirect("Categorias.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            lblMensaje.Text = "";
        }
    }
}
