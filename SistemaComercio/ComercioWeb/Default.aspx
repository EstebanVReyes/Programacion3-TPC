<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ComercioWeb.Default1" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Compra Gamer</title>

    <style>
        .home-title {
            margin-bottom: 8px;
            color: #111827;
        }

        .home-description {
            color: #6b7280;
            margin-bottom: 24px;
        }

        .dashboard-container {
            width: 100%;
            display: flex;
            flex-direction: column;
            gap: 24px;
            margin-top: 24px;
            margin-bottom: 32px;
        }

        .cards-row {
            width: 100%;
            display: flex;
            gap: 20px;
        }

        .info-card {
            flex: 1;
            background-color: white;
            border-radius: 10px;
            padding: 18px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.10);
            border-left: 5px solid #1f2937;
            box-sizing: border-box;
        }

        .info-card h3 {
            margin: 0;
            font-size: 16px;
            color: #374151;
            font-weight: 600;
        }

        .info-card p {
            margin: 8px 0 0;
            font-size: 28px;
            font-weight: bold;
            color: #111827;
        }

        .chart-container {
            width: 100%;
            background-color: white;
            border-radius: 10px;
            padding: 20px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.10);
            box-sizing: border-box;
            overflow: hidden;
        }

        .chart-full {
            width: 100% !important;
            max-width: 100% !important;
        }

        .products-section {
            width: 100%;
            margin-top: 24px;
        }

        .products-section h2 {
            margin-bottom: 16px;
            color: #111827;
        }

        .product-card {
            border: 1px solid #d1d5db;
            background-color: white;
            border-radius: 10px;
            padding: 15px;
            margin: 10px;
            width: 200px;
            box-shadow: 0 3px 8px rgba(0, 0, 0, 0.08);
            box-sizing: border-box;
        }

        .product-card h4 {
            margin: 0 0 8px;
            color: #111827;
        }

        .product-card p {
            margin: 0 0 8px;
            color: #6b7280;
        }

        .product-card strong {
            color: #111827;
        }

        .product-card span {
            color: #374151;
        }

        @media (max-width: 768px) {
            .cards-row {
                flex-direction: column;
            }

            .info-card {
                width: 100%;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="home-title">Dashboard de Ventas</h1>

    <p class="home-description">
        Información sobre las ventas realizadas los últimos 30 días.
    </p>

    <div class="dashboard-container">

        <div class="cards-row">

            <div class="info-card">
                <h3>Total de productos vendidos</h3>
                <p>
                    <asp:Label ID="lblProductosVendidos" runat="server" Text="0"></asp:Label>
                </p>
            </div>

            <div class="info-card">
                <h3>Importe total vendido</h3>
                <p>
                    <asp:Label ID="lblImporteTotal" runat="server" Text="0"></asp:Label>
                </p>
            </div>

            <div class="info-card">
                <h3>Ventas del mes</h3>
                <p>
                    <asp:Label ID="lblCantidadVentas" runat="server" Text="0"></asp:Label>
                </p>
            </div>

        </div>

        <div class="chart-container">
            <asp:Chart ID="chartProductos" runat="server" Width="1200px" Height="400px" CssClass="chart-full">
                <Series>
                    <asp:Series Name="Productos" ChartType="Column"></asp:Series>
                </Series>

                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </div>

    </div>

    <div class="products-section">
       <h2>Productos con menos stock</h2>

<asp:DataList ID="dlProductosMenosStock" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
    <ItemTemplate>
        <div class="product-card">
            <h4><%# Eval("NombreProducto") %></h4>
            <p>Precio: $<%# Eval("Precio") %></p>
            <span style="color: red;">
                Stock actual: <%# Eval("StockActual") %>
            </span>
            <br />
            <span>
                Stock mínimo: <%# Eval("StockMinimo") %>
            </span>
        </div>
    </ItemTemplate>
</asp:DataList>
    </div>

</asp:Content>