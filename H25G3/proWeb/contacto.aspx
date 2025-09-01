<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="contacto.aspx.cs" Inherits="proWeb.contacto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet" />
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet" />

    <style>
        body {
            background-color: #f8f9fa;
        }

        .contact-container {
            background: #fff;
            padding: 3rem;
            margin: 2rem auto;
            max-width: 1200px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.05);
            display: flex;
            flex-wrap: wrap;
            gap: 2rem;
        }

        .map iframe {
            width: 100%;
            height: 100%;
            min-height: 400px;
            border: none;
        }

        .contact-form {
            flex: 1 1 45%;
        }

        .contact-form h1 {
            font-size: 2rem;
            color: #0056b3;
            margin-bottom: 1rem;
        }

        .contact-form h2 {
            font-size: 1rem;
            color: #555;
            margin-bottom: 2rem;
        }

        .contact-form .form-group {
            margin-bottom: 1rem;
        }

        .contact-form input,
        .contact-form textarea {
            width: 100%;
            padding: 0.75rem;
            border: 1px solid #ced4da;
            border-radius: 4px;
        }

        .contact-form .btn-send {
            background: #0056b3;
            color: #fff;
            border: none;
            padding: 0.75rem 2rem;
            font-weight: bold;
            border-radius: 4px;
            cursor: pointer;
        }

        .contact-info {
            display: flex;
            justify-content: space-between;
            margin-top: 2rem;
        }

        .info-box {
            flex: 1;
            text-align: center;
            background: #f1f1f1;
            padding: 1rem;
            border-radius: 8px;
            margin: 0 0.5rem;
        }

        .info-box i {
            font-size: 1.5rem;
            margin-bottom: 0.5rem;
            color: #0056b3;
        }

        @media screen and (max-width: 768px) {
            .contact-container {
                flex-direction: column;
            }

            .contact-info {
                flex-direction: column;
                gap: 1rem;
            }

            .info-box {
                margin: 0;
            }
        }
    </style>

    <div class="contact-container">
        <!-- Mapa -->
        <div class="map" style="flex: 1 1 45%;">
            <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3105.0791642771266!2d-0.5112375!3d38.3868132!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0xd6236ba2a07b50f%3A0x161c6e192605005b!2sEdificio%2016%20-%20Escuela%20Politecnica%20Superior%201%2C%2003690%20San%20Vicente%20del%20Raspeig%2C%20Alicante!5e0!3m2!1ses!2ses!4v1621321137058!5m2!1ses!2ses" allowfullscreen></iframe>
        </div>

        <!-- Formulario -->
        <div class="contact-form">
            <h1>Contáctanos</h1>
            <h2>Estamos aquí para ayudarte.</h2>

            <div class="form-group">
                <label for="txtFrom">De:</label>
                <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="txtTo">Para:</label>
                <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="txtSubject">Asunto:</label>
                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="txtMessage">Mensaje:</label>
                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="5" />
            </div>

            <div class="form-group">
                <label for="fileAttachment">Adjuntar Archivo:</label>
                <asp:FileUpload ID="fileAttachment" runat="server" CssClass="form-control-file" />
            </div>

            <asp:Button ID="btnSend" runat="server" Text="Enviar" CssClass="btn-send" OnClick="btnSend_Click" />
        </div>
    </div>

    <!-- Info contacto -->
    <div class="container my-5">
    <div class="row text-center justify-content-center">
        <!-- Email -->
        <div class="col-md-4 mb-4">
            <div class="card shadow-sm border-0 h-100">
                <div class="card-body">
                    <i class="fas fa-envelope fa-2x text-primary mb-3"></i>
                    <h4 class="card-title font-weight-bold">Email</h4>
                    <p class="card-text">movilgotienda@gmail.com</p>
                </div>
            </div>
        </div>

        <!-- Teléfono -->
        <div class="col-md-4 mb-4">
            <div class="card shadow-sm border-0 h-100">
                <div class="card-body">
                    <i class="fas fa-phone fa-2x text-primary mb-3"></i>
                    <h4 class="card-title font-weight-bold">Teléfono</h4>
                    <p class="card-text">Tel. 96 590 3400<br />Fax 96 590 3464</p>
                </div>
            </div>
        </div>

        <!-- Dirección -->
        <div class="col-md-4 mb-4">
            <div class="card shadow-sm border-0 h-100">
                <div class="card-body">
                    <i class="fas fa-map-marker-alt fa-2x text-primary mb-3"></i>
                    <h4 class="card-title font-weight-bold">Dirección</h4>
                    <p class="card-text">
                        Escuela Politécnica Superior<br />
                        Universidad de Alicante<br />
                        03690 San Vicente del Raspeig
                    </p>
                </div>
            </div>
        </div>
    </div>
</div>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

</asp:Content>
