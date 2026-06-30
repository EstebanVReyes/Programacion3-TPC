using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace ComercioWeb
{
    public partial class FormularioProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                CargarDesplegables();

                
                if (Request.QueryString["id"] != null)
                {
                    try
                    {
                        int id = int.Parse(Request.QueryString["id"]);
                        ProductoNegocio negocio = new ProductoNegocio();

                        List<Producto> lista = negocio.Listar();
                        Producto seleccionado = lista.Find(x => x.Id == id);

                        if (seleccionado != null)
                        {
                            txtId.Text = seleccionado.Id.ToString();
                            txtNombre.Text = seleccionado.Nombre;
                            txtPrecio.Text = Math.Round(seleccionado.Precio, 2).ToString();
                            txtStock.Text = seleccionado.StockActual.ToString();
                            txtDescripcion.Text = seleccionado.Descripcion;

                            
                            ddlCategorias.SelectedValue = seleccionado.Categoria.Id.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensajes.Text = "Error al cargar el producto: " + ex.Message;
                        lblMensajes.ForeColor = System.Drawing.Color.Red;
                    }
                }
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
            }
            catch (Exception ex)
            {
                lblMensajes.Text = "Error al cargar categorías: " + ex.Message;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;

            try
            {
                Producto nuevoProducto = new Producto();
                ProductoNegocio negocio = new ProductoNegocio();

                nuevoProducto.Nombre = txtNombre.Text;
                nuevoProducto.Precio = decimal.Parse(txtPrecio.Text);
                nuevoProducto.StockActual = int.Parse(txtStock.Text);
                nuevoProducto.Descripcion = txtDescripcion.Text;

                nuevoProducto.Codigo = "PROD-" + DateTime.Now.ToString("HHmmss");
                nuevoProducto.PorcentajeGanancia = 30;
                nuevoProducto.StockMinimo = 5;

               
                nuevoProducto.Categoria = new Categoria();
                nuevoProducto.Categoria.Id = int.Parse(ddlCategorias.SelectedValue);

                
                nuevoProducto.Marca = new Marca();
                nuevoProducto.Marca.Id = 1;

                if (Request.QueryString["id"] != null)
                {
                    nuevoProducto.Id = int.Parse(txtId.Text);
                    negocio.Modificar(nuevoProducto);
                }
                else
                {
                    negocio.Agregar(nuevoProducto);
                }

                Response.Redirect("Productos.aspx", false);
            }
            catch (FormatException)
            {
                lblMensajes.Text = "Por favor, ingrese números válidos en Precio y Stock.";
                lblMensajes.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                lblMensajes.Text = "Error al guardar: " + ex.Message;
                lblMensajes.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            imgProducto.ImageUrl = txtUrlImagen.Text;
        }

       


    }
}