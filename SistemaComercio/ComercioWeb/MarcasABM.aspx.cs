using System;
using Dominio;
using Negocio;

namespace ComercioWeb
{
    public partial class MarcasABM : System.Web.UI.Page
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
                MarcaNegocio negocio = new MarcaNegocio();
                Marca nueva = new Marca();
                nueva.Descripcion = txtNombre.Text.Trim();

                negocio.Agregar(nueva);

                Session["MensajeExito"] = "Marca agregada correctamente.";
                Response.Redirect("Marcas.aspx");
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
