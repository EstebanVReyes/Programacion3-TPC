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
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Seguridad.esAdmin(Session["usuario"]) || Seguridad.esVendedor(Session["usuario"])))
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                CargarProveedores();
            }

         
        }

        protected void CargarProveedores()
        {
            ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
            List<Proveedor> listaProveedores = proveedorNegocio.Listar();
            ddlProveedor.DataSource = listaProveedores;
            ddlProveedor.DataTextField = "Nombre";
            ddlProveedor.DataValueField = "Id";
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("--Seleccione un proveedor--", "0"));
        }



        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;

            try
            {
                Producto nuevoProducto = new Producto
                {
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    Precio = decimal.Parse(txtPrecio.Text),
                    StockActual = int.Parse(txtStock.Text),
                    PorcentajeGanancia = decimal.Parse(txtPorcentajeGanancia.Text) 

                };

                
                nuevoProducto.Codigo = "PROD-" + DateTime.Now.ToString("HHmmss"); 
             
                nuevoProducto.StockMinimo = 5;      

                nuevoProducto.Marca = new Marca();
                nuevoProducto.Marca.Id = 1;            

                nuevoProducto.Categoria = new Categoria();
                nuevoProducto.Categoria.Id = 1;        

                ProductoNegocio negocio = new ProductoNegocio();
                negocio.Agregar(nuevoProducto);

               

                LimpiarFormulario();

                lblMensaje.Text = "Producto guardado correctamente en la Base de Datos.";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
            }
            catch (FormatException)
            {
               
                lblMensaje.Text = "Por favor, ingrese números válidos en Precio y Stock.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
             
                lblMensaje.Text = "Error al guardar: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
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
            txtPorcentajeGanancia.Text = "";
        }
    }
}