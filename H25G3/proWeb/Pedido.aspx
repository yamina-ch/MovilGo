<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pedido.aspx.cs" Inherits="proWeb.pedido" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="checkout-container">
        <!-- Progress Bar -->
        <div class="progress-steps mb-5">
            <div class="step completed">
                <div class="step-number">1</div>
                <div class="step-label">Carrito</div>
            </div>
            <div class="step active">
                <div class="step-number">2</div>
                <div class="step-label">Envío</div>
            </div>
            <div class="step">
                <div class="step-number">3</div>
                <div class="step-label">Pago</div>
            </div>
            <div class="step">
                <div class="step-number">4</div>
                <div class="step-label">Confirmación</div>
            </div>
        </div>

        <div class="row g-4">
            <!-- Resumen del Pedido -->
            <div class="col-lg-5">
                <div class="order-summary-card">
                    <div class="card-header">
                        <h3 class="fw-bold"><i class="fas fa-box-open me-2"></i>Resumen de tu pedido</h3>
                    </div>
                    <div class="card-body">
                        <asp:Repeater ID="rptPedido" runat="server">
                            <ItemTemplate>
                                <div class="order-item">
                                    <div class="product-image">
                                        <img src='<%# Eval("ImagenURL") %>' alt='<%# Eval("Nombre") %>' class="img-fluid rounded">
                                    </div>
                                    <div class="product-details">
                                        <h5 class="product-name"><%# Eval("Nombre") %></h5>
                                        <div class="product-meta">
                                            <span class="quantity">Cantidad: <%# Eval("Cantidad") %></span>
                                            <span class="price"><%# (Convert.ToDouble(Eval("Precio")) * Convert.ToInt32(Eval("Cantidad"))) %> €</span>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        
                        <div class="order-totals">
                            <div class="total-row">
                                <span>Subtotal:</span>
                                <span class="subtotal"><asp:Literal ID="litSubtotal" runat="server" /> €</span>
                            </div>                            
                            <div class="total-row">
                                <span>Envío:</span>
                                <span class="shipping">4.5€ </span>
                            </div>
                            <div class="total-row grand-total">
                                <span>Total:</span>
                                <span class="total-price"><asp:Literal ID="litTotal" runat="server" /> €</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Formulario de Envío -->
            <div class="col-lg-7">
                <div class="shipping-form-card">
                    <div class="card-header">
                        <h3 class="fw-bold"><i class="fas fa-truck me-2"></i>Información de envío</h3>
                    </div>
                    <div class="card-body">
                        <div class="form-section">
                            <h4 class="section-title">Datos personales</h4>
                            <div class="row g-3">
                                <div class="col-md-6">
                                    <div class="form-floating">
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" 
                                            placeholder="Nombre completo" Required="true"></asp:TextBox>
                                        <label for="txtNombre">Nombre completo*</label>
                                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtNombre"
                                            ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]{5,100}$"
                                            ErrorMessage="Nombre inválido (mínimo 5 letras)"
                                            CssClass="invalid-feedback" Display="Dynamic"/>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-floating">
                                        <div class="input-group">
                                            <span class="input-group-text">+34</span>
                                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" 
                                                MaxLength="9" placeholder="Teléfono" Required="true"></asp:TextBox>
                                            <label for="txtTelefono">Teléfono*</label>
                                        </div>
                                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtTelefono"
                                            ValidationExpression="^[6-9]\d{8}$" 
                                            ErrorMessage="Teléfono inválido (ej: 612345678)"
                                            CssClass="invalid-feedback" Display="Dynamic"/>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-section">
                            <h4 class="section-title">Dirección de envío</h4>
                            <div class="row g-3">
                                <div class="col-md-8">
                                    <div class="form-floating">
                                        <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" 
                                            placeholder="Calle" Required="true"></asp:TextBox>
                                        <label for="txtCalle">Calle*</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" 
                                            placeholder="Número" Required="true"></asp:TextBox>
                                        <label for="txtNumero">Número*</label>
                                    </div>
                                </div>
                            </div>

                            <div class="row g-3 mt-2">
                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox ID="txtPlanta" runat="server" CssClass="form-control" 
                                            placeholder="Planta"></asp:TextBox>
                                        <label for="txtPlanta">Planta</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox ID="txtPuerta" runat="server" CssClass="form-control" 
                                            placeholder="Puerta"></asp:TextBox>
                                        <label for="txtPuerta">Puerta</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-floating">
                                        <asp:TextBox ID="txtCP" runat="server" CssClass="form-control" 
                                            MaxLength="5" placeholder="Código Postal" Required="true"></asp:TextBox>
                                        <label for="txtCP">Código Postal*</label>
                                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtCP"
                                            ValidationExpression="^\d{5}$" 
                                            ErrorMessage="Código postal inválido"
                                            CssClass="invalid-feedback" Display="Dynamic"/>
                                    </div>
                                </div>
                            </div>

                            <div class="row g-3 mt-2">
                                <div class="col-md-6">
                                    <div class="form-floating">
                                        <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" 
                                            placeholder="Ciudad" Required="true"></asp:TextBox>
                                        <label for="txtCiudad">Ciudad*</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-floating">
                                        <asp:DropDownList ID="ddlProvincia" runat="server" CssClass="form-select" Required="true">
                                            <asp:ListItem Value="">Seleccione provincia...</asp:ListItem>
                                            <asp:ListItem>Alicante</asp:ListItem>
                                            <asp:ListItem>Madrid</asp:ListItem>
                                            <asp:ListItem>Barcelona</asp:ListItem>
                                            <asp:ListItem>Valencia</asp:ListItem>
                                            <asp:ListItem>Sevilla</asp:ListItem>
                                            <asp:ListItem>Cadiz</asp:ListItem>
                                            <asp:ListItem>A Coruña</asp:ListItem>
                                        </asp:DropDownList>
                                        <label for="ddlProvincia">Provincia*</label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-section">
                            <h4 class="section-title">Método de envío</h4>
                            <div class="shipping-methods">
                                <div class="shipping-method-info">
                                    <span class="method-name">Envío estándar</span>
                                    <span class="method-duration">3-5 días laborables</span>
                                    <span class="method-price">Gratis</span>
                                </div>
                            </div>
                        </div>

                        <div class="d-grid mt-4">
                            <asp:Button ID="btnContinuarPago" runat="server" Text="Continuar al pago" 
                                CssClass="btn btn-primary btn-lg checkout-btn" OnClick="btnContinuarPago_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <style>
        /* Estilos personalizados */
        .checkout-container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 2rem 1rem;
        }
        
        /* Progress Steps */
        .progress-steps {
            display: flex;
            justify-content: space-between;
            position: relative;
        }
        
        .progress-steps::before {
            content: '';
            position: absolute;
            top: 15px;
            left: 0;
            right: 0;
            height: 2px;
            background: #e9ecef;
            z-index: 1;
        }
        
        .step {
            display: flex;
            flex-direction: column;
            align-items: center;
            position: relative;
            z-index: 2;
        }
        
        .step-number {
            width: 30px;
            height: 30px;
            border-radius: 50%;
            background: #e9ecef;
            color: #6c757d;
            display: flex;
            align-items: center;
            justify-content: center;
            font-weight: bold;
            margin-bottom: 0.5rem;
        }
        
        .step-label {
            font-size: 0.875rem;
            color: #6c757d;
        }
        
        .step.active .step-number {
            background: #0d6efd;
            color: white;
        }
        
        .step.active .step-label {
            color: #0d6efd;
            font-weight: 500;
        }
        
        .step.completed .step-number {
            background: #198754;
            color: white;
        }
        
        .step.completed .step-label {
            color: #198754;
        }
        
        /* Cards */
        .order-summary-card, .shipping-form-card {
            border: 1px solid #e9ecef;
            border-radius: 0.5rem;
            overflow: hidden;
            box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.075);
            margin-bottom: 1.5rem;
        }
        
        .card-header {
            background-color: #f8f9fa;
            padding: 1rem 1.5rem;
            border-bottom: 1px solid #e9ecef;
        }
        
        .card-body {
            padding: 1.5rem;
        }
        
        /* Order Items */
        .order-item {
            display: flex;
            padding: 1rem 0;
            border-bottom: 1px solid #f1f1f1;
        }
        
        .product-image {
            width: 80px;
            height: 80px;
            border-radius: 0.25rem;
            overflow: hidden;
            margin-right: 1rem;
            flex-shrink: 0;
        }
        
        .product-image img {
            width: 100%;
            height: 100%;
            object-fit: cover;
        }
        
        .product-details {
            flex-grow: 1;
        }
        
        .product-name {
            font-size: 1rem;
            margin-bottom: 0.25rem;
            color: #212529;
        }
        
        .product-meta {
            display: flex;
            justify-content: space-between;
            font-size: 0.875rem;
            color: #6c757d;
        }
        
        /* Order Totals */
        .order-totals {
            margin-top: 1.5rem;
            padding-top: 1rem;
            border-top: 1px solid #e9ecef;
        }
        
        .total-row {
            display: flex;
            justify-content: space-between;
            margin-bottom: 0.5rem;
        }
        
        .grand-total {
            font-size: 1.1rem;
            font-weight: 600;
            margin-top: 1rem;
            padding-top: 1rem;
            border-top: 1px solid #e9ecef;
        }
        
        /* Form Sections */
        .form-section {
            margin-bottom: 2rem;
        }
        
        .section-title {
            font-size: 1.1rem;
            margin-bottom: 1rem;
            color: #495057;
            position: relative;
            padding-bottom: 0.5rem;
        }
        
        .section-title::after {
            content: '';
            position: absolute;
            bottom: 0;
            left: 0;
            width: 40px;
            height: 2px;
            background: #0d6efd;
        }
        
        /* Shipping Methods */
        .shipping-methods {
            border: 1px solid #e9ecef;
            border-radius: 0.25rem;
            padding: 1rem;
        }
        
        .shipping-method-info {
            display: flex;
            justify-content: space-between;
            width: 100%;
        }
        
        .method-name {
            font-weight: 500;
        }
        
        .method-duration {
            color: #6c757d;
            font-size: 0.875rem;
        }
        
        .method-price {
            font-weight: 600;
            color: #198754;
        }
        
        /* Buttons */
        .checkout-btn {
            padding: 0.75rem;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            background: linear-gradient(135deg, #0d6efd, #0b5ed7);
            border: none;
        }
        
        .checkout-btn:hover {
            background: linear-gradient(135deg, #0b5ed7, #0a58ca);
        }
        
        /* Responsive */
        @media (max-width: 992px) {
            .progress-steps {
                flex-wrap: wrap;
                gap: 1rem;
            }
            
            .progress-steps::before {
                display: none;
            }
        }
    </style>
</asp:Content>