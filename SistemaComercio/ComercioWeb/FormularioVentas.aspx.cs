using System;
using System.Collections.Generic;
using System.Web.UI;
using Dominio;
using Negocio;

namespace ComercioWeb
{
    public partial class FormularioVenta : Page
    {
        private VentaNegocio negocioVenta = new VentaNegocio();
        private int idVenta;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out idVenta))
                {
                    CargarDetalle();
                }
                else
                {
                    lblMensajes.Visible = true;
                    lblMensajes.Text = "No se especificó una venta válida.";
                }
            }
            else
            {
                idVenta = int.Parse(Request.QueryString["id"]);
            }
        }

        private void CargarDetalle()
        {
            List<Venta> ventas = negocioVenta.listar();
            Venta venta = ventas.Find(v => v.Id == idVenta);

            if (venta == null)
            {
                lblMensajes.Visible = true;
                lblMensajes.Text = "No se encontró la venta.";
                return;
            }

            txtIdVenta.Text = venta.Id.ToString();
            txtNumeroFactura.Text = venta.NumeroFactura;
            txtFecha.Text = venta.Fecha.ToString("dd/MM/yyyy");
            txtCliente.Text = venta.Cliente.Nombre + " " + venta.Cliente.Apellido;
            lblEstado.Text = venta.Estado;
            txtTotal.Text = venta.Total.ToString("N2");

            dgvDetalles.DataSource = venta.Detalles;
            dgvDetalles.DataBind();

            if (venta.Estado == "Pendiente")
            {
                btnInformarPago.Visible = true;
                btnAnular.Visible = true;
            }
            else
            {
                btnInformarPago.Visible = false;
                btnAnular.Visible = false;
            }
        }

        protected void btnInformarPago_Click(object sender, EventArgs e)
        {
            try
            {
                negocioVenta.informarPago(idVenta);
                Response.Redirect("FormularioVentas.aspx?id=" + idVenta);
            }
            catch (Exception ex)
            {
                lblMensajes.Visible = true;
                lblMensajes.Text = "Error al informar el pago: " + ex.Message;
            }
        }

        protected void btnAnular_Click(object sender, EventArgs e)
        {
            try
            {
                negocioVenta.anularVenta(idVenta);
                Response.Redirect("FormularioVentas.aspx?id=" + idVenta);
            }
            catch (Exception ex)
            {
                lblMensajes.Visible = true;
                lblMensajes.Text = "Error al anular la venta: " + ex.Message;
            }
        }
    }
}
