<%@ Page Title="Categorias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="ComercioWeb.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-title { margin-bottom: 24px; }
        .page-title h1 { margin: 0; font-size: 28px; color: #111827; }
        .page-title p { margin-top: 6px; color: #6b7280; }
        .card { background: white; border-radius: 10px; padding: 24px; box-shadow: 0 4px 12px rgba(0, 0, 0, 0.10); margin-bottom: 24px; }
        .table { width: 100%; border-collapse: collapse; }
        .table th { background-color: #f3f4f6; text-align: left; padding: 12px; color: #374151; border-bottom: 1px solid #d1d5db; }
        .table td { padding: 12px; border-bottom: 1px solid #e5e7eb; }
        .btn { border: none; border-radius: 6px; padding: 8px 14px; cursor: pointer; font-size: 14px; text-decoration: none; display: inline-block; }
        .btn-primary { background-color: #2563eb; color: white; }
        .btn-primary:hover { background-color: #1d4ed8; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-title">
        <h1>Categorias</h1>
        <p>Listado de categorias con cantidad de productos asociados.</p>
    </div>

    <div class="card">
        <asp:GridView ID="gvCategorias" runat="server" CssClass="table"
            AutoGenerateColumns="false" DataKeyNames="Id"
            EmptyDataText="No hay categorias cargadas.">
            <Columns>
                <asp:BoundField DataField="Descripcion" HeaderText="Categoria" />
                <asp:BoundField DataField="CantidadProductos" HeaderText="Cantidad de Productos" />
            </Columns>
        </asp:GridView>

        <div style="margin-top: 15px;">
            <asp:Button ID="btnNuevaCategoria" runat="server" Text="Nueva Categoria" CssClass="btn btn-primary" OnClick="btnNuevaCategoria_Click" />
        </div>
    </div>

</asp:Content>
