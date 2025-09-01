<%@ Page Title="admin" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="proWeb.admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <!-- CSS y librerías externas -->
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
                    <asp:HyperLink CssClass="nav-link" NavigateUrl="mejoras.aspx" runat="server">
                            <i class="fas fa-tachometer-alt me-2"></i> DASHBOARD
                        </asp:HyperLink>
                        <asp:HyperLink CssClass="nav-link" NavigateUrl="producto.aspx" runat="server">
                        <i class="fas fa-box-open me-2"></i> PRODUCTOS
                    </asp:HyperLink>

                        <asp:HyperLink CssClass="nav-link active" NavigateUrl="#" runat="server">
                            <i class="fas fa-users me-2"></i> USUARIOS
                        </asp:HyperLink>
                        <asp:HyperLink CssClass="nav-link" NavigateUrl="proveedor.aspx" runat="server">
                            <i class="fas fa-truck me-2"></i> PROVEEDORES
                        </asp:HyperLink>
                        
                        <asp:HyperLink CssClass="nav-link" NavigateUrl="transaccion.aspx" runat="server">
                            <i class="fas fa-dollar-sign me-2"></i> TRANSACCION
                        </asp:HyperLink>
                    <asp:LinkButton ID="btnLogout" runat="server" CssClass="btn btn-danger mt-4">
                        <i class="fas fa-sign-out-alt"></i> Cerrar Sesión
                    </asp:LinkButton>
                </nav>
            </div>

            <!-- Contenido principal -->
            <div class="col-md-10 p-4">
                <div class="user-card d-flex align-items-center mb-4">
                   <asp:Image ID="imgUser" runat="server"  CssClass=""  ImageUrl="https://cdn-icons-png.flaticon.com/512/847/847969.png"  AlternateText="user" Width="100" Style="border-radius: 50%;" />

                    <div class="ms-3">
                        <h4>
                            <asp:Label ID="lblAdminName" runat="server" Text='<%# Session["AdminName"] ?? "Admin" %>'></asp:Label>
                            <i class="fa fa-star text-warning"></i>
                        </h4>
                        <p>
                            Administrador • 
                            <asp:HyperLink ID="lnkLearnMore" runat="server" NavigateUrl="#">Learn more</asp:HyperLink>
                        </p>
                        <p>
                            <i class="fa fa-coffee"></i> 
                            <asp:Label ID="lblExp" runat="server" Text="8,977 exp"></asp:Label>
                        </p>
                    </div>
                </div>

               <h4>Lista de Usuarios</h4>
                 <table id="tablaUsuarios" class="table table-bordered display">
                    <thead class="table-light">
                        <tr>
                            <th>Username</th>
                            <th>Nombre</th>
                            <th>Apellidos</th>
                            <th>Email</th>
                            <th>Teléfono</th>
                            <th>Localidad</th>
                            <th>Provincia</th>
                            <th>Admin</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        <% foreach (var u in new Library.CAD.CADUsuario().ListarUsuarios()) { %>
                        <tr>
                            <td><%= u.Username %></td>
                            <td><%= u.Nombre %></td>
                            <td><%= u.Apellidos %></td>
                            <td><%= u.Email %></td>
                            <td><%= u.Telefono %></td>
                            <td><%= u.Localidad %></td>
                            <td><%= u.Provincia %></td>
                            <td>
                                <span class="badge bg-<%= u.Admin ? "success" : "danger" %>"><%= u.Admin ? "Sí" : "No" %></span>
                            </td>
                            <td>
                                <button type="button" class="btn btn-success btn-sm" onclick="editarUsuarioSwal('<%= u.Username %>')">
                                    <i class="fa fa-edit"></i>
                                </button>
                               <button type="button" class="btn btn-danger btn-sm" onclick="eliminarUsuario('<%= u.Username %>')">

                                    <i class="fa fa-trash"></i>
                                </button>
                            </td>
                        </tr>
                        <% } %>
                    </tbody>
                </table>


            </div>
        </div>
    </div>

    <!-- Scripts -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.7.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <!-- DataTables Buttons -->
<link rel="stylesheet" href="https://cdn.datatables.net/buttons/2.4.1/css/buttons.dataTables.min.css" />

