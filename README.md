

	👤  NUESTRO EQUIPO  👤
👤 Rahma Chibane

	
	* rc75-ua

👤 Yamina Chibane 

	  
	* yc27-ua
	




## 👑 Administradores

| Usuario  | Email                | Contraseña   |
|----------|----------------------|--------------|
| yc       | yc27@gcloud.ua.es     | amina1234    |
| rc       | rc75@gcloud.ua.es     | rahma1234    |

---

## 👤 Usuarios Normales Activos  
Estos usuarios han realizado compras, tienen productos en carrito y/o en favoritos.

| Usuario     | Email                   | Contraseña |
|-------------|--------------------------|------------|
| user2025    | carlosn@gmail.com        | 98765      |
| javier_g    | javierg@gmail.com        | 11234      |
| maria27     | maria.romero@gmail.com   | 44556      |
| elena76     | elenamoreno@gmail.com    | 11223      |


   
# 🛒 MovilGo - Plataforma de Compraventa de Tecnología

## 📝 Descripción General del Proyecto

**MovilGo** es una plataforma dedicada a la **compraventa de dispositivos tecnológicos** reacondicionados y nuevos. A través de nuestro marketplace, los usuarios pueden:

🔍 **Buscar** dispositivos como móviles, tablets, portátiles, smartwatches y más.

⭐ **Consultar valoraciones y comentarios** antes de comprar, obteniendo una visión real de la experiencia de otros usuarios.

📢 **Publicar anuncios** fácilmente para vender cualquier dispositivo tecnológico.

🧭 Navegar una página con **estructura clara y sencilla**, accesible para todo tipo de usuarios.

🤝 **Contactar directamente con nosotros**, lo que garantiza seguridad y confianza antes de realizar la compra.

🧾 **Gestionar su perfil** como usuario registrado, donde pueden:
- actualizar contraseña y editar y actualizar cuenta 📢


📌 Nuestro objetivo es ofrecer una experiencia **segura, confiable y de calidad**, tanto para compradores como para vendedores.

---

## 🌍 Parte Pública

Accesible a cualquier visitante del sitio web. Las funcionalidades incluyen:

📬 **Página de Contacto**  
Información sobre cómo comunicarse con la empresa: dirección, teléfono, redes sociales y correo electrónico.

🛍️ **Catálogo de Productos**  
Explora los dispositivos disponibles aplicando filtros como:
- Categoría 📱 (smartphones, tablets, etc.)
- Marca 🏷️
- Modelo
- Sistema Operativo 🧠
- Estado del producto ⚙️

📦 **Detalle del Artículo**  
Cada producto muestra:
- Valoración media ⭐
- Comentarios de usuarios 💬
- Cantidad disponible (stock) 📦
- Estado y características técnicas 🧾



### 🧩 Entidades de Negocio - Parte Pública

| 📦 **Entidad**     | 📝 **Descripción**                                                                                    		  |
|--------------------|--------------------------------------------------------------------------------------------------------------------|
| **ENContacto**     | Contiene los métodos de contacto con la empresa, incluyendo dirección, teléfono y redes sociales.   		  |
| **ENCatalogo**     | Muestra el catálogo de dispositivos reacondicionados, con filtros por categoría, marca, estado, etc. 		  |	  |
| **ENArticulo**     | Muestra el estado del producto , el stock , la media de valoraciones , el vendedor la descripción del producto,etc.|

## 🔐 Parte Privada

Accesible únicamente para **usuarios registrados**, esta sección permite una gestión completa de la actividad en la plataforma:

🛒 **Venta de dispositivos** y administración de anuncios.  
🧾 **Compra de productos**, pagos, financiación y contacto con el vendedor.  
👥 **Gestión de usuarios**: tipo (empresa, particular), datos fiscales y de contacto.  
📦 **Administración de artículos**, carritos, favoritos, comentarios y valoraciones.  
🏢 **Gestión de proveedores**: se puede consultar nombre, CIF, dirección y sus artículos publicados.

---

### 🔧 Funcionalidades Específicas

- 🛍️ Métodos para realizar la **venta** de productos, controlar anuncios, precios y detalles técnicos.
- 💳 Métodos para realizar la **compra**, incluyendo formas de pago, financiación y contacto con el vendedor.
- 👤 Entidad de los **usuarios**: nombre , dirección, teléfono, email y datos fiscales.
- 📱 Entidad de los **artículos**: categoría (móvil, portátil, tablet…), marca, modelo, año, sistema operativo, estado, memoria, batería, precio, etc.
- 🛒 Entidad de los **carritos de compra**: productos añadidos, precio total y usuario que los añadió.
- 💳 Entidad de los **métodos de pago**: número de tarjeta, fecha de caducidad, CVV, etc.
- ❤️ Entidad de los **favoritos**: productos guardados por cada usuario.
- 💬 Entidad de los **comentarios** sobre los productos y su autor.
- 🌟 Entidad de las **valoraciones** entre usuarios (comprador/vendedor).

---

### 🧩 Entidades de Negocio - Parte Privada

