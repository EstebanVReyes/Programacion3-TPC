<%@ Page Title="Detalle de Venta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleVenta.aspx.cs" Inherits="ComercioWeb.DetalleVenta" %>

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
            background-color: #f9fafb;
            width: 100%;
            box-sizing: border-box;
        }

        .form-value {
            padding: 10px 12px;
            font-size: 15px;
            color: #111827;
            background-color: #f9fafb;
            border-radius: 6px;
            min-height: 20px;
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
            text-decoration: none;
            display: inline-block;
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

        .lbl-total {
            font-weight: bold;
            font-size: 18px;
            color: #059669;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-title">
        <h1>Detalle de Venta</h1>
        <p>Informacion completa de la venta seleccionada.</p>
    </div>

    <asp:Label ID="lblMensaje" runat="server" Font-Bold="true" style="margin-bottom: 15px; display: block;" />

    <div class="card">
        <h2 style="margin-top: 0;">Datos de la venta</h2>

        <div class="form-grid">
            <div class="form-group">
                <label>Nro. Factura:</label>
                <asp:Label ID="lblNumeroFactura" runat="server" CssClass="form-value" />
            </div>

            <div class="form-group">
                <label>Fecha:</label>
                <asp:Label ID="lblFecha" runat="server" CssClass="form-value" />
            </div>

            <div class="form-group">
                <label>Cliente:</label>
                <asp:Label ID="lblCliente" runat="server" CssClass="form-value" />
            </div>

            <div class="form-group">
                <label>Estado:</label>
                <asp:Label ID="lblEstado" runat="server" CssClass="form-value" />
            </div>

            <div class="form-group">
                <label>Total:</label>
                <asp:Label ID="lblTotal" runat="server" CssClass="form-value lbl-total" />
            </div>
        </div>

        <div class="actions">
            <a href="Ventas.aspx" class="btn btn-secondary">Volver a Ventas</a>
        </div>
    </div>

    <div class="card">
        <h2 style="margin-top: 0;">Productos</h2>

        <asp:GridView ID="gvDetalles" runat="server" CssClass="table" AutoGenerateColumns="false" EmptyDataText="No hay productos registrados en esta venta.">
            <Columns>
                <asp:TemplateField HeaderText="Codigo">
                    <ItemTemplate>
                        <%# Eval("Producto.Codigo") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Producto">
                    <ItemTemplate>
                        <%# Eval("Producto.Nombre") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                <asp:TemplateField HeaderText="Subtotal">
                    <ItemTemplate>
                        <%# String.Format("{0:C}", Convert.ToDecimal(Eval("Cantidad")) * Convert.ToDecimal(Eval("PrecioUnitario"))) %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
