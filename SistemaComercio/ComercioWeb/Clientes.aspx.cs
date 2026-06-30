using Dominio;
using Negocio; // 1. Agregamos el namespace para acceder a ClienteNegocio
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace ComercioWeb
{
    public partial class Clientes : System.Web.UI.Page
    {
        // ELIMINADO: private static List<Cliente> listaClientes = new List<Cliente>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();
            }
        }

        private void CargarClientes()
        {

            ClienteNegocio negocio = new ClienteNegocio();

            try
            {

                gvClientes.DataSource = negocio.Listar();
                gvClientes.DataBind();
            }
            catch (Exception ex)
            {


                Session.Add("error", ex.ToString());
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idCliente = int.Parse(btn.CommandArgument);

            Response.Redirect($"FormularioCliente.aspx?id={idCliente}");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;

                int idCliente = int.Parse(btn.CommandArgument);

                ClienteNegocio negocio = new ClienteNegocio();
                negocio.Eliminar(idCliente);

                CargarClientes();

                lblMensajes.Text = "Cliente eliminado correctamente.";
                lblMensajes.CssClass = "text-success";
            }
            catch (Exception ex)
            {
                lblMensajes.Text = "Error al eliminar el cliente: " + ex.Message;
                lblMensajes.CssClass = "text-danger";
            }
        }
    }
}
