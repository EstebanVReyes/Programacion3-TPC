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

        .table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 15px;
        }

        .table th {
            background-color: #f3f4f6;
            text-align: left;
            padding: 12px;
            border-bottom: 1px solid #d1d5db;
        }

        .table td {
            padding: 12px;
            border-bottom: 1px solid #e5e7eb;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-title">
        <h1>Proveedores</h1>
        <p>Seleccioná un proveedor y cargá los productos que trae.</p>
    </div>

    <div class="card">
        <h2>Registrar productos del proveedor</h2>

        <asp:Label ID="lblMensaje" runat="server" Font-Bold="true" style="display:block; margin-bottom: 15px;" Visible="false" />

        <div class="form-group" style="margin-bottom: 20px; max-width: 50%;">
            <label>Proveedor</label>
            <asp:Label ID="lblNombreProveedor" runat="server" CssClass="form-control"
                style="background:#f3f4f6; padding:10px 12px; border-radius:6px;" />
        </div>

        <hr style="border: 1px solid #e5e7eb; margin: 20px 0;" />

        <h4>Agregar Productos</h4>
        <div class="form-grid">
            <div class="form-group">
                <label for="ddlProducto">Producto</label>
                <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Seleccione un producto..." Value="" />
                </asp:DropDownList>
            </div>

            <div class="form-group" style="justify-content: flex-end;">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar a la lista"
                    CssClass="btn btn-secondary" OnClick="btnAgregar_Click" />
            </div>
        </div>

        <asp:GridView ID="dgvCarrito" runat="server" AutoGenerateColumns="false"
            CssClass="table" OnRowCommand="dgvCarrito_RowCommand"
            EmptyDataText="Todavía no se agregaron productos.">
            <Columns>
                <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
           
                <asp:ButtonField CommandName="Quitar" Text="❌ Quitar"
                    ControlStyle-CssClass="btn btn-danger" HeaderText="Acción" />
            </Columns>
        </asp:GridView>

        <hr style="border: 1px solid #e5e7eb; margin: 20px 0;" />

        <div class="actions">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar productos del proveedor"
                CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Cancelar / Limpiar"
                CssClass="btn btn-secondary" OnClick="btnLimpiar_Click" />
        </div>
    </div>

</asp:Content>