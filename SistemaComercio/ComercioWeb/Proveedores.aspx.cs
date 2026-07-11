using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace ComercioWeb
{
    public partial class Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["MensajeExito"] != null)
                {
                    lblMensajes.Text = Session["MensajeExito"].ToString();
                    lblMensajes.ForeColor = System.Drawing.Color.Green;
                    Session["MensajeExito"] = null;
                }

                CargarProveedores();
            }
        }

        private void CargarProveedores()
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            try
            {
                gvProveedores.DataSource = negocio.Listar();
                gvProveedores.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }
    }
}