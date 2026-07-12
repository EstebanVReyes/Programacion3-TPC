using Dominio;
using Negocio;
using System;
using System.Collections.Generic;

namespace ComercioWeb
{
    public partial class DetalleCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarCompra(id);
                    CargarDetalles(id);
                }
                else
                {
                    lblMensaje.Text = "No se especifico una compra.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void CargarCompra(int id)
        {
            try
            {
                CompraNegocio negocio = new CompraNegocio();
                List<Compra> lista = negocio.listar();
                Compra seleccionada = lista.Find(x => x.Id == id);

                if (seleccionada != null)
                {
                    lblNumeroCompra.Text = seleccionada.Id.ToString();
                    lblFecha.Text = seleccionada.Fecha.ToString("dd/MM/yyyy");
                    lblProveedor.Text = seleccionada.Proveedor.Nombre;
                    lblTotal.Text = seleccionada.Total.ToString("C");
                }
                else
                {
                    lblMensaje.Text = "No se encontro la compra solicitada.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar la compra: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void CargarDetalles(int id)
        {
            try
            {
                CompraNegocio negocio = new CompraNegocio();
                List<Dominio.DetalleCompra> listaDetalles = negocio.listarDetalles(id);

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
