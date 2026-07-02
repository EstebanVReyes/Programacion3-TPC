using System;
using System.Collections.Generic;
using Dominio;
using Negocio;
using System.Web.UI.WebControls;

namespace ComercioWeb
{
    public partial class VentasABM : System.Web.UI.Page
    {
        
        public List<Dominio.DetalleVenta> ListaCarritoNueva
        {
            get
            {
                if (Session["CarritoNuevaVenta"] == null)
                    Session["CarritoNuevaVenta"] = new List<Dominio.DetalleVenta>();
                return (List<Dominio.DetalleVenta>)Session["CarritoNuevaVenta"];
            }
            set
            {
                Session["CarritoNuevaVenta"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDesplegables();
               
                Session["CarritoNuevaVenta"] = null;
                ActualizarTablaYTotal();
            }
        }

        private void CargarDesplegables()
        {
            try
            {
                ClienteNegocio clienteNegocio = new ClienteNegocio();
                ddlCliente.DataSource = clienteNegocio.Listar();
                ddlCliente.DataValueField = "Id";
                ddlCliente.DataTextField = "NombreCompleto"; // Ajustado según tu clase Cliente
                ddlCliente.DataBind();
                ddlCliente.Items.Insert(0, new ListItem("Seleccione un cliente...", ""));

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
                        
                        txtPrecioUnitario.Text = productoSeleccionado.Precio.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                else
                {
                    txtPrecioUnitario.Text = string.Empty;
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
                    MostrarMensaje("La cantidad debe ser un número mayor a cero.", System.Drawing.Color.Orange);
                    return;
                }

                if (!decimal.TryParse(txtPrecioUnitario.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal precio))
                {
                    MostrarMensaje("No se pudo determinar el precio del producto.", System.Drawing.Color.Orange);
                    return;
                }

                ProductoNegocio prodNegocio = new ProductoNegocio();
                Producto prodSelect = prodNegocio.Listar().Find(x => x.Id == idProducto);

                if (prodSelect != null)
                {
                    
                    if (cantidad > prodSelect.StockActual)
                    {
                        MostrarMensaje($"❌ Stock insuficiente. Solo quedan {prodSelect.StockActual} unidades de {prodSelect.Nombre}.", System.Drawing.Color.Red);
                        return;
                    }

                    List<Dominio.DetalleVenta> temporal = ListaCarritoNueva;

                    
                    Dominio.DetalleVenta detalleExistente = temporal.Find(x => x.Producto.Id == idProducto);
                    if (detalleExistente != null)
                    {
                     
                        if ((detalleExistente.Cantidad + cantidad) > prodSelect.StockActual)
                        {
                            MostrarMensaje($"❌ No puede agregar más unidades. El stock máximo es {prodSelect.StockActual}.", System.Drawing.Color.Red);
                            return;
                        }
                        detalleExistente.Cantidad += cantidad;
                    }
                    else
                    {
                        
                        Dominio.DetalleVenta nuevoDetalle = new Dominio.DetalleVenta();
                        nuevoDetalle.Producto = prodSelect;
                        nuevoDetalle.Cantidad = cantidad;
                        nuevoDetalle.PrecioUnitario = precio;
                        temporal.Add(nuevoDetalle);
                    }

                    ListaCarritoNueva = temporal; 

                 
                    ddlProducto.SelectedIndex = 0;
                    txtPrecioUnitario.Text = "";
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
                    List<Dominio.DetalleVenta> temporal = ListaCarritoNueva;

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

            decimal totalVenta = 0;
            foreach (var item in ListaCarritoNueva)
            {
                totalVenta += (item.PrecioUnitario * item.Cantidad);
            }

            lblTotal.Text = totalVenta.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
        }

        
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddlCliente.SelectedValue))
                {
                    MostrarMensaje("Debe seleccionar un cliente.", System.Drawing.Color.Orange);
                    return;
                }

                if (ListaCarritoNueva.Count == 0)
                {
                    MostrarMensaje("Debe agregar al menos un producto a la venta.", System.Drawing.Color.Orange);
                    return;
                }

                Venta nuevaVenta = new Venta();
                VentaNegocio negocio = new VentaNegocio();

                
                nuevaVenta.NumeroFactura = "FAC-" + DateTime.Now.Ticks.ToString(); 
                nuevaVenta.Total = decimal.Parse(lblTotal.Text, System.Globalization.CultureInfo.InvariantCulture);
                nuevaVenta.Cliente = new Cliente();
                nuevaVenta.Cliente.Id = int.Parse(ddlCliente.SelectedValue);

                
                nuevaVenta.Detalles = ListaCarritoNueva;

            
                negocio.agregar(nuevaVenta);

              
                Session["CarritoNuevaVenta"] = null;

                // Redirigimos al listado
                Response.Redirect("Ventas.aspx", false);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al registrar la venta: " + ex.Message, System.Drawing.Color.Red);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Session["CarritoNuevaVenta"] = null;
            ddlCliente.SelectedIndex = 0;
            ddlProducto.SelectedIndex = 0;
            txtCantidad.Text = "";
            txtPrecioUnitario.Text = "";
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