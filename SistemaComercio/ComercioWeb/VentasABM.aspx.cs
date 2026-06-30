using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace ComercioWeb
{
    public partial class VentasABM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Seguridad.esAdmin(Session["usuario"]) || Seguridad.esVendedor(Session["usuario"]) || Seguridad.esCajero(Session["usuario"])))
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                CargarClientes();
                CargarProductos();
            }
        }

        private void CargarClientes()
        {
            ClienteNegocio negocioCliente = new ClienteNegocio();
            ddlCliente.DataSource = negocioCliente.Listar();
            ddlCliente.DataValueField = "Id";
            ddlCliente.DataTextField = "Nombre";
            ddlCliente.DataBind();

            ddlCliente.Items.Insert(0, new ListItem("Seleccione un cliente...", ""));
        }

        private void CargarProductos()
        {
            ProductoNegocio negocioProducto = new ProductoNegocio();
            List<Producto> listaProductos = negocioProducto.Listar();


            Session["listaProductos"] = listaProductos;

            ddlProducto.DataSource = listaProductos;
            ddlProducto.DataValueField = "Id";
            ddlProducto.DataTextField = "Nombre";
            ddlProducto.DataBind();

            ddlProducto.Items.Insert(0, new ListItem("Seleccione un producto...", ""));


            ddlProducto.SelectedIndex = 0;
        }

        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";

            if (ddlProducto.SelectedValue != "")
            {
                List<Producto> listaProductos = (List<Producto>)Session["listaProductos"];
                int idSeleccionado = int.Parse(ddlProducto.SelectedValue);

                Producto prodSeleccionado = listaProductos.Find(p => p.Id == idSeleccionado);

                if (prodSeleccionado != null)
                {
                    txtPrecioUnitario.Text = prodSeleccionado.Precio.ToString("0.00");

                    lblMensaje.Text = "Stock disponible: " + prodSeleccionado.StockActual;
                    lblMensaje.ForeColor = System.Drawing.Color.Blue;
                }
            }
            else
            {
                txtPrecioUnitario.Text = "";
                lblMensaje.Text = "";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;

            try
            {

                if (ddlCliente.SelectedValue == "" || ddlProducto.SelectedValue == "")
                {
                    lblMensaje.Text = "Por favor, seleccione un cliente y un producto.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (string.IsNullOrEmpty(txtCantidad.Text))
                {
                    lblMensaje.Text = "Debe ingresar una cantidad.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                int cantidadIngresada = int.Parse(txtCantidad.Text);


                List<Producto> listaProductos = (List<Producto>)Session["listaProductos"];
                int idProducto = int.Parse(ddlProducto.SelectedValue);
                Producto prodSeleccionado = listaProductos.Find(p => p.Id == idProducto);

                if (cantidadIngresada > prodSeleccionado.StockActual)
                {
                    lblMensaje.Text = $"Error: No podés vender {cantidadIngresada} unidades. Solo hay {prodSeleccionado.StockActual} disponibles.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }


                Venta nuevaVenta = new Venta();
                nuevaVenta.Cliente = new Cliente();
                nuevaVenta.Detalles = new List<Dominio.DetalleVenta>();

                nuevaVenta.NumeroFactura = "FAC-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                nuevaVenta.Cliente.Id = int.Parse(ddlCliente.SelectedValue);
                nuevaVenta.Total = decimal.Parse(txtPrecioUnitario.Text) * cantidadIngresada;


                Dominio.DetalleVenta detalle = new Dominio.DetalleVenta();
                detalle.Producto = new Producto();
                detalle.Producto.Id = idProducto;
                detalle.Cantidad = cantidadIngresada;
                detalle.PrecioUnitario = decimal.Parse(txtPrecioUnitario.Text);

                nuevaVenta.Detalles.Add(detalle);


                VentaNegocio negocioVenta = new VentaNegocio();
                negocioVenta.agregar(nuevaVenta);

                btnLimpiar_Click(null, null);


                CargarProductos();


                lblMensaje.Text = "¡Venta registrada con éxito!";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Ocurrió un error al intentar guardar: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlCliente.SelectedIndex = 0;
            ddlProducto.SelectedIndex = 0;
            txtCantidad.Text = "";
            txtPrecioUnitario.Text = "";
            lblMensaje.Text = "";
        }
    }
}