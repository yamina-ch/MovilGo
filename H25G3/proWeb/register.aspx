<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="proWeb.register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    
    <style>
        .register-container {
            max-width: 900px;
            margin: 50px auto;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 0 20px rgba(0, 128, 0, 0.1);
            border: 2px solid #1abc9c;
            background-color: #fff;
        }
        .form-footer {
            text-align: center;
            margin-top: 20px;
        }
        .form-label {
            font-weight: 500;
        }
    </style>

    <div class="register-container">
        <h2 class="text-center mb-4">Registro</h2>
        <div class="row g-3">

            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtUsername" CssClass="form-label">Nombre de Usuario:</asp:Label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtNombre" CssClass="form-label">Nombre:</asp:Label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="First Name"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtApellidos" CssClass="form-label">Apellidos:</asp:Label>
                <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtTelefono" CssClass="form-label">Teléfono:</asp:Label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Phone"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="form-label">Correo Electrónico:</asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email address"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtCalle" CssClass="form-label">Calle:</asp:Label>
                <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" placeholder="Street Address"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtLocalidad" CssClass="form-label">Ciudad:</asp:Label>
                <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" placeholder="City"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtProvincia" CssClass="form-label">Estado/Provincia:</asp:Label>
                <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" placeholder="State/Province"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtCodigoPostal" CssClass="form-label">Código Postal:</asp:Label>
                <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" placeholder="Postal Code"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtPassword" CssClass="form-label">Contraseña:</asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtConfirmPassword" CssClass="form-label">Confirmar Contraseña:</asp:Label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Confirm Password"></asp:TextBox>
            </div>

            <div class="col-12 text-center mt-3">
                <asp:Button ID="Registrarse" runat="server" Text="CREAR" CssClass="btn btn-success btn-lg w-50" OnClick="registrar" />
                <asp:Label ID="lblSuccessMessage" runat="server" CssClass="text-success d-block mt-2" Visible="false"></asp:Label>
                <asp:Label ID="lblErrorMessage" runat="server" CssClass="text-danger d-block mt-2" Visible="false"></asp:Label>
            </div>

            <div class="col-12 form-footer">
                <asp:HyperLink ID="lnkLogin" runat="server" NavigateUrl="~/login.aspx">Iniciar sesión con una cuenta existente</asp:HyperLink>
                <span class="mx-2">|</span>
                <asp:HyperLink ID="lnkReturnToStore" runat="server" NavigateUrl="~/Default.aspx">Volver a la Tienda</asp:HyperLink>
            </div>

        </div>
    </div>

    <script type="text/javascript">
        function redirectToLogin() {
            setTimeout(function () {
                window.location.href = 'login.aspx';
            }, 2000);
        }
    </script>
</asp:Content>
