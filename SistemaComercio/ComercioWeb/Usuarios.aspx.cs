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
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {

            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
               
                gvUsuarios.DataSource = negocio.ListarActivos();
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
               
                Session["Error"] = "Hubo un problema al cargar los usuarios: " + ex.Message;
               
            }
        }
    }
}