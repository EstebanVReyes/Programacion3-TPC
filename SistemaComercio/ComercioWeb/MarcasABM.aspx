<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarcasABM.aspx.cs" Inherits="ComercioWeb.MarcasABM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-title { margin-bottom: 24px; }
        .page-title h1 { margin: 0; font-size: 28px; color: #111827; }
        .page-title p { margin-top: 6px; color: #6b7280; }
        .card { background: white; border-radius: 10px; padding: 24px; box-shadow: 0 4px 12px rgba(0, 0, 0, 0.10); margin-bottom: 24px; }
        .form-group { display: flex; flex-direction: column; margin-bottom: 18px; }
        .form-group label { font-weight: bold; margin-bottom: 6px; color: #374151; }
        .form-control { padding: 10px 12px; border: 1px solid #d1d5db; border-radius: 6px; font-size: 15px; max-width: 400px; }
        .actions { margin-top: 20px; display: flex; gap: 10px; }
        .btn { border: none; border-radius: 6px; padding: 10px 18px; cursor: pointer; font-size: 15px; }
        .btn-primary { background-color: #2563eb; color: white; }
        .btn-secondary { background-color: #e5e7eb; color: #111827; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-title">
        <h1>Marcas</h1>
        <p>Alta de nueva marca.</p>
    </div>

    <div class="card">
        <h2>Nueva marca</h2>

        <div class="form-group">
            <label for="txtNombre">Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                ControlToValidate="txtNombre"
                ErrorMessage="El nombre es obligatorio."
                ForeColor="Red" Display="Dynamic" />
        </div>

        <div class="actions">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar marca" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="btnLimpiar_Click" />
        </div>

        <br />
        <asp:Label ID="lblMensaje" runat="server" />
    </div>
</asp:Content>