<script src="https://cdn.datatables.net/buttons/2.4.1/js/dataTables.buttons.min.js"></script>
<script src="https://cdn.datatables.net/buttons/2.4.1/js/buttons.html5.min.js"></script>
<script src="https://cdn.datatables.net/buttons/2.4.1/js/buttons.print.min.js"></script>
<script src="https://cdn.datatables.net/buttons/2.4.1/js/buttons.colVis.min.js"></script>

<!-- Dependencias de exportación -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            $('#tablaUsuarios').DataTable({
                language: {
                    url: "//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json"
                },
                pageLength: 5,
                lengthChange: false,
                info: false,
                dom: 'Bfrtip', // Agrega botones arriba
                buttons: [
                    'copy', 'excel', 'csv', 'pdf', 'print'
                ]
            });
        });

        function editarUsuarioSwal(username) {
            $.ajax({
                type: "POST",
                url: "admin.aspx/ObtenerDatosUsuario",
                data: JSON.stringify({ username: username }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    const u = response.d;
                    if (!u) return Swal.fire("Error", "No se encontró el usuario.", "error");

                    Swal.fire({
                        title: 'Editar Usuario',
                        html:
                            `<input id="swalNombre" class="swal2-input" placeholder="Nombre" value="${u.Nombre}" />` +
                            `<input id="swalApellidos" class="swal2-input" placeholder="Apellidos" value="${u.Apellidos}" />` +
                            `<input id="swalEmail" class="swal2-input" placeholder="Email" value="${u.Email}" />` +
                            `<input id="swalTelefono" class="swal2-input" placeholder="Teléfono" value="${u.Telefono}" />` +
                            `<input id="swalLocalidad" class="swal2-input" placeholder="Localidad" value="${u.Localidad}" />` +
                            `<input id="swalProvincia" class="swal2-input" placeholder="Provincia" value="${u.Provincia}" />` +
                            `<input id="swalCalle" class="swal2-input" placeholder="Calle" value="${u.Calle}" />` +
                            `<input id="swalCP" class="swal2-input" placeholder="Código Postal" value="${u.Codigo_Postal}" />` +
                            `<label><input type="checkbox" id="swalAdmin" ${u.Admin ? "checked" : ""} /> Es Admin</label>`,
                        confirmButtonText: 'Guardar',
                        focusConfirm: false,
                        preConfirm: () => {
                            return {
                                Username: u.Username,
                                Nombre: document.getElementById('swalNombre').value,
                                Apellidos: document.getElementById('swalApellidos').value,
                                Email: document.getElementById('swalEmail').value,
                                Telefono: document.getElementById('swalTelefono').value,
                                Localidad: document.getElementById('swalLocalidad').value,
                                Provincia: document.getElementById('swalProvincia').value,
                                Calle: document.getElementById('swalCalle').value,
                                Codigo_Postal: document.getElementById('swalCP').value,
                                Admin: document.getElementById('swalAdmin').checked
                            };
                        }
                    }).then((result) => {
                        if (result.isConfirmed) {
                            $.ajax({
                                type: "POST",
                                url: "admin.aspx/GuardarEdicionUsuario",
                                data: JSON.stringify({ usuario: result.value }),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (res) {
                                    if (res.d) {
                                        Swal.fire("Guardado", "Usuario actualizado correctamente", "success").then(() => {
                                            location.reload();
                                        });
                                    } else {
                                        Swal.fire("Error", "No se pudo actualizar el usuario", "error");
                                    }
                                },
                                error: function () {
                                    Swal.fire("Error", "No se pudo conectar con el servidor", "error");
                                }
                            });
                        }
                    });
                },
                error: function () {
                    Swal.fire("Error", "No se pudo obtener datos del usuario", "error");
                }
            });
        }

        function eliminarUsuario(username) {
            Swal.fire({
                title: '¿Eliminar usuario?',
                text: 'No podrás deshacer esta acción.',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Sí, eliminar',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed) {
                    PageMethods.EliminarUsuario(username, function (response) {
                        if (response === "OK") {
                            Swal.fire('Eliminado', 'Usuario eliminado correctamente.', 'success')
                                .then(() => location.reload());
                        } else {
                            Swal.fire('Error', response, 'error');  // Muestra el error devuelto
                        }
                    }, function (err) {
                        console.error('Error:', err);
                        Swal.fire('Error', 'Ocurrió un problema al comunicarse con el servidor.', 'error');
                    });
                }
            });
        }

        

        
        

    </script>
</asp:Content>