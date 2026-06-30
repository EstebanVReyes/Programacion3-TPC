using System;
using System.Collections.Generic;
using Dominio;
using Negocio;

namespace ComercioWeb
{
    public partial class FormularioCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    ClienteNegocio negocio = new ClienteNegocio();
                    List<Cliente> lista = negocio.Listar();
                    Cliente seleccionado = lista.Find(x => x.Id == id);

                    if (seleccionado != null)
                    {
                        txtId.Text = seleccionado.Id.ToString();
                        txtNombre.Text = seleccionado.Nombre;
                        txtApellido.Text = seleccionado.Apellido;
                        txtDNI.Text = seleccionado.DNI;
                        txtEmail.Text = seleccionado.Email;
                        txtTelefono.Text = seleccionado.Telefono;
                    }
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente nuevo = new Cliente();
                ClienteNegocio negocio = new ClienteNegocio();

                nuevo.Nombre = txtNombre.Text;
                nuevo.Apellido = txtApellido.Text;
                nuevo.DNI = txtDNI.Text;
                nuevo.Email = txtEmail.Text;
                nuevo.Telefono = txtTelefono.Text;

                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(txtId.Text);
                    negocio.Modificar(nuevo);
                }
                else
                {
                    negocio.Agregar(nuevo);
                }

                Response.Redirect("Clientes.aspx", false);
            }
            catch (Exception ex)
            {
                lblMensajes.Text = "Error: " + ex.Message;
            }
        }

     
        
    }
}