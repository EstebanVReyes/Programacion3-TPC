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
    public partial class ProveedoresABM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();

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
        private void CargarProductos()
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            try
            {
                var productos = negocio.ListarProductosActivos();
                cblProductos.DataSource = productos;
                cblProductos.DataValueField = "Key"; 
                cblProductos.DataTextField = "Value";
                cblProductos.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar productos: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
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

                List<int> productosAsociados = negocio.ObtenerProductosDeProveedor(id);
                foreach (ListItem item in cblProductos.Items)
                {
                    int productoId = int.Parse(item.Value);
                    item.Selected = productosAsociados.Contains(productoId);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar el proveedor: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        private List<int> ObtenerProductosSeleccionados()
        {
            List<int> seleccionados = new List<int>();
            foreach (ListItem item in cblProductos.Items)
            {
                if (item.Selected)
                    seleccionados.Add(int.Parse(item.Value));
            }
            return seleccionados;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            try
            {
                Proveedor proveedor = new Proveedor();
                proveedor.Nombre = txtNombre.Text.Trim();
                proveedor.Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text)
                    ? null : txtTelefono.Text.Trim();
                proveedor.Descripcion = string.IsNullOrWhiteSpace(txtDescripcion.Text)
                    ? null : txtDescripcion.Text.Trim();

                List<int> productosSeleccionados = ObtenerProductosSeleccionados();

                string idQueryString = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(idQueryString))
                {
                    proveedor.Id = int.Parse(idQueryString);
                    negocio.Modificar(proveedor);
                    negocio.ActualizarProductosProveedor(proveedor.Id, productosSeleccionados);

                    lblMensaje.Text = "Proveedor modificado correctamente.";
                }
                else
                {
                    int nuevoId = negocio.Agregar(proveedor);
                    negocio.ActualizarProductosProveedor(nuevoId, productosSeleccionados);

                    lblMensaje.Text = "Proveedor agregado correctamente.";
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
                lblMensaje.Text = "Error al eliminar: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }


    }
}