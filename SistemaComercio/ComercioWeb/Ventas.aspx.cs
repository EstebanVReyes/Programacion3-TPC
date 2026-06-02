using System;
using System.Collections.Generic;
using Dominio;

namespace ComercioWeb
{
    public partial class Ventas : System.Web.UI.Page
    {
        private static List<Venta> ventas = new List<Venta>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarVentas();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
           
            int cantidad = int.Parse(txtCantidad.Text);
            decimal precioUnitario = decimal.Parse(txtPrecioUnitario.Text);



            Venta venta = new Venta
            {

                Cliente = new Cliente
                {
                    Nombre = txtCliente.Text
                },

                Producto = new Producto
                {
                    Nombre = txtProducto.Text
                },

                Cantidad = cantidad,
                PrecioUnitario = precioUnitario,
                Total = cantidad * precioUnitario,
                Fecha = DateTime.Now,
                NumeroFactura = "FAC-" + DateTime.Now.Ticks
            };

            ventas.Add(venta);

            LimpiarFormulario();
            CargarVentas();

            lblMensaje.Text = "Venta registrada correctamente.";
            lblMensaje.ForeColor = System.Drawing.Color.Green;
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblMensaje.Text = "";
        }

        private void CargarVentas()
        {
            gvVentas.DataSource = ventas;
            gvVentas.DataBind();
        }

        private void LimpiarFormulario()
        {
            txtCliente.Text = "";
            txtProducto.Text = "";
            txtCantidad.Text = "";
            txtPrecioUnitario.Text = "";
        }
    }
}