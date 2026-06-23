using System;
using System.Collections.Generic;
using System.Web.UI;
using Dominio;
using Negocio;



namespace ComercioWeb
{
    public partial class Default1 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarResumenVentas();
                CargarProductosMasVendidos();
                CargarProductosConMenosStock();
            }
        }

        private void CargarResumenVentas()
        {
            VentaNegocio negocio = new VentaNegocio();

            ResumenVenta resumen = negocio.ObtenerResumenVentasUltimos30Dias();

            lblCantidadVentas.Text = resumen.CantidadVentas.ToString();
            lblProductosVendidos.Text = resumen.CantidadProductosVendidos.ToString();
            lblImporteTotal.Text = "$" + resumen.ImporteTotal.ToString("N2");
        }

        private void CargarProductosMasVendidos()
        {
            VentaNegocio negocio = new VentaNegocio();

            List<ProductoMasVendido> productos = negocio.ListarProductosMasVendidosUltimos30Dias();

            chartProductos.Series["Productos"].Points.Clear();

            foreach (ProductoMasVendido producto in productos)
            {
                chartProductos.Series["Productos"].Points.AddXY(
                    producto.NombreProducto,
                    producto.CantidadVendida
                );
            }
        }

        private void CargarProductosConMenosStock()
        {
            ProductoNegocio negocio = new ProductoNegocio();

            List<ProductoBajoStock> productos = negocio.ListarProductosConMenosStock();

            dlProductosMenosStock.DataSource = productos;
            dlProductosMenosStock.DataBind();
        }
    }
}