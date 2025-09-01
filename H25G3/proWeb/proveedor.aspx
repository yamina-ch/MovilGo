<%@ Page Title="Proveedores" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="proveedor.aspx.cs" Inherits="proWeb.proveedor" %>

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
        
        /* Estilos específicos para proveedores */
        .proveedores-table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 1rem;
        }
        
        .proveedores-table th {
            background-color: #f8f9fa;
            color: #2c3e50;
            padding: 12px 15px;
            text-align: left;
            font-weight: 500;
        }
        
        .proveedores-table td {
            padding: 12px 15px;
            border-bottom: 1px solid #e0e0e0;
        }
        
        .proveedores-table tr:hover {
            background-color: #f5f5f5;
        }
        
        .action-btn {
            padding: 5px 10px;
            border: none;
            border-radius: 3px;
            cursor: pointer;
            margin-right: 5px;
            font-size: 0.85rem;
            transition: opacity 0.3s;
            text-decoration: none;
            display: inline-block;
        }
        
        .action-btn:hover {
            opacity: 0.9;
        }
        
        .view-btn {
            background-color: #3498db;
            color: white;
        }
        
        .edit-btn {
            background-color: #f39c12;
            color: white;
        }
        
        .delete-btn {
            background-color: #e74c3c;
            color: white;
        }

        .products-btn {
            background-color: #9b59b6;
            color: white;
        }
        
        .add-btn {
            background-color: #2ecc71;
            color: white;
            padding: 8px 15px;
            margin-bottom: 1rem;
            border: none;
            border-radius: 3px;
            cursor: pointer;
            font-size: 1rem;
        }
        
        /* Search and Filter */
        .search-container {
            display: flex;
            margin-bottom: 1.5rem;
            gap: 10px;
            align-items: center;
        }
        
        .search-input {
            flex: 1;
            padding: 10px;
            border: 1px solid #e0e0e0;
            border-radius: 3px;
            font-size: 1rem;
        }
        
        .search-btn {
            background-color: #d1d1d1;
            color: #2c3e50;
            border: none;
            padding: 10px 20px;
            border-radius: 3px;
            cursor: pointer;
            font-size: 1rem;
        }
        
        .search-btn:hover {
            background-color: #34495e;
            color: white;
        }
        
        /* Status indicators */
        .status-active {
            color: #2ecc71;
            font-weight: 500;
        }
        
        .status-inactive {
            color: #e74c3c;
            font-weight: 500;
        }
        
        /* Empty state */
        .empty-state {
            text-align: center;
            padding: 2rem;
            color: #7f8c8d;
            border: 1px dashed #e0e0e0;
            border-radius: 5px;
            margin-top: 1rem;
        }
        
        /* Form styles */
        .form-container {
            background-color: white;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
            margin-bottom: 20px;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-group label {
            display: block;
            margin-bottom: 5px;
            font-weight: 500;
        }

        .form-control {
            width: 100%;
            padding: 8px 12px;
            border: 1px solid #e0e0e0;
            border-radius: 3px;
            font-size: 1rem;
        }

        .btn-submit {
            background-color: #2ecc71;
            color: white;
            padding: 8px 15px;
            border: none;
            border-radius: 3px;
            cursor: pointer;
            font-size: 1rem;
        }

        .btn-cancel {
            background-color: #e74c3c;
            color: white;
            padding: 8px 15px;
            border: none;
            border-radius: 3px;
            cursor: pointer;
            font-size: 1rem;
            margin-left: 10px;
        }

        .validation-error {
            color: #e74c3c;
            font-size: 0.85rem;
            margin-top: 5px;
            display: block;
        }
        
        /* Responsive adjustments */
        @media (max-width: 768px) {
            .search-container {
                flex-direction: column;
            }
            
            .proveedores-table {
                display: block;
                overflow-x: auto;
            }
            
            .action-btn {
                display: block;
                margin-bottom: 5px;
                width: 100%;
            }
        }
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
                    <asp:HyperLink CssClass="nav-link" NavigateUrl="admin.aspx" runat="server">
                        <i class="fas fa-users me-2"></i> USUARIOS
                    </asp:HyperLink>
                    <asp:HyperLink CssClass="nav-link active" NavigateUrl="proveedor.aspx" runat="server">
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

                    <div class="ms-3">
                    </div>
                </div>

                
                <!-- Sección del formulario (oculta inicialmente) -->
                <div id="formProveedor" class="form-container" runat="server">
                    <h2 runat="server" id="formTitle">Nuevo Proveedor</h2>
                    
                    <asp:HiddenField ID="hdnProveedorId" runat="server" />
                    
                    <div class="form-group">
                        <label for="txtNombre">Nombre:</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                            ControlToValidate="txtNombre" ErrorMessage="El nombre es requerido"
                            CssClass="validation-error" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    
                    <div class="form-group">
                        <label for="txtCIF">CIF:</label>
                        <asp:TextBox ID="txtCIF" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCIF" runat="server" 
                            ControlToValidate="txtCIF" ErrorMessage="El CIF es requerido"
                            CssClass="validation-error" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    
                    <div class="form-group">
                        <label for="txtDireccion">Dirección:</label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    
                    <div class="form-group">
                        <label for="txtTelefono">Teléfono:</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    
                    <div class="form-group">
                        <label for="txtEmail">Email:</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server"
                            ControlToValidate="txtEmail" ErrorMessage="Formato de email inválido"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            CssClass="validation-error" Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                    
                    <div class="form-group">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn-submit" 
                            OnClick="btnGuardar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn-cancel" 
                            OnClick="btnCancelar_Click" CausesValidation="false" />
                    </div>
                </div>

                <!-- Sección de productos del proveedor seleccionado -->
                <div id="productosProveedorSection" runat="server" visible="false" class="form-container">
                    <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 20px;">
                        <h2>Productos del Proveedor: <asp:Label ID="lblProveedorNombre" runat="server" /></h2>
                        <asp:Button ID="btnVolver" runat="server" Text="Volver" 
                            CssClass="btn-cancel" OnClick="btnVolver_Click" />
                    </div>

                    <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="false" CssClass="proveedores-table">
                        <Columns>
                            <asp:BoundField DataField="articulo_id" HeaderText="ID" />
                            <asp:BoundField DataField="modelo" HeaderText="Modelo" />
                            <asp:BoundField DataField="marca_id" HeaderText="Marca ID" />
                            <asp:BoundField DataField="precio" HeaderText="Precio" DataFormatString="{0:C}" />
                            <asp:BoundField DataField="stock" HeaderText="Stock" />
                            <asp:BoundField DataField="estado" HeaderText="Estado" />
                        </Columns>
                    </asp:GridView>
                </div>

                <!-- Sección del listado -->
                <section class="content-section" id="listSection" runat="server">
                    <h4>Listado de Proveedores</h4>
                    
                    <!-- Search and Filter -->
                    <div class="search-container">
                        <asp:TextBox ID="txtBusqueda" runat="server" CssClass="search-input" 
                            placeholder="Buscar por nombre, CIF, teléfono o email..."></asp:TextBox>
        
                        <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="search-input">
                            <asp:ListItem Text="Todos los proveedores" Value="all" Selected="True" />
                            <asp:ListItem Text="Con dirección registrada" Value="with_address" />
                            <asp:ListItem Text="Sin dirección registrada" Value="without_address" />
                        </asp:DropDownList>
        
                        <asp:DropDownList ID="ddlOrden" runat="server" CssClass="search-input">
                            <asp:ListItem Text="Nombre (A-Z)" Value="nombre_asc" Selected="True" />
                            <asp:ListItem Text="Nombre (Z-A)" Value="nombre_desc" />
                            <asp:ListItem Text="CIF (Ascendente)" Value="cif_asc" />
                            <asp:ListItem Text="CIF (Descendente)" Value="cif_desc" />
                        </asp:DropDownList>

                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                        CssClass="search-btn" OnClick="btnBuscar_Click" />
        
                    </div>
                    
                    <asp:Button ID="btnAddNew" runat="server" Text="+ Añadir Proveedor" 
                        CssClass="add-btn" OnClick="btnAddNew_Click" />
                    
                    <table class="proveedores-table">
                        <thead>
                            <tr>
                                <th>Nombre</th>
                                <th>CIF</th>
                                <th>Dirección</th>
                                <th>Teléfono</th>
                                <th>Email</th>
                                <th style="width: 220px;">Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptProveedores" runat="server" OnItemDataBound="rptProveedores_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("nombre") %></td>
                                        <td><%# Eval("cif") %></td>
                                        <td><%# Eval("direccion") %></td>
                                        <td><%# Eval("telefono") %></td>
                                        <td><%# Eval("email") %></td>
                                        <td>
                                            <div style="display: flex; gap: 5px;">
                                                <asp:LinkButton ID="btnView" runat="server" CssClass="action-btn view-btn" 
                                                    CommandArgument='<%# Eval("proveedor_id") %>' OnCommand="HandleAction" CommandName="View">
                                                    <i class="fas fa-eye"></i> Ver
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="action-btn edit-btn" 
                                                    CommandArgument='<%# Eval("proveedor_id") %>' OnCommand="HandleAction" CommandName="Edit">
                                                    <i class="fas fa-edit"></i> Editar
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="action-btn delete-btn" 
                                                    CommandArgument='<%# Eval("proveedor_id") %>' OnCommand="HandleAction" CommandName="Delete"
                                                    OnClientClick="return confirm('¿Está seguro de eliminar este proveedor?');">
                                                    <i class="fas fa-trash"></i> Eliminar
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnViewProducts" runat="server" CssClass="action-btn products-btn" 
                                                    CommandArgument='<%# Eval("proveedor_id") %>' OnCommand="HandleAction" CommandName="ViewProducts">
                                                    <i class="fas fa-boxes"></i> Productos
                                                </asp:LinkButton>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr id="trEmpty" runat="server" visible='<%# ((Repeater)Container.NamingContainer).Items.Count == 0 %>'>
                                        <td colspan="6">
                                            <div class="empty-state">
                                                <i class="fas fa-box-open fa-2x" style="margin-bottom: 1rem;"></i>
                                                <h3>No se encontraron proveedores</h3>
                                                <p>Utilice el botón "Añadir Proveedor" para comenzar</p>
                                            </div>
                                        </td>
                                    </tr>
                                </FooterTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </section>
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
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            // Inicializar DataTables SOLO en tablas que no sean gvProductos
            $('.proveedores-table').not('#<%= gvProductos.ClientID %>').DataTable({
                language: {
                    url: "//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json"
                },
                pageLength: 5,
                lengthChange: false,
                info: false,
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'excel', 'csv', 'pdf', 'print'
                ]
            });
        });
    </script>
</asp:Content>