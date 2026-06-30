using System;
using System.Collections.Generic;
using Dominio;
using Negocio;
using System.Web.UI.WebControls;

namespace ComercioWeb
{
    public partial class Productos : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        private void CargarProductos()
        {
          
            ProductoNegocio negocio = new ProductoNegocio();

            try
            {
               
                gvProductos.DataSource = negocio.Listar();
                gvProductos.DataBind();
            }
            catch (Exception ex)
            {
               

               
                Session.Add("error", ex.ToString());
               
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idProducto = int.Parse(btn.CommandArgument);
            Response.Redirect($"FormularioProducto.aspx?id={idProducto}");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int idProducto = int.Parse(btn.CommandArgument);
                ProductoNegocio negocio = new ProductoNegocio();
                negocio.Eliminar(idProducto);
                CargarProductos();

                lblMensaje.Text = "Producto eliminado correctamente.";
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                lblMensaje.Text = "Error al eliminar el producto. Por favor, inténtelo de nuevo.";
            }
        }

        protected void btnEditar_Click1(object sender, EventArgs e)
        {

        }
    }
}