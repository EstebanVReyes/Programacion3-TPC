using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ComercioWeb
{
    public partial class Usuarios : System.Web.UI.Page
    {
        private static List<Usuarios> usuarios = new List<Usuarios>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }




        private void CargarProductos()
        {
            gvUsuarios.DataSource = usuarios;
            gvUsuarios.DataBind();
        }

    }
}