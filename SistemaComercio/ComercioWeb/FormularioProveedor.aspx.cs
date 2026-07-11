using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace ComercioWeb
{
    public partial class FormularioProveedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idQueryString = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(idQueryString))
                {
                    int id = int.Parse(idQueryString);
                    CargarDatosProveedor(id);
                    litTitulo.Text = "Modificar Proveedor";
                    btnEliminar.Visible = true;
                }
            
            }
        }

        private void CargarDatosProveedor(int id)
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            try
            {
                Proveedor prov = negocio.ObtenerPorId(id);
                if (prov != null)
                {
                    txtNombre.Text = prov.Nombre;
                    txtTelefono.Text = prov.Telefono ?? "";
                    txtDescripcion.Text = prov.Descripcion ?? "";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar el proveedor: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    lblMensaje.Text = "El nombre es obligatorio.";
                    lblMensaje.ForeColor = System.Drawing.Color.Orange;
                    return;
                }

                Proveedor proveedor = new Proveedor();
                proveedor.Nombre = txtNombre.Text.Trim();
                proveedor.Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text)
                    ? null : txtTelefono.Text.Trim();
                proveedor.Descripcion = string.IsNullOrWhiteSpace(txtDescripcion.Text)
                    ? null : txtDescripcion.Text.Trim();

                string idQueryString = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(idQueryString))
                {
                    proveedor.Id = int.Parse(idQueryString);
                    negocio.Modificar(proveedor);
                    lblMensaje.Text = "Proveedor modificado correctamente.";
                }
                else
                {
                    negocio.Agregar(proveedor);
                    lblMensaje.Text = "Proveedor agregado correctamente.";
                    LimpiarFormulario();
                }

                lblMensaje.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            try
            {
                int id = int.Parse(Request.QueryString["id"]);
                negocio.Eliminar(id);

                lblMensaje.Text = "Proveedor dado de baja correctamente.";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                btnEliminar.Visible = false;
                btnGuardar.Enabled = false;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al dar de baja: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtDescripcion.Text = "";
        }
    }
}