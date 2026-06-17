using System;
using System.Collections.Generic;
using Dominio;
using Negocio;

namespace ComercioWeb
{
    
    public partial class FormularioProducto : System.Web.UI.Page
    {
      


        protected void btnAceptar_Click(object sender, EventArgs e)
        {
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

               
                if (Request.QueryString["id"] != null)
                {
                    nuevoProducto.Id = int.Parse(txtId.Text);
                    negocio.Modificar(nuevoProducto);
                    lblMensajes.Text = "Producto modificado correctamente.";
                }
                else
                {
                    negocio.Agregar(nuevoProducto);
                    lblMensajes.Text = "Producto guardado correctamente.";
                }

                lblMensajes.ForeColor = System.Drawing.Color.Green;

              
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

      
        protected void chkConfirmarEliminacion_CheckedChanged(object sender, EventArgs e)
        {
            btnEliminar.Visible = chkConfirmarEliminacion.Checked;
        }

        // Este método responde al botón Eliminar
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmarEliminacion.Checked)
                {
                    ProductoNegocio negocio = new ProductoNegocio();
                    int idAEliminar = int.Parse(txtId.Text);

                    negocio.Eliminar(idAEliminar);

                    Response.Redirect("Productos.aspx", false);
                }
            }
            catch (Exception ex)
            {
                lblMensajes.Text = "Error al eliminar: " + ex.Message;
                lblMensajes.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
