<%@ Page Title="Artículo" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="articulo.aspx.cs" Inherits="proWeb.articulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        :root {
            --turquesa-principal: #17cfc4;
            --turquesa-oscuro: #139e96;
            --turquesa-claro: #d1f7f5;
            --gris-fondo: #f8f9fa;
        }
        
        .articulo-detalle {
            max-width: 1200px;
            margin: 40px auto;
            padding: 40px;
            background: white;
            border-radius: 15px;
            box-shadow: 0 10px 30px rgba(0,0,0,0.08);
            border: 1px solid #e0e0e0;
        }
        
        .articulo-header {
            display: flex;
            gap: 60px;
            margin-bottom: 40px;
            flex-wrap: wrap;
        }
        
        .articulo-imagen-container {
            flex: 1;
            min-width: 300px;
            max-width: 500px;
            position: relative;
        }
        
        .articulo-imagen {
            width: 100%;
            border-radius: 12px;
            object-fit: cover;
            box-shadow: 0 5px 15px rgba(0,0,0,0.1);
            transition: transform 0.3s;
        }
        
        .articulo-imagen:hover {
            transform: scale(1.02);
        }
        
        .badge-nuevo {
            position: absolute;
            top: 15px;
            left: 15px;
            background-color: var(--turquesa-principal);
            color: white;
            padding: 5px 15px;
            border-radius: 20px;
            font-weight: 600;
            font-size: 0.9rem;
            box-shadow: 0 2px 5px rgba(0,0,0,0.2);
        }
        
        .articulo-info {
            flex: 1;
            min-width: 300px;
            display: flex;
            flex-direction: column;
        }
        
        .articulo-titulo {
            font-size: 2.5rem;
            font-weight: 700;
            margin-bottom: 15px;
            color: #333;
            line-height: 1.2;
        }
        
        .articulo-vendedor {
            font-size: 1.1rem;
            color: #555;
            margin-bottom: 20px;
            display: flex;
            align-items: center;
            gap: 8px;
        }
        
        .articulo-vendedor i {
            color: var(--turquesa-principal);
        }
        
        .articulo-descripcion {
            font-size: 1.15rem;
            line-height: 1.7;
            margin-bottom: 25px;
            color: #555;
        }
        
        .articulo-precio-container {
            margin-bottom: 20px;
        }
        
        .articulo-precio {
            font-size: 2.2rem;
            font-weight: 700;
            color: var(--turquesa-principal);
            margin-right: 15px;
        }
        
        .articulo-precio-original {
            font-size: 1.5rem;
            color: #999;
            text-decoration: line-through;
        }
        
        .articulo-acciones {
            display: flex;
            gap: 15px;
            margin-top: 20px;
            flex-wrap: wrap;
        }
        
        .btn-comprar {
            background: var(--turquesa-principal);
            color: white;
            border: none;
            padding: 12px 35px;
            font-size: 1.1rem;
            border-radius: 8px;
            cursor: pointer;
            transition: all 0.3s;
            font-weight: 600;
            display: flex;
            align-items: center;
            gap: 10px;
        }
        
        .btn-comprar:hover {
            background: var(--turquesa-oscuro);
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(40, 167, 69, 0.3);
        }
        
        .btn-secundario {
            background: white;
            color: var(--turquesa-principal);
            border: 2px solid var(--turquesa-principal);
            padding: 12px 25px;
            font-size: 1.1rem;
            border-radius: 8px;
            cursor: pointer;
            transition: all 0.3s;
            font-weight: 600;
            display: flex;
            align-items: center;
            gap: 10px;
        }
        
        .btn-secundario:hover {
            background: var(--turquesa-claro);
            color: var(--turquesa-oscuro);
            border-color: var(--turquesa-oscuro);
        }
        
        .especificaciones {
            margin-top: 50px;
            border-top: 1px solid #eee;
            padding-top: 30px;
        }
        
        .especificaciones h3 {
            font-size: 1.8rem;
            margin-bottom: 25px;
            color: #333;
            position: relative;
            padding-bottom: 10px;
        }
        
        .especificaciones h3::after {
            content: '';
            position: absolute;
            bottom: 0;
            left: 0;
            width: 60px;
            height: 3px;
            background: var(--turquesa-principal);
        }
        
        .especificaciones-grid {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
            gap: 20px;
        }
        
        .especificacion-item {
            background: var(--gris-fondo);
            padding: 15px;
            border-radius: 8px;
            display: flex;
            align-items: center;
            gap: 15px;
            transition: all 0.3s;
        }
        
        .especificacion-item:hover {
            transform: translateY(-3px);
            box-shadow: 0 5px 15px rgba(0,0,0,0.05);
        }
        
        .especificacion-icono {
            width: 40px;
            height: 40px;
            background: var(--turquesa-principal);
            color: white;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 1.2rem;
        }
        
        .especificacion-contenido {
            flex: 1;
        }
        
        .especificacion-label {
            font-weight: 600;
            color: #555;
            font-size: 0.9rem;
            margin-bottom: 3px;
        }
        
        .especificacion-valor {
            font-weight: 500;
            color: #333;
            font-size: 1.1rem;
        }

        /* Estilos para comentarios */
        .comment-section {
            margin-top: 50px;
            border-top: 1px solid #eee;
            padding-top: 30px;
        }

        .comment-box {
            width: 100%;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 4px;
            margin-bottom: 10px;
        }

        .comment-list {
            margin-top: 20px;
        }

        .comment {
            background: #f9f9f9;
            padding: 15px;
            border-radius: 5px;
            margin-bottom: 15px;
            border-left: 4px solid var(--turquesa-principal);
        }

        .comment-header {
            display: flex;
            justify-content: space-between;
            margin-bottom: 8px;
            font-size: 0.9rem;
        }

        .comment-author {
            font-weight: bold;
            color: var(--turquesa-oscuro);
        }

        .comment-date {
            color: #777;
        }

        .comment-body {
            margin-bottom: 10px;
        }

        .comment-actions {
            display: flex;
            gap: 10px;
        }
        
        /* Responsive */
        @media (max-width: 768px) {
            .articulo-header {
                gap: 30px;
            }
            
            .articulo-titulo {
                font-size: 2rem;
            }
            
            .articulo-acciones {
                flex-direction: column;
            }
            
            .btn-comprar, .btn-secundario {
                width: 100%;
                justify-content: center;
            }
        }

        .articulo-estado {
            display: flex;
            gap: 15px;
            align-items: center;
            margin-bottom: 15px;
            flex-wrap: wrap;
        }

        .badge-estado {
            background-color: var(--turquesa-principal);
            color: white;
            padding: 5px 15px;
            border-radius: 20px;
            font-weight: 600;
            font-size: 0.9rem;
        }

        .badge-estado.vendido {
            background-color: #dc3545;
        }

        .stock-info, .estado-info {
            display: flex;
            align-items: center;
            gap: 5px;
            font-size: 0.95rem;
            color: #555;
        }

        .stock-info i, .estado-info i {
            color: var(--turquesa-principal);
        }

        .rating-section {
            margin: 15px 0;
        }
        .user-rating select {
            padding: 5px;
            border-radius: 4px;
            margin: 0 10px;
        }

        .rating-section {
            margin: 15px 0;
            padding: 15px;
            background: rgba(209, 247, 245, 0.3); /* Fondo semitransparente */
            border-radius: 8px;
            border-left: 4px solid var(--turquesa-principal);
        }

        .rating-section h3 {
            color: var(--turquesa-oscuro);
            margin-bottom: 10px;
            font-size: 1.3rem;
            display: inline-block;
            margin-right: 15px;
        }

        .rating-container {
            display: flex;
            align-items: center;
            flex-wrap: wrap;
            gap: 10px;
        }

        .rating-value {
            font-size: 1.5rem;
            font-weight: bold;
            color: var(--turquesa-principal);
        }

        .rating-stars {
            color: #FFC107; /* Amarillo más vibrante */
            font-size: 1.3rem;
        }

        .user-rating {
            margin-top: 12px;
            display: flex;
            align-items: center;
            flex-wrap: wrap;
            gap: 8px;
        }

        .user-rating select {
            padding: 6px 10px;
            border-radius: 5px;
            border: 1px solid var(--turquesa-principal);
            background: white;
            font-size: 0.9rem;
            min-width: 120px;
        }

        .btn-rating {
            background: var(--turquesa-principal);
            color: white;
            border: none;
            padding: 6px 15px;
            border-radius: 5px;
            cursor: pointer;
            transition: all 0.2s;
            font-weight: 600;
            font-size: 0.9rem;
        }

        .btn-rating:hover {
            background: var(--turquesa-oscuro);
            transform: translateY(-1px);
        }


    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="articulo-detalle">
        <div class="articulo-header">
            <div class="articulo-imagen-container">
                <asp:Image ID="imgArticulo" runat="server" CssClass="articulo-imagen" />
                <span class="badge-nuevo">
                    <asp:Literal ID="LiteralEstado" runat="server"></asp:Literal>
                </span>
            </div>
            
            <div class="articulo-info">
                <h1 class="articulo-titulo">
                    <asp:Literal ID="LiteralModelo" runat="server"></asp:Literal>
                </h1>
                <p class="articulo-vendedor">
                    <i class="fas fa-store"></i> Vendido por: <asp:Literal ID="LiteralVendedor" runat="server"></asp:Literal>
                </p>
                <p class="articulo-descripcion">
                    <asp:Literal ID="LiteralDescripcion" runat="server"></asp:Literal>
                </p>
                
                <div class="articulo-precio-container">
                    <span class="articulo-precio">
                        <asp:Literal ID="LiteralPrecio" runat="server"></asp:Literal>
                    </span>
                </div>
                
                <!-- Sección de estado, stock y proveedor -->
                <div class="articulo-estado">
                    <span class="badge-estado">
                        <asp:Literal ID="LiteralVendido" runat="server"></asp:Literal>
                    </span>
                    <span class="stock-info">
                        <i class="fas fa-box"></i> Stock: <asp:Literal ID="LiteralStock" runat="server"></asp:Literal>
                    </span>
                </div>
                
                <p class="articulo-vendedor">
                    <asp:Literal ID="LiteralProveedor" runat="server" Visible="false"></asp:Literal>
                </p>
                
                <!-- Sección de valoración -->
                <div class="rating-section">
                    <h3>Valoración:</h3>
                    <asp:Literal ID="LiteralValoracion" runat="server" Text="0.0"></asp:Literal>
                    <asp:Literal ID="LiteralStars" runat="server"></asp:Literal>
            
                    <div class="user-rating" style="margin-top: 10px;">
                        <asp:Label ID="lblRating" runat="server" Text="Tu valoración: " Visible="false"></asp:Label>
                        <asp:DropDownList ID="ddlRating" runat="server" Visible="false">
                            <asp:ListItem Value="1">★ </asp:ListItem>
                            <asp:ListItem Value="2">★★ </asp:ListItem>
                            <asp:ListItem Value="3">★★★</asp:ListItem>
                            <asp:ListItem Value="4">★★★★ </asp:ListItem>
                            <asp:ListItem Value="5">★★★★★</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnEnviarValoracion" runat="server" Text="Enviar" 
                            OnClick="SubmitRating_Click" CssClass="btn btn-primary" Visible="false" />
                    </div>
                </div>
                
                <div class="articulo-acciones">
                    <asp:Button ID="btnFavoritos" runat="server" Text="Añadir a favoritos" 
                        OnClick="AccionArticulo_Click" CommandName="Favoritos" CssClass="btn-favoritos" />

                    <asp:Button ID="btnCarrito" runat="server" Text="Añadir al carrito" 
                        OnClick="AccionArticulo_Click" CommandName="Carrito" CssClass="btn-carrito" />

                    <asp:Button ID="btnComprar" runat="server" Text="Comprar ahora" 
                        OnClick="AccionArticulo_Click" CommandName="Compra" CssClass="btn-comprar" />
                </div>
            </div>
        </div>

        <div class="especificaciones">
            <h3>Especificaciones técnicas</h3>
            <div class="especificaciones-grid">
                
                <div class="especificacion-item">
                    <div class="especificacion-icono">
                        <i class="fas fa-mobile-alt"></i>
                    </div>
                    <div class="especificacion-contenido">
                        <div class="especificacion-label">Marca</div>
                        <div class="especificacion-valor">
                            <asp:Literal ID="LiteralMarca" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>

                <div class="especificacion-item">
                    <div class="especificacion-icono">
                        <i class="fas fa-calendar-alt"></i>
                    </div>
                    <div class="especificacion-contenido">
                        <div class="especificacion-label">Año de lanzamiento</div>
                        <div class="especificacion-valor">
                            <asp:Literal ID="LiteralAnyo" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>

                <div class="especificacion-item">
                    <div class="especificacion-icono">
                        <i class="fas fa-memory"></i>
                    </div>
                    <div class="especificacion-contenido">
                        <div class="especificacion-label">Memoria</div>
                        <div class="especificacion-valor">
                            <asp:Literal ID="LiteralMemoria" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>

                <div class="especificacion-item">
                    <div class="especificacion-icono">
                        <i class="fas fa-hdd"></i>
                    </div>
                    <div class="especificacion-contenido">
                        <div class="especificacion-label">Almacenamiento</div>
                        <div class="especificacion-valor">
                            <asp:Literal ID="LiteralAlmacenamiento" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>

                <div class="especificacion-item">
                    <div class="especificacion-icono">
                        <i class="fas fa-robot"></i>
                    </div>
                    <div class="especificacion-contenido">
                        <div class="especificacion-label">Sistema Operativo</div>
                        <div class="especificacion-valor">
                            <asp:Literal ID="LiteralSistemaOperativo" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>

                <div class="especificacion-item">
                    <div class="especificacion-icono">
                        <i class="fas fa-battery-full"></i>
                    </div>
                    <div class="especificacion-contenido">
                        <div class="especificacion-label">Batería</div>
                        <div class="especificacion-valor">
                            <asp:Literal ID="LiteralBateria" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>

                <div class="especificacion-item">
                    <div class="especificacion-icono">
                        <i class="fas fa-palette"></i>
                    </div>
                    <div class="especificacion-contenido">
                        <div class="especificacion-label">Color</div>
                        <div class="especificacion-valor">
                            <asp:Literal ID="LiteralColor" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Sección de comentarios -->
        <div class="comment-section">
            <h3>Comentarios</h3>
            
            <asp:TextBox ID="TextBoxComentario" runat="server" CssClass="comment-box" TextMode="MultiLine" Rows="3" placeholder="Escribe tu comentario aquí..."></asp:TextBox>
            <asp:Button ID="ButtonAgregarComentario" runat="server" Text="Agregar Comentario" OnClick="ButtonAgregarComentario_Click" CssClass="btn-comprar" Visible='<%# Session["usuarios"] != null %>' />
            
            <div class="comment-list">
                <asp:Repeater ID="RepeaterComentarios" runat="server">
                    <ItemTemplate>
                        <div class="comment">
                            <div class="comment-header">
                                <span class="comment-author"><%# Eval("UsuarioUName") %></span>
                                <span class="comment-date"><%# Eval("FechaComentario", "{0:dd/MM/yyyy HH:mm}") %></span>
                            </div>
                            <div class="comment-body">
                                <asp:Label ID="LabelComentario" runat="server" Text='<%# Eval("Comentario") %>'></asp:Label>
                                <asp:TextBox ID="TextBoxEditComentario" runat="server" Text='<%# Eval("Comentario") %>' Visible="false" CssClass="comment-box"></asp:TextBox>
                            </div>
                            <div class="comment-actions">
                                <asp:Button ID="ButtonEditar" runat="server" Text="Editar" CommandArgument='<%# Eval("ComentarioId") %>' OnClick="ButtonEditar_Click" CssClass="btn-secundario" Visible='<%# IsEditButtonVisible(Eval("UsuarioUName").ToString()) %>' />
                                <asp:Button ID="ButtonGuardar" runat="server" Text="Guardar" CommandArgument='<%# Eval("ComentarioId") %>' OnClick="ButtonGuardar_Click" CssClass="btn-comprar" Visible="false" />
                                <asp:Button ID="ButtonEliminar" runat="server" Text="Eliminar" CommandArgument='<%# Eval("ComentarioId") %>' OnClick="ButtonEliminar_Click" CssClass="btn-secundario" Visible='<%# IsDeleteButtonVisible(Eval("UsuarioUName").ToString()) %>' />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>