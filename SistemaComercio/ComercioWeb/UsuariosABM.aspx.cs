using Dominio;
using Negocio;
using System;

namespace ComercioWeb
{
    public partial class UsuariosABM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
               
                Usuario nuevoUsuario = new Usuario
                {
                    
                    Username = txtUsername.Text,
                    PasswordHash = txtPassword.Text,
                    TipoUsuario = ddlTipoUsuario.SelectedValue
                };

                
                UsuarioNegocio negocio = new UsuarioNegocio();

                
                if (negocio.Agregar(nuevoUsuario))
                {
                    LimpiarFormulario();
                    lblMensaje.Text = "Usuario guardado correctamente en la base de datos.";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                }
            }
            catch (Exception ex)
            {
                
                lblMensaje.Text = "Error al guardar el usuario: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblMensaje.Text = "";
        }

        private void LimpiarFormulario()
        {
          
            txtUsername.Text = "";
            txtPassword.Text = "";
            ddlTipoUsuario.SelectedIndex = 0;
            lblMensaje.Text = "";
        }

        protected void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}