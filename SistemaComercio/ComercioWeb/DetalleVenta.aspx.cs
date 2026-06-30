using Dominio;
using Negocio;
using System;
using System.Collections.Generic;

namespace ComercioWeb
{
    public partial class DetalleVenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarVenta(id);
                    CargarDetalles(id);
                }
                else
                {
                    lblMensaje.Text = "No se especificó una venta.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void CargarVenta(int id)
        {
            try
            {
                VentaNegocio negocio = new VentaNegocio();
                List<Venta> lista = negocio.listar();
                Venta seleccionada = lista.Find(x => x.Id == id);

                if (seleccionada != null)
                {
                    lblNumeroFactura.Text = seleccionada.NumeroFactura;
                    lblFecha.Text = seleccionada.Fecha.ToString("dd/MM/yyyy");
                    lblCliente.Text = seleccionada.Cliente.Nombre + " " + seleccionada.Cliente.Apellido;
                    lblEstado.Text = seleccionada.Estado;
                    lblTotal.Text = seleccionada.Total.ToString("C");
                }
                else
                {
                    lblMensaje.Text = "No se encontró la venta solicitada.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar la venta: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void CargarDetalles(int id)
        {
            try
            {
                VentaNegocio negocio = new VentaNegocio();
                List<Dominio.DetalleVenta> listaDetalles = negocio.listarDetalles(id);

                gvDetalles.DataSource = listaDetalles;
                gvDetalles.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los detalles: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
