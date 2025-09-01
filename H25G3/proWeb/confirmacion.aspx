<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="confirmacion.aspx.cs" Inherits="proWeb.confirmacion" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="checkout-container">
        <!-- Barra de progreso -->
        <div class="progress-steps mb-5">
            <div class="step completed">
                <div class="step-number">1</div>
                <div class="step-label">Carrito</div>
            </div>
            <div class="step completed">
                <div class="step-number">2</div>
                <div class="step-label">Envío</div>
            </div>
            <div class="step completed">
                <div class="step-number">3</div>
                <div class="step-label">Pago</div>
            </div>
            <div class="step active">
                <div class="step-number">4</div>
                <div class="step-label">Confirmación</div>
            </div>
        </div>

        <!-- Mensaje de confirmación -->
        <div class="confirmation-card">
            <div class="card-header bg-success text-white">
                <h2 class="mb-0"><i class="fas fa-check-circle me-2"></i>¡Pedido Confirmado!</h2>
            </div>
            <div class="card-body">
                <div class="alert alert-success">
                    <h4 class="alert-heading">¡Gracias por tu compra!</h4>
                    <p>Tu pedido <strong><asp:Literal ID="litNumeroPedido" runat="server" /></strong> ha sido procesado correctamente.</p>
                    <hr>
                    <p class="mb-0">Hemos enviado un correo de confirmación a tu dirección de email.</p>
                </div>

                <!-- Información del cliente y pago -->
                <div class="row mt-4">
                    <div class="col-md-6">
                        <div class="confirmation-section">
                            <h4><i class="fas fa-user me-2"></i>Datos del Cliente</h4>
                            <div class="confirmation-details">
                                <asp:Literal ID="litDatosCliente" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="confirmation-section">
                            <h4><i class="fas fa-credit-card me-2"></i>Método de Pago</h4>
                            <div class="confirmation-details">
                                <asp:Literal ID="litMetodoPago" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Resumen del pedido -->
                <div class="confirmation-section mt-4">
                    <h4><i class="fas fa-boxes me-2"></i>Resumen del Pedido</h4>
                    <div class="table-responsive">
                        <table class="table table-bordered">
                            <thead class="table-light">
                                <tr>
                                    <th>Producto</th>
                                    <th class="text-end">Precio Unitario</th>
                                    <th class="text-center">Cantidad</th>
                                    <th class="text-end">Subtotal</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptResumen" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("Nombre") %></td>
                                            <td class="text-end"><%# Eval("Precio", "{0:0.00}") %> €</td>
                                            <td class="text-center"><%# Eval("Cantidad") %></td>
                                            <td class="text-end"><%# Eval("Subtotal", "{0:0.00}") %> €</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                            <tfoot class="table-light">
                                <tr>
                                    <th colspan="3" class="text-end">Subtotal:</th>
                                    <th class="text-end"><asp:Literal ID="litSubtotal" runat="server" /> €</th>
                                </tr>    
                                <tr>
                                    <th colspan="3" class="text-end">Envío:</th>
                                    <th class="text-end"><asp:Literal ID="litEnvio" runat="server" /> €</th>
                                </tr>
                                <tr class="table-active">
                                    <th colspan="3" class="text-end">TOTAL:</th>
                                    <th class="text-end"><asp:Literal ID="litTotal" runat="server" /> €</th>
                                </tr>
                             </tfoot>
                        </table>
                    </div>
                </div>

                <!-- Botones de acción -->
                <div class="d-flex justify-content-between mt-4">
                    <a href="catalogo.aspx" class="btn btn-outline-primary">
                        <i class="fas fa-arrow-left me-2"></i>Seguir Comprando
                    </a>
                    <asp:Button ID="btnGenerarPDF" runat="server" Text="Descargar Factura" 
                        CssClass="btn btn-success" OnClick="btnGenerarPDF_Click" />
                    <a href="Default.aspx" class="btn btn-primary">
                        <i class="fas fa-home me-2"></i>Ir a Inicio
                    </a>
                </div>
            </div>
        </div>
    </div>

    <!-- Estilos CSS -->
    <style>
        .checkout-container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 2rem 1rem;
        }
        .progress-steps {
            display: flex;
            justify-content: space-between;
            position: relative;
        }
        .step {
            display: flex;
            flex-direction: column;
            align-items: center;
        }
        .step-number {
            width: 30px;
            height: 30px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            font-weight: bold;
            margin-bottom: 0.5rem;
        }
        .step.active .step-number {
            background: #0d6efd;
            color: white;
        }
        .step.completed .step-number {
            background: #198754;
            color: white;
        }
        .confirmation-card {
            border: 1px solid #dee2e6;
            border-radius: 0.5rem;
            overflow: hidden;
        }
        .confirmation-section {
            background-color: #f8f9fa;
            padding: 1.5rem;
            border-radius: 0.5rem;
            margin-bottom: 1.5rem;
        }
    </style>
</asp:Content>
