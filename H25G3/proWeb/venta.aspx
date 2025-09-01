<%@ Page Title="Venta" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="venta.aspx.cs" Inherits="proWeb.venta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        body {
            background-color: #f9f9f9;
        }

        .encabezado-venta {
            text-align: center;
            margin-top: 40px;
            margin-bottom: 20px;
        }

        .encabezado-venta i {
            font-size: 60px;
            color: #28a745;
        }

        .encabezado-venta h1 {
            margin-top: 10px;
            font-weight: 600;
        }

        .encabezado-venta p {
            color: #6c757d;
        }

        .contenedor-venta {
            background-color: white;
            border-radius: 15px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.08);
            padding: 40px;
            margin-bottom: 60px;
        }

        .progress-container {
            position: relative;
            width: 100%;
            height: 3px;
            background-color: #e0e0e0;
            margin: 40px 0 30px 0;
        }

        .progress-bar {
            position: absolute;
            height: 100%;
            background-color: #28a745;
            transition: width 0.4s;
        }

        .step {
            position: absolute;
            top: -14px;
            width: 28px;
            height: 28px;
            background-color: #e0e0e0;
            border: 2px solid #28a745;
            border-radius: 50%;
            text-align: center;
            line-height: 24px;
            color: #28a745;
            font-weight: bold;
        }

        .step.active {
            background-color: #28a745;
            color: white;
        }

        .btn-success {
            background-color: #28a745;
            border-color: #28a745;
        }

        .btn-success:hover {
            background-color: #218838;
        }
         
    .error-box {
        background-color: #ffe6e6;
        color: #cc0000;
        border: 1px solid #cc0000;
        padding: 12px;
        margin-bottom: 15px;
        border-radius: 5px;
        font-weight: bold;
    }


    </style>

    <div class="container">
        <div class="encabezado-venta">
            <i class="fas fa-box-open"></i>
            <h1>Publicar un Producto</h1>
            <p class="lead">Completa los siguientes pasos para anunciar tu producto en MOVILGO.</p>
        </div>

        <div class="contenedor-venta">
            <!-- Barra de pasos -->
            <div class="progress-container">
                <asp:Panel ID="progressBarContainer" runat="server" CssClass="progress-bar" Style="width: 25%;"></asp:Panel>
                <asp:Label ID="step1" runat="server" CssClass="step active" Style="left: 0%;">1</asp:Label>
                <asp:Label ID="step2" runat="server" CssClass="step" Style="left: 33%;">2</asp:Label>
                <asp:Label ID="step3" runat="server" CssClass="step" Style="left: 66%;">3</asp:Label>
                <asp:Label ID="step4" runat="server" CssClass="step" Style="left: 100%;">4</asp:Label>
            </div>

            <!-- MultiView -->
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

               
             <!-- View 1 -->
<asp:View ID="View1" runat="server">
    <div class="form-group">
        <label>Modelo</label>
        <asp:TextBox ID="txtModelo" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvModelo" runat="server" ControlToValidate="txtModelo"
            ErrorMessage="El modelo es obligatorio" CssClass="text-danger" Display="Dynamic" />
    </div>

    <div class="form-group">
        <label>Marca</label>
        <asp:DropDownList ID="ddlMarca" runat="server" AutoPostBack="true"
                          OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged"
                          CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvMarca" runat="server" ControlToValidate="ddlMarca"
            InitialValue="" ErrorMessage="Seleccione una marca" CssClass="text-danger" Display="Dynamic" />

        <asp:TextBox ID="txtNuevaMarca" runat="server" CssClass="form-control mt-2"
                     Placeholder="Introduce nueva marca" Visible="false" />
        <asp:CustomValidator ID="cvNuevaMarca" runat="server" ControlToValidate="txtNuevaMarca"
            ErrorMessage="Debe introducir una nueva marca"
            ClientValidationFunction="validateNuevaMarca"
            Display="Dynamic" CssClass="text-danger" Enabled="false" />
    </div>

    <div class="form-group">
        <label>Categoría</label>
        <asp:DropDownList ID="ddlCategoria" runat="server" AutoPostBack="true"
                          OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged"
                          CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvCategoria" runat="server" ControlToValidate="ddlCategoria"
            InitialValue="" ErrorMessage="Seleccione una categoría" CssClass="text-danger" Display="Dynamic" />

        <asp:TextBox ID="txtNuevaCategoria" runat="server" CssClass="form-control mt-2"
                     Placeholder="Introduce nueva categoría" Visible="false" />
        <asp:CustomValidator ID="cvNuevaCategoria" runat="server" ControlToValidate="txtNuevaCategoria"
            ErrorMessage="Debe introducir una nueva categoría"
            ClientValidationFunction="validateNuevaCategoria"
            Display="Dynamic" CssClass="text-danger" Enabled="false" />
    </div>

    <div class="form-group">
        <label>Sistema Operativo</label>
        <asp:TextBox ID="txtSistemaOperativo" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvSO" runat="server" ControlToValidate="txtSistemaOperativo"
            ErrorMessage="El sistema operativo es obligatorio" CssClass="text-danger" Display="Dynamic" />
    </div>

    <div class="form-group">
        <label>Año</label>
        

        <asp:TextBox ID="txtAnyo" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvAnyo" runat="server" ControlToValidate="txtAnyo"
            ErrorMessage="El año es obligatorio" CssClass="text-danger" Display="Dynamic" />
        <asp:RegularExpressionValidator ID="revAnyo" runat="server" ControlToValidate="txtAnyo"
            ValidationExpression="^\d{4}$" ErrorMessage="Debe ingresar un año válido de 4 dígitos"
            CssClass="text-danger" Display="Dynamic" />
    </div>

    <asp:Button ID="Next1" runat="server" Text="Siguiente" CssClass="btn btn-success" OnClick="Next1_Click" />
    <asp:Literal ID="litErrores" runat="server" EnableViewState="false" />
</asp:View>


                <!-- View 2 -->
                <asp:View ID="View2" runat="server">
    <div class="form-group">
        <label>Color</label>
        <asp:TextBox ID="txtColor" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvColor" runat="server"
            ControlToValidate="txtColor"
            ErrorMessage="El color es obligatorio."
            CssClass="text-danger" Display="Dynamic" />
    </div>

    <div class="form-group">
        <label>Memoria</label>
        <asp:TextBox ID="txtMemoria" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvMemoria" runat="server"
            ControlToValidate="txtMemoria"
            ErrorMessage="La memoria es obligatoria."
            CssClass="text-danger" Display="Dynamic" />
    </div>

    <div class="form-group">
        <label>Batería</label>
        <asp:TextBox ID="txtBateria" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvBateria" runat="server"
            ControlToValidate="txtBateria"
            ErrorMessage="La batería es obligatoria."
            CssClass="text-danger" Display="Dynamic" />
    </div>

    <div class="form-group">
        <label>Estado</label>
        <asp:TextBox ID="txtEstado" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvEstado" runat="server"
            ControlToValidate="txtEstado"
            ErrorMessage="El estado es obligatorio."
            CssClass="text-danger" Display="Dynamic" />
    </div>

    <asp:Button ID="Back1" runat="server" Text="Atrás" CssClass="btn btn-secondary" OnClick="Back1_Click" CausesValidation="false" />
    <asp:Button ID="Next2" runat="server" Text="Siguiente" CssClass="btn btn-success" OnClick="Next2_Click" />
</asp:View>

                <!-- View 3 -->
              <asp:View ID="View3" runat="server">
    <div class="form-group">
        <label>Precio Original (€)</label>
        <asp:TextBox ID="txtPrecioOriginal" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvPrecioOriginal" runat="server"
            ControlToValidate="txtPrecioOriginal"
            ErrorMessage="El precio original es obligatorio."
            CssClass="text-danger" Display="Dynamic" />
        <asp:RegularExpressionValidator ID="revPrecioOriginal" runat="server"
            ControlToValidate="txtPrecioOriginal"
            ValidationExpression="^\d+(\.\d{1,2})?$"
            ErrorMessage="Formato de precio no válido."
            CssClass="text-danger" Display="Dynamic" />
    </div>

    <div class="form-group">
        <label>Precio Publicado (€)</label>
        <asp:TextBox ID="txtPrecioPublicado" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvPrecioPublicado" runat="server"
            ControlToValidate="txtPrecioPublicado"
            ErrorMessage="El precio publicado es obligatorio."
            CssClass="text-danger" Display="Dynamic" />
        <asp:RegularExpressionValidator ID="revPrecioPublicado" runat="server"
            ControlToValidate="txtPrecioPublicado"
            ValidationExpression="^\d+(\.\d{1,2})?$"
            ErrorMessage="Formato de precio no válido."
            CssClass="text-danger" Display="Dynamic" />
    </div>

    <div class="form-group">
        <label>Descripción</label>
        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server"
            ControlToValidate="txtDescripcion"
            ErrorMessage="La descripción es obligatoria."
            CssClass="text-danger" Display="Dynamic" />
    </div>

    <div class="form-group">
        <label>Imagen del producto</label>
        <asp:FileUpload ID="fileImagen" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvFileImagen" runat="server"
            ControlToValidate="fileImagen"
            ErrorMessage="La imagen del producto es obligatoria."
            CssClass="text-danger" Display="Dynamic" />
    </div>

    <asp:Button ID="Back2" runat="server" Text="Atrás" CssClass="btn btn-secondary" OnClick="Back2_Click" CausesValidation="false" />
    <asp:Button ID="Next3" runat="server" Text="Siguiente" CssClass="btn btn-success" OnClick="Next3_Click" />
</asp:View>



                <!-- View 4 -->
              <asp:View ID="View4" runat="server">
    <div class="form-group">
        <label>Motivo de la Venta</label>
        <asp:TextBox ID="txtMotivo" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvMotivo" runat="server"
            ControlToValidate="txtMotivo"
            ErrorMessage="El motivo de la venta es obligatorio."
            CssClass="text-danger" Display="Dynamic" />
    </div>

  <div class="form-group">
    <label>Disponible a partir de</label>
    <asp:TextBox ID="txtDisponibleHasta" runat="server" TextMode="Date" CssClass="form-control" />
    
    <asp:RequiredFieldValidator ID="rfvDisponibleHasta" runat="server"
        ControlToValidate="txtDisponibleHasta"
        ErrorMessage="Debe indicar una fecha de disponibilidad."
        CssClass="text-danger" Display="Dynamic" />
        
    <asp:RegularExpressionValidator ID="revDisponibleHasta" runat="server"
        ControlToValidate="txtDisponibleHasta"
        ValidationExpression="^\d{4}-\d{2}-\d{2}$"
        ErrorMessage="Formato de fecha no válido (YYYY-MM-DD)."
        CssClass="text-danger" Display="Dynamic" />
</div>

<script type="text/javascript">
    window.addEventListener("DOMContentLoaded", function () {
        const inputFecha = document.getElementById("<%= txtDisponibleHasta.ClientID %>");
        if (inputFecha) {
            const hoy = new Date().toISOString().split("T")[0];
            inputFecha.setAttribute("min", hoy); // evita seleccionar fechas anteriores
        }
    });
</script>


    <div class="form-group" id="divStock" runat="server">
        <label>Cantidad en stock</label>
        <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" />
        <asp:RegularExpressionValidator ID="revStock" runat="server"
            ControlToValidate="txtStock"
            ValidationExpression="^\d+$"
            ErrorMessage="El stock debe ser un número entero."
            CssClass="text-danger" Display="Dynamic" />
    </div>
<div class="form-group" id="divProveedor" runat="server">
    <label>Proveedor</label>
   <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-control">
</asp:DropDownList>
<asp:RequiredFieldValidator ID="rfvProveedor" runat="server"
    ControlToValidate="ddlProveedor"
    InitialValue=""
    ErrorMessage="Debe seleccionar un proveedor."
    CssClass="text-danger" Display="Dynamic" />
    </div>

    <asp:Button ID="Back3" runat="server" Text="Atrás" CssClass="btn btn-secondary" OnClick="Back3_Click" CausesValidation="false" />
    <asp:Button ID="btnPublicar" runat="server" Text="Publicar Anuncio" CssClass="btn btn-success" OnClick="btnPublicar_Click" />
</asp:View>


            </asp:MultiView>
        </div>
    </div>
    <script type="text/javascript">
    function validateNuevaMarca(sender, args) {
        var ddl = document.getElementById('<%= ddlMarca.ClientID %>');
        var txt = document.getElementById('<%= txtNuevaMarca.ClientID %>');
        args.IsValid = !(ddl.value === "Otro" && txt.value.trim() === "");
    }

    function validateNuevaCategoria(sender, args) {
        var ddl = document.getElementById('<%= ddlCategoria.ClientID %>');
        var txt = document.getElementById('<%= txtNuevaCategoria.ClientID %>');
        args.IsValid = !(ddl.value === "Otro" && txt.value.trim() === "");
    }
    </script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>


</asp:Content>

