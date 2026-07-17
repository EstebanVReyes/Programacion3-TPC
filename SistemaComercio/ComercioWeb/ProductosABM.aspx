<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductosABM.aspx.cs" Inherits="ComercioWeb.ProductosABM" %>
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

      <div class="page-title">
      <h1>Productos</h1>
      <p>Alta y listado de productos del sistema.</p>
  </div>

  <div class="card">
      <h2>Nuevo producto</h2>

      <div class="form-grid">
          

          <div class="form-group">
              <label for="txtNombre">Nombre</label>
              <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
              <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                  ControlToValidate="txtNombre"
                  ErrorMessage="El nombre es obligatorio."
                  ForeColor="Red" Display="Dynamic" />
          </div>

          <div class="form-group">
              <label for="txtDescripcion">Descripción</label>
              <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" />
              <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server"
                  ControlToValidate="txtDescripcion"
                  ErrorMessage="La descripción es obligatoria."
                  ForeColor="Red" Display="Dynamic" />
          </div>

          <div class="form-group">
              <label for="txtPorcentajeGanancia">Porcentaje Ganancia</label>
              <asp:TextBox ID="txtPorcentajeGanancia" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="txtPorcentajeGanancia"
                    ErrorMessage="El porcentaje de la ganancia es obligatorio."
                    ForeColor="Red" Display="Dynamic" />
                <asp:RangeValidator ID="RangeValidator1" runat="server"
                    ControlToValidate="txtPorcentajeGanancia"
                    Type="Integer"
                    MinimumValue="15"
                    MaximumValue="40"
                    ErrorMessage="El porcentaje de ganacia debe estar entre 15 y 40."
                    ForeColor="Red" Display="Dynamic" />
          </div>
      
          <div class="form-group">
              <label for="ddlCategorias">Categoría</label>
              <asp:DropDownList ID="ddlCategorias" runat="server" CssClass="form-control" />
              <asp:RequiredFieldValidator ID="rfvCategoria" runat="server"
                  ControlToValidate="ddlCategorias"
                  InitialValue=""
                  ErrorMessage="Debe seleccionar una categoría."
                  ForeColor="Red" Display="Dynamic" />
          </div>

          <div class="form-group">
              <label for="ddlMarcas">Marca</label>
              <asp:DropDownList ID="ddlMarcas" runat="server" CssClass="form-control" />
              <asp:RequiredFieldValidator ID="rfvMarca" runat="server"
                  ControlToValidate="ddlMarcas"
                  InitialValue=""
                  ErrorMessage="Debe seleccionar una marca."
                  ForeColor="Red" Display="Dynamic" />
          </div>

          <div class="form-group">
              <label for="txtUrlImagen">URL Imagen</label>
              <asp:TextBox ID="txtUrlImagen" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtUrlImagen_TextChanged" />
          </div>

          <div class="form-group">
              <label>Vista Previa</label>
              <asp:Image ID="imgProducto" runat="server" ImageUrl="https://t3.ftcdn.net/jpg/02/48/42/64/360_F_248426448_NVKLywWqArG2ADUxDq6QprtIzsF82dMF.jpg" style="max-width: 100%; max-height: 200px; border-radius: 8px; border: 1px solid #d1d5db;" />
          </div>
      
      </div>

      <div class="actions">
          <asp:Button ID="btnGuardar" runat="server" Text="Guardar producto" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
          <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="btnLimpiar_Click" />
      </div>

      <br />

      <asp:Label ID="lblMensaje" runat="server" />
  </div>
</asp:Content>
