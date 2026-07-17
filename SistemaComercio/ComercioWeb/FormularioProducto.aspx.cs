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
                            txtPorcentajeGanancia.Text = Math.Round(seleccionado.PorcentajeGanancia, 2).ToString();
                            txtDescripcion.Text = seleccionado.Descripcion;

                            
                            ddlCategorias.SelectedValue = seleccionado.Categoria.Id.ToString();
                            ddlMarcas.SelectedValue = seleccionado.Marca.Id.ToString();

                            if (!string.IsNullOrWhiteSpace(seleccionado.UrlImagen))
                            {
                                txtUrlImagen.Text = seleccionado.UrlImagen;
                                imgProducto.ImageUrl = seleccionado.UrlImagen;
                            }
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

                MarcaNegocio negocioMarca = new MarcaNegocio();
                ddlMarcas.DataSource = negocioMarca.Listar();
                ddlMarcas.DataTextField = "Descripcion";
                ddlMarcas.DataValueField = "Id";
                ddlMarcas.DataBind();
                ddlMarcas.Items.Insert(0, new ListItem("Seleccione una marca...", ""));
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
                nuevoProducto.Descripcion = txtDescripcion.Text;
                nuevoProducto.UrlImagen = txtUrlImagen.Text.Trim();

                nuevoProducto.Codigo = "PROD-" + DateTime.Now.ToString("HHmmss");
                nuevoProducto.PorcentajeGanancia = decimal.Parse(txtPorcentajeGanancia.Text);
                nuevoProducto.StockMinimo = 5;

                nuevoProducto.Categoria = new Categoria();
                nuevoProducto.Categoria.Id = int.Parse(ddlCategorias.SelectedValue);

                nuevoProducto.Marca = new Marca();
                nuevoProducto.Marca.Id = int.Parse(ddlMarcas.SelectedValue);

                if (Request.QueryString["id"] != null)
                {
                    nuevoProducto.Id = int.Parse(txtId.Text);
                    
                    ProductoNegocio negAux = new ProductoNegocio();
                    Producto original = negAux.Listar().Find(p => p.Id == nuevoProducto.Id);
                    if (original != null) nuevoProducto.Codigo = original.Codigo;

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
                lblMensajes.Text = "Por favor, ingrese un número válido en el Porcentaje de Ganancia.";
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
            imgProducto.ImageUrl = string.IsNullOrWhiteSpace(txtUrlImagen.Text)
                ? "https://t3.ftcdn.net/jpg/02/48/42/64/360_F_248426448_NVKLywWqArG2ADUxDq6QprtIzsF82dMF.jpg" : txtUrlImagen.Text;
        }


    }
}