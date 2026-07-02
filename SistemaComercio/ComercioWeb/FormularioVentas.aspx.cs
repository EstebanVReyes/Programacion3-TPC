using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace ComercioWeb
{
    public partial class FormularioVenta : System.Web.UI.Page
    {
        
        public List<Dominio.DetalleVenta> ListaCarritoEdicion
        {
            get
            {
                if (Session["CarritoEdicionVenta"] == null)
                    Session["CarritoEdicionVenta"] = new List<Dominio.DetalleVenta>();
                return (List<Dominio.DetalleVenta>)Session["CarritoEdicionVenta"];
            }
            set
            {
                Session["CarritoEdicionVenta"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDesplegables();
                Session["CarritoEdicionVenta"] = null;

                if (Request.QueryString["id"] != null)
                {
                    try
                    {
                        int id = int.Parse(Request.QueryString["id"]);
                        VentaNegocio negocio = new VentaNegocio();

                        List<Dominio.Venta> lista = negocio.listar();
                        Dominio.Venta seleccionado = lista.Find(x => x.Id == id);

                        if (seleccionado != null)
                        {
                            txtIdVenta.Text = seleccionado.Id.ToString();
                            txtNumeroFactura.Text = seleccionado.NumeroFactura;
                            txtFecha.Text = seleccionado.Fecha.ToString("yyyy-MM-dd");
                            txtTotal.Text = seleccionado.Total.ToString(System.Globalization.CultureInfo.InvariantCulture);

                            if (!string.IsNullOrEmpty(seleccionado.Estado))
                            {
                                ddlEstadoPago.SelectedValue = seleccionado.Estado;
                            }

                            if (seleccionado.Cliente != null && seleccionado.Cliente.Id != 0)
                            {
                                string idClienteStr = seleccionado.Cliente.Id.ToString();
                                ListItem itemCliente = ddlClientes.Items.FindByValue(idClienteStr);

                                if (itemCliente != null)
                                {
                                    ddlClientes.SelectedValue = idClienteStr;
                                }
                                else
                                {
                                    string nombreInactivo = seleccionado.Cliente.Nombre + " " + seleccionado.Cliente.Apellido + " (Inactivo)";
                                    ddlClientes.Items.Add(new ListItem(nombreInactivo, idClienteStr));
                                    ddlClientes.SelectedValue = idClienteStr;

                                    lblMensajes.Visible = true;
                                    lblMensajes.Text = "Aviso: El cliente original de esta venta se encuentra inactivo/eliminado en el sistema.";
                                    lblMensajes.ForeColor = System.Drawing.Color.Orange;
                                }
                            }

                           
                            if (seleccionado.Detalles != null && seleccionado.Detalles.Count > 0)
                            {
                                ListaCarritoEdicion = new List<Dominio.DetalleVenta>(seleccionado.Detalles);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensajes.Visible = true;
                        lblMensajes.Text = "Error al cargar la venta: " + ex.Message;
                        lblMensajes.ForeColor = System.Drawing.Color.Red;
                    }
                }

                ActualizarGridYTotal();
            }
        }

       
        protected void btnAgregarAlCarrito_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddlProductos.SelectedValue) || ddlProductos.SelectedValue == "0" || string.IsNullOrEmpty(txtCantidad.Text))
                {
                    lblMensajes.Visible = true;
                    lblMensajes.Text = "Seleccione un producto y especifique la cantidad.";
                    lblMensajes.ForeColor = System.Drawing.Color.Orange;
                    return;
                }

                int idProducto = int.Parse(ddlProductos.SelectedValue);

                if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                {
                    lblMensajes.Visible = true;
                    lblMensajes.Text = "La cantidad debe ser un número mayor a cero.";
                    lblMensajes.ForeColor = System.Drawing.Color.Orange;
                    return;
                }

                ProductoNegocio prodNegocio = new ProductoNegocio();
                Dominio.Producto prodSeleccionado = prodNegocio.Listar().Find(x => x.Id == idProducto);

                if (prodSeleccionado == null)
                {
                    lblMensajes.Visible = true;
                    lblMensajes.Text = "El producto seleccionado ya no existe.";
                    lblMensajes.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                List<Dominio.DetalleVenta> temporal = ListaCarritoEdicion;
                Dominio.DetalleVenta existente = temporal.Find(x => x.Producto.Id == idProducto);

                if (existente != null)
                {
                    existente.Cantidad += cantidad;
                }
                else
                {
                    Dominio.DetalleVenta nuevo = new Dominio.DetalleVenta();
                    nuevo.Producto = prodSeleccionado;
                    nuevo.Cantidad = cantidad;
                    nuevo.PrecioUnitario = prodSeleccionado.Precio;
                    temporal.Add(nuevo);
                }

                ListaCarritoEdicion = temporal;
                ActualizarGridYTotal();

                ddlProductos.SelectedIndex = 0;
                txtCantidad.Text = "";

                lblMensajes.Visible = true;
                lblMensajes.Text = "Producto agregado correctamente.";
                lblMensajes.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMensajes.Visible = true;
                lblMensajes.Text = "Error al agregar producto: " + ex.Message;
                lblMensajes.ForeColor = System.Drawing.Color.Red;
            }
        }

        .
        protected void dgvDetalles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Quitar")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    List<Dominio.DetalleVenta> temporal = ListaCarritoEdicion;

                    if (index >= 0 && index < temporal.Count)
                    {
                        temporal.RemoveAt(index);
                        ListaCarritoEdicion = temporal;
                        ActualizarGridYTotal();
                    }
                }
                catch (Exception ex)
                {
                    lblMensajes.Visible = true;
                    lblMensajes.Text = "Error al quitar el producto: " + ex.Message;
                    lblMensajes.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void ActualizarGridYTotal()
        {
            dgvDetalles.DataSource = ListaCarritoEdicion;
            dgvDetalles.DataBind();

            decimal total = 0;
            foreach (var item in ListaCarritoEdicion)
            {
                total += item.PrecioUnitario * item.Cantidad;
            }

            txtTotal.Text = total.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                int idVenta = int.Parse(txtIdVenta.Text);

                if (string.IsNullOrEmpty(ddlClientes.SelectedValue) || ddlClientes.SelectedValue == "0")
                {
                    lblMensajes.Visible = true;
                    lblMensajes.Text = "Debe seleccionar un cliente.";
                    lblMensajes.ForeColor = System.Drawing.Color.Orange;
                    return;
                }

                if (ListaCarritoEdicion.Count == 0)
                {
                    lblMensajes.Visible = true;
                    lblMensajes.Text = "Debe agregar al menos un producto a la venta.";
                    lblMensajes.ForeColor = System.Drawing.Color.Orange;
                    return;
                }

                VentaNegocio negocio = new VentaNegocio();
                ProductoNegocio prodNegocio = new ProductoNegocio();
                List<Dominio.Producto> productos = prodNegocio.Listar();

                List<Dominio.Venta> listaVentas = negocio.listar();
                Dominio.Venta ventaOriginal = listaVentas.Find(x => x.Id == idVenta);

                
                foreach (var item in ListaCarritoEdicion)
                {
                    Dominio.Producto prodActual = productos.Find(x => x.Id == item.Producto.Id);
                    if (prodActual == null)
                    {
                        lblMensajes.Visible = true;
                        lblMensajes.Text = $"El producto '{item.Producto.Nombre}' ya no existe.";
                        lblMensajes.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    int stockDisponible = prodActual.StockActual;

                    if (ventaOriginal != null && ventaOriginal.Detalles != null)
                    {
                        Dominio.DetalleVenta detalleOriginal = ventaOriginal.Detalles.Find(x => x.Producto.Id == item.Producto.Id);
                        if (detalleOriginal != null)
                        {
                            stockDisponible += detalleOriginal.Cantidad;
                        }
                    }

                    if (item.Cantidad > stockDisponible)
                    {
                        lblMensajes.Visible = true;
                        lblMensajes.Text = $"❌ Stock insuficiente para '{prodActual.Nombre}'. Disponible: {stockDisponible}.";
                        lblMensajes.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }

                Dominio.Venta ventaModificada = new Dominio.Venta();
                ventaModificada.Id = idVenta;
                ventaModificada.NumeroFactura = txtNumeroFactura.Text;
                ventaModificada.Fecha = DateTime.Parse(txtFecha.Text);
                ventaModificada.Estado = ddlEstadoPago.SelectedValue;
                ventaModificada.Cliente = new Dominio.Cliente();
                ventaModificada.Cliente.Id = int.Parse(ddlClientes.SelectedValue);
                ventaModificada.Detalles = ListaCarritoEdicion;

                decimal total = 0;
                foreach (var item in ventaModificada.Detalles)
                {
                    total += item.PrecioUnitario * item.Cantidad;
                }
                ventaModificada.Total = total;

                negocio.modificar(ventaModificada);

                Session["CarritoEdicionVenta"] = null;
                Response.Redirect("Ventas.aspx", false);
            }
            catch (Exception ex)
            {
                lblMensajes.Visible = true;
                lblMensajes.Text = "Error al guardar los cambios: " + ex.Message;
                lblMensajes.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void chkConfirmarEliminacion_CheckedChanged(object sender, EventArgs e)
        {
            btnEliminar.Visible = chkConfirmarEliminacion.Checked;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmarEliminacion.Checked)
                {
                    VentaNegocio negocio = new VentaNegocio();
                    negocio.anularVenta(int.Parse(txtIdVenta.Text));
                    Session["CarritoEdicionVenta"] = null;
                    Response.Redirect("Ventas.aspx", false);
                }
            }
            catch (Exception ex)
            {
                lblMensajes.Visible = true;
                lblMensajes.Text = "Error al eliminar: " + ex.Message;
                lblMensajes.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void CargarDesplegables()
        {
            try
            {
                ClienteNegocio clienteNegocio = new ClienteNegocio();
                ddlClientes.DataSource = clienteNegocio.Listar();
                ddlClientes.DataValueField = "Id";
                ddlClientes.DataTextField = "NombreCompleto";
                ddlClientes.DataBind();
                ddlClientes.Items.Insert(0, new ListItem("Seleccione un cliente...", "0"));

                ProductoNegocio productoNegocio = new ProductoNegocio();
                ddlProductos.DataSource = productoNegocio.Listar();
                ddlProductos.DataValueField = "Id";
                ddlProductos.DataTextField = "Nombre";
                ddlProductos.DataBind();
                ddlProductos.Items.Insert(0, new ListItem("Seleccione un producto...", "0"));
            }
            catch (Exception ex)
            {
                lblMensajes.Visible = true;
                lblMensajes.Text = "Error al cargar los datos de los desplegables: " + ex.Message;
                lblMensajes.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}