<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="carrito.aspx.cs" Inherits="proWeb.carrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #ContentPlaceHolder1 {
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 100%;
    min-height: 100vh;
    background-color: #f8f9fa;
    padding: 10px;
}

h1 {
    color: #007bff;
    text-align: center;
    margin-bottom: 2rem;
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    font-weight: 600;
}

#container, #container1 {
    display: flex;
    flex-direction: column;
    width: 100%;
    gap: 1.5rem;
    background-color: white;
    border-radius: 8px;
    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
    padding: 1.5rem;
}

.producto {
    display: flex;
    flex-direction: row;
    align-items: center;
    padding: 1rem;
    border-bottom: 3px solid #eee;
    gap: 1.5rem;
}

.producto:last-child {
    border-bottom: none;
}

.producto img {
    width: 120px;
    height: 120px;
    object-fit: cover;
    border-radius: 6px;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
}

.producto-info {
    flex: 1;
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
}

.producto h5 {
    font-weight: 600;
    font-size: 1.25rem;
    margin: 0;
    color: #343a40;
}

.producto p {
    color: #495057;
    margin: 0;
    font-size: 1.1rem;
}

.producto-actions {
    display: flex;
    flex-direction: column;
    gap: 0.8rem;
    align-items: center;
}

.quantity-controls {
    display: flex;
    align-items: center;
    border-radius: 4px;
    border: 1px solid #dee2e6;
    overflow: hidden;
    background-color: white;
}

.quantity-btn {
    background-color: #f8f9fa;
    border: none;
    color: #212529;
    width: 30px;
    height: 30px;
    font-size: 16px;
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: center;
    user-select: none;
}

.quantity-btn:hover {
    background-color: #e9ecef;
}

.quantity-display {
    padding: 0 10px;
    min-width: 40px;
    text-align: center;
    border-left: 1px solid #dee2e6;
    border-right: 1px solid #dee2e6;
    font-weight: 500;
}

.button {
    background-color: #d6183b;
    color: white;
    border: none;
    padding: 0.5rem 1rem;
    border-radius: 4px;
    cursor: pointer;
    font-weight: 500;
    transition: background-color 0.3s;
    width: 100%;
    text-align: center;
}

.button:hover {
    background-color: red;
}

.cart-total {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-top: 1.5rem;
    padding-top: 1.5rem;
    border-top: 2px solid #eee;
    font-size: 1.2rem;
    font-weight: 600;
}

.checkout-btn {
    background-color: black;
    color: white;
    border: none;
    padding: 0.75rem 1.5rem;
    border-radius: 4px;
    cursor: pointer;
    font-weight: 500;
    font-size: 1.1rem;
    margin-top: 1.5rem;
    margin-bottom: 1.5rem;
    margin-left: 10px;
    margin-right: 10px;
    transition: background-color 0.3s;
    width: 100%;
}

.checkout-btn:hover {
    background-color: #343a40;
}

@media (max-width: 700px) {
    .producto {
        flex-direction: column;
        align-items: flex-start;
    }
    
    .producto img {
        width: 100%;
        height: auto;
        max-height: 200px;
    }
    
    .producto-actions {
        flex-direction: row;
        width: 100%;
        justify-content: space-between;
        margin-top: 1rem;
    }
    
    .button {
        width: auto;
    }
}

