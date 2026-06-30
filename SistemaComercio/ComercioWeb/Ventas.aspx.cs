using Dominio;
using negocio;
using Negocio;
using System;
using System.Collections.Generic;

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


        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {
            try
            {
                EmailService emailService = new EmailService();

                emailService.armarCorreo(
                    txtEmailDestino.Text,
                    txtAsunto.Text,
                    txtMensaje.Text
                );

                emailService.enviarEmail();

                lblMensajeMail.Text = "Mail enviado correctamente.";
                lblMensajeMail.CssClass = "text-success";
            }
            catch (Exception ex)
            {
                lblMensajeMail.Text = "Error al enviar el mail: " + ex.Message;
                lblMensajeMail.CssClass = "text-danger";
            }
        }
    }


}