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
                CargarDesplegables();
            }

        }


        private void CargarDesplegables()
        {
            try
            {
                CategoriaNegocio negocioCategoria = new CategoriaNegocio();
                ddlCategorias.DataSource = negocioCategoria.Listar();
                ddlCategorias.DataTextField = "Descripcion";
                ddlCategorias.DataValueField = "Id";
                ddlCategorias.DataBind();
                ddlCategorias.Items.Insert(0, new ListItem("Seleccione una categoría...", ""));

                MarcaNegocio negocioMarca = new MarcaNegocio();
                ddlMarcas.DataSource = negocioMarca.Listar();
                ddlMarcas.DataTextField = "Descripcion";
                ddlMarcas.DataValueField = "Id";
                ddlMarcas.DataBind();
                ddlMarcas.Items.Insert(0, new ListItem("Seleccione una marca...", ""));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar listas: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            imgProducto.ImageUrl = string.IsNullOrWhiteSpace(txtUrlImagen.Text)
                ? "https://t3.ftcdn.net/jpg/02/48/42/64/360_F_248426448_NVKLywWqArG2ADUxDq6QprtIzsF82dMF.jpg" : txtUrlImagen.Text;
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
                    PorcentajeGanancia = decimal.Parse(txtPorcentajeGanancia.Text),
                    UrlImagen = txtUrlImagen.Text.Trim()
                };

                
                nuevoProducto.Codigo = "PROD-" + DateTime.Now.ToString("HHmmss"); 
                nuevoProducto.StockMinimo = 5;

                nuevoProducto.Marca = new Marca();
                nuevoProducto.Marca.Id = int.Parse(ddlMarcas.SelectedValue);

                nuevoProducto.Categoria = new Categoria();
                nuevoProducto.Categoria.Id = int.Parse(ddlCategorias.SelectedValue);

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
            txtPorcentajeGanancia.Text = "";

            txtUrlImagen.Text = "";
            ddlCategorias.SelectedIndex = 0;
            ddlMarcas.SelectedIndex = 0;
            imgProducto.ImageUrl = "https://t3.ftcdn.net/jpg/02/48/42/64/360_F_248426448_NVKLywWqArG2ADUxDq6QprtIzsF82dMF.jpg";
        }
    }
}