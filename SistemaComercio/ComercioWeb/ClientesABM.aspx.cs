using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace ComercioWeb
{
    public partial class ClientesABM : System.Web.UI.Page
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

                lblMensaje.Text = "Cliente guardado correctamente.";
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
                txtDireccion.Text = "";
            }
        }

    
}