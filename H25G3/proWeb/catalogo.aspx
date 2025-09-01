<%@ Page Title="Catálogo" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="catalogo.aspx.cs" Inherits="proWeb.catalogo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <style>
        .titulo-catalogo {
            background-color: #212529;
            color: white;
            padding: 40px 0;   
            text-align: center;
        }
        .titulo-catalogo h1 {
            font-size: 2.8em;
            font-weight: bold;
             color: white;
        }
        .titulo-catalogo p {
            font-size: 1.2em;
            color: #ccc;
        }
        .top-bar {
            max-width: 1200px;
            margin: 30px auto;
            display: flex;
            justify-content: space-between;
            align-items: center;
            flex-wrap: wrap;
            gap: 10px;
        }
        .top-bar .categorias-menu {
            display: flex;
            gap: 20px;
            flex-wrap: wrap;
        }
        .top-bar .categorias-menu a {
            text-decoration: none;
            color: #6c757d;
            font-weight: 500;
        }
        .top-bar .categorias-menu a:hover {
            color: #000;
            text-decoration: underline;
        }
        .top-bar .acciones {
            display: flex;
            gap: 10px;
        }
        .top-bar .acciones button {
            border: 1px solid #6c757d;
            background-color: white;
            color: #6c757d;
            padding: 6px 14px;
            border-radius: 5px;
        }
        .productos-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 25px;
            max-width: 1200px;
            margin: auto;
        }
        .producto-card {
            border: 1px solid #dee2e6;
            border-radius: 10px;
            overflow: hidden;
            background-color: white;
            display: flex;
            flex-direction: column;
        }
     .producto-card img {
    width: 100%;
    height: 200px;
    object-fit: contain; /* 👈 Mostrar la imagen completa sin recorte */
    object-position: center;
    background-color: white; /* Opcional: para rellenar el fondo */
    padding: 10px; /* Opcional: separa del borde */
}

        .producto-body {
            padding: 15px;
            text-align: center;
        }
        .producto-body h5 {
            font-weight: bold;
            font-size: 1.2em;
        }
        .producto-body p {
            color: #6c757d;
            margin: 0.5rem 0;
        }
        .producto-footer {
            padding: 10px;
            text-align: center;
            border-top: 1px solid #eee;
        }
        .producto-footer a {
            text-decoration: none;
            padding: 8px 15px;
            border: 1px solid #343a40;
            border-radius: 5px;
            color: #343a40;
            font-weight: 500;
        }
        .producto-footer a:hover {
            background-color: #343a40;
            color: white;
        }
        .badge-finalizado {
            position: absolute;
            top: 10px;
            left: 10px;
            background-color: red;
            color: white;
            font-weight: bold;
            padding: 5px 10px;
            border-radius: 5px;
            z-index: 10;
        }

        .paginacion-container {
      display: flex;
      justify-content: center;
      margin-top: 40px;
  }

  .pagination {
      display: flex;
      justify-content: center;
      align-items: center;
      gap: 6px;
  }

  .pagination button {
      background-color: #fff;
      border: 1px solid #343a40;
      color: #343a40;
      padding: 6px 12px;
      border-radius: 4px;
      cursor: pointer;
      font-weight: 500;
  }

  .pagination button.active {
      background-color: #343a40;
      color: #fff;
      font-weight: bold;
  }

  .pagination button:disabled {
      opacity: 0.5;
      cursor: not-allowed;
  }
    </style>

    <!-- Encabezado -->
    <div class="titulo-catalogo">
        <h1>MOVILGO SHOP</h1>
        <p>Explora nuestros productos exclusivos</p>
    </div>

    <!-- Barra de filtros y búsqueda -->
    <div class="top-bar">
        <div class="categorias-menu">
