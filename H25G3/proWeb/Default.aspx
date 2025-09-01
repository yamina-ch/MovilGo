<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="proWeb.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Hero Section estilo Kelly */
        #hero {
            position: relative;
            overflow: hidden;
            height: 100vh;
            background-color: #f8f9fa;
        }

        #hero img {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            object-fit: cover;
            z-index: 1;
        }

        #hero .container {
            position: relative;
            z-index: 2;
        }

        #hero h2 {
            font-size: 48px;
            font-weight: 700;
            margin-bottom: 10px;
        }

        #hero p {
            font-size: 20px;
            margin-bottom: 20px;
        }

        .btn-get-started {
            display: inline-block;
            padding: 12px 30px;
            background: #0d6efd;
            color: #fff;
            border-radius: 50px;
            transition: 0.3s;
            font-weight: 600;
            text-decoration: none;
        }

        .btn-get-started:hover {
            background: #084298;
        }

        @media (max-width: 768px) {
            #hero h2 {
                font-size: 32px;
            }

            #hero p {
                font-size: 16px;
            }
        }

        .hover-card {
    transition: transform 0.3s ease, box-shadow 0.3s ease;
    cursor: pointer;
    background-color: #fff;
}

.hover-card:hover {
    transform: translateY(-12px);
    box-shadow: 0 12px 25px rgba(0, 0, 0, 0.12);
}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="hero" class="hero section">
        <img src="assets/img/hero-bg.jpg" alt="Fondo hero" />

    <div class="container text-center d-flex flex-column justify-content-center align-items-center h-100">
            <div class="row justify-content-center">
                <div class="col-lg-8">
                  
                    <a href="About.aspx" class="btn-get-started">Descúbrenos</a>
                </div>
            </div>
        </div>
    </section>
    <section id="servicios" class="valores-section py-5 text-center">
        <!-- Alerta de disponibilidad -->
<div id="alerta-disponibilidad" class="position-fixed top-0 end-0 m-4 p-3 rounded shadow-lg bg-white border d-none" style="z-index: 1050; width: 320px;">
    <div class="d-flex justify-content-between align-items-center">
        <div>
            <h5 class="fw-bold text-success mb-1">¡Ya disponible!</h5>
            <p class="mb-0 text-muted" id="mensaje-alerta">Un artículo nuevo ha sido publicado. ¡Échale un vistazo!</p>
        </div>
        <button type="button" class="btn-close ms-2" aria-label="Cerrar" onclick="cerrarAlerta()"></button>
    </div>
</div>
       <!-- NUEVOS Y PRÓXIMOS PRODUCTOS -->
  <section id="nuevos-productos" class="py-5 bg-light">
    <div class="container">
        <div class="card shadow-sm border-0 rounded-4 p-4">
            <h2 class="text-center fw-bold mb-4 text-dark">
                🆕 Productos Recién Llegados & Próximamente Disponibles
            </h2>
            <div class="scroll-wrapper px-2">
                <asp:Repeater ID="rptProductosNuevos" runat="server">
                   <ItemTemplate>
    <div class="card-producto" onclick="window.location.href='Catalogo.aspx';">
        <img src='<%# Eval("Imagen") %>' alt='<%# Eval("Modelo") %>' />
        <div class="card-producto-body">
            <h5><%# Eval("Modelo") %></h5>
            <p>
                <%# 
                    (Eval("FechaDisponible") != DBNull.Value && Convert.ToDateTime(Eval("FechaDisponible")) > DateTime.Today)
                    ? $"Disponible a partir del {Convert.ToDateTime(Eval("FechaDisponible")).ToString("yyyy-MM-dd")}"
                    : "¡Ya disponible!"
                %>
            </p>
        </div>
    </div>
</ItemTemplate>

                </asp:Repeater>
            </div>
        </div>
    </div>
</section>

