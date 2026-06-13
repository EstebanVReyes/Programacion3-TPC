using System;
using System.Collections.Generic;
using Dominio;

namespace ComercioWeb
{
    public partial class Clientes : System.Web.UI.Page
    {
        private static List<Usuario> Usuarios = new List<Usuario>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();
            }
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
            Direcciones = new List<Direccion>
{
    new Direccion
    {
        Calle = txtDireccion.Text
    }
}
        };

            Usuarios.Add(usuario);

            LimpiarFormulario();
            CargarClientes();

            lblMensaje.Text = "Cliente guardado correctamente.";
            lblMensaje.ForeColor = System.Drawing.Color.Green;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblMensaje.Text = "";
        }

        private void CargarClientes()
        {
            gvClientes.DataSource = Usuarios;
            gvClientes.DataBind();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDni.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
        }
    }

}