<asp:LinkButton ID="lnkTodos" runat="server" Text="All Products" OnClick="lnkCategoria_Click" CommandArgument="" CssClass="link-categoria" />
<asp:LinkButton ID="lnkSmartphones" runat="server" Text="Smartphones" OnClick="lnkCategoria_Click" CommandArgument="1" CssClass="link-categoria" />
<asp:LinkButton ID="lnkTablets" runat="server" Text="Tablets" OnClick="lnkCategoria_Click" CommandArgument="2" CssClass="link-categoria" />
<asp:LinkButton ID="lnkLaptops" runat="server" Text="Laptops" OnClick="lnkCategoria_Click" CommandArgument="3" CssClass="link-categoria" />
<asp:LinkButton ID="lnkSmartwatches" runat="server" Text="Smartwatches" OnClick="lnkCategoria_Click" CommandArgument="4" CssClass="link-categoria" />
<asp:LinkButton ID="lnkAuriculares" runat="server" Text="Auriculares" OnClick="lnkCategoria_Click" CommandArgument="5" CssClass="link-categoria" />

        </div>
        <div class="acciones">
            <button type="button" onclick="toggleFilter()">🧰 Filter</button>
            <button type="button" onclick="toggleSearch()">🔍 Search</button>
        </div>
    </div>

    <!-- Panel de Filtro -->
  
<div id="filterPanel" style="display:none; margin: 20px auto; max-width: 1200px; background-color: #f8f9fa; padding: 20px; border-radius: 10px;">
    <h4>Filtrar por:</h4>
    <div style="display: flex; flex-wrap: wrap; gap: 20px; margin-top: 10px;">
        <div>
            <strong>Categoría:</strong><br />
            <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control" />
        </div>
         <div>
        <strong>Marca:</strong><br />
        <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control" />
    </div>
        <div>
    <strong>Color:</strong><br />
    <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-control" />
</div>

        <div>
            <strong>Precio:</strong><br />
            <asp:DropDownList ID="ddlPrecio" runat="server" CssClass="form-control">
                <asp:ListItem Text="Todos" Value="" />
                <asp:ListItem Text="Menos de 200€" Value="200" />
                <asp:ListItem Text="200€ - 500€" Value="500" />
                <asp:ListItem Text="Más de 500€" Value="1000" />
            </asp:DropDownList>
        </div>
        <div style="align-self: flex-end;">
            <asp:Button ID="btnFiltrar" runat="server" Text="Aplicar Filtros" CssClass="btn btn-dark" OnClick="btnFiltrar_Click" />
            <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar Filtros" CssClass="btn btn-dark" OnClick="btnLimpiarFiltros_Click" />
        </div>
    </div>
</div>


    <!-- Panel de Búsqueda -->
    <div id="searchPanel" style="display:none; margin: 20px auto; max-width: 1200px;">
        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por nombre..." Style="padding: 10px;" />
        <div style="margin-top: 10px;">
           <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-secondary" OnClick="btnBuscar_Click" />

        </div>
    </div>

    <!-- Grid de productos con ASP.NET Repeater -->
    <div class="productos-grid">
      <asp:Repeater ID="rptCatalogo" runat="server">
   <ItemTemplate>
    <div class="producto-card position-relative">
        
       
        <asp:Panel runat="server" 
            Visible='<%# Convert.ToInt32(Eval("vendido")) == 1 && 
                      (Eval("disponible_hasta") == DBNull.Value || Convert.ToDateTime(Eval("disponible_hasta")) <= DateTime.Now) %>' 
            CssClass="badge-finalizado">
            FINALIZADO
        </asp:Panel>

        <!-- BADGE PRÓXIMAMENTE (solo si no vendido y fecha futura) -->
        <asp:Panel runat="server"
            Visible='<%# Convert.ToInt32(Eval("vendido")) == 0 &&
                      Eval("disponible_hasta") != DBNull.Value && 
                      Convert.ToDateTime(Eval("disponible_hasta")) > DateTime.Now %>'
            CssClass="badge-finalizado" 
            style="background-color: orange;">
            PRÓXIMAMENTE
        </asp:Panel>

        <!-- Imagen -->
        <asp:Image ID="imgProducto" runat="server" ImageUrl='<%# Eval("ImagenUrl") %>' AlternateText="Producto" />

        <!-- Info -->
        <div class="producto-body">
            <h5><%# Eval("Nombre") %></h5>
            <p class="text-muted"><%# Eval("Color") %> - <%# Eval("Memoria") %></p>
            <p><strong><%# Eval("Precio", "{0:C}") %></strong></p>
            <p><%# MostrarDisponibilidad(Eval("disponible_hasta")) %></p>
        </div>

        <!-- Acciones -->
        <div class="producto-footer">
            <asp:HyperLink 
                ID="lnkVerProducto" 
                runat="server" 
                NavigateUrl='<%# Eval("catalogo_id", "articulo.aspx?id={0}") %>' 
                CssClass="btn btn-outline-dark">
                Ver producto
            </asp:HyperLink>

            <asp:LinkButton 
                ID="lnkCarrito" 
                runat="server"  
                CssClass="btn btn-outline-danger" 
                CommandName="AddCarro" 
                CommandArgument='<%# Eval("catalogo_id") %>'
                ToolTip="Añadir al carrito"
                OnCommand="AddCarro"
                Enabled='<%# Convert.ToInt32(Eval("vendido")) == 0 && 
                            (Eval("disponible_hasta") == DBNull.Value || 
                             Convert.ToDateTime(Eval("disponible_hasta")) <= DateTime.Now) %>'>
                🛒
            </asp:LinkButton>

            <asp:LinkButton 
                ID="lnkFavorito" 
                runat="server"  
                CssClass="btn btn-outline-danger" 
                CommandName="AddFavorito" 
                CommandArgument='<%# Eval("catalogo_id") %>'
                ToolTip="Agregar a favoritos"
                OnCommand="AddFavorito"
                Enabled='<%# Convert.ToInt32(Eval("vendido")) == 0 && 
                            (Eval("disponible_hasta") == DBNull.Value || 
                             Convert.ToDateTime(Eval("disponible_hasta")) <= DateTime.Now) %>'>
                ❤️
            </asp:LinkButton>
        </div>
    </div>