<style>
    .scroll-wrapper {
        overflow-x: auto;
        display: flex;
        gap: 20px;
        scroll-behavior: smooth;
        padding-bottom: 10px;
    }

    #nuevos-productos .card-producto {
        min-width: 220px;
        max-width: 240px;
        flex: 0 0 auto;
        border: 1px solid #ddd;
        border-radius: 10px;
        overflow: hidden;
        box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05);
        background-color: #fff;
        transition: transform 0.3s ease;
        cursor: pointer;
    }

    #nuevos-productos .card-producto:hover {
        transform: translateY(-6px);
        box-shadow: 0 6px 20px rgba(0, 0, 0, 0.1);
    }

    #nuevos-productos .card-producto img {
        width: 100%;
        height: 180px;
        object-fit: cover;
    }

    #nuevos-productos .card-producto-body {
        padding: 15px;
        text-align: center;
    }

    #nuevos-productos .card-producto-body h5 {
        margin-bottom: 8px;
        font-weight: 600;
        font-size: 1.1rem;
        color: #212529;
    }

    #nuevos-productos .card-producto-body p {
        margin: 0;
        font-size: 0.9rem;
        color: #555;
    }
</style>




    <div class="container">
        <h2 class="mb-5">Nuestros Servicios</h2>
        <div class="row g-4 justify-content-center">
            <div class="col-md-4">
                <div class="card hover-card border rounded-3 p-4">
                    <i class="fas fa-shield-alt fa-3x text-primary mb-3"></i>
                    <h5 class="fw-bold">Seguridad</h5>
                    <p>Plataforma con validación de identidad, verificación de productos y pagos seguros.</p>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card hover-card border rounded-3 p-4">
                    <i class="fas fa-recycle fa-3x text-success mb-3"></i>
                    <h5 class="fw-bold">Sostenibilidad</h5>
                    <p>Impulsamos el consumo responsable y la reutilización tecnológica con impacto positivo.</p>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card hover-card border rounded-3 p-4">
                    <i class="fas fa-tags fa-3x text-warning mb-3"></i>
                    <h5 class="fw-bold">Accesibilidad</h5>
                    <p>Precios justos en productos nuevos y reacondicionados, sin costes ocultos.</p>
                </div>
            </div>
        </div>
    </div>
</section>
    <!---Contacto------------>

<section class="py-5 bg-light">
  <div class="container">
    <h2 class="text-center mb-5 fw-bold">Contáctanos</h2>

    <div class="row">
      <!-- Mapa -->
      <div class="col-lg-6 mb-4 mb-lg-0">
        <div class="h-100 w-100 rounded shadow">
          <iframe
            src="https://www.google.com/maps?q=Universidad de Alicante%20Alicante&output=embed"
            style="border:0; width:100%; height:100%; min-height: 350px;"
            allowfullscreen=""
            loading="lazy"
            referrerpolicy="no-referrer-when-downgrade">
          </iframe>
        </div>
      </div>

      <!-- Información de contacto -->
      <div class="col-lg-6 d-flex flex-column justify-content-center">
        <ul class="list-unstyled ps-3">
          <li class="mb-4 d-flex align-items-start">
            <i class="fas fa-map-marker-alt fa-2x text-primary me-3"></i>
            <div>
              <strong>Dirección:</strong><br />
               Escuela Politécnica Superior<br />
              Universidad de Alicante<br />
                 03690 San Vicente del Raspeig
            </div>
          </li>
          <li class="mb-4 d-flex align-items-start">
            <i class="fas fa-phone fa-2x text-primary me-3"></i>
            <div>
              <strong>Teléfono:</strong><br />
              Tel. +34 96 590 3400<br />
              Fax 96 590 3464
            </div>
          </li>
          <li class="mb-4 d-flex align-items-start">
            <i class="fas fa-envelope fa-2x text-primary me-3"></i>
            <div>
              <strong>Email:</strong><br />
              movilgotienda@gmail.com
            </div>
          </li>
        </ul>
      </div>
    </div>
  </div>
</section>


