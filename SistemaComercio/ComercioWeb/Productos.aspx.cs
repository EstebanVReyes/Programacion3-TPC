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
            Producto producto = new Producto
            {
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                Precio = decimal.Parse(txtPrecio.Text),
                StockActual = int.Parse(txtStock.Text)
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