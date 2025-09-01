<%@ Page Title="Transacciones" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="transaccion.aspx.cs" Inherits="proWeb.Transaccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .sidebar {
            background-color: #2c3e50;
            min-height: 100vh;
            color: white;
        }

        .sidebar .nav-link {
            color: #ccc;
        }

        .sidebar .nav-link:hover {
            background-color: #1abc9c;
            color: white;
        }
    </style>

    <div class="container-fluid">
        <div class="row">
            <!-- Sidebar -->
            <div class="col-md-2 sidebar p-3">
                <div class="mb-4">
                    <h5><%= Session["AdminName"] ?? "Admin" %></h5>
                </div>
                <nav class="nav flex-column">
                    <asp:HyperLink CssClass="nav-link" NavigateUrl="mejoras.aspx" runat="server">
                        <i class="fas fa-tachometer-alt me-2"></i> DASHBOARD
                    </asp:HyperLink>
                    <asp:HyperLink CssClass="nav-link" NavigateUrl="producto.aspx" runat="server">
                        <i class="fas fa-box-open me-2"></i> PRODUCTOS
                    </asp:HyperLink>
                    <asp:HyperLink CssClass="nav-link" NavigateUrl="admin.aspx" runat="server">
                        <i class="fas fa-users me-2"></i> USUARIOS
                    </asp:HyperLink>
                    <asp:HyperLink CssClass="nav-link" NavigateUrl="proveedor.aspx" runat="server">
                        <i class="fas fa-truck me-2"></i> PROVEEDORES
                    </asp:HyperLink>
                    
                    <asp:HyperLink CssClass="nav-link active" NavigateUrl="transaccion.aspx" runat="server">
                        <i class="fas fa-dollar-sign me-2"></i> TRANSACCION
                    </asp:HyperLink>
                    <asp:LinkButton ID="btnLogout" runat="server" CssClass="btn btn-danger mt-4">
                        <i class="fas fa-sign-out-alt"></i> Cerrar Sesión
                    </asp:LinkButton>
                </nav>
            </div>

            <!-- Contenido -->
            <div class="col-md-10 p-4">
                <h4>Mis Transacciones</h4>

                <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>

                <asp:GridView ID="gvTransacciones" runat="server" CssClass="table table-bordered"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="nombre_articulo" HeaderText="Producto" />
                        <asp:BoundField DataField="precio_venta" HeaderText="Precio de Venta" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="comision" HeaderText="Comisión" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="ganancia" HeaderText="Ganancia Neta" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="fecha_transaccion" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
