<%@ Page Title="Formulario de Cliente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioCliente.aspx.cs" Inherits="ComercioWeb.FormularioCliente" %>

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
        .form-group {
            display: flex;
            flex-direction: column;
        }
        .form-group label {
            font-weight: bold;
            margin-bottom: 6px;
            color: #374151;
        }
        .form-control {
            padding: 10px 12px;
            border: 1px solid #d1d5db;
            border-radius: 6px;
            font-size: 15px;
            width: 100%;
            box-sizing: border-box;
        }
        .btn { 
            padding: 10px 15px; 
            border: none; 
            border-radius: 6px; 
            cursor: pointer; 
            color: white; 
            font-size: 15px;
        }
        .btn-primary { background-color: #0d6efd; }
        .btn-danger { background-color: #dc3545; }
        .acciones-inferiores { 
            margin-top: 20px; 
            display: flex;
            align-items: center;
        }
        .seccion-eliminar {
            margin-top: 30px; 
            padding-top: 20px; 
            border-top: 1px solid #ccc;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-bottom: 24px;">
        <h2 style="margin: 0; font-size: 28px; color: #111827;">Formulario de Cliente</h2>
    </div>

    <asp:Label ID="lblMensajes" runat="server" Font-Bold="true" style="margin-bottom: 15px; display: block;" />

    <div class="form-container">
     
        <div class="columna-izq">
            
            <asp:TextBox ID="txtId" runat="server" Visible="false"></asp:TextBox>
            
            <div class="form-group">
                <label>Nombre:</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                    ControlToValidate="txtNombre"
                    ErrorMessage="El nombre es obligatorio."
                    ForeColor="Red" Display="Dynamic" />
            </div>

            <div class="form-group">
                <label>Apellido:</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvApellido" runat="server"
                    ControlToValidate="txtApellido"
                    ErrorMessage="El apellido es obligatorio."
                    ForeColor="Red" Display="Dynamic" />
            </div>

            <div class="form-group">
                <label>DNI:</label>
                <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvDNI" runat="server"
                    ControlToValidate="txtDNI"
                    ErrorMessage="El DNI es obligatorio."
                    ForeColor="Red" Display="Dynamic" />
            </div>

            <div class="acciones-inferiores">
                <asp:Button ID="btnAceptar" runat="server" Text="Guardar Cliente" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
                <a href="Clientes.aspx" style="margin-left: 15px; text-decoration: none; color: #6b7280; font-weight: bold;">Cancelar</a>
            </div>

            
        </div>

        <div class="columna-der">
            <div class="form-group">
                <label>Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                    ControlToValidate="txtEmail"
                    ErrorMessage="El email es obligatorio."
                    ForeColor="Red" Display="Dynamic" />
            </div>

            <div class="form-group">
                <label>Teléfono:</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
            </div>
            
            </div>
    </div>
</asp:Content>