<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientesABM.aspx.cs" Inherits="ComercioWeb.ClientesABM" %>
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

    .btn {
        border: none;
        border-radius: 6px;
        padding: 10px 18px;
        cursor: pointer;
        font-size: 15px;
    }

    .btn-primary {
        background-color: #2563eb;
        color: white;
    }

    .btn-primary:hover {
        background-color: #1d4ed8;
    }

    .btn-secondary {
        background-color: #e5e7eb;
        color: #111827;
    }

    .btn-secondary:hover {
        background-color: #d1d5db;
    }

    .table {
        width: 100%;
        border-collapse: collapse;
    }

    .table th {
        background-color: #f3f4f6;
        text-align: left;
        padding: 12px;
        color: #374151;
        border-bottom: 1px solid #d1d5db;
    }

    .table td {
        padding: 12px;
        border-bottom: 1px solid #e5e7eb;
    }

    .empty-message {
        color: #6b7280;
        font-style: italic;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="page-title">
        <h1>Clientes</h1>
        <p>Alta y listado de clientes del sistema.</p>
    </div>

    <div class="card">
        <h2>Nuevo cliente</h2>

        <div class="form-grid">
            <div class="form-group">
                <label for="txtNombre">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="txtApellido">Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="txtDni">DNI</label>
                <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
            </div>

            <div class="form-group">
                <label for="txtTelefono">Teléfono</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
            </div>
        </div>

        <div class="actions">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar cliente" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="btnLimpiar_Click" />
        </div>

        <br />

        <asp:Label ID="lblMensaje" runat="server" />
    </div>
</asp:Content>
