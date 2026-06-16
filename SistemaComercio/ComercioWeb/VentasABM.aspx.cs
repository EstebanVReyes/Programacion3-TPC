using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ComercioWeb
{
    public partial class VentasABM : System.Web.UI.Page
    {
        private static List<Venta> pedidos = new List<Venta>();

        protected void Page_Load(object sender, EventArgs e)

        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            int cantidad = int.Parse(txtCantidad.Text);
            decimal precioUnitario = decimal.Parse(txtPrecioUnitario.Text);


            Venta pedido = new Venta
            {
                Usuario = new Usuario
                {
                    Nombre = txtCliente.Text
                },

                Fecha = DateTime.Now,
                Total = cantidad * precioUnitario,

                Detalles = new List<DetalleVenta>
    {
        new DetalleVenta
        {
            Producto = new Producto
            {
                Nombre = txtProducto.Text
            },
            Cantidad = cantidad,
            PrecioUnitario = precioUnitario
        }
    }
            };

            pedidos.Add(pedido);

            LimpiarFormulario();

            lblMensaje.Text = "Venta registrada correctamente.";
            lblMensaje.ForeColor = System.Drawing.Color.Green;
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblMensaje.Text = "";
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