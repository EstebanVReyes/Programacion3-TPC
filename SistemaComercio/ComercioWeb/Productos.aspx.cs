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
            if (!(Seguridad.esAdmin(Session["usuario"]) || Seguridad.esVendedor(Session["usuario"]) || Seguridad.esDeposito(Session["usuario"])))
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                CargarCategorias();
                CargarProductos();
            }
        }

        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            ddlCategoria.DataSource = negocio.Listar();
            ddlCategoria.DataTextField = "Descripcion";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Todas", ""));
        }

        private void CargarProductos()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            List<Producto> lista = negocio.Listar();
            Session["listaProductosPagina"] = lista;
            gvProductos.DataSource = lista;
            gvProductos.DataBind();
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            FiltrarProductos();
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarProductos();
        }

        private void FiltrarProductos()
        {
            List<Producto> lista = (List<Producto>)Session["listaProductosPagina"];
            string filtro = txtFiltro.Text.ToUpper();
            string idCategoria = ddlCategoria.SelectedValue;

            List<Producto> listaFiltrada = lista.FindAll(x =>
                (filtro == "" || x.Nombre.ToUpper().Contains(filtro)) &&
                (idCategoria == "" || x.Categoria.Id.ToString() == idCategoria)
            );

            gvProductos.DataSource = listaFiltrada;
            gvProductos.DataBind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idProducto = int.Parse(btn.CommandArgument);
            Response.Redirect($"FormularioProducto.aspx?id={idProducto}");
        }

        protected void gvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnEliminar = (Button)e.Row.FindControl("btnEliminar");
                if (btnEliminar != null)
                {
                    btnEliminar.Visible = Seguridad.esAdmin(Session["usuario"]) || Seguridad.esVendedor(Session["usuario"]);
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!(Seguridad.esAdmin(Session["usuario"]) || Seguridad.esVendedor(Session["usuario"])))
            {
                Response.Redirect("Default.aspx");
            }

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
    }
}