using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio; // 1. Agregamos el namespace de la capa de negocio

namespace ComercioWeb
{
    public partial class ClientesABM : System.Web.UI.Page
    {
        // ELIMINADO: Ya no usamos la lista en memoria
        // private static List<Cliente> listaClientes = new List<Cliente>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Seguridad.esAdmin(Session["usuario"]) || Seguridad.esVendedor(Session["usuario"])))
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
                // 2. Instanciamos el cliente y le pasamos los valores
                Cliente nuevoCliente = new Cliente();
                nuevoCliente.Nombre = txtNombre.Text;
                nuevoCliente.Apellido = txtApellido.Text;

                // NOTA: Asegúrate de que la propiedad en tu clase de Dominio 
                // se llame exactamente "DNI" o "Dni" según como lo hayas creado.
                nuevoCliente.DNI = txtDni.Text;
                nuevoCliente.Email = txtEmail.Text;

                // Como el teléfono admite NULL en la base de datos, 
                // si el TextBox está vacío le mandamos null explícitamente.
                if (string.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    nuevoCliente.Telefono = null;
                }
                else
                {
                    nuevoCliente.Telefono = txtTelefono.Text;
                }

                // 3. Instanciamos la clase de negocio y mandamos a guardar
                ClienteNegocio negocio = new ClienteNegocio();
                negocio.Agregar(nuevoCliente);

                // ELIMINADO: listaClientes.Add(nuevoCliente);

                LimpiarFormulario();

                lblMensaje.Text = "Cliente guardado correctamente en la Base de Datos.";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                // Si salta un error (ej: el DNI o Email ya existen), lo atajamos acá
                lblMensaje.Text = "Error al guardar: " + ex.Message;
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
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDni.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
        }
    }
}