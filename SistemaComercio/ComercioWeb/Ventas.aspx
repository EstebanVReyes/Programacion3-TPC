<%@ Page Title="Ventas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="ComercioWeb.Ventas" %>

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

   
    <div class="card">
    <h2>Listado de ventas</h2>
    
    <asp:GridView ID="gvVentas" runat="server" CssClass="table" AutoGenerateColumns="false" 
                  DataKeyNames="Id" OnSelectedIndexChanged="gvVentas_SelectedIndexChanged" EmptyDataText="No hay ventas cargadas.">
        <Columns>
            <asp:BoundField DataField="NumeroFactura" HeaderText="N° Factura" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:TemplateField HeaderText="Cliente">
                <ItemTemplate>
                    <%# Eval("Cliente.Nombre") %> <%# Eval("Cliente.Apellido") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Estado" HeaderText="Estado" />
            <asp:CommandField ShowSelectButton="True" SelectText="🔍 Ver Detalle" ControlStyle-CssClass="btn btn-primary" />
            <asp:TemplateField HeaderText="Enviar email">
                <ItemTemplate>
                    <button
                        type="button"
                        class="btn btn-primary btn-sm"
                        data-bs-toggle="modal"
                        data-bs-target="#modalEnviarMail"
                        data-email='<%# Eval("Cliente.Email") %>'
                        data-factura='<%# Eval("NumeroFactura") %>'
                        onclick="cargarDatosMail(this)">
                        📧 Enviar
                    </button>
                </ItemTemplate>
</asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

<div class="card">
    <h2><asp:Label ID="lblTituloDetalle" runat="server" Text="Seleccione una venta para ver sus detalles"></asp:Label></h2>
    
    <asp:GridView ID="gvDetalles" runat="server" CssClass="table" AutoGenerateColumns="false" EmptyDataText="Esperando selección...">
        <Columns>
            <asp:TemplateField HeaderText="Producto">
                <ItemTemplate>
                    <%# Eval("Producto.Nombre") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
            <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
          
        </Columns>
    </asp:GridView>
</div>

    <div class="modal fade" id="modalEnviarMail" tabindex="-1" aria-labelledby="modalEnviarMailLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">

            <div class="modal-header">
                <h5 class="modal-title" id="modalEnviarMailLabel">Enviar Mail</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
            </div>

            <div class="modal-body">

                <div class="mb-3">
                    <label class="form-label">Email destino</label>
                    <asp:TextBox 
                        ID="txtEmailDestino" 
                        runat="server" 
                        CssClass="form-control" 
                        placeholder="cliente@gmail.com" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Asunto</label>
                    <asp:TextBox 
                        ID="txtAsunto" 
                        runat="server" 
                        CssClass="form-control" 
                        placeholder="Asunto del mail" />
                </div>

                <div class="mb-3">
                    <label class="form-label">Mensaje</label>
                    <asp:TextBox 
                        ID="txtMensaje" 
                        runat="server" 
                        CssClass="form-control" 
                        TextMode="MultiLine" 
                        Rows="5" 
                        placeholder="Escribí el mensaje..." />
                </div>

                <asp:Label 
                    ID="lblMensajeMail" 
                    runat="server" />

            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                    Cancelar
                </button>

                <asp:Button 
                    ID="btnEnviarMail" 
                    runat="server" 
                    Text="📧 Enviar"
                    CssClass="btn btn-success"
                    OnClick="btnEnviarMail_Click" />
            </div>

        </div>
    </div>
</div>

</asp:Content>