| 📦 **Entidad**         | 📝 **Descripción**                                                                 |
|------------------------|-------------------------------------------------------------------------------------|
| **ENVenta**            | Gestión de venta de productos, anuncios y precios.                                |
| **ENUsuario**          | Información detallada del usuario: tipo, dirección, contacto, datos fiscales.     |
| **ENArticulo**         | Detalles técnicos de los dispositivos: marca, modelo, memoria, precio, etc.       |
| **ENCarrito**          | Productos añadidos al carrito y el usuario correspondiente.                        |
| **ENMetodoPago**       | Gestión segura de los métodos de pago.                                             |
| **ENListaFavorito**    | Control de los productos marcados como favoritos por los usuarios.                |
| **ENComentario**       | Comentarios sobre productos publicados por usuarios registrados.                   |
| **ENPedido**           | Registro de compras realizadas, productos incluidos y datos del comprador.         |


## Posibles mejoras
* Sistema de valoraciones para usuarios frecuentes, con beneficios o distintivos.

* Seguridad reforzada mediante encriptación de datos personales y bancarios.

* Aumento del rendimiento de la web usando sistemas de caché.

* Sistema de recomendaciones personalizadas por tipo de usuario.

* Control de calidad para los productos antes de su publicación.

* Sistema de garantías y posibilidad de reservar productos.

* Modo oscuro disponible para mejor experiencia.

* Web disponible en varios idiomas.

* Diseño responsive moderno con Bootstrap.










- He creado base datos con Rahma Chibane.
  
-EN Default he hecho secciones de servicios ,contaccto y testiomonios visuales .

-LOGIN ,Register y Forgot Password: He desarrollado un sistema interfaces de autenticación en ASP.NET que incluye registro, login y recuperación de contraseña. Utilizando ENUsuario para manejar los datos del usuario y CADUsuario para interactuar con la base de datos. En el registro validas los campos y creas un nuevo usuario, en el login verificas credenciales y gestionas sesiones según el rol (usuario o admin), y en la recuperación de contraseña se  envía un correo con los datos del usuario usando SMTP. Todo el sistema usa SweetAlert para mostrar notificaciones claras al usuario y manera bonita y clara.

-ENContacto y CADContacto Contacto.aspx: he desarollado  interfaz contacto.aspx mejorado con bootsrap incluyendo mensajes de exito de envio con sweetalert con envio de correo correcto hecho como en Forgotpassword.

- CORREO:movilgotienda@gmail.com
- contraseña: movilgo1234#
- al iniciar te pide codigo de verifcacion y cada código de verificación alternativo solo se puede utilizar una vez.

 0266 3550

 
 5605 8858
 
 
 4491 7567
 
 
 6468 5258



- He desarrollado user.aspx perfil para usuarios normales y administradores que permite editar el perfil, cambiar la contraseña y eliminar la cuenta, utilizando SweetAlert para notificaciones,editar,y eliminar; se diferencia visualmente al mostrar el panel de administrador solo si el usuario es admin, y se usan las clases ENUsuario y CADUsuario para gestionar la lógica y el acceso a datos del perfil del usuario.

- Panel admin : He creado un panel de administración en ASP.NET se inicia con el nombre del admin que permite listar, editar y eliminar usuarios,productos,provedores,transaccuion dashboard(mejoras), así como cambiar su rol de admin

- He creado lisatdo usuarios usando ENUsuario y CADUsuario para la lógica y acceso a dato ,editando datos ,eliminar y controlar si es admin o no. Integre SweetAlert y DataTables para una experiencia moderna y dinámica   con funciones de exportación(print,copiar,imprimir,csv),paginacion ,filtro de busqued,ordenaciona y edición vía AJAX.

- He creado listado productos editando y eliminando productos he agregado funcion de eliminar en CADARTICULO para obtener articulo en tabla y eliminarlo definitivamente de base datos y CAtalogo ,tambian he añadido una etiqueta al finalizar producto se muestra etiqueta en catalogo finalizada.he usado sweetalert ,paginacion fiktro de busqueda,ordenacion.

- En Dahborad mejoras he añadido top 10 ventas


👩‍💻📚💼 TRABAJO de Rahma Chibane  👩‍💻📚 💼


- he creado la base de datos y la pagina master  y en default he añaido solo la seccion de Productos Recién Llegados


- catalogo.aspx : He implementado un sistema de catálogo donde los productos se muestran con su información detallada. Además, se han añadido funciones de filtrado y búsqueda mediante el uso de CADMarcas y CADCategoria. para mejorar la experiencia del usuario


- venta.aspx :He implementado la funcionalidad de publicación de nuevos artículos utilizando las clases CADVenta, CADArticulo, CADMarcas y CADCategoria, desarrollando funciones para crear y actualizar artículos, y mostrando una alerta SweetAlert con un resumen del producto publicado.


- mejoras.aspx : hecho la parte de ganancia y  meta mensual y un gráfico que relaciona las ventas mensuales con el rendimiento por mes.




## Modelo de negocio
MovilGo ganará dinero cobrando una comisión por cada venta realizada en la plataforma.
Cuando un usuario vende un dispositivo a través del marketplace, la empresa retiene un pequeño porcentaje del precio final como comisión por el uso del servicio.
Esto permite mantener la plataforma gratuita para los compradores y accesible para todos los vendedores.

## EL PDF de la BBDD esta esta en esta misma carpeta con el nombre: BD_pagina_Web.pdf

## El zip esta en drive (pesaba mucho): https://drive.google.com/file/d/1-OrAtpbXu0AQgdSSRb46huEOl0x639uN/view?usp=sharing





