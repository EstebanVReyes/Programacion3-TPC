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
        
            private static List<Cliente> listaClientes = new List<Cliente>();

            protected void Page_Load(object sender, EventArgs e)
            {
               
            }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Cliente nuevoCliente = new Cliente();
            nuevoCliente.Nombre = txtNombre.Text;
            nuevoCliente.Apellido = txtApellido.Text;
            nuevoCliente.DNI = txtDni.Text;
            nuevoCliente.Email = txtEmail.Text;
            nuevoCliente.Telefono = txtTelefono.Text;

            listaClientes.Add(nuevoCliente);

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
        }


    }

    
}