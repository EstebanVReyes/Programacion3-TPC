using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ComercioWeb
{
    public partial class ProductosABM : System.Web.UI.Page
    {
        private static List<Producto> productos = new List<Producto>();

        protected void Page_Load(object sender, EventArgs e)
        {
      
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Producto nuevoProducto = new Producto
            {
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                Precio = decimal.Parse(txtPrecio.Text),
                StockActual = int.Parse(txtStock.Text)
            };

            ProductoNegocio negocio = new ProductoNegocio();
            negocio.Agregar(nuevoProducto);

            productos.Add(nuevoProducto);

            LimpiarFormulario();

            lblMensaje.Text = "Producto guardado correctamente.";
            lblMensaje.ForeColor = System.Drawing.Color.Green;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblMensaje.Text = "";
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
