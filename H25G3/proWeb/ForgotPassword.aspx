
<%@ Page Title="ForgotPassword" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="proWeb.ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="container">
     <h2>Rrestablece tu contraseña</h2>  
     <asp:Panel ID="pnlForm" runat="server">  
         <p>Le enviaremos un correo electrónico para restablecer su contraseña.</p>
         <p>
             <asp:Image ID="Image1" runat="server" ImageUrl="logimg/mail.png" Height="100px" Width="179px"/>
         </p>
         <asp:Label ID="lblEmail" runat="server" Text="Email" AssociatedControlID="txtEmail"></asp:Label>
         <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" AutoCompleteType="Email" required="required" OnTextChanged="txtEmail_TextChanged"></asp:TextBox>
        <asp:Button ID="btnSubmit" runat="server" Text="Enviar" CssClass="btn-green" OnClick="btnSubmit_Click" />

         <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-default" OnClientClick="window.location='Login.aspx';return false;" />
         <asp:Label ID="lblErrorMessage" runat="server" Text="" Visible="false" />
     </asp:Panel>
    
 </div>
    <style>
    .btn-green {
        background-color: #28a745;
        color: white;
        border: none;
        padding: 10px 20px;
        border-radius: 4px;
        cursor: pointer;
    }
    .btn-green:hover {
        background-color: #218838;
    }
</style>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    
</asp:Content>