span {
    margin-left: 10px;
    margin-right: 10px;
}
    </style>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        function updateQuantity(productId, change) {
            var quantityElement = document.getElementById('quantity_' + productId);
            var currentQuantity = parseInt(quantityElement.innerText);
            var newQuantity = currentQuantity + change;

            if (newQuantity >= 1) {
                // Mostrar loading
                var btn = event.target;
                var originalText = btn.innerHTML;
                btn.innerHTML = '<span class="loading">...</span>';
                btn.disabled = true;

                // Actualizar visualmente primero
                quantityElement.innerText = newQuantity;

                // Obtener el ID de la línea del carrito
                var lineaId = document.getElementById('hiddenLineaId_' + productId).value;

                // Actualizar en la base de datos via AJAX
                $.ajax({
                    type: "POST",
                    url: "carrito.aspx/ActualizarCantidad",
                    data: JSON.stringify({
                        lineaId: parseInt(lineaId),
                        nuevaCantidad: newQuantity
                    }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d) {
                            // Recalcular totales después de actualizar la BD
                            actualizarTotales(productId);
                            recalcularTotal();
                        } else {
                            alert('No se pudo actualizar la cantidad');
                            // Revertir visualmente si falla
                            quantityElement.innerText = currentQuantity;
                        }
                    },
                    error: function (xhr, status, error) {
                        alert('Error al comunicarse con el servidor: ' + error);
                        quantityElement.innerText = currentQuantity;
                        console.error('Error AJAX:', xhr.responseText);
                    },
                    complete: function () {
                        // Restaurar botón
                        btn.innerHTML = originalText;
                        btn.disabled = false;
                    }
                });
            }
        }

        function actualizarTotales(productId) {
            try {
                // Obtener precio y cantidad actual
                var priceElement = document.getElementById('price_' + productId);
                var quantityElement = document.getElementById('quantity_' + productId);
                var lineTotalElement = document.getElementById('lineTotal_' + productId);

                if (priceElement && quantityElement && lineTotalElement) {
                    var precio = parseFloat(priceElement.innerText);
                    var cantidad = parseInt(quantityElement.innerText);
                    var subtotal = precio * cantidad;

                    // Actualizar el subtotal de la línea
                    lineTotalElement.innerText = subtotal.toFixed(2);

                    // Recalcular el total general
                    recalcularTotal();
                }
            } catch (error) {
                console.error('Error al actualizar totales:', error);
            }
        }

        function eliminarLinea(lineaId, elemento) {
            if (confirm('¿Estás seguro de que quieres eliminar este producto?')) {
                $.ajax({
                    type: "POST",
                    url: "carrito.aspx/EliminarLineaCarrito",
                    data: JSON.stringify({ lineaId: parseInt(lineaId) }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d) {
                            $(elemento).closest('.producto').fadeOut(300, function () {
                                $(this).remove();
                                recalcularTotal();
                                // Si no quedan productos, mostrar mensaje
                                if ($('.producto').length === 0) {
                                    $('#container1').html('<p class="empty-cart-message" style="text-align: center; padding: 2rem; color: #6c757d;">Tu carrito está vacío</p>');
                                }
                            });
                        } else {
                            alert('No se pudo eliminar el producto');
                        }
                    },
                    error: function (xhr, status, error) {
                        alert('Error al comunicarse con el servidor: ' + error);
                        console.error('Error AJAX:', xhr.responseText);
                    }
                });
                recalcularTotal();
            }
        }

        function recalcularTotal() {
            try {
                var subtotals = document.querySelectorAll('[id^="lineTotal_"]');
                var grandTotal = 0;

                subtotals.forEach(function (subtotalElement) {
                    var value = parseFloat(subtotalElement.innerText) || 0;
                    grandTotal += value;
                });

                var totalElement = document.getElementById('litTotal');
                if (totalElement) {
                    totalElement.innerText = grandTotal.toFixed(2);
                }
            } catch (error) {
                console.error('Error al recalcular total:', error);
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Tu carro</h1>
    <div id="container1">
        <asp:Repeater ID="container" runat="server">
            <ItemTemplate>
                <div class="producto">
                    <asp:Image ID="imgProducto" runat="server" ImageUrl='<%# Eval("ImagenUrl") %>' AlternateText="Producto" />
                    <div class="producto-info">
                        <h5><%# Eval("Nombre") %></h5>
                        <p><strong id="price_<%# Container.ItemIndex %>"><%# Eval("Precio", "{0:0.00}") %></strong></p>
                        <p>Subtotal: <strong id="lineTotal_<%# Container.ItemIndex %>"><%# ((double)Eval("Precio") * (int)Eval("Cantidad")).ToString("0.00") %></strong></p>
                    </div>
                    <div class="producto-actions">
                        <div class="quantity-controls">
                            <button type="button" class="quantity-btn" onclick="updateQuantity('<%# Container.ItemIndex %>', -1)">-</button>
                            <span id="quantity_<%# Container.ItemIndex %>" class="quantity-display"><%# Eval("Cantidad") %></span>
                            <button type="button" class="quantity-btn" onclick="updateQuantity('<%# Container.ItemIndex %>', 1)">+</button>
                        </div>
                        <button type="button" class="button" onclick="eliminarLinea('<%# Eval("Linea_carrito_id") %>', this)">Eliminar</button>
                        <!-- Hidden fields con IDs únicos -->
                        <input type="hidden" id="hiddenQuantity_<%# Container.ItemIndex %>" value="<%# Eval("Cantidad") %>" />
                        <input type="hidden" id="hiddenLineaId_<%# Container.ItemIndex %>" value="<%# Eval("Linea_carrito_id") %>" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        
        <div class="cart-total">
            <span>Total:</span>
            <span>$<asp:Literal ID="litTotal" runat="server" ClientIDMode="Static" /></span>
        </div>
        
        <asp:Button ID="btnCheckout" CssClass="checkout-btn" Text="Proceed to Checkout" runat="server" OnClick="btnCheckout_Click" />
    </div>
</asp:Content>