<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="ComercioWeb.Usuarios" %>
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

        .btn-danger {
            background-color: #dc3545;
            color: white;
        }

        .btn-danger:hover {
            background-color: #b02a37;
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

        .btn-sm {
            padding: 0.25rem 0.5rem;
            font-size: 0.875rem;
            border-radius: 0.2rem;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-title">
        <h1>Usuarios</h1>
        <p>Listado de usuarios del sistema.</p>
    </div>

    <div class="card">
        <h2>Listado de usuarios</h2>

        <asp:Label ID="lblMensajes" runat="server" Text=""></asp:Label>

        <asp:GridView ID="gvUsuarios" runat="server" CssClass="table" AutoGenerateColumns="false" DataKeyNames="ID" EmptyDataText="No hay usuarios cargados.">
            <Columns>
                <asp:BoundField DataField="Username" HeaderText="Nombre de Usuario" />
                <asp:BoundField DataField="TipoUsuario" HeaderText="Rol del Sistema" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button 
                            ID="btnEditar" 
                            runat="server"
                            Text="✍️ Modificar"
                            CssClass="btn btn-primary btn-sm"
                            CommandArgument='<%# Eval("ID") %>' 
                            OnClick="btnEditar_Click"/>
                        <asp:Button
                            ID="btnEliminar"
                            runat="server"
                            Text="🗑️ Eliminar"
                            CssClass="btn btn-danger btn-sm"
                            CommandArgument='<%# Eval("ID") %>'
                            OnClick="btnEliminar_Click"
                            OnClientClick="return confirm('¿Seguro que querés eliminar este usuario?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
