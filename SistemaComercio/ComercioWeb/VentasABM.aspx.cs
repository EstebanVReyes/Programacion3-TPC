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

                ProductoNegocio prodNegocio = new ProductoNegocio();
                Session["productosVenta"] = prodNegocio.Listar();

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
                ddlCliente.DataTextField = "NombreCompleto";
                ddlCliente.DataBind();
                ddlCliente.Items.Insert(0, new ListItem("Seleccione un cliente...", ""));

                CategoriaNegocio catNegocio = new CategoriaNegocio();
                ddlCategoria.DataSource = catNegocio.Listar();
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataTextField = "Descripcion";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("Todas", ""));

                MarcaNegocio marcaNegocio = new MarcaNegocio();
                ddlMarca.DataSource = marcaNegocio.Listar();
                ddlMarca.DataValueField = "Id";
                ddlMarca.DataTextField = "Descripcion";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("Todas", ""));

                FiltrarProductos();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar datos: " + ex.Message, System.Drawing.Color.Red);
            }
        }

        private void FiltrarProductos()
        {
            List<Producto> todos = (List<Producto>)Session["productosVenta"];
            List<Producto> filtrados = new List<Producto>(todos);

            if (!string.IsNullOrEmpty(ddlCategoria.SelectedValue))
            {
                int idCat = int.Parse(ddlCategoria.SelectedValue);
                filtrados = filtrados.FindAll(p => p.Categoria.Id == idCat);
            }

            if (!string.IsNullOrEmpty(ddlMarca.SelectedValue))
            {
                int idMarca = int.Parse(ddlMarca.SelectedValue);
                filtrados = filtrados.FindAll(p => p.Marca.Id == idMarca);
            }

            ddlProducto.Items.Clear();
            ddlProducto.DataSource = filtrados;
            ddlProducto.DataValueField = "Id";
            ddlProducto.DataTextField = "Nombre";
            ddlProducto.DataBind();
            ddlProducto.Items.Insert(0, new ListItem("Seleccione un producto...", ""));
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMarca.SelectedIndex = 0;
            FiltrarProductos();
        }

        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarProductos();
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
                        decimal precioConGanancia = productoSeleccionado.Precio * (1 + productoSeleccionado.PorcentajeGanancia / 100);
                        txtPrecioUnitario.Text = precioConGanancia.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                      
                        txtStock.Text = productoSeleccionado.StockActual.ToString();
                    }
                }
                else
                {
                    txtPrecioUnitario.Text = string.Empty;
                    txtStock.Text = string.Empty;
                }
                ActualizarStockVisual();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al obtener el precio y stock: " + ex.Message, System.Drawing.Color.Red);
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
                        decimal precioConGanancia = prodSelect.Precio * (1 + prodSelect.PorcentajeGanancia / 100);
                        nuevoDetalle.PrecioUnitario = precioConGanancia;
                        temporal.Add(nuevoDetalle);
                    }

                   
                    ListaCarritoNueva = temporal;

                   
                    ActualizarTablaYTotal();

                    
                    ddlProducto.SelectedIndex = 0;
                    txtPrecioUnitario.Text = "";
                    txtCantidad.Text = "";
                    txtStock.Text = "";
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
        private void ActualizarStockVisual()
        {
           
            if (!string.IsNullOrEmpty(ddlProducto.SelectedValue) && ddlProducto.SelectedValue != "0")
            {
                int idProd = int.Parse(ddlProducto.SelectedValue);
                ProductoNegocio prodNegocio = new ProductoNegocio();
                Producto prodSeleccionado = prodNegocio.Listar().Find(x => x.Id == idProd);

                if (prodSeleccionado != null)
                {
                    int stockRealDisponible = prodSeleccionado.StockActual;

                   
                    Dominio.DetalleVenta detalleExistente = ListaCarritoNueva.Find(x => x.Producto.Id == idProd);
                    if (detalleExistente != null)
                    {
                      
                        stockRealDisponible -= detalleExistente.Cantidad;
                    }

                  
                    txtStock.Text = stockRealDisponible.ToString();
                }
            }
            else
            {
                
                txtStock.Text = "";
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
                        ActualizarStockVisual();
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
                totalVenta += item.Subtotal ;
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

                ProductoNegocio prodNegocio = new ProductoNegocio();
                List<Producto> listaProductosDB = prodNegocio.Listar();

                
                foreach (var item in ListaCarritoNueva)
                {
                    Producto prodDB = listaProductosDB.Find(x => x.Id == item.Producto.Id);

                    if (prodDB == null)
                    {
                        MostrarMensaje($"❌ El producto '{item.Producto.Nombre}' ya no existe en el sistema.", System.Drawing.Color.Red);
                        return;
                    }

                    if (item.Cantidad > prodDB.StockActual)
                    {
                        MostrarMensaje($"❌ Stock insuficiente para '{item.Producto.Nombre}'. Stock disponible actual: {prodDB.StockActual}. Por favor, quite el artículo o ajuste la cantidad.", System.Drawing.Color.Red);
                        return;
                    }
                }

                
                Venta nuevaVenta = new Venta();
                VentaNegocio negocio = new VentaNegocio();

                nuevaVenta.NumeroFactura = "FAC-" + DateTime.Now.Ticks.ToString();
                nuevaVenta.Total = decimal.Parse(lblTotal.Text, System.Globalization.CultureInfo.InvariantCulture);
                nuevaVenta.Cliente = new Cliente();
                nuevaVenta.Cliente.Id = int.Parse(ddlCliente.SelectedValue);
                nuevaVenta.Detalles = ListaCarritoNueva;

               
                negocio.agregar(nuevaVenta);

                
                foreach (var item in ListaCarritoNueva)
                {
                    Producto prodDB = listaProductosDB.Find(x => x.Id == item.Producto.Id);
                    prodDB.StockActual -= item.Cantidad; 
                    
                }

                
                Session["CarritoNuevaVenta"] = null;

                
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
            ddlCategoria.SelectedIndex = 0;
            ddlMarca.SelectedIndex = 0;
            ddlProducto.SelectedIndex = 0;
            txtCantidad.Text = "";
            txtPrecioUnitario.Text = "";
            txtStock.Text = "";
            lblMensaje.Text = "";
            FiltrarProductos();
            ActualizarTablaYTotal();
        }

        private void MostrarMensaje(string mensaje, System.Drawing.Color color)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.ForeColor = color;
        }
    }
}