using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace ComercioWeb
{
    public partial class ProveedoresABM : System.Web.UI.Page
    {
        public List<DetalleProveedor> ListaCarritoProveedor
        {
            get
            {
                if (Session["CarritoProveedor"] == null)
                    Session["CarritoProveedor"] = new List<DetalleProveedor>();
                return (List<DetalleProveedor>)Session["CarritoProveedor"];
            }
            set
            {
                Session["CarritoProveedor"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int idProveedor = int.Parse(Request.QueryString["id"]);
                    ViewState["IdProveedor"] = idProveedor;
                    ProveedorNegocio provNegocio = new ProveedorNegocio();
                    Proveedor prov = provNegocio.ObtenerPorId(idProveedor);
                    lblNombreProveedor.Text = prov.Nombre;
                }

                CargarDesplegables();

                if (Session["CarritoProveedor"] == null)
                {
                    ProveedorNegocio provNeg = new ProveedorNegocio();
                    Session["CarritoProveedor"] = provNeg.ObtenerProductosDeProveedor(
                        (int)ViewState["IdProveedor"]);
                }

                ActualizarTabla();
            }
        }

        private void CargarDesplegables()
        {
            try
            {
                ProductoNegocio productoNegocio = new ProductoNegocio();
                ddlProducto.DataSource = productoNegocio.Listar();
                ddlProducto.DataValueField = "Id";
                ddlProducto.DataTextField = "Nombre";
                ddlProducto.DataBind();
                ddlProducto.Items.Insert(0, new ListItem("Seleccione un producto...", ""));
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar los datos: " + ex.Message, System.Drawing.Color.Red);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddlProducto.SelectedValue))
                {
                    MostrarMensaje("Seleccioná un producto.", System.Drawing.Color.Orange);
                    return;
                }

                int idProducto = int.Parse(ddlProducto.SelectedValue);

                ProductoNegocio prodNegocio = new ProductoNegocio();
                Producto prodSeleccionado = prodNegocio.Listar().Find(p => p.Id == idProducto);

                if (prodSeleccionado == null)
                {
                    MostrarMensaje("El producto seleccionado no existe.", System.Drawing.Color.Red);
                    return;
                }

                List<DetalleProveedor> temporal = ListaCarritoProveedor;

                DetalleProveedor detalleExistente = temporal.Find(d => d.Producto.Id == idProducto);
                if (detalleExistente != null)
                {
                    MostrarMensaje("El producto ya está en la lista.", System.Drawing.Color.Orange);
                    return;
                }

                DetalleProveedor nuevoDetalle = new DetalleProveedor();
                nuevoDetalle.Producto = new Producto();
                nuevoDetalle.Producto.Id = prodSeleccionado.Id;
                nuevoDetalle.Producto.Nombre = prodSeleccionado.Nombre;
                temporal.Add(nuevoDetalle);

                ListaCarritoProveedor = temporal;

                ddlProducto.SelectedIndex = 0;

                ActualizarTabla();
                MostrarMensaje("Producto agregado correctamente.", System.Drawing.Color.Green);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al agregar el producto: " + ex.Message, System.Drawing.Color.Red);
            }
        }

        protected void dgvCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Quitar")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    List<DetalleProveedor> temporal = ListaCarritoProveedor;

                    if (index >= 0 && index < temporal.Count)
                    {
                        temporal.RemoveAt(index);
                        ListaCarritoProveedor = temporal;
                        ActualizarTabla();
                        MostrarMensaje("Producto quitado de la lista.", System.Drawing.Color.OrangeRed);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al quitar el producto: " + ex.Message, System.Drawing.Color.Red);
                }
            }
        }

        private void ActualizarTabla()
        {
            dgvCarrito.DataSource = ListaCarritoProveedor;
            dgvCarrito.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListaCarritoProveedor.Count == 0)
                {
                    MostrarMensaje("Debe agregar al menos un producto.", System.Drawing.Color.Orange);
                    return;
                }

                int idProveedor = (int)ViewState["IdProveedor"];

                ProveedorNegocio negocio = new ProveedorNegocio();
                negocio.GuardarProductosProveedor(idProveedor, ListaCarritoProveedor);

               

                Session["CarritoProveedor"] = null;
                Session["MensajeExito"] = "✅ Productos asociados al proveedor correctamente.";
                Response.Redirect("Proveedores.aspx", false);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al guardar: " + ex.Message, System.Drawing.Color.Red);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Session["CarritoProveedor"] = null;
            Response.Redirect("Proveedores.aspx", false);
        }

        private void MostrarMensaje(string texto, System.Drawing.Color color)
        {
            lblMensaje.Text = texto;
            lblMensaje.ForeColor = color;
            lblMensaje.Visible = true;
        }
    }
}
