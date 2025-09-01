<%@ Page Title="Acerca de MovilGo" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="proWeb.About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet" />

    <style>
        .hero-banner {
            background: linear-gradient(to bottom, rgba(0,0,0,0.5), rgba(0,0,0,0.7)), 
                        url('assets/img/about-banner.jpg') center center/cover no-repeat;
            height: 220px;
            display: flex;
            align-items: center;
            justify-content: center;
            text-align: center;
        }

        .hero-banner h1 {
            font-size: 48px;
            font-weight: 700;
            color: white;
            text-shadow: 1px 1px 4px rgba(0, 0, 0, 0.5);
        }

        .about-section {
            padding: 60px 0;
        }

        .about-section h2,
        .about-section h3,
        .about-section h5 {
            font-weight: 700;
            color: #333; /* Negro suave */
        }

        .about-section h2 {
            font-size: 32px;
            margin-bottom: 20px;
        }

        .about-section h3 {
            font-size: 26px;
            margin-top: 40px;
            margin-bottom: 20px;
        }

        .about-section h5 {
            font-size: 20px;
            margin-top: 15px;
        }

        .about-section p {
            font-size: 18px;
            line-height: 1.8;
            color: #444;
        }

        .about-img {
            max-width: 100%;
            border-radius: 12px;
            box-shadow: 0 4px 16px rgba(0, 0, 0, 0.1);
        }

        .icon-feature {
            font-size: 2rem;
            margin-bottom: 10px;
        }

        .btn-cta {
            font-size: 18px;
            padding: 12px 30px;
            border-radius: 30px;
            font-weight: 600;
            
        }

        @media (max-width: 768px) {
            .hero-banner h1 {
                font-size: 32px;
            }

            .about-section h2 {
                font-size: 28px;
            }

            .about-section h3 {
                font-size: 22px;
            }
        }
    </style>

    <!-- Hero Banner -->
    <div class="hero-banner">
        <h1>Acerca de MovilGo</h1>
    </div>

    <!-- Main About Section -->
    <section class="about-section container">

        <!-- ¿Quiénes somos? -->
        <div class="row align-items-center">
            <div class="col-lg-6 mb-4">
                <img src="assets/img/about-us.jpg" alt="Sobre MovilGo" class="about-img" />
            </div>
            <div class="col-lg-6">
                <h2><i class="fas fa-users me-2"></i>¿Quiénes somos?</h2>
                <p>
                    En <strong>MovilGo</strong> ofrecemos una plataforma digital especializada en la compra y venta de dispositivos tecnológicos, tanto nuevos como de segunda mano. Nuestro objetivo es facilitar el acceso a tecnología confiable y accesible, conectando personas que desean renovar, vender o reutilizar sus productos electrónicos.
                </p>
                <p>
                    Nuestro catálogo incluye smartphones, tablets, laptops, smartwatches, auriculares y más. Garantizamos calidad gracias a un sistema de verificación de vendedores y revisión de productos.
                </p>
            </div>
        </div>

        <!-- Nuestra misión -->
        <div class="row mt-5 align-items-center">
            <div class="col-lg-6 order-lg-2 mb-4">
                <img src="assets/img/mission.jpg" alt="Nuestra misión" class="about-img" />
            </div>
            <div class="col-lg-6 order-lg-1">
                <h3><i class="fas fa-bullseye me-2"></i>Nuestra Misión</h3>
                <p>
                    Promover el acceso justo a la tecnología, contribuir a la sostenibilidad a través del consumo responsable, y brindar una experiencia segura y moderna a nuestros usuarios. MovilGo apuesta por una economía circular que beneficie a las personas y al planeta.
                </p>
                <p>
                    Además, fomentamos la compra informada mediante contenido educativo, comparativas, guías y novedades del sector tech.
                </p>
            </div>
        </div>

        <!-- Fortalezas destacadas -->
        <div class="row text-center mt-5">
            <div class="col-md-4 mb-4">
                <i class="fas fa-shield-alt text-primary icon-feature"></i>
                <h5>Seguridad</h5>
                <p>Plataforma con validación de identidad, verificación de productos y pagos seguros.</p>
            </div>
            <div class="col-md-4 mb-4">
                <i class="fas fa-recycle text-success icon-feature"></i>
                <h5>Sostenibilidad</h5>
                <p>Impulsamos el consumo responsable y la reutilización tecnológica con impacto positivo.</p>
            </div>
            <div class="col-md-4 mb-4">
                <i class="fas fa-tags text-warning icon-feature"></i>
                <h5>Accesibilidad</h5>
                <p>Precios justos en productos nuevos y reacondicionados, sin costes ocultos.</p>
            </div>
        </div>

        <!-- Modelo de negocio -->
        <div class="row mt-5">
            <div class="col">
                <h3><i class="fas fa-briefcase me-2"></i>Modelo de Negocio</h3>
                <p>
                    Nuestra plataforma es gratuita para los compradores. Los vendedores abonan una comisión por cada venta realizada, permitiendo mantener una experiencia sin anuncios invasivos y con soporte técnico real. También ofrecemos soluciones premium para comercios, tiendas y técnicos autorizados.
                </p>
            </div>
        </div>

        <!--  final -->
       <div class="text-center mt-5">
            <h3 class="mb-3">¿Listo para comenzar con MovilGo?</h3>
            <a href="login.aspx" class="btn btn-success btn-lg btn-cta">Crear mi cuenta</a>
        </div>

    </section>
</asp:Content>
