<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VentasABM.aspx.cs" Inherits="ComercioWeb.VentasABM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-title { margin-bottom: 24px; }
        .page-title h1 { margin: 0; font-size: 28px; color: #111827; }
        .page-title p { margin-top: 6px; color: #6b7280; }
        .card { background: white; border-radius: 10px; padding: 24px; box-shadow: 0 4px 12px rgba(0, 0, 0, 0.10); margin-bottom: 24px; }
        .form-grid { display: grid; grid-template-columns: repeat(2, 1fr); gap: 18px; margin-bottom: 20px;}
        .form-group { display: flex; flex-direction: column; }
        .form-group label { font-weight: bold; margin-bottom: 6px; color: #374151; }
        .form-control { padding: 10px 12px; border: 1px solid #d1d5db; border-radius: 6px; font-size: 15px; }
        .actions { margin-top: 20px; display: flex; gap: 10px; }
        .btn { border: none; border-radius: 6px; padding: 10px 18px; cursor: pointer; font-size: 15px; }
        .btn-primary { background-color: #2563eb; color: white; }
        .btn-secondary { background-color: #e5e7eb; color: #111827; }
        .btn-danger { background-color: #dc3545; color: white; }
        .table { width: 100%; border-collapse: collapse; margin-top: 15px; }
        .table th { background-color: #f3f4f6; text-align: left; padding: 12px; border-bottom: 1px solid #d1d5db; }
        .table td { padding: 12px; border-bottom: 1px solid #e5e7eb; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-title">
        <h1>Ventas</h1>
        <p>Registro de nueva venta con múltiples artículos.</p>
    </div>

    <div class="card">
        <h2>Nueva venta</h2>
        
        <asp:Label ID="lblMensaje" runat="server" Font-Bold="true" style="display:block; margin-bottom: 15px;" />

        <div class="form-group" style="margin-bottom: 20px; max-width: 50%;">
            <label for="ddlCliente">Cliente</label>
            <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control">
                <asp:ListItem Text="Seleccione un cliente..." Value="" />
            </asp:DropDownList>
        </div>

        <hr style="border: 1px solid #e5e7eb; margin: 20px 0;" />

        <h4>Agregar Componentes</h4>
        <div class="form-grid">
            <div class="form-group">
                <label for="ddlProducto">Producto</label>
                <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged">
                    <asp:ListItem Text="Seleccione un producto..." Value="" />
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="txtPrecioUnitario">Precio unitario ($)</label>
                <asp:TextBox ID="txtPrecioUnitario" runat="server" CssClass="form-control" ReadOnly="true" BackColor="#f3f4f6" />
            </div>

            <div class="form-group">
                <label for="txtCantidad">Cantidad</label>
                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number" />
            </div>

            <div class="form-group" style="justify-content: flex-end;">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar a la lista" CssClass="btn btn-secondary" OnClick="btnAgregar_Click" />
            </div>
        </div>

        <asp:GridView ID="dgvCarrito" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="dgvCarrito_RowCommand" DataKeyNames="Producto.Id">
            <Columns>
                <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                <asp:ButtonField CommandName="Quitar" Text="❌ Quitar" ControlStyle-CssClass="btn btn-danger" HeaderText="Acción" />
            </Columns>
        </asp:GridView>

        <div style="text-align: right; margin-top: 15px; font-size: 18px;">
            <strong>Total: $ </strong>
            <asp:Label ID="lblTotal" runat="server" Text="0.00"></asp:Label>
        </div>

        <hr style="border: 1px solid #e5e7eb; margin: 20px 0;" />

        <div class="actions">
            <asp:Button ID="btnGuardar" runat="server" Text="Registrar venta completa" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Cancelar / Limpiar" CssClass="btn btn-secondary" OnClick="btnLimpiar_Click" />
        </div>
    </div>
</asp:Content>