<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="favorito.aspx.cs" Inherits="proWeb.favoritos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>MOVILGO</title>

    <!-- Fonts -->
    <link href="https://fonts.googleapis.com/css2?family=Raleway:wght@400;600;700&display=swap" rel="stylesheet" />

    <!-- Bootstrap + Font Awesome -->
    <link href="assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet" />
    <link href="assets/css/main.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <style>
        /* Import de fuentes */
@import url('https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;600;700&display=swap'); /* Fuente más moderna y legible */

/* Variables de color para fácil modificación */
:root {
    --primary-color: #4A90E2; /* Azul vibrante */
    --secondary-color: #50E3C2; /* Verde azulado complementario */
    --text-dark: #333333;
    --text-medium: #555555;
    --text-light: #888888;
    --background-light: #F0F2F5;
    --card-background: #FFFFFF;
    --border-color: #E0E0E0;
    --shadow-light: rgba(0, 0, 0, 0.08);
    --shadow-hover: rgba(0, 0, 0, 0.15);
    --danger-color: #FF6B6B; /* Rojo para eliminar */
    --danger-hover: #E04D4D;
}

/* Estilos globales y de diseño principal */
body {
    font-family: 'Poppins', sans-serif; /* Aplicar la nueva fuente */
    margin: 0;
    padding: 0;
    box-sizing: border-box;
    background-color: var(--background-light);
    color: var(--text-dark);
}

#ContentPlaceHolder1 {
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 100%;
    min-height: 100vh;
    padding: 30px 20px; /* Más padding para una mejor separación */
    box-sizing: border-box;
}

h1 {
    color: var(--primary-color);
    text-align: center;
    margin-bottom: 3rem; /* Aumentar el margen inferior */
    font-weight: 700;
    font-size: 2.8rem; /* Título más grande */
    text-shadow: 1px 1px 3px var(--shadow-light);
    letter-spacing: 0.5px; /* Ligero espaciado entre letras */
}

/* Contenedor de la cuadrícula de productos */
.productos-grid {
    display: grid;
    /* Grid fijo para asegurar el tamaño de las tarjetas */
    grid-template-columns: repeat(auto-fill, minmax(280px, 1fr)); /* Mínimo 280px, crece hasta 1fr */
    gap: 25px; /* Espacio uniforme entre tarjetas */
    max-width: 1200px; /* Ancho máximo para la cuadrícula */
    width: 100%; /* Asegura que ocupe el ancho disponible */
    margin: 0 auto; /* Centrar la cuadrícula */
    padding: 0; /* Eliminar padding extra aquí para que el gap maneje el espacio */
    justify-content: center; /* Centrar las tarjetas si hay menos de las que caben en una fila */
}

/* Estilos de la tarjeta de producto individual */
.producto-card {
    border: 1px solid var(--border-color);
    border-radius: 12px;
    overflow: hidden;
    background-color: var(--card-background);
    display: flex;
    flex-direction: column;
    transition: transform 0.3s ease-in-out, box-shadow 0.3s ease-in-out;
    box-shadow: 0 6px 20px var(--shadow-light); /* Sombra más definida */
    height: 100%; /* Asegura que todas las tarjetas tengan la misma altura dentro de la cuadrícula */
}

.producto-card:hover {
    transform: translateY(-8px) scale(1.02);
    box-shadow: 0 15px 30px var(--shadow-hover);
}

.producto-card img {
    width: 100%;
    height: 200px; /* Altura fija para todas las imágenes */
    object-fit: cover; /* Asegura que la imagen cubra el espacio sin distorsionarse */
    border-bottom: 1px solid var(--border-color);
}

.producto-body {
    padding: 20px;
    text-align: center;
    flex-grow: 1;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
}

.producto-body h5 {
    font-weight: 600;
    font-size: 1.3em;
    margin-bottom: 10px;
    color: var(--text-dark);
}

.producto-body .text-muted {
    color: var(--text-medium);
    font-size: 0.95em;
    margin-bottom: 10px;
}

.producto-body p strong {
    color: var(--primary-color);
    font-size: 1.4em; /* Precio más destacado */
    font-weight: 700;
    display: block;
    margin-top: 15px;
}

.producto-footer {
    padding: 15px;
    text-align: center;
    border-top: 1px solid var(--border-color);
    background-color: #F9F9F9; /* Fondo ligero para el pie de la tarjeta */
}

/* Botón de eliminar favorito */
.btn-remove-favorite {
    background-color: var(--danger-color);
    color: white;
    border: none;
    padding: 10px 20px;
    border-radius: 8px; /* Bordes más redondeados */
    cursor: pointer;
    transition: background-color 0.3s ease, transform 0.2s ease;
    font-size: 1rem;
    font-weight: 600;
    display: inline-flex;
    align-items: center;
    gap: 8px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1); /* Sombra suave para el botón */
}

.btn-remove-favorite:hover {
    background-color: var(--danger-hover);
    transform: translateY(-2px);
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);
}

.btn-remove-favorite i {
    font-size: 1.1em;
}

/* Mensaje de favoritos vacíos */
.empty-favorites {
    text-align: center;
    padding: 50px;
    font-size: 1.5rem;
    color: var(--text-light);
    font-weight: 500;
    width: 100%;
    margin-top: 50px; /* Margen superior para separarlo del título */
    background-color: var(--card-background);
    border-radius: 12px;
    box-shadow: 0 4px 15px var(--shadow-light);
}

/* Media Queries para responsividad */
@media (max-width: 992px) {
    h1 {
        font-size: 2.5rem;
        margin-bottom: 2.5rem;
    }

    .productos-grid {
        grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
        gap: 20px;
    }

    .producto-card img {
        height: 180px;
    }
}

@media (max-width: 768px) {
    h1 {
        font-size: 2rem;
        margin-bottom: 2rem;
    }

    .productos-grid {
        grid-template-columns: repeat(auto-fill, minmax(220px, 1fr));
        gap: 15px;
        padding: 0 10px;
    }

    .producto-card img {
        height: 160px;
    }

    .producto-body {
        padding: 15px;
    }

    .producto-body h5 {
        font-size: 1.1em;
    }

    .producto-body p strong {
        font-size: 1.2em;
    }

    .btn-remove-favorite {
        padding: 8px 15px;
        font-size: 0.9em;
    }

    .empty-favorites {
        font-size: 1.3rem;
        padding: 40px;
    }
}

@media (max-width: 480px) {
    #ContentPlaceHolder1 {
        padding: 20px 10px;
    }

    h1 {
        font-size: 1.8rem;
        margin-bottom: 1.5rem;
    }

    .productos-grid {
        grid-template-columns: 1fr; /* Una columna para pantallas muy pequeñas */
        gap: 15px;
    }

    .producto-card img {
        height: 140px;
    }

    .producto-body {
        padding: 10px;
    }

    .producto-body h5 {
        font-size: 1em;
    }

    .producto-body p strong {
        font-size: 1.1em;
    }

    .btn-remove-favorite {
        padding: 7px 12px;
        font-size: 0.85em;
        gap: 5px;
    }

    .empty-favorites {
        font-size: 1.1rem;
        padding: 30px;
    }
}
    </style>
    <script type="text/javascript">
        function eliminarFavorito(listaFavoritoId, elemento) {
            if (confirm('¿Estás seguro de que quieres eliminar este producto de tus favoritos?')) {
                $.ajax({
                    type: "POST",
                    url: "favorito.aspx/EliminarFavorito",
                    data: JSON.stringify({ listaFavoritoId: listaFavoritoId }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d) {
                            $(elemento).closest('.producto-card').fadeOut(300, function () {
                                $(this).remove();
                                // Si no quedan productos, mostrar mensaje
                                if ($('.producto-card').length === 0) {
                                    $('.productos-grid').html('<div class="empty-favorites">No tienes productos en tus favoritos</div>');
                                }
                            });
                        } else {
                            alert('No se pudo eliminar el producto de favoritos');
                        }
                    },
                    error: function () {
                        alert('Error al comunicarse con el servidor');
                    }
                });
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Tus favoritos</h1>
    <div class="productos-grid">
        <asp:Repeater ID="container" runat="server">
            <ItemTemplate>
                <div class="producto-card">
                    <asp:Image ID="imgProducto" runat="server" ImageUrl='<%# Eval("ImagenUrl") %>' AlternateText="Producto" />
                    <div class="producto-body">
                        <h5><%# Eval("Nombre") %></h5>
                        <p class="text-muted"><%# Eval("Color") %></p>
                        <p><strong><%# Eval("Precio", "{0:C}") %></strong></p>
                    </div>
                    <div class="producto-footer">
                        <button type="button" class="btn-remove-favorite" 
                                onclick="eliminarFavorito('<%# Eval("Lista_favorito_id") %>', this)">
                            <i class="fas fa-trash-alt"></i> Eliminar
                        </button>
                        <asp:HiddenField ID="hiddenListaFavoritoId" runat="server" Value='<%# Eval("Lista_favorito_id") %>' />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Label ID="lblEmpty" runat="server" CssClass="empty-favorites" 
                  Text="No tienes productos en tus favoritos" Visible="false"></asp:Label>
    </div>
</asp:Content>