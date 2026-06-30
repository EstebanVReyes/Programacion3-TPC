<%@ Page Title="Alta de Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsuariosABM.aspx.cs" Inherits="ComercioWeb.UsuariosABM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-title {
            margin-bottom: 24px;
        }

        .page-title h1 {
            margin: 0;
            font-size: 28px;
            color: #111827;
        }

        .page-title p {
            margin-top: 6px;
            color: #6b7280;
        }

        .card {
            background: white;
            border-radius: 10px;
            padding: 24px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.10);
            margin-bottom: 24px;
        }

        .form-grid {
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 18px;
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
        }

        .form-control:focus {
            outline: none;
            border-color: #2563eb;
        }

        .actions {
            margin-top: 20px;
            display: flex;
            gap: 10px;
        }

        .btn-form {
            border: none;
            border-radius: 6px;
            padding: 10px 18px;
            cursor: pointer;
            font-size: 15px;
        }

        .btn-form-primary {
            background-color: #2563eb;
            color: white;
        }

        .btn-form-primary:hover {
            background-color: #1d4ed8;
        }

        .btn-form-secondary {
            background-color: #e5e7eb;
            color: #111827;
        }

        .btn-form-secondary:hover {
            background-color: #d1d5db;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-title">
        <h1>Usuarios</h1>
        <p>Alta de usuarios del sistema.</p>
    </div>

    <div class="card">
        <h2>Nuevo usuario</h2>

        <div class="form-grid">

            <div class="form-group">
                <label for="txtUsername">Nombre de usuario (Username)</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"  OnTextChanged="txtUsername_TextChanged" />
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server"
                    ControlToValidate="txtUsername"
                    ErrorMessage="El username es obligatorio."
                    ForeColor="Red" Display="Dynamic" />
            </div>

            <div class="form-group">
                <label for="txtPassword">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                    ControlToValidate="txtPassword"
                    ErrorMessage="La contraseña es obligatoria."
                    ForeColor="Red" Display="Dynamic" />
            </div>

            <div class="form-group">
                <label for="ddlTipoUsuario">Tipo de usuario (Rol)</label>
                <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Seleccione un rol..." Value="" />
                    <asp:ListItem Text="Administrador" Value="Administrador" />
                    <asp:ListItem Text="Vendedor" Value="Vendedor" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvTipoUsuario" runat="server"
                    ControlToValidate="ddlTipoUsuario"
                    InitialValue=""
                    ErrorMessage="Debe seleccionar un rol."
                    ForeColor="Red" Display="Dynamic" />
            </div>

        </div>

        <div class="actions">
            <asp:Button 
                ID="btnGuardar" 
                runat="server" 
                Text="Guardar usuario" 
                CssClass="btn-form btn-form-primary" 
                OnClick="btnGuardar_Click" />

            <asp:Button 
                ID="btnLimpiar" 
                runat="server" 
                Text="Limpiar" 
                CssClass="btn-form btn-form-secondary" 
                OnClick="btnLimpiar_Click" />
        </div>

        <br />

        <asp:Label ID="lblMensaje" runat="server" />
    </div>

</asp:Content>