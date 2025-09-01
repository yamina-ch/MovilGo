<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="mejoras.aspx.cs" Inherits="proWeb.mejoras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .sidebar {
            background-color: #2c3e50;
            min-height: 100vh;
            color: white;
        }

        .sidebar .nav-link {
            color: #ccc;
            font-weight: 500;
            margin-bottom: 5px;
        }

        .sidebar .nav-link:hover {
            background-color: #1abc9c;
            color: white;
        }

        .btn-danger {
            width: 100%;
        }

        .dashboard-content {
            padding: 30px;
        }

        .dashboard-content h2 {
            font-weight: 700;
        }

        .card-metric {
            padding: 20px;
            background: white;
            border-radius: 12px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.05);
            margin-bottom: 20px;
            text-align: center;
        }

        .metric-icon {
            font-size: 2rem;
            margin-bottom: 10px;
            color: #1abc9c;
        }

        .metric-title {
            font-weight: 600;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                    
                    <asp:HyperLink CssClass="nav-link" NavigateUrl="transaccion.aspx" runat="server">
                        <i class="fas fa-dollar-sign me-2"></i> TRANSACCIÓN
                    </asp:HyperLink>
                                       <asp:LinkButton ID="btnLogout" runat="server" CssClass="btn btn-danger mt-4" OnClick="btnLogout_Click">
    <i class="fas fa-sign-out-alt"></i> Cerrar Sesión
</asp:LinkButton>

                </nav>
            </div>

            <!-- Main Content -->
            <div class="col-md-10 dashboard-content">
                <h2 class="mb-4">Bienvenido al Panel de Administración</h2>

                <div class="row">
                    <div class="col-md-4">
                        <div class="card-metric">
                            <div class="metric-icon"><i class="fas fa-euro-sign"></i></div>
                            <div class="metric-title">Ganancias Totales</div>
                            <p class="fw-bold text-success">&euro;<%= TotalGanancias.ToString("N2") %></p>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="card-metric">
                            <div class="metric-icon"><i class="fas fa-chart-line"></i></div>
                            <div class="metric-title">Gastos Totales</div>
                            <p class="fw-bold text-danger">&euro;<%= TotalGastos.ToString("N2") %></p>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="card-metric">
                            <div class="metric-icon"><i class="fas fa-star"></i></div>
                            <div class="metric-title">Meta Mensual</div>
                            <p class="fw-bold text-warning">&euro;<%= MetaMensual.ToString("N2") %></p>
                        </div>
                    </div>
                </div>

                <div class="row mt-5">
                    <div class="col-12">
                        <canvas id="chartVentas" height="100"></canvas>
                    </div>
                </div>
                  <div class="col-12">
                      <h4 class="mb-3">Top 10 Productos Más Vendidos</h4>
                      <%= HtmlTablaTopProductos %>
                 </div>
                <div class="col-12">
                    <h4 class="mb-3">Top 10 Clientes con Más Compras</h4>
                    <%= HtmlTablaTopClientes %>
                </div>

            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            var ctx = document.getElementById("chartVentas").getContext("2d");
            new Chart(ctx, {
                type: 'line',
                data: {
                    labels: <%= Labels %>,
                    datasets: [{
                        label: 'Ventas',
                        data: <%= SalesData %>,
                        backgroundColor: 'rgba(26, 188, 156, 0.2)',
                        borderColor: '#1abc9c',
                        borderWidth: 2,
                        tension: 0.3,
                        fill: true
                    }]
                },
                options: {
                    responsive: true,
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        });
    </script>
</asp:Content>
