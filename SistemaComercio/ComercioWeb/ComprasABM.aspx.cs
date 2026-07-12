using System;
using System.Collections.Generic;
using Dominio;
using Negocio;
using System.Web.UI.WebControls;

namespace ComercioWeb
{
    public partial class ComprasABM : System.Web.UI.Page
    {
        public string ObtenerNombresProveedores(List<Dominio.Proveedor> proveedores)
        {
            if (proveedores == null || proveedores.Count == 0)
                return "-";
            return string.Join(", ", proveedores.ConvertAll(p => p.Nombre));
        }

        public List<Dominio.DetalleCompra> ListaCarritoNueva
        {
            get
            {
                if (Session["CarritoNuevaCompra"] == null)
                    Session["CarritoNuevaCompra"] = new List<Dominio.DetalleCompra>();
                return (List<Dominio.DetalleCompra>)Session["CarritoNuevaCompra"];
            }
            set
            {
                Session["CarritoNuevaCompra"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDesplegables();

                Session["CarritoNuevaCompra"] = null;
                ActualizarTablaYTotal();
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
                MostrarMensaje("Error al cargar datos: " + ex.Message, System.Drawing.Color.Red);
            }
        }

        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ddlProducto.SelectedValue))
                {
                    int idProd = int.Parse(ddlProducto.SelectedValue);
                    ProductoNegocio prodNegocio = new ProductoNegocio();
                    Producto productoSeleccionado = prodNegocio.Listar().Find(x => x.Id == idProd);

                    if (productoSeleccionado != null)
                    {
                        txtPrecioCosto.Text = productoSeleccionado.Precio.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                else
                {
                    txtPrecioCosto.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al obtener el precio: " + ex.Message, System.Drawing.Color.Red);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddlProducto.SelectedValue) || string.IsNullOrEmpty(txtCantidad.Text))
                {
                    MostrarMensaje("Seleccione un producto y especifique la cantidad.", System.Drawing.Color.Orange);
                    return;
                }

                int idProducto = int.Parse(ddlProducto.SelectedValue);

                if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                {
                    MostrarMensaje("La cantidad debe ser un numero mayor a cero.", System.Drawing.Color.Orange);
                    return;
                }

                if (!decimal.TryParse(txtPrecioCosto.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal precio))
                {
                    MostrarMensaje("No se pudo determinar el precio del producto.", System.Drawing.Color.Orange);
                    return;
                }

                ProductoNegocio prodNegocio = new ProductoNegocio();
                Producto prodSelect = prodNegocio.Listar().Find(x => x.Id == idProducto);

                if (prodSelect != null)
                {
                    prodSelect.Proveedores = prodNegocio.ObtenerProveedores(prodSelect.Id);

                    List<Dominio.DetalleCompra> temporal = ListaCarritoNueva;

                    Dominio.DetalleCompra detalleExistente = temporal.Find(x => x.Producto.Id == idProducto);
                    if (detalleExistente != null)
                    {
                        detalleExistente.Cantidad += cantidad;
                    }
                    else
                    {
                        Dominio.DetalleCompra nuevoDetalle = new Dominio.DetalleCompra();
                        nuevoDetalle.Producto = prodSelect;
                        nuevoDetalle.Cantidad = cantidad;
                        nuevoDetalle.PrecioCosto = precio;
                        temporal.Add(nuevoDetalle);
                    }

                    ListaCarritoNueva = temporal;

                    ActualizarTablaYTotal();

                    ddlProducto.SelectedIndex = 0;
                    txtPrecioCosto.Text = "";
                    txtCantidad.Text = "";
                    MostrarMensaje("Producto agregado correctamente.", System.Drawing.Color.Green);
                }
                else
                {
                    MostrarMensaje("El producto seleccionado ya no existe.", System.Drawing.Color.Red);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al agregar producto: " + ex.Message, System.Drawing.Color.Red);
            }
        }

        protected void dgvCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Quitar")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    List<Dominio.DetalleCompra> temporal = ListaCarritoNueva;

                    if (index >= 0 && index < temporal.Count)
                    {
                        temporal.RemoveAt(index);
                        ListaCarritoNueva = temporal;
                        ActualizarTablaYTotal();
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al quitar el producto: " + ex.Message, System.Drawing.Color.Red);
                }
            }
        }

        private void ActualizarTablaYTotal()
        {
            dgvCarrito.DataSource = ListaCarritoNueva;
            dgvCarrito.DataBind();

            decimal totalCompra = 0;
            foreach (var item in ListaCarritoNueva)
            {
                totalCompra += (item.PrecioCosto * item.Cantidad);
            }

            lblTotal.Text = totalCompra.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
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

                if (ListaCarritoNueva.Count == 0)
                {
                    MostrarMensaje("Debe agregar al menos un producto a la compra.", System.Drawing.Color.Orange);
                    return;
                }

                Compra nuevaCompra = new Compra();
                CompraNegocio negocio = new CompraNegocio();

                nuevaCompra.Fecha = DateTime.Now;
                nuevaCompra.Total = decimal.Parse(lblTotal.Text, System.Globalization.CultureInfo.InvariantCulture);
                nuevaCompra.Proveedor = new Proveedor();
                nuevaCompra.Proveedor.Id = int.Parse(ddlProveedor.SelectedValue);
                nuevaCompra.Detalles = ListaCarritoNueva;

                negocio.agregar(nuevaCompra);

                Session["CarritoNuevaCompra"] = null;

                Response.Redirect("Compras.aspx", false);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al registrar la compra: " + ex.Message, System.Drawing.Color.Red);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Session["CarritoNuevaCompra"] = null;
            ddlProveedor.SelectedIndex = 0;
            ddlProducto.SelectedIndex = 0;
            txtCantidad.Text = "";
            txtPrecioCosto.Text = "";
            lblMensaje.Text = "";
            ActualizarTablaYTotal();
        }

        private void MostrarMensaje(string mensaje, System.Drawing.Color color)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.ForeColor = color;
        }
    }
}
