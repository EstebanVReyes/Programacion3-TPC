using System;
using System.Collections.Generic;
using Dominio;
using Negocio;

namespace ComercioWeb
{
    public partial class Ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarVentas();
            }
        }

        private void CargarVentas()
        {
            VentaNegocio negocio = new VentaNegocio();

            
            gvVentas.DataSource = negocio.listar();
            gvVentas.DataBind();
        }

        
        protected void gvVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            int idVentaSeleccionada = Convert.ToInt32(gvVentas.SelectedDataKey.Value);

           
            VentaNegocio negocio = new VentaNegocio();
            List<DetalleVenta> listaDetalles = negocio.listarDetalles(idVentaSeleccionada);

           
            gvDetalles.DataSource = listaDetalles;
            gvDetalles.DataBind();

           
            string nroFactura = gvVentas.SelectedRow.Cells[1].Text;
            lblTituloDetalle.Text = "Productos de la Factura: " + nroFactura;
        }
    }
}