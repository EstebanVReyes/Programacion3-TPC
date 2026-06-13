using Dominio;
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
        private static List<Articulo> articulos = new List<Articulo>();

        protected void Page_Load(object sender, EventArgs e)
        {
      
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Articulo articulo = new Articulo
            {
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                Precio = decimal.Parse(txtPrecio.Text),
                StockActual = int.Parse(txtStock.Text)
            };

            articulos.Add(articulo);

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
