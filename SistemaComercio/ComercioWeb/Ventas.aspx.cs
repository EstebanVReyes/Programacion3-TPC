using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace ComercioWeb
{
    public partial class Ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Seguridad.esAdmin(Session["usuario"]) || Seguridad.esVendedor(Session["usuario"])))
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                CargarVentas();
                CargarCatalogo(); 
            }
        }

        private void CargarVentas()
        {
            VentaNegocio negocio = new VentaNegocio();
            List<Venta> lista = negocio.listar();
            Session["listaVentas"] = lista;
            gvVentas.DataSource = lista;
            gvVentas.DataBind();
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Venta> lista = (List<Venta>)Session["listaVentas"];
            string filtro = txtFiltro.Text.ToUpper();

            List<Venta> listaFiltrada = lista.FindAll(x =>
                x.Cliente.Nombre.ToUpper().Contains(filtro) ||
                x.Cliente.Apellido.ToUpper().Contains(filtro) ||
                x.NumeroFactura.ToUpper().Contains(filtro)
            );

            gvVentas.DataSource = listaFiltrada;
            gvVentas.DataBind();
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Venta> lista = (List<Venta>)Session["listaVentas"];

            if (ddlEstado.SelectedValue == "")
            {
                gvVentas.DataSource = lista;
            }
            else
            {
                List<Venta> listaFiltrada = lista.FindAll(x =>
                    x.Estado == ddlEstado.SelectedValue
                );
                gvVentas.DataSource = listaFiltrada;
            }

            gvVentas.DataBind();
        }



        public void CargarCatalogo()
        {
            
            CategoriaNegocio negocio = new CategoriaNegocio();

            ddlCategoria.DataSource = negocio.Listar();
            ddlCategoria.DataTextField = "Descripcion"; 
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();


            ddlCategoria.Items.Insert(0, new ListItem("Seleccione una Categoría...", "0"));

       
            ddlProducto.Items.Clear();
            ddlProducto.Items.Insert(0, new ListItem("Esperando categoría...", "0"));
        }

      
        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCategoria = int.Parse(ddlCategoria.SelectedValue);

            if (idCategoria > 0)
            {
             
                CargarProductos(idCategoria);
            }
            else
            {
             
                ddlProducto.Items.Clear();
                ddlProducto.Items.Insert(0, new ListItem("Esperando categoría...", "0"));
            }
        }

        private void CargarProductos(int idCategoria)
        {
            ProductoNegocio negocio = new ProductoNegocio();

            
            ddlProducto.DataSource = negocio.listarPorCategoria(idCategoria);
            ddlProducto.DataTextField = "Nombre"; 
            ddlProducto.DataValueField = "Id";   
            ddlProducto.DataBind();

            ddlProducto.Items.Insert(0, new ListItem("Seleccione un Producto...", "0"));
        }


        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProducto = int.Parse(ddlProducto.SelectedValue);

           
            List<Venta> lista = (List<Venta>)Session["listaVentas"];

            if (idProducto > 0)
            {
                
                List<Venta> listaFiltrada = lista.FindAll(venta =>
                    venta.Detalles != null &&
                    venta.Detalles.Exists(detalle => detalle.Producto.Id == idProducto)
                );

                gvVentas.DataSource = listaFiltrada;
            }
            else
            {
               
                gvVentas.DataSource = lista;
            }

           
            gvVentas.DataBind();
        }
    }
    }
