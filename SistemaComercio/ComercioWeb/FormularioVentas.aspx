<%@ Page Title="Modificar Venta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioVenta.aspx.cs" Inherits="ComercioWeb.FormularioVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Modificar o Eliminar Venta</h2>
        
        <asp:Label ID="lblMensajes" runat="server" Visible="false" CssClass="alert alert-warning d-block"></asp:Label>

        <div class="row mt-3">
            <div class="col-md-6">
                <label>Id Venta (Autogenerado):</label>
                <asp:TextBox ID="txtIdVenta" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label>Número de Factura:</label>
                <asp:TextBox ID="txtNumeroFactura" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-6">
                <label>Fecha de Venta:</label>
                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label>Cliente:</label>
                <asp:DropDownList ID="ddlClientes" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-6">
                <label>Estado del Pago:</label>
                <asp:DropDownList ID="ddlEstadoPago" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Pagado" Value="Pagado"></asp:ListItem>
                    <asp:ListItem Text="Pendiente" Value="Pendiente"></asp:ListItem>
                    <asp:ListItem Text="Anulado" Value="Anulado"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <hr class="mt-4 mb-4" />
        <h4>Componentes de la Venta</h4>
        
        <div class="row mt-2 align-items-end">
            <div class="col-md-5">
                <label>Producto a Vender:</label>
                <asp:DropDownList ID="ddlProductos" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-3">
                <label>Cantidad:</label>
                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <asp:Button ID="btnAgregarAlCarrito" runat="server" Text="Agregar a la lista" CssClass="btn btn-secondary w-100" OnClick="btnAgregarAlCarrito_Click" />
            </div>
        </div>

        <br />

        <!--
            Sin DataKeyNames: "Producto.Id" no es válido (DetalleVenta no tiene esa
            propiedad anidada accesible desde DataKeys) y "ProductoId" tampoco existe
            como propiedad plana. El RowCommand identifica la fila a quitar por índice
            (e.CommandArgument), así que no hace falta DataKeyNames.
        -->
        <asp:GridView ID="dgvDetalles" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowCommand="dgvDetalles_RowCommand" ShowHeaderWhenEmpty="true">
            <Columns>
                <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                <asp:ButtonField CommandName="Quitar" Text="❌ Quitar" ControlStyle-CssClass="btn btn-danger btn-sm" HeaderText="Acción" />
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
                <asp:Button ID="btnAceptar" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
                <a href="Ventas.aspx" class="btn btn-light border ms-2">Cancelar</a>
            </div>
            <div class="col-md-6 text-end">
                <asp:CheckBox ID="chkConfirmarEliminacion" runat="server" Text=" Confirmar Anulación" AutoPostBack="true" OnCheckedChanged="chkConfirmarEliminacion_CheckedChanged" />
                <asp:Button ID="btnEliminar" runat="server" Text="Anular Venta" CssClass="btn btn-danger ms-2" OnClick="btnEliminar_Click" Visible="false" />
            </div>
        </div>
    </div>
</asp:Content>