<!-- Testimonial Section -->
<!-- SECCIÓN DE TESTIMONIOS -->
<section class="testimonial-section text-center py-5">
  <div class="container">
    <h2 class="mb-5 fw-bold">TESTIMONIOS</h2>

    <div id="testimonialCarousel" class="carousel slide" data-bs-ride="carousel">
      <div class="carousel-inner">

        <!-- TESTIMONIO 1 -->
        <div class="carousel-item active">
          <div class="testimonial-box mx-auto" style="max-width: 800px;">
            <img src="assets/img/client3.png" alt="Cliente 3" class="rounded-circle mb-3" style="width: 150px; height: 150px; object-fit: cover;">

            <h4 class="text-success mb-3">Laura Gómez</h4>
            <p class="mb-4 px-3">
              La atención en MovilGo fue excelente. ¡Recomiendo totalmente su profesionalidad!
            </p>
            <i class="fa fa-quote-left mb-4" style="font-size: 28px;"></i>
          </div>
        </div>

        <!-- TESTIMONIO 2 -->
        <div class="carousel-item">
          <div class="testimonial-box mx-auto" style="max-width: 800px;">
            <img src="assets/img/client1.png" alt="Cliente 1" class="rounded-circle mb-3" style="width: 150px; height: 150px; object-fit: cover;">

            <h4 class="text-success mb-3">Carlos Méndez</h4>
            <p class="mb-4 px-3">
              Me sorprendió la rapidez y calidad del servicio y el equipo  muy atento. ¡Una experiencia muy positiva!
            </p>
            <i class="fa fa-quote-left mb-4" style="font-size: 28px;"></i>
          </div>
        </div>

        <!-- TESTIMONIO 3 -->
        <div class="carousel-item">
          <div class="testimonial-box mx-auto" style="max-width: 800px;">
            <img src="assets/img/client2.png" alt="Cliente 2" class="rounded-circle mb-3" style="width: 150px; height: 150px; object-fit: cover;">

            <h4 class="text-success mb-3">Pedro Ruiz Garcia</h4>
            <p class="mb-4 px-3">
              Estoy muy agradecido con el personal de MovilGo. Me trataron con mucha empatía y profesionalismo. Sin duda volveré a comprar desde vuestra tienda.
            </p>
            <i class="fa fa-quote-left mb-4" style="font-size: 28px;"></i>
          </div>
        </div>

      </div>

      <!-- FLECHAS DE NAVEGACIÓN -->
      <div class="d-flex justify-content-center gap-3 mt-4">
        <button class="arrow-btn" data-bs-target="#testimonialCarousel" data-bs-slide="prev">
          <i class="fas fa-arrow-left"></i>
        </button>
        <button class="arrow-btn" data-bs-target="#testimonialCarousel" data-bs-slide="next">
          <i class="fas fa-arrow-right"></i>
        </button>
      </div>
    </div>
  </div>
</section>
    <script>
        function mostrarAlertaDisponibilidad(mensaje) {
            const alerta = document.getElementById("alerta-disponibilidad");
            const mensajeTexto = document.getElementById("mensaje-alerta");

            if (mensajeTexto && mensaje) {
                mensajeTexto.innerText = mensaje;
            }

            alerta.classList.remove("d-none");
            alerta.classList.add("animate__animated", "animate__fadeInRight");

            // Ocultar automáticamente a los 8 segundos
            setTimeout(() => {
                cerrarAlerta();
            }, 8000);
        }

        function cerrarAlerta() {
            const alerta = document.getElementById("alerta-disponibilidad");
            alerta.classList.add("animate__fadeOutRight");
            setTimeout(() => {
                alerta.classList.add("d-none");
                alerta.classList.remove("animate__animated", "animate__fadeInRight", "animate__fadeOutRight");
            }, 500);
        }

        // Simulación: mostrar si hay artículos disponibles/próximos (lógica real vendría del servidor)
        window.addEventListener("DOMContentLoaded", () => {
            const tieneArticulosDisponibles = true; // Esta lógica deberías traerla desde el backend (e.g. desde BD)
            if (tieneArticulosDisponibles) {
                mostrarAlertaDisponibilidad("¡Nuevo producto disponible desde hoy! 🚀");
            }
        });
    </script>

</asp:Content>
