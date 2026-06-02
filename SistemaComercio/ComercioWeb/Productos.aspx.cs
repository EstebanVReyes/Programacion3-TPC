using System;
using System.Collections.Generic;
using Dominio;

namespace ComercioWeb
{
    public partial class Productos : System.Web.UI.Page
    {
        private static List<Producto> productos = new List<Producto>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            decimal precio;
            int stock;

            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                lblMensaje.Text = "El precio debe ser numérico.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (!int.TryParse(txtStock.Text, out stock))
            {
                lblMensaje.Text = "El stock debe ser numérico.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            Producto producto = new Producto
            {
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                Precio = precio,
                StockActual = stock
            };

            productos.Add(producto);

            LimpiarFormulario();
            CargarProductos();

            lblMensaje.Text = "Producto guardado correctamente.";
            lblMensaje.ForeColor = System.Drawing.Color.Green;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblMensaje.Text = "";
        }

        private void CargarProductos()
        {
            gvProductos.DataSource = productos;
            gvProductos.DataBind();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
        }
    }
}