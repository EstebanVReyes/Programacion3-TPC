<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ComercioWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Login - Compra Gamer</title>

    <style>
        body {
            margin: 0;
            font-family: Arial, Helvetica, sans-serif;
            background-color: #f3f4f6;
            color: #111827;
        }

        .login-container {
            min-height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .login-card {
            background-color: white;
            width: 380px;
            padding: 32px;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.12);
        }

        .login-title {
            text-align: center;
            margin-bottom: 24px;
        }

        .login-title h1 {
            margin: 0;
            font-size: 28px;
            color: #111827;
        }

        .login-title p {
            margin-top: 8px;
            color: #6b7280;
            font-size: 14px;
        }

        .form-group {
            display: flex;
            flex-direction: column;
            margin-bottom: 16px;
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

        .form-control:focus {
            outline: none;
            border-color: #2563eb;
        }

        .btn-login {
            width: 100%;
            border: none;
            border-radius: 6px;
            padding: 11px;
            cursor: pointer;
            font-size: 15px;
            background-color: #2563eb;
            color: white;
        }

        .btn-login:hover {
            background-color: #1d4ed8;
        }

        .mensaje {
            display: block;
            margin-top: 14px;
            text-align: center;
            font-size: 14px;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">

        <div class="login-container">

            <div class="login-card">

                <div class="login-title">
                    <h1>Compra Gamer</h1>
                    <p>Ingresá tus datos para acceder al sistema</p>
                </div>

                <div class="form-group">
                    <label for="txtUsuario">Usuario</label>
                    <asp:TextBox 
                        ID="txtUsuario" 
                        runat="server" 
                        CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvUsuario" runat="server"
                        ControlToValidate="txtUsuario"
                        ErrorMessage="El usuario es obligatorio."
                        ForeColor="Red" Display="Dynamic" />
                </div>

                <div class="form-group">
                    <label for="txtPassword">Contraseña</label>
                    <asp:TextBox 
                        ID="txtPassword" 
                        runat="server" 
                        CssClass="form-control" 
                        TextMode="Password" />
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                        ControlToValidate="txtPassword"
                        ErrorMessage="La contraseña es obligatoria."
                        ForeColor="Red" Display="Dynamic" />
                </div>

                <asp:Button 
                    ID="btnIngresar" 
                    runat="server" 
                    Text="Ingresar" 
                    CssClass="btn-login" 
                    OnClick="btnIngresar_Click" />

                <asp:Label 
                    ID="lblMensaje" 
                    runat="server" 
                    CssClass="mensaje" />

            </div>

        </div>

    </form>
</body>
</html>