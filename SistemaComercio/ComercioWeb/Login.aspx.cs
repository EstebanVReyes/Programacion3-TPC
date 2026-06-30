using System;
using Negocio;
using Dominio;

namespace ComercioWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            Usuario usuario = new Usuario();


            usuario.Username = txtUsuario.Text;
            usuario.PasswordHash = txtPassword.Text;

            Usuario usuarioLogueado = usuarioNegocio.ValidarLogin(usuario.Username, usuario.PasswordHash);

            if (usuarioLogueado != null)
            {
                Session.Add("usuario", usuarioLogueado);
                Response.Redirect("Default.aspx");
            }
            else
            {
                Session.Add("error", "user o password incorrectos.");
                lblMensaje.Text = "Usuario o contraseña incorrectos.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}