</ItemTemplate>


</asp:Repeater>

      


    </div>
                  <div class="text-center my-4">
    <div id="pagination" class="pagination"></div>
</div>

  

    <script>
        function toggleFilter() {
            const filter = document.getElementById("filterPanel");
            const search = document.getElementById("searchPanel");
            filter.style.display = (filter.style.display === "none") ? "block" : "none";
            search.style.display = "none";
        }

        function toggleSearch() {
            const filter = document.getElementById("filterPanel");
            const search = document.getElementById("searchPanel");
            search.style.display = (search.style.display === "none") ? "block" : "none";
            filter.style.display = "none";
        }
    </script>
   <script>
       const itemsPerPage = 6;
       let currentPage = 1;

       function showPage(page) {
           const items = document.querySelectorAll('.productos-grid > .producto-card');
           const totalPages = Math.ceil(items.length / itemsPerPage);
           if (page < 1) page = 1;
           if (page > totalPages) page = totalPages;

           currentPage = page;

           // Mostrar productos correctos
           items.forEach((item, index) => {
               if (index >= (currentPage - 1) * itemsPerPage && index < currentPage * itemsPerPage) {
                   item.style.removeProperty("display");
               } else {
                   item.style.display = "none";
               }
           });

           renderPagination(totalPages);
       }

       function renderPagination(totalPages) {
           const pagination = document.getElementById("pagination");
           pagination.innerHTML = "";

           // Botón anterior
           const prevBtn = document.createElement("button");
           prevBtn.innerText = "«";
           prevBtn.disabled = currentPage === 1;
           prevBtn.onclick = () => showPage(currentPage - 1);
           pagination.appendChild(prevBtn);

           // Números de página
           for (let i = 1; i <= totalPages; i++) {
               const btn = document.createElement("button");
               btn.innerText = i;
               btn.className = i === currentPage ? "active" : "";
               btn.onclick = () => showPage(i);
               pagination.appendChild(btn);
           }

           // Botón siguiente
           const nextBtn = document.createElement("button");
           nextBtn.innerText = "»";
           nextBtn.disabled = currentPage === totalPages;
           nextBtn.onclick = () => showPage(currentPage + 1);
           pagination.appendChild(nextBtn);
       }

       // Ejecutar al cargar
       window.onload = function () {
           showPage(1);
       };
   </script>


</asp:Content>

