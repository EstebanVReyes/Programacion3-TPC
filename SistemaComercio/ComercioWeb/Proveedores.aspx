<%@ Page Title="Proveedores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="ComercioWeb.Proveedores" %>

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

        .btn {
            border: none;
            border-radius: 6px;
            padding: 8px 14px;
            cursor: pointer;
            font-size: 14px;
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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-title">
        <h1>Proveedores</h1>
        <p>Listado de proveedores activos del sistema.</p>
    </div>

    <div style="margin-bottom: 16px;">
        <a href="ProveedoresABM.aspx" class="btn btn-primary">📦 Registrar productos de proveedor</a>
    </div>

    <div class="card">
        <asp:Label ID="lblMensajes" runat="server" Text=""></asp:Label>

        <asp:GridView ID="gvProveedores" runat="server" CssClass="table"
            AutoGenerateColumns="false" DataKeyNames="Id"
            EmptyDataText="No hay proveedores cargados.">
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />

                <asp:HyperLinkField HeaderText="Acción"
                    Text="✍️ Editar datos"
                    DataNavigateUrlFields="Id"
                    DataNavigateUrlFormatString="FormularioProveedor.aspx?id={0}"
                    ControlStyle-CssClass="btn btn-secondary" />
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>