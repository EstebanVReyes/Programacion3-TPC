<%@ Page Title="Detalle de Venta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioVenta.aspx.cs" Inherits="ComercioWeb.FormularioVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Detalle de Venta</h2>
        
        <asp:Label ID="lblMensajes" runat="server" Visible="false" CssClass="alert alert-warning d-block"></asp:Label>

        <div class="row mt-3">
            <div class="col-md-6">
                <label>Id Venta:</label>
                <asp:TextBox ID="txtIdVenta" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label>Número de Factura:</label>
                <asp:TextBox ID="txtNumeroFactura" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-6">
                <label>Fecha de Venta:</label>
                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label>Cliente:</label>
                <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-6">
                <label>Estado:</label>
                <asp:Label ID="lblEstado" runat="server" CssClass="form-control bg-light"></asp:Label>
            </div>
        </div>

        <hr class="mt-4 mb-4" />
        <h4>Productos</h4>

        <asp:GridView ID="dgvDetalles" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" ShowHeaderWhenEmpty="true">
            <Columns>
                <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>

        <div class="row mt-3">
            <div class="col-md-4 offset-md-8 text-end">
                <label class="fw-bold">Total de la Venta ($):</label>
                <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control fw-bold text-end bg-light" ReadOnly="true"></asp:TextBox>
            </div>
        </div>

        <hr class="mt-4 mb-4" />

        <div class="row mt-4 mb-5">
            <div class="col-md-6">
                <asp:Button ID="btnInformarPago" runat="server" Text="Informar Pago" CssClass="btn btn-success" OnClick="btnInformarPago_Click" Visible="false" />
                <asp:Button ID="btnAnular" runat="server" Text="Anular Venta" CssClass="btn btn-danger ms-2" OnClick="btnAnular_Click" Visible="false" />
            </div>
            <div class="col-md-6 text-end">
                <a href="Ventas.aspx" class="btn btn-light border">Volver</a>
            </div>
        </div>
    </div>
</asp:Content>
