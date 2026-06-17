<%@ Page Title="Formulario de Producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioProducto.aspx.cs" Inherits="ComercioWeb.FormularioProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-container {
            display: flex;
            gap: 30px;
            background: white;
            padding: 24px;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.10);
        }
        .columna-izq, .columna-der {
            flex: 1; /* Mitad y mitad de la pantalla */
            display: flex;
            flex-direction: column;
            gap: 15px;
        }
        .form-control {
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
            width: 100%;
        }
        .img-preview {
            max-width: 100%;
            height: auto;
            border: 1px solid #ddd;
            border-radius: 8px;
        }
        .btn { padding: 10px 15px; border: none; border-radius: 4px; cursor: pointer; color: white; }
        .btn-primary { background-color: #0d6efd; }
        .btn-danger { background-color: #dc3545; }
        .acciones-inferiores { margin-top: 20px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Formulario de Producto</h2>

    <div class="form-container">
     
        <div class="columna-izq">
            <div>
                <label>Id</label>
                <asp:TextBox ID="txtId" runat="server" CssClass="form-control" ReadOnly="true" BackColor="#e9ecef" />
            </div>
            
            <div>
                <label>Nombre:</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            </div>

            <div>
                <label>Precio:</label>
                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
            </div>

            <div>
                <label>Stock:</label>
                <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" />
            </div>

        
            

            <div class="acciones-inferiores">
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
                <a href="Productos.aspx" style="margin-left: 10px;">Cancelar</a>
            </div>

            
            <div style="margin-top: 30px; padding-top: 20px; border-top: 1px solid #ccc;">
                <asp:CheckBox ID="chkConfirmarEliminacion" runat="server" Text="Confirmar Eliminación" AutoPostBack="true" OnCheckedChanged="chkConfirmarEliminacion_CheckedChanged" />
                <br /><br />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" Visible="false" />
            </div>
        </div>

        
        <div class="columna-der">
            <div>
                <label>Descripción:</label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
            </div>

            <div>
                <label>Url Imagen:</label>
                <asp:TextBox ID="txtUrlImagen" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtUrlImagen_TextChanged" />
            </div>

            <div>
             
                <asp:Image ID="imgProducto" runat="server" CssClass="img-preview" ImageUrl="https://t3.ftcdn.net/jpg/02/48/42/64/360_F_248426448_NVKLywWqArG2ADUxDq6QprtIzsF82dMF.jpg" />
            </div>
        </div>
    </div>
</asp:Content>