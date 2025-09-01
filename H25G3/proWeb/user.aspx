<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="user.aspx.cs" Inherits="proWeb.user" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .container {
            max-width: 1100px;
        }

        .user-info {
            display: flex;
            flex-direction: column;
            align-items: center;
            padding: 20px;
        }

        .user-info img {
            width: 150px;
            height: 150px;
            object-fit: cover;
            border-radius: 50%;
            margin-bottom: 10px;
        }

        .btn-success,
        .btn-outline-success,
        .btn-outline-danger {
            min-width: 180px;
            padding: 10px 20px;
            font-weight: bold;
            border-radius: 10px;
            text-align: center;
        }

        .profile-actions,
        .btn-group {
            display: flex;
            justify-content: center;
            flex-wrap: wrap;
            gap: 15px;
            margin-top: 20px;
        }

        .form-control[disabled],
        .form-control[readonly] {
            background-color: #f5f7fa;
        }
    </style>

    <div class="container rounded bg-white mt-5 mb-5 p-4 shadow-sm">
        <div class="row">
            <div class="col-md-4 user-info border-end">
                <img src="https://cdn-icons-png.flaticon.com/512/149/149071.png" alt="Perfil" />
                <span class="font-weight-bold mb-3">
                    <asp:Button ID="btnAdmin" runat="server" Text="Admin" CssClass="btn btn-outline-success" Visible="false" OnClick="btnAdmin_Click" Width="124px" />
                    <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>
                </span>
                <asp:Button ID="btnLogOut" runat="server" Text="Log Out" CssClass="btn btn-success" OnClick="btnLogOut_Click" />
            </div>

            <div class="col-md-8 profile-config">
                <h4 class="mb-4">Configuración del perfil</h4>
                <div class="row g-3">
                    <div class="col-md-6">
                        <label>Nombre</label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Enabled="false" />
                    </div>
                    <div class="col-md-6">
                        <label>Apellidos</label>
                        <asp:TextBox ID="txtSurname" runat="server" CssClass="form-control" Enabled="false" />
                    </div>
                    <div class="col-md-6">
                        <label>Teléfono</label>
                        <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control" Enabled="false" />
                    </div>
                    <div class="col-md-6">
                        <label>Calle</label>
                        <asp:TextBox ID="txtAddressLine1" runat="server" CssClass="form-control" Enabled="false" />
                    </div>
                    <div class="col-md-6">
                        <label>Código Postal</label>
                        <asp:TextBox ID="txtPostcode" runat="server" CssClass="form-control" Enabled="false" />
                    </div>
                    <div class="col-md-6">
                        <label>Ciudad</label>
                        <asp:TextBox ID="txtState" runat="server" CssClass="form-control" Enabled="false" />
                    </div>
                    <div class="col-md-6">
                        <label>Correo Electrónico</label>
                        <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control" TextMode="Email" Enabled="false" />
                    </div>
                    <div class="col-md-6">
                        <label>Provincia</label>
                        <asp:TextBox ID="txtArea" runat="server" CssClass="form-control" Enabled="false" />
                    </div>
                </div>

                <div class="text-center mt-4">
                    <asp:Button ID="btnSaveProfile" runat="server" Text="Guardar perfil" CssClass="btn btn-success" Visible="false" OnClick="btnSaveProfile_Click" />
                </div>

                

                <div class="btn-group">
                    <asp:Button ID="btnEditProfile" runat="server" Text="Editar perfil" CssClass="btn btn-outline-success" OnClick="btnEditProfile_Click" />
                    <asp:Button ID="btnChangePassword" runat="server" Text="Cambiar contraseña" CssClass="btn btn-outline-success" OnClick="btnChangePassword_Click" />
                    <asp:Button ID="btnDeleteAccount" runat="server" Text="Eliminar cuenta" CssClass="btn btn-outline-danger" OnClick="btnDeleteAccount_Click" />
                    
                </div>

                <asp:Button ID="ConfirmDeleteBtn" runat="server" Style="display:none;" OnClick="btnConfirmDelete_Click" />
                <asp:Button ID="ConfirmChangePasswordBtn" runat="server" Style="display:none;" OnClick="btnSubmitPasswordChange_Click" UseSubmitBehavior="false" />
                <asp:HiddenField ID="hiddenNewPassword" runat="server" />
                <asp:HiddenField ID="hiddenConfirmPassword" runat="server" />
            </div>

        </div>
       

    </div>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
        function showChangePasswordModal() {
            Swal.fire({
                title: 'Cambiar contraseña',
                html:
                    '<div style="position:relative;">' +
                    '  <input type="password" id="newPwd" class="swal2-input" placeholder="Nueva contraseña" style="padding-right:40px;">' +
                    '  <i class="fas fa-eye toggle-password" onclick="toggleVisibility(\'newPwd\', this)" style="position:absolute; top:16px; right:20px; cursor:pointer;"></i>' +
                    '</div>' +
                    '<div style="position:relative;">' +
                    '  <input type="password" id="confirmPwd" class="swal2-input" placeholder="Confirmar contraseña" style="padding-right:40px;">' +
                    '  <i class="fas fa-eye toggle-password" onclick="toggleVisibility(\'confirmPwd\', this)" style="position:absolute; top:16px; right:20px; cursor:pointer;"></i>' +
                    '</div>',
                confirmButtonText: 'Guardar',
                confirmButtonColor: '#198754',
                showCancelButton: true,
                cancelButtonText: 'Cancelar',
                focusConfirm: false,
                preConfirm: () => {
                    const newPwd = document.getElementById('newPwd').value;
                    const confirmPwd = document.getElementById('confirmPwd').value;

                    if (!newPwd || !confirmPwd) {
                        Swal.showValidationMessage('Rellena ambos campos');
                        return false;
                    }

                    if (newPwd !== confirmPwd) {
                        Swal.showValidationMessage('Las contraseñas no coinciden');
                        return false;
                    }

                    document.getElementById('<%= hiddenNewPassword.ClientID %>').value = newPwd;
                    document.getElementById('<%= hiddenConfirmPassword.ClientID %>').value = confirmPwd;

                    setTimeout(function () {
                        __doPostBack('<%= ConfirmChangePasswordBtn.UniqueID %>', '');
                    }, 100);
                }
            });
        }

        function toggleVisibility(inputId, icon) {
            const input = document.getElementById(inputId);
            const isPassword = input.type === 'password';
            input.type = isPassword ? 'text' : 'password';
            icon.classList.toggle('fa-eye');
            icon.classList.toggle('fa-eye-slash');
        }
    </script>
    
</asp:Content>