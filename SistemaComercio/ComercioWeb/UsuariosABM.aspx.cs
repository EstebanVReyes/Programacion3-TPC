using Dominio;
using System;
using System.Collections.Generic;

namespace ComercioWeb
{
    public partial class UsuariosABM : System.Web.UI.Page
    {
        private static List<Usuario> Usuarios = new List<Usuario>();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                DNI = txtDni.Text,
                Email = txtEmail.Text,
                Telefono = txtTelefono.Text,
                Rol = ddlRol.SelectedValue
            };

            Usuarios.Add(usuario);

            LimpiarFormulario();

            lblMensaje.Text = "Usuario guardado correctamente.";
            lblMensaje.ForeColor = System.Drawing.Color.Green;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblMensaje.Text = "";
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDni.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            ddlRol.Text = "";
        }
    }
}