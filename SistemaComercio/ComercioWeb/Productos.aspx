
  
    <%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="ComercioWeb.Productos" %>

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

        .btn-secondary {
            background-color: #e5e7eb;
            color: #111827;
        }

        .table {
            width: 100%;
            border-collapse: collapse;
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

    <asp:Label
        ID="lblMensaje"
        runat="server" />


    <div class="card">
        <h2>Listado de productos</h2>

       <asp:GridView ID="gvProductos" runat="server" CssClass="table" AutoGenerateColumns="false" DataKeyNames="Id" OnRowDataBound="gvProductos_RowDataBound">
   <Columns>
    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
    <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
    <asp:BoundField DataField="StockActual" HeaderText="Stock" />
    
    <asp:TemplateField HeaderText="Categoría">
        <ItemTemplate>
            <%# Eval("Categoria.Descripcion") %>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:Button 
                    ID="btnEditar" 
                    runat="server"
                    Text="✍️ Editar"
                    CssClass="btn btn-primary btn-sm"
                    CommandName="EditarProducto"
                    CommandArgument='<%# Eval("Id") %>'
                    OnClick="btnEditar_Click"/>

                <asp:Button
                    ID="btnEliminar"
                    runat="server"
                    Text="🗑️ Eliminar"
                    CssClass="btn btn-danger btn-sm"
                    CommandName="EliminarProducto"
                    CommandArgument='<%# Eval("Id") %>'
                    OnClick="btnEliminar_Click"
                    OnClientClick="return confirm('¿Seguro que querés eliminar este producto?');" />
            </ItemTemplate>
        </asp:TemplateField>

</Columns>
</asp:GridView>
    </div>

</asp:Content>