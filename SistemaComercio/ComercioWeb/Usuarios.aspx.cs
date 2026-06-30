using Dominio;
using Negocio;
using System;
using System.Web.UI.WebControls;

namespace ComercioWeb
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Seguridad.esAdmin(Session["usuario"])))
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                gvUsuarios.DataSource = negocio.ListarActivos();
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Session["Error"] = "Hubo un problema al cargar los usuarios: " + ex.Message;
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idUsuario = int.Parse(btn.CommandArgument);

            Response.Redirect($"FormularioUsuarios.aspx?id={idUsuario}");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;

                int idUsuario = int.Parse(btn.CommandArgument);

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.BajaLogica(idUsuario);

                CargarUsuarios();

                lblMensajes.Text = "Usuario eliminado correctamente.";
                lblMensajes.CssClass = "text-success";
            }
            catch (Exception ex)
            {
                lblMensajes.Text = "Error al eliminar el usuario: " + ex.Message;
                lblMensajes.CssClass = "text-danger";
            }
        }
    }
}
