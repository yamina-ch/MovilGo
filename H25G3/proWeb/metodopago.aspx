<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="metodopago.aspx.cs" Inherits="proWeb.metodopago" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="checkout-container">
        <!-- Progress Bar -->
        <div class="progress-steps mb-5">
            <div class="step completed">
                <div class="step-number">1</div>
                <div class="step-label">Carrito</div>
            </div>
            <div class="step completed">
                <div class="step-number">2</div>
                <div class="step-label">Envío</div>
            </div>
            <div class="step active">
                <div class="step-number">3</div>
                <div class="step-label">Pago</div>
            </div>
            <div class="step">
                <div class="step-number">4</div>
                <div class="step-label">Confirmación</div>
            </div>
        </div>

        <div class="row g-4">
            <!-- Resumen del Pedido con envío -->
            <div class="col-lg-5">
                <div class="order-summary-card">
                    <div class="card-header">
                        <h3 class="fw-bold"><i class="fas fa-receipt me-2"></i>Resumen de tu pedido</h3>
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
                                            <span class="price"><%# (Convert.ToDouble(Eval("Precio")) * Convert.ToInt32(Eval("Cantidad"))).ToString("0.00") %> €</span>
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

            <!-- Formulario de Pago -->
            <div class="col-lg-7">
                <div class="payment-form-card">
                    <div class="card-header">
                        <h3 class="fw-bold"><i class="far fa-credit-card me-2"></i>Método de pago</h3>
                    </div>
                    <div class="card-body">
                        
                        <div class="form-section">
                            <h4 class="section-title">Método de envío</h4>
                            <div class="shipping-methods">
                                <div class="form-check">
                                    <input class="form-check-input" type="radio" name="shippingMethod" id="standardShipping" checked disabled>
                                    <label class="form-check-label" for="standardShipping">
                                        <span class="method-name">Envío estándar</span>
                                        <span class="method-duration">3-5 días laborables</span>
                                        <span class="method-price">4,5€</span>
                                    </label>
                                </div>
                            </div>
                        </div>

                        <!-- Métodos de pago -->
                        <div class="payment-methods mb-4">
                            <div class="form-check">
                                <input class="form-check-input" type="radio" name="paymentMethod" id="creditCard" checked>
                                <label class="form-check-label" for="creditCard">
                                    <i class="fab fa-cc-visa"></i>
                                    <i class="fab fa-cc-mastercard"></i>
                                    <i class="fab fa-cc-amex"></i>
                                    Tarjeta de crédito/débito
                                </label>
                            </div>
                            <div class="form-check">
                                <input class="form-check-input" type="radio" name="paymentMethod" id="paypal">
                                <label class="form-check-label" for="paypal">
                                    <i class="fab fa-paypal"></i> PayPal
                                </label>
                            </div>
                        </div>

                        <!-- Tarjeta de crédito (visible solo cuando se selecciona) -->
                        <div id="creditCardForm">
                            <div class="form-section">
                                <h4 class="section-title">Información de la tarjeta</h4>
                                
                                <!-- Número de tarjeta -->
                                <div class="mb-4">
                                    <label class="form-label">Número de tarjeta*</label>
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="far fa-credit-card"></i></span>
                                        <asp:TextBox ID="txtNumeroTarjeta" runat="server" CssClass="form-control" 
                                            MaxLength="19" placeholder="1234 5678 9012 3456"></asp:TextBox>
                                    </div>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtNumeroTarjeta"
                                        ValidationExpression="^\d{16}$" ErrorMessage="Tarjeta inválida (16 dígitos)"
                                        CssClass="invalid-feedback" Display="Dynamic"/>
                                </div>
                                
                                <div class="row g-3">
                                    <!-- Fecha expiración -->
                                    <div class="col-md-6">
                                        <label class="form-label">Fecha de expiración*</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtExpiracion" runat="server" CssClass="form-control" 
                                                placeholder="MM/AA" MaxLength="5"></asp:TextBox>
                                        </div>
                                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtExpiracion"
                                            ValidationExpression="^(0[1-9]|1[0-2])\/?([0-9]{2})$" 
                                            ErrorMessage="Formato MM/AA"
                                            CssClass="invalid-feedback" Display="Dynamic"/>
                                    </div>
                                    
                                    <!-- CVV -->
                                    <div class="col-md-6">
                                        <label class="form-label">Código de seguridad (CVV)*</label>
                                        <div class="input-group">
                                            <span class="input-group-text"><i class="fas fa-lock"></i></span>
                                            <asp:TextBox ID="txtCVV" runat="server" CssClass="form-control" 
                                                MaxLength="3" placeholder="123" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                            <!-- Titular de la tarjeta -->
                            <div class="form-section">
                                <h4 class="section-title">Titular de la tarjeta</h4>
                                <div class="mb-3">
                                    <label class="form-label">Nombre completo*</label>
                                    <asp:TextBox ID="txtTitular" runat="server" CssClass="form-control" 
                                        placeholder="Como aparece en la tarjeta"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        
                        <!-- PayPal (oculto inicialmente) -->
                        <div id="paypalForm" style="display:none;">
                            <div class="alert alert-info">
                                <p>Serás redirigido a PayPal para completar tu pago de manera segura.</p>
                                <p>Al hacer clic en "Confirmar pedido", aceptas los <a href="#">Términos y Condiciones</a> de MovilGo.</p>
                            </div>
                        </div>
                        
                        <!-- Términos y condiciones -->
                        <div class="form-check mt-4">
                            <input class="form-check-input" type="checkbox" id="termsCheck" required>
                            <label class="form-check-label" for="termsCheck">
                                Acepto los <a href="#" data-bs-toggle="modal" data-bs-target="#termsModal">Términos y Condiciones</a> y la <a href="#">Política de Privacidad</a>
                            </label>
                        </div>
                        
                        <!-- Botón de confirmación -->
                        <div class="d-grid mt-4">
                            <asp:Button ID="btnConfirmarPago" runat="server" Text="Confirmar pedido" 
                                CssClass="btn btn-primary btn-lg checkout-btn" OnClick="btnConfirmarPago_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Términos y Condiciones -->
    <div class="modal fade" id="termsModal" tabindex="-1" aria-labelledby="termsModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="termsModalLabel">Términos y Condiciones</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <!-- Contenido de términos y condiciones -->
                    <p>Aquí iría el contenido extenso de los términos y condiciones...</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <style>
        /* Estilos consistentes con la página anterior */
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
        .order-summary-card, .payment-form-card {
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
        
        .shipping-cost {
            padding-bottom: 0.5rem;
            border-bottom: 1px dashed #dee2e6;
        }
        
        .grand-total {
            font-size: 1.1rem;
            font-weight: 600;
            margin-top: 1rem;
            padding-top: 1rem;
            border-top: 1px solid #e9ecef;
        }
        
        /* Payment Methods */
        .payment-methods {
            border: 1px solid #e9ecef;
            border-radius: 0.25rem;
            overflow: hidden;
        }
        
        .payment-methods .form-check {
            padding: 1rem;
            margin: 0;
            border-bottom: 1px solid #e9ecef;
        }
        
        .payment-methods .form-check:last-child {
            border-bottom: none;
        }
        
        .payment-methods .form-check-label {
            display: flex;
            align-items: center;
            gap: 10px;
        }
        
        .payment-methods .fab {
            font-size: 1.5rem;
        }
        
        .fa-cc-visa { color: #1a1f71; }
        .fa-cc-mastercard { color: #eb001b; }
        .fa-cc-amex { color: #0070d1; }
        .fa-paypal { color: #003087; }
        
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
            overflow: hidden;
        }
        
        .shipping-methods .form-check {
            padding: 1rem;
            margin: 0;
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
    </style>

    <script>
        // Formatear número de tarjeta
        $('#<%= txtNumeroTarjeta.ClientID %>').on('input', function () {
            this.value = this.value.replace(/\D/g, '')
                .replace(/(\d{4})(?=\d)/g, '$1 ');
        });

        // Formatear fecha expiración
        $('#<%= txtExpiracion.ClientID %>').on('input', function () {
            this.value = this.value.replace(/\D/g, '')
                .replace(/(\d{2})(?=\d)/g, '$1/');
        });

        // Mostrar/ocultar formularios según método de pago
        $(document).ready(function () {
            $('input[name="paymentMethod"]').change(function () {
                if ($('#creditCard').is(':checked')) {
                    $('#creditCardForm').show();
                    $('#paypalForm').hide();
                } else {
                    $('#creditCardForm').hide();
                    $('#paypalForm').show();
                }
            });
        });
    </script>
    
    <asp:HiddenField ID="hdnTotal" runat="server" />
</asp:Content>