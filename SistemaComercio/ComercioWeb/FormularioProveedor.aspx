<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioProveedor.aspx.cs" Inherits="ComercioWeb.FormularioProveedor" %>

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

    .form-group {
        display: flex;
        flex-direction: column;
        margin-bottom: 18px;
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
        margin-top: 24px;
        display: flex;
        gap: 10px;
        flex-wrap: wrap;
    }

    .btn {
        border: none;
        border-radius: 6px;
        padding: 10px 18px;
        cursor: pointer;
        font-size: 15px;
        text-decoration: none;
        display: inline-block;
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

    .btn-danger {
        background-color: #dc2626;
        color: white;
    }

    .btn-danger:hover {
        background-color: #b91c1c;
    }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-title">
        <h1><asp:Literal ID="litTitulo" runat="server" Text="Nuevo Proveedor" /></h1>
        <p>Completá los campos del formulario.</p>
    </div>

    <div class="card">

        <div class="form-group">
            <label>Nombre *</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Teléfono</label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Descripción</label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"
                TextMode="MultiLine" Rows="3" />
        </div>

        <div class="actions">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar"
                CssClass="btn btn-primary" OnClick="btnGuardar_Click" />

            <asp:Button ID="btnEliminar" runat="server" Text="Dar de baja"
                CssClass="btn btn-danger" OnClick="btnEliminar_Click"
                Visible="false"
                OnClientClick="return confirm('¿Confirma que desea dar de baja este proveedor?');" />

            <a href="Proveedores.aspx" class="btn btn-secondary">Cancelar</a>
        </div>

        <br />
        <asp:Label ID="lblMensaje" runat="server" />

    </div>

</asp:Content>