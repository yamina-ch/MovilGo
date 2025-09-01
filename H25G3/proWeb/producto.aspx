<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="producto.aspx.cs" Inherits="proWeb.producto" %>
<%@ Import Namespace="library.CAD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <asp:HiddenField ID="hfEditID" runat="server" />
    <asp:HiddenField ID="hfEditModelo" runat="server" />
    <asp:HiddenField ID="hfEditPrecio" runat="server" />
    <asp:HiddenField ID="hfEditMarca" runat="server" />
    <asp:HiddenField ID="hfEditStock" runat="server" />
    <asp:HiddenField ID="hfEditVendido" runat="server" />


    <!-- Estilos -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.min.css" />

    <style>
        .sidebar {
            background-color: #2c3e50;
            min-height: 100vh;
            color: white;
        }
        .sidebar .nav-link { color: #ccc; }
        .sidebar .nav-link:hover { background-color: #1abc9c; color: white; }
        .user-card img { width: 100px; border-radius: 50%; }
    </style>

    <div class="container-fluid">
        <div class="row">
            <!-- Sidebar -->
            <div class="col-md-2 sidebar p-3">
                <div class="mb-4">
                    <h5><%= Session["AdminName"] ?? "Admin" %></h5>
                </div>
                <nav class="nav flex-column">
                    <asp:HyperLink CssClass="nav-link" NavigateUrl="admin.aspx" runat="server"><i class="fas fa-tachometer-alt me-2"></i> DASHBOARD</asp:HyperLink>
                    <asp:HyperLink CssClass="nav-link active" NavigateUrl="producto.aspx" runat="server"><i class="fas fa-box-open me-2"></i> PRODUCTOS</asp:HyperLink>
                    <asp:HyperLink CssClass="nav-link" NavigateUrl="admin.aspx" runat="server"><i class="fas fa-users me-2"></i> USUARIOS</asp:HyperLink>
                    <asp:HyperLink CssClass="nav-link" NavigateUrl="proveedor.aspx" runat="server"><i class="fas fa-truck me-2"></i> PROVEEDORES</asp:HyperLink>
                  
                    <asp:HyperLink CssClass="nav-link" NavigateUrl="transaccion.aspx" runat="server"><i class="fas fa-dollar-sign me-2"></i> TRANSACCIÓN</asp:HyperLink>
                                        </asp:HyperLink>
                                       <asp:LinkButton ID="btnLogout" runat="server" CssClass="btn btn-danger mt-4" OnClick="btnLogout_Click">
    <i class="fas fa-sign-out-alt"></i> Cerrar Sesión
</asp:LinkButton>
                </nav>
            </div>

            <!-- Panel Productos -->
            <div class="col-md-10 p-4">
                <h3>Lista de Productos</h3>
             <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" 
    CssClass="table table-bordered" 
    ClientIDMode="Static" 
    UseAccessibleHeader="true"
    GridLines="None" 
    HeaderStyle-HorizontalAlign="Center">



    <Columns>
        <asp:BoundField DataField="Articulo_id" HeaderText="ID" />
        <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
        <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
        <asp:BoundField DataField="Marca_id" HeaderText="Marca" />
        <asp:BoundField DataField="Stock" HeaderText="Stock" />
        <asp:TemplateField HeaderText="Vendido">
            <ItemTemplate>
                <asp:Label ID="lblVendido" runat="server" Text='<%# (Eval("Vendido").ToString() == "1") ? "Sí" : "No" %>' CssClass='<%# (Eval("Vendido").ToString() == "1") ? "badge bg-danger" : "badge bg-success" %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
               <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-sm btn-warning me-1"
    OnClientClick='<%# "mostrarSweetEdit(" + Eval("Articulo_id") + "); return false;" %>'>
    ✏️
</asp:LinkButton>



                <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-sm btn-danger me-1"
    OnClientClick='<%# "confirmarEliminar(" + Eval("Articulo_id") + "); return false;" %>'>
    🗑️
</asp:LinkButton>

            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

               
            </div>


        </div>
    </div>

    <!-- Scripts -->
    <script src="https://code.jquery.com/jquery-3.7.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>
  
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.min.css" />
<script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>



<script>


    var articulos = [];

    // Esto es lo que genera el array en JS
    window.onload = function () {
        <% foreach (var a in new CADArticulo().ListarArticulos()) { %>
            articulos.push({
                id: <%= a.Articulo_id %>,
                modelo: "<%= a.Modelo.Replace("\"", "\\\"") %>",
                precio: "<%= a.Precio.ToString("0.00").Replace(",", ".") %>",
                marca: "<%= a.Marca_id %>",
                stock: <%= a.Stock %>,
                vendido: <%= a.Vendido %>
            });
        <% } %>

        console.log("✅ Lista de artículos cargada correctamente:", articulos);
    };

    function mostrarSweetEdit(id) {
        const articulo = articulos.find(a => a.id == id);
        if (!articulo) {
            alert("❌ Artículo no encontrado con ID: " + id);
            return;
        }

        console.log("✏️ Editando artículo: ", articulo);

        Swal.fire({
            title: 'Editar Artículo',
            html:
                `<input id="sw-modelo" class="swal2-input" value="${articulo.modelo}" placeholder="Modelo">
                <input id="sw-precio" class="swal2-input" value="${articulo.precio}" placeholder="Precio">
                <input id="sw-marca" class="swal2-input" value="${articulo.marca}" placeholder="Marca">
                <input id="sw-stock" class="swal2-input" value="${articulo.stock}" placeholder="Stock">
                <div class="form-check mt-2">
                    <input type="checkbox" id="sw-vendido" class="form-check-input" ${articulo.vendido == 1 ? "checked" : ""}>
                    <label for="sw-vendido" class="form-check-label">Vendido</label>
                </div>`,
            confirmButtonText: 'Guardar',
            showCancelButton: true,
            focusConfirm: false,
            preConfirm: () => {
                return {
                    id: id,
                    modelo: document.getElementById('sw-modelo').value,
                    precio: document.getElementById('sw-precio').value,
                    marca: document.getElementById('sw-marca').value,
                    stock: document.getElementById('sw-stock').value,
                    vendido: document.getElementById('sw-vendido').checked ? 1 : 0
                };
            }
        }).then((result) => {
            if (result.isConfirmed) {
                enviarArticuloEditado(result.value);
            }
        });
    }

    function enviarArticuloEditado(data) {
        document.getElementById('<%= hfEditID.ClientID %>').value = data.id;
        document.getElementById('<%= hfEditModelo.ClientID %>').value = data.modelo;
        document.getElementById('<%= hfEditPrecio.ClientID %>').value = data.precio;
        document.getElementById('<%= hfEditMarca.ClientID %>').value = data.marca;
        document.getElementById('<%= hfEditStock.ClientID %>').value = data.stock;
        document.getElementById('<%= hfEditVendido.ClientID %>').value = data.vendido;

        console.log("📤 Enviando datos editados:", data);

        __doPostBack('GuardarEdicion', '');
    }
     function confirmarEliminar(id) {
        Swal.fire({
            title: '¿Estás seguro?',
            text: "No podrás revertir esto",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#6c757d',
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                __doPostBack('EliminarArticulo', id);
            }
        });
    }

   

</script>

    <script>
        $(document).ready(function () {
            console.log("🔧 Reparando tabla para DataTables...");

            // DataTables necesita un <thead> válido. GridView no lo genera a veces.
            var $table = $('#gvProductos');
            if ($table.find('thead').length === 0) {
                $table.prepend('<thead></thead>');
                $table.find('thead').append($table.find('tr').first());
            }

            console.log("Inicializando DataTables...");
            $table.DataTable({
                language: {
                    url: "//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json"
                },
                columnDefs: [
                    { orderable: false, targets: -1 } // No ordenar última columna (acciones)
                ]
            });
        });
    </script>






</asp:Content>
