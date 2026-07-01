<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProveedoresABM.aspx.cs" Inherits="ComercioWeb.ProveedoresABM" %>

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

        .productos-lista {
            margin-top: 20px;
        }

        .productos-lista label {
            font-weight: bold;
            color: #374151;
            display: block;
            margin-bottom: 8px;
        }

        .productos-scroll {
            max-height: 200px;
            overflow-y: auto;
            border: 1px solid #d1d5db;
            border-radius: 6px;
            padding: 12px;
            background-color: #f9fafb;
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

        <div class="form-grid">
            <div class="form-group">
                <label for="txtNombre">Nombre *</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="txtTelefono">Teléfono</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group" style="grid-column: span 2;">
                <label for="txtDescripcion">Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"
                    TextMode="MultiLine" Rows="3" />
            </div>
        </div>

        <div class="productos-lista">
            <label>Productos asociados a este proveedor</label>
            <div class="productos-scroll">
                <asp:CheckBoxList ID="cblProductos" runat="server" />
            </div>
        </div>

        <div class="actions">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar"
                CssClass="btn btn-primary" OnClick="btnGuardar_Click" />

            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Proveedor"
                CssClass="btn btn-danger" OnClick="btnEliminar_Click"
                Visible="false"
                OnClientClick="return confirm('¿Estás seguro de que deseas dar de baja a este proveedor?');" />

            <a href="Proveedores.aspx" class="btn btn-secondary">Cancelar</a>
        </div>

        <br />
        <asp:Label ID="lblMensaje" runat="server" />

    </div>

</asp:Content>
