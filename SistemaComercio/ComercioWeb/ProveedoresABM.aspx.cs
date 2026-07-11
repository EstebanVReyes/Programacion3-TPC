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
                CargarDesplegables();
                Session["CarritoProveedor"] = null;
                ActualizarTabla();
            }
        }

        private void CargarDesplegables()
        {
            try
            {
                ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
                ddlProveedor.DataSource = proveedorNegocio.Listar();
                ddlProveedor.DataValueField = "Id";
                ddlProveedor.DataTextField = "Nombre";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new ListItem("Seleccione un proveedor...", ""));

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

        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CarritoProveedor"] = null;
            ActualizarTabla();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddlProveedor.SelectedValue))
                {
                    MostrarMensaje("Primero seleccioná un proveedor.", System.Drawing.Color.Orange);
                    return;
                }

                if (string.IsNullOrEmpty(ddlProducto.SelectedValue))
                {
                    MostrarMensaje("Seleccioná un producto.", System.Drawing.Color.Orange);
                    return;
                }

                if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                {
                    MostrarMensaje("La cantidad debe ser un número mayor a cero.", System.Drawing.Color.Orange);
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
                    detalleExistente.Cantidad += cantidad;
                }
                else
                {
                    DetalleProveedor nuevoDetalle = new DetalleProveedor();
                    nuevoDetalle.Producto = prodSeleccionado;
                    nuevoDetalle.Cantidad = cantidad;
                    temporal.Add(nuevoDetalle);
                }

                ListaCarritoProveedor = temporal;

                ddlProducto.SelectedIndex = 0;
                txtCantidad.Text = "";

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
                if (string.IsNullOrEmpty(ddlProveedor.SelectedValue))
                {
                    MostrarMensaje("Debe seleccionar un proveedor.", System.Drawing.Color.Orange);
                    return;
                }

                if (ListaCarritoProveedor.Count == 0)
                {
                    MostrarMensaje("Debe agregar al menos un producto.", System.Drawing.Color.Orange);
                    return;
                }

                int idProveedor = int.Parse(ddlProveedor.SelectedValue);

                ProveedorNegocio negocio = new ProveedorNegocio();
                negocio.GuardarProductosProveedor(idProveedor, ListaCarritoProveedor);

                ProductoNegocio productoNegocio = new ProductoNegocio();
                foreach (DetalleProveedor item in ListaCarritoProveedor)
                {
                    productoNegocio.actualizarStock(item.Producto.Id, item.Cantidad);
                }

                Session["CarritoProveedor"] = null;
                Session["MensajeExito"] = "✅ Productos ingresados al depósito correctamente.";
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
            Response.Redirect("ProveedoresABM.aspx", false);
        }

        private void MostrarMensaje(string texto, System.Drawing.Color color)
        {
            lblMensaje.Text = texto;
            lblMensaje.ForeColor = color;
            lblMensaje.Visible = true;
        }
    }
}