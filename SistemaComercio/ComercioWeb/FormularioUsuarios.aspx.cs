using System;
using System.Collections.Generic;
using Dominio;
using Negocio;

namespace ComercioWeb
{
    public partial class FormularioUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Seguridad.esAdmin(Session["usuario"])))
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    List<Usuario> lista = negocio.ListarActivos();
                    Usuario seleccionado = lista.Find(x => x.ID == id);

                    if (seleccionado != null)
                    {
                        txtId.Text = seleccionado.ID.ToString();
                        txtUsername.Text = seleccionado.Username;
                        txtPassword.Text = seleccionado.PasswordHash;
                        ddlTipoUsuario.SelectedValue = seleccionado.TipoUsuario;

                        lblTitulo.Text = "Modificar usuario";
                        lblSubtitle.Text = "Modifique los datos del usuario seleccionado.";
                    }
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario nuevo = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();

                nuevo.Username = txtUsername.Text;
                nuevo.PasswordHash = txtPassword.Text;
                nuevo.TipoUsuario = ddlTipoUsuario.SelectedValue;
                nuevo.Estado = true;

                if (Request.QueryString["id"] != null)
                {
                    nuevo.ID = int.Parse(txtId.Text);
                    negocio.Modificar(nuevo);
                }
                else
                {
                    negocio.Agregar(nuevo);
                }

                Response.Redirect("Usuarios.aspx", false);
            }
            catch (Exception ex)
            {
                lblMensajes.Text = "Error: " + ex.Message;
            }
        }
    }
}
