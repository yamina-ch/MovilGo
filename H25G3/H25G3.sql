DROP TABLE IF EXISTS metodos_pago;
DROP TABLE IF EXISTS lista_favoritos;
DROP TABLE IF EXISTS comentario;
DROP TABLE IF EXISTS lineacarrito;
DROP TABLE IF EXISTS carrito;
DROP TABLE IF EXISTS contactos;
DROP TABLE IF EXISTS transaccion;
DROP TABLE IF EXISTS lineapedido;
DROP TABLE IF EXISTS pedido;
DROP TABLE IF EXISTS venta;
DROP TABLE IF EXISTS articulo;
DROP TABLE IF EXISTS catalogo;
DROP TABLE IF EXISTS proveedor;
DROP TABLE IF EXISTS marcas;
DROP TABLE IF EXISTS categoria; 

DROP TABLE IF EXISTS usuario;



-- Tabla Categoría
CREATE TABLE categoria (
    categoria_id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL
);

-- Tabla usuario
CREATE TABLE usuario (
    username VARCHAR(50) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    apellidos VARCHAR(50) NOT NULL,
    telefono VARCHAR(18) NOT NULL,
    contrasenya VARCHAR(30) NOT NULL,
    calle VARCHAR(50),
    localidad VARCHAR(45),
    provincia VARCHAR(45),
    codigo_postal VARCHAR(6),
    email VARCHAR(45) NOT NULL,
    admin INT NOT NULL DEFAULT 0
);

-- Tabla Marcas
CREATE TABLE marcas (
    marca_id INT PRIMARY KEY,
    nombre VARCHAR(45) NOT NULL
);

-- Tabla Proveedor
CREATE TABLE proveedor (
    proveedor_id INT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    cif VARCHAR(20) NOT NULL,
    direccion VARCHAR(100),
    telefono VARCHAR(20),
    email VARCHAR(100)
);

-- Tabla Catálogo
CREATE TABLE catalogo (
    catalogo_id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    descripcion VARCHAR(255),
    precio DECIMAL(10,2) NOT NULL,
    vendido INT NOT NULL,
    categoria_id INT NOT NULL,
    url_imagen VARCHAR(255),
    CONSTRAINT FK_CATALOGO_CATEGORIA FOREIGN KEY (categoria_id) REFERENCES categoria(categoria_id)
);

-- Tabla Artículo
CREATE TABLE articulo (
    articulo_id INT PRIMARY KEY,
    stock INT NOT NULL,
    marca_id INT NOT NULL,
    categoria_id INT NOT NULL,
    catalogo_id INT NULL,
    color VARCHAR(45),
    modelo VARCHAR(45) NOT NULL,
    sistema_operativo VARCHAR(45) NOT NULL,
    anyo INT,
    estado VARCHAR(45) NOT NULL,
    memoria VARCHAR(45) NOT NULL,
    bateria VARCHAR(45),
    precio DECIMAL(10,2) NOT NULL,
    descripcion VARCHAR(200),
    valoracion DECIMAL(2,1),
    url_imagen VARCHAR(100),
    vendido INT NOT NULL,
    vendedor_UName VARCHAR(50) NOT NULL,
    proveedor_id INT NULL,

    CONSTRAINT FK_ARTICULO_MARCA FOREIGN KEY (marca_id) REFERENCES marcas(marca_id),
    CONSTRAINT FK_ARTICULO_CATEGORIA FOREIGN KEY (categoria_id) REFERENCES categoria(categoria_id),
    CONSTRAINT FK_ARTICULO_CATALOGO FOREIGN KEY (catalogo_id) REFERENCES catalogo(catalogo_id),
    CONSTRAINT FK_ARTICULO_USUARIO FOREIGN KEY (vendedor_UName) REFERENCES usuario(username),
    CONSTRAINT FK_ARTICULO_PROVEEDOR FOREIGN KEY (proveedor_id) REFERENCES proveedor(proveedor_id)
);


-- Tabla Pedido
CREATE TABLE pedido (
    pedido_id INT PRIMARY KEY,
    comprador_UName VARCHAR(50) NOT NULL,
    fecha_pedido DATE NOT NULL,
    importe FLOAT NOT NULL,
    CONSTRAINT FK_PEDIDO_COMPRADOR FOREIGN KEY (comprador_UName) REFERENCES usuario(username)

);

-- Tabla Lineapedido
CREATE TABLE lineapedido (
    linea_pedido_id INT,
    pedido_id INT,
    comprador_UName VARCHAR(50) NOT NULL,
    vendedor_UName VARCHAR(50) NOT NULL,
    articulo_id INT NOT NULL,
    importe FLOAT,
    CONSTRAINT PK_LINEAPEDIDO PRIMARY KEY (linea_pedido_id),
    CONSTRAINT FK_LINEAPEDIDO_PEDIDO FOREIGN KEY (pedido_id) REFERENCES pedido(pedido_id),
    CONSTRAINT FK_LINEAPEDIDO_ARTICULO FOREIGN KEY (articulo_id) REFERENCES articulo(articulo_id),
    CONSTRAINT FK_LINEAPEDIDO_USUARIO FOREIGN KEY (comprador_UName) REFERENCES usuario(username),
    CONSTRAINT FK_LINEAPEDIDO_VENDEDOR FOREIGN KEY (vendedor_UName) REFERENCES usuario(username)
);

-- Tabla Transacción
CREATE TABLE transaccion (
    transaccion_id INT,
    linea_pedido_id INT NOT NULL,
    articulo_id INT NOT NULL,
    vendedor_UName VARCHAR(50) NOT NULL,
    importe_total FLOAT NOT NULL,
    comision_vendedor FLOAT NOT NULL,
    comision_empresa FLOAT NOT NULL,
    fecha_transaccion DATE NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_TRANSACCION PRIMARY KEY (transaccion_id),
    CONSTRAINT FK_TRANSACCION_LINEAPEDIDO FOREIGN KEY (linea_pedido_id) REFERENCES lineapedido(linea_pedido_id),
    CONSTRAINT FK_TRANSACCION_ARTICULO FOREIGN KEY (articulo_id) REFERENCES articulo(articulo_id),
    CONSTRAINT FK_TRANSACCION_VENDEDOR FOREIGN KEY (vendedor_UName) REFERENCES usuario(username)
);



-- Tabla Venta
CREATE TABLE venta (
    venta_id INT IDENTITY(1,1) PRIMARY KEY,
    articulo_id INT NOT NULL,
    fecha_anuncio DATE NOT NULL,
    vendedor_UName VARCHAR(50) NOT NULL,
    precio_original DECIMAL(10,2),
    precio_publicado DECIMAL(10,2) NOT NULL,
    motivo_venta VARCHAR(100),
    disponible_hasta DATE,
    CONSTRAINT FK_VENTA_ARTICULO FOREIGN KEY (articulo_id) REFERENCES articulo(articulo_id),
    CONSTRAINT FK_VENTA_USUARIO FOREIGN KEY (vendedor_UName) REFERENCES usuario(username)
);



-- Tabla Contactos
CREATE TABLE contactos (
    contacto_id INT IDENTITY(1,1) PRIMARY KEY,
    usuario_UName VARCHAR(50) NULL,
    nombre VARCHAR(50)  NULL,
    apellidos VARCHAR(50) NULL,
    email VARCHAR(45) NOT NULL,
    asunto VARCHAR(100) NOT NULL ,
    mensaje TEXT NOT NULL,
    fecha_contacto DATE NOT NULL,
    CONSTRAINT FK_CONTACTOS_USUARIO FOREIGN KEY (usuario_UName) REFERENCES usuario(username)
);

-- Tabla Carrito
CREATE TABLE carrito (
    carrito_id INT IDENTITY(1,1) PRIMARY KEY,
    usuario_UName VARCHAR(50) NOT NULL,
    CONSTRAINT FK_CARRITO_USUARIO FOREIGN KEY (usuario_UName) REFERENCES usuario(username)
);

-- Tabla Lineacarrito
CREATE TABLE lineacarrito (
    linea_carrito_id INT IDENTITY(1,1),
    carrito_id INT,
    articulo_id INT,
    importe FLOAT,
    cantidad INT DEFAULT 1,
    CONSTRAINT PK_LINEACARRITO PRIMARY KEY (linea_carrito_id, carrito_id),
    CONSTRAINT FK_LINEACARRITO_CARRITO FOREIGN KEY (carrito_id) REFERENCES carrito(carrito_id),
    CONSTRAINT FK_LINEACARRITO_ARTICULO FOREIGN KEY (articulo_id) REFERENCES articulo(articulo_id)
);

-- Tabla comentario
CREATE TABLE comentario (
    comentario_id INT PRIMARY KEY,
    articulo_id INT NOT NULL,
    usuario_UName VARCHAR(50) NOT NULL,
    comentario TEXT NOT NULL,
    fecha_comentario DATE NOT NULL,
    CONSTRAINT FK_comentario_ARTICULO FOREIGN KEY (articulo_id) REFERENCES articulo(articulo_id),
    CONSTRAINT FK_comentario_USUARIO FOREIGN KEY (usuario_UName) REFERENCES usuario(username)
);

-- Tabla Lista de Favoritos
CREATE TABLE lista_favoritos (
    lista_favorito_id INT IDENTITY(1,1) PRIMARY KEY,
    usuario_UName VARCHAR(50) NOT NULL,
    articulo_id INT NOT NULL,
    CONSTRAINT FK_FAVORITOS_USUARIO FOREIGN KEY (usuario_UName) REFERENCES usuario(username),
    CONSTRAINT FK_FAVORITOS_ARTICULO FOREIGN KEY (articulo_id) REFERENCES articulo(articulo_id)
);

-- Tabla Métodos de Pago
CREATE TABLE metodos_pago (
    numTarjeta VARCHAR(16) PRIMARY KEY,
    cvv VARCHAR(3),
    mes_cad INT NOT NULL,
    anyo_cad INT NOT NULL,
    usuario VARCHAR(50) NOT NULL,
    CONSTRAINT FK_METODO_USUARIO FOREIGN KEY (usuario) REFERENCES usuario(username)
);

-- Inserts para la tabla usuario
INSERT INTO usuario (username, nombre, apellidos, telefono, contrasenya, calle, localidad, provincia, codigo_postal, email, admin) VALUES
('sb', 'Sabrine', 'bentaleb', '611222333', '123456', 'Serrano, nº3, 6ºA', 'Alicante', 'Alicante', '03003', 'sb23@gcloud.ua.es', 1),
('cs', 'Rachid', 'Rachid ben', '626333770', 'rachid1234', 'Gran Via, nº2, 8ºB', 'Alicante', 'Alicante', '03402', 'rachidben@gcloud.ua.es', 1),
('mb', 'Meriem', 'ben', '611654326', '060504', 'Bono Guarner, nº12, 3ºC', 'Alicante', 'Alicante', '03005', 'mb12@gcloud.ua.es', 1),
('dg', 'David', 'garcia', '625421300', '012345', 'Pinto, nº1, 1ºA', 'Alicante', 'Alicante', '03008', 'dg2@gcloud.ua.es', 1),
('yc', 'Yamina', 'Chibane', '602426879', 'amina1234', 'Gabinete, nº11, 2ºB', 'Alicante', 'Alicante', '03006', 'yc27@gcloud.ua.es', 1),
('rc', 'Rahma', 'Chibane', '611259249', 'rahma1234', 'Pintor, nº9, 8ºC', 'Alicante', 'Alicante', '03030', 'rc75@gcloud.ua.es', 1),
('user2025', 'Carlos', 'Navarro', '690112233', '98765', 'Calle Nueva 21', 'Bilbao', 'Vizcaya', '48001', 'carlosn@gmail.com', 0),
('maria87', 'Isabel', 'Castro', '692223344', 'abcde', 'Avenida España 10', 'Granada', 'Granada', '18001', 'isabelc@gmail.com', 0),
('pedro_m', 'Alejandro', 'Ruiz', '693334455', 'zxcvb', 'Calle Paz 5', 'Toledo', 'Toledo', '45001', 'alejandroruiz@gmail.com', 0),
('elena76', 'elena', 'Moreno', '694445566', '11223', 'Plaza Mayor 8', 'Valladolid', 'Valladolid', '47001', 'elenamoreno@gmail.com', 0),
('maria27', 'maria', 'Romero', '695556677', '44556', 'Av. Libertad 15', 'Cádiz', 'Cádiz', '11001', 'maria.romero@gmail.com', 0),
('jose90', 'jose', 'Serrano', '696667788', '77889', 'Calle Norte 7', 'Logroño', 'La Rioja', '26001', 'ivanserrano@gmail.com', 0),
('laura89', 'laura', 'Vega', '697778899', '99887', 'Camino Real 3', 'Oviedo', 'Asturias', '33001', 'laura.vega@gmail.com', 0),
('hugo01', 'Hugo', 'Blanco', '698889900', '55432', 'Calle del Sol 9', 'Pamplona', 'Navarra', '31001', 'hugoblanco@gmail.com', 0),
('pm67', 'Patricia', 'Marín', '699990011', '66778', 'Calle Luna 12', 'Santander', 'Cantabria', '39001', 'patricia.marin@gmail.com', 0),
('javier_g', 'javier', 'Giménez', '600101112', '11234', 'Calle Castilla 4', 'Zaragoza', 'Zaragoza', '50001', 'javierg@gmail.com', 0);

-- Tabla Categoria
INSERT INTO categoria (nombre) VALUES
('Smartphones'), ('Tablets'), ('Laptops'), ('Smartwatches'), ('Auriculares'),
('Accesorios'), ('Monitores'), ('Cámaras'), ('Drones'), ('Componentes PC');


 -- Tabla Marcas
INSERT INTO marcas (marca_id, nombre) VALUES
(1, 'Apple'),
(2, 'Samsung'),
(3, 'Google'),
(4, 'Xiaomi'),
(5, 'OnePlus'),
(6, 'Huawei'),
(7, 'ASUS'),
(8, 'DJI'),
(9, 'Sony'),
(10, 'Logitech');
-- Tabla Proveedor
INSERT INTO proveedor (proveedor_id, nombre, cif, direccion, telefono, email) VALUES
(1, 'TechDistribuciones SL', 'B12345678', 'Calle Electrónica 12, Madrid', '912345678', 'contacto@techdistribuciones.com'),
(2, 'Gadgets Global', 'C87654321', 'Av. Tecnología 8, Barcelona', '931234567', 'ventas@gadgetsglobal.com'),
(3, 'MegaTech Proveedor', 'D23456789', 'Calle Circuito 45, Valencia', '962345678', 'info@megatech.com'),
(4, 'DigitalZoom SL', 'E34567890', 'Calle Foto 22, Sevilla', '954123456', 'ventas@digitalzoom.com'),
(5, 'SkyDrone Imports', 'F45678901', 'Av. Vuelo 10, Málaga', '951234567', 'pedidos@skydrone.com'),
(6, 'Accesorios4U', 'G56789012', 'Calle Conectores 33, Bilbao', '944567890', 'contacto@accesorios4u.com'),
(7, 'PC Master S.A.', 'H67890123', 'Av. Gamer 18, Zaragoza', '976345678', 'clientes@pcmaster.com'),
(8, 'MovilTech España', 'I78901234', 'Calle Movil 99, Valladolid', '983234567', 'info@moviltech.com'),
(9, 'Tablets World', 'J89012345', 'Calle Tablet 65, Murcia', '968456789', 'contact@tabletsworld.com'),
(10, 'FotoGenius', 'K90123456', 'Paseo Imagen 3, Alicante', '965234123', 'soporte@fotogenius.com');

-- Tabla Catalogo 

INSERT INTO catalogo (nombre, descripcion, precio, vendido, categoria_id, url_imagen) VALUES
('iPhone 14 Pro', 'iPhone 14 Pro con chip A16 y pantalla ProMotion.', 1299.99, 0, 1, '/imagenes/iphone14pro.jpg'),
('Galaxy S23', 'Galaxy S23 con Snapdragon 8 Gen 2 y gran cámara.', 1099.50, 0, 1, '/imagenes/galaxys23.jpg'),
('Xiaomi 13', 'Xiaomi 13 con pantalla AMOLED y carga rápida.', 749.99, 0, 1, '/imagenes/xiaomi13.jpg'),
('MacBook Pro M1', 'MacBook Pro con chip M1 para alto rendimiento.', 1299.99, 0, 3, '/imagenes/macbookpro.jpg'),
('Huawei MateBook 14', 'Huawei MateBook 14 con autonomía excelente.', 950.00, 0, 3, '/imagenes/matebook.jpg'),
('ASUS ZenWatch 3', 'ZenWatch 3 con GPS y monitor cardíaco.', 199.99, 0, 4, '/imagenes/zenwatch.jpg'),
('DJI Mini 2', 'Dron DJI Mini 2 con grabación 4K.', 499.00, 0, 9, '/imagenes/dji_mini2.jpg'),
('Sony WH-1000XM4', 'Auriculares con cancelación activa y sonido Hi-Res.', 299.00, 0, 5, '/imagenes/sony_wh1000.jpg'),  
('Google Pixel 8', 'Google Pixel 8 con cámara avanzada y Android puro.', 849.00, 0, 1, '/imagenes/pixel8.jpg'),
('Samsung Monitor 27"', 'Monitor curvo 2K con HDMI y bajo consumo.', 249.99, 0, 7, '/imagenes/monitor27.jpg'),
('Motorola Edge 40', 'Smartphone 5G con pantalla OLED y cámara avanzada.', 599.00, 0, 1, '/imagenes/motorola_edge40.png'),
('iPad Air 2024', 'iPad ligero y potente para productividad y entretenimiento.', 679.00, 0, 2, '/imagenes/ipad_air.png'),
('Lenovo Legion 5', 'Portátil gamer con RTX 4060 y Ryzen 7.', 1299.00, 0, 3, '/imagenes/legion5.png'),
('Realme Watch 3', 'Smartwatch económico con seguimiento deportivo.', 79.00, 0, 4, '/imagenes/realme_watch3.png'),
('Canon EOS M50 II', 'Cámara sin espejo ideal para creadores de contenido.', 749.00, 0, 8, '/imagenes/canon_m50.png');




INSERT INTO articulo (
  articulo_id, stock, marca_id, categoria_id, catalogo_id, color, modelo, sistema_operativo,
  anyo, estado, memoria, bateria, precio, descripcion, valoracion, url_imagen,
  vendido, vendedor_UName, proveedor_id
) VALUES
-- Usuario normal → stock=1, proveedor_id=NULL
(1, 1, 1, 1, 1, 'Negro', 'iPhone 14 Pro', 'iOS', 2023, 'Nuevo', '128GB', '3200mAh', 1299.99,
 'iPhone 14 Pro con chip A16 y pantalla ProMotion.', 4.8, '/imagenes/iphone14pro.jpg', 1, 'maria27', NULL),

-- Admin
(2, 17, 2, 1, 2, 'Plateado', 'Galaxy S23', 'Android', 2023, 'Nuevo', '256GB', '3900mAh', 1099.50,
 'Galaxy S23 con Snapdragon 8 Gen 2 y gran cámara.', 4.7, '/imagenes/galaxys23.jpg', 0, 'cs', 2),

-- Usuario normal
(3, 1, 4, 1, 3, 'Azul', 'Xiaomi 13', 'Android', 2023, 'Nuevo', '256GB', '4500mAh', 749.99,
 'Xiaomi 13 con pantalla AMOLED y carga rápida.', 4.5, '/imagenes/xiaomi13.jpg', 0, 'elena76', NULL),

-- Admin
(4, 9, 1, 3, 4, 'Gris', 'MacBook Pro M1', 'macOS', 2022, 'Nuevo', '512GB', '6000mAh', 1299.99,
 'MacBook Pro con chip M1 para alto rendimiento.', 4.9, '/imagenes/macbookpro.jpg', 0, 'sb', 1),

-- Usuario normal
(5, 1, 6, 3, 5, 'Verde', 'MateBook 14', 'Windows', 2022, 'Nuevo', '256GB', '5000mAh', 950.00,
 'Huawei MateBook 14 con autonomía excelente.', 4.6, '/imagenes/matebook.jpg', 0, 'jose90', NULL),

-- Admin
(6, 26, 7, 4, 6, 'Negro', 'ZenWatch 3', 'Wear OS', 2021, 'Nuevo', '8GB', '300mAh', 199.99,
 'ZenWatch 3 con GPS y monitor cardíaco.', 4.4, '/imagenes/zenwatch.jpg', 0, 'rc', 3),

-- Admin
(7, 78, 8, 9, 7, 'Blanco', 'DJI Mini 2', 'DJI OS', 2022, 'Nuevo', '32GB', '2250mAh', 499.00,
 'Dron DJI Mini 2 con grabación 4K.', 4.7, '/imagenes/dji_mini2.jpg', 0, 'cs', 4),

-- Admin
(8, 66, 9, 5, 8, 'Negro', 'WH-1000XM4', 'Sony OS', 2021, 'Nuevo', '64GB', '3800mAh', 299.00,
 'Auriculares con cancelación activa y sonido Hi-Res.', 4.8, '/imagenes/sony_wh1000.jpg', 0, 'dg', 5),

-- Admin
(9, 73, 3, 1, 9, 'Gris', 'Pixel 8', 'Android', 2023, 'Nuevo', '128GB', '4200mAh', 849.00,
 'Google Pixel 8 con cámara avanzada y Android puro.', 4.7, '/imagenes/pixel8.jpg', 0, 'sb', 3),

-- Usuario normal
(10, 1, 2, 7, 10, 'Negro', 'Samsung Monitor 27"', 'Samsung FW', 2022, 'Nuevo', '128GB', '3000mAh', 249.99,
 'Monitor curvo 2K con HDMI y bajo consumo.', 4.6, '/imagenes/monitor27.jpg', 0, 'elena76', NULL),
(11, 1, 4, 1, 11, 'Azul', 'Motorola Edge 40', 'Android', 2024, 'Nuevo', '256GB', '4400mAh', 599.00,
 'Motorola Edge 40 con pantalla OLED y Android 13.', 4.5, '/imagenes/motorola_edge40.png', 0, 'jose90', NULL),

(12, 1, 1, 2, 12, 'Gris', 'iPad Air 2024', 'iPadOS', 2024, 'Nuevo', '128GB', '7500mAh', 679.00,
 'Nuevo iPad Air con chip M2.', 4.8, '/imagenes/ipad_air.png', 0, 'laura89', NULL),

(13, 1, 7, 3, 13, 'Negro', 'Legion 5', 'Windows', 2024, 'Nuevo', '512GB SSD', '7000mAh', 1299.00,
 'Gaming laptop con alto rendimiento.', 4.9, '/imagenes/legion5.png', 0, 'maria27', NULL),

(14, 1, 4, 4, 14, 'Verde', 'Realme Watch 3', 'Realme UI Watch', 2023, 'Nuevo', '32GB', '340mAh', 79.00,
 'Smartwatch con múltiples funciones deportivas.', 4.4, '/imagenes/realme_watch3.png', 0, 'user2025', NULL),

(15, 1, 9, 8, 15, 'Negro', 'Canon EOS M50 II', 'Canon OS', 2022, 'Nuevo', '64GB', '1200mAh', 749.00,
 'Cámara perfecta para vlogs y streaming.', 4.6, '/imagenes/canon_m50.png', 0, 'pedro_m', NULL);


 
-- Inserts para la tabla Pedido
INSERT INTO pedido (pedido_id, comprador_UName, fecha_pedido, importe) VALUES
(1, 'user2025', '2024-03-14', 1299.99),
(2, 'javier_g', '2024-03-15', 1099.50),
(3, 'maria27', '2024-02-10', 899.00),
(4, 'elena76', '2024-04-20', 749.99),
(5, 'jose90', '2024-01-05', 829.00);
-- Inserts para la tabla Lineapedido
INSERT INTO lineapedido (linea_pedido_id, pedido_id, comprador_UName, vendedor_UName, articulo_id, importe) VALUES
(1, 1, 'user2025', 'maria27', 1, 1299.99),
(2, 2, 'javier_g', 'cs', 2, 1099.50),
(3, 3, 'maria27', 'sb', 3, 899.00),
(4, 4, 'elena76', 'elena76', 4, 749.99),
(5, 5, 'jose90', 'pedro_m', 5, 829.00);
-- Tabla Transaccion


INSERT INTO transaccion (transaccion_id, linea_pedido_id, articulo_id, vendedor_UName, importe_total, comision_vendedor, comision_empresa, fecha_transaccion) VALUES

(1, 1, 1, 'maria27', 1299.99, 1169.99, 130.00, '2024-03-14'),


(2, 2, 2, 'cs', 1099.50, 0.00, 1099.50, '2024-03-15'),


(3, 3, 3, 'sb', 899.00, 0.00, 899.00, '2024-02-10'),


(4, 4, 4, 'elena76', 749.99, 674.99, 75.00, '2024-04-20'),


(5, 5, 5, 'pedro_m', 829.00, 746.10, 82.90, '2024-01-05');


-- Tabla Venta
INSERT INTO venta (
    articulo_id, fecha_anuncio, vendedor_UName,
    precio_original, precio_publicado, motivo_venta, disponible_hasta
) VALUES
(1, '2023-03-01', 'maria27', 1399.99, 1299.99, 'Cambio a nuevo modelo', '2024-06-01'),
(2, '2023-03-05', 'cs', 1199.00, 1099.50, 'No lo necesito', '2024-06-10'),
(3, '2023-04-01', 'sb', 950.00, 899.00, 'Comprado por error', '2024-07-15'),
(4, '2023-04-15', 'elena76', 799.99, 749.99, 'Me regalaron otro', '2024-07-01'),
(5, '2023-05-01', 'pedro_m', 879.00, 829.00, 'Quiero renovar equipo', '2024-06-20'),
(11, '2024-05-22', 'jose90', 629.00, 599.00, 'Cambio de modelo', '2025-06-01'),
(12, '2024-05-22', 'laura89', 699.00, 679.00, 'Doble regalo', '2025-05-25'),
(13, '2024-05-22', 'maria27', 1349.00, 1299.00, 'Uso profesional innecesario', '2025-05-31'),
(14, '2024-05-22', 'user2025', 89.00, 79.00, 'Regalo duplicado', '2025-06-15'),
(15, '2024-05-22', 'pedro_m', 799.00, 749.00, 'Cambio a modelo superior', '2025-05-29');


-- Inserts para la tabla comentario
INSERT INTO comentario (comentario_id, articulo_id, usuario_UName, comentario, fecha_comentario) VALUES
(1, 1, 'user2025', 'Excelente dispositivo.', '2024-01-01'),
(2, 2, 'javier_g', 'Muy potente.', '2024-01-02'),
(3, 3, 'maria27', 'Buena cámara.', '2024-01-03'),
(4, 4, 'elena76', 'Pantalla brillante.', '2024-01-04'),
(5, 5, 'jose90', 'Carga rápida funciona bien.', '2024-01-05');


-- Tabla Contactos


INSERT INTO contactos (usuario_UName, nombre, apellidos, email, asunto, mensaje, fecha_contacto) VALUES
('user2025', 'Carlos', 'Navarro', 'carlosn@gmail.com', 'Garantía', 'Consulta sobre garantía del producto.', '2024-03-01'),
('javier_g', 'Mario', 'Giménez', 'javierg@gmail.com', 'Envío', 'Pregunta sobre el tiempo de entrega.', '2024-03-02'),
(NULL, NULL, NULL, 'lucia@example.com', 'Devolución', 'Quiero devolver el producto.', '2024-03-03'),
(NULL, NULL, NULL, 'ana@example.com', 'Financiación', '¿Puedo pagar a plazos?', '2024-03-04'),
(NULL, NULL, NULL, 'david@example.com', 'Reclamación', 'No me ha llegado el pedido.', '2024-03-05');





-- Inserts para la tabla Carrito
INSERT INTO carrito (usuario_UName) VALUES
('user2025'),
('javier_g'),
('maria27'),
('elena76'),
('jose90'),
('laura89');

-- Inserts para la tabla Lineacarrito
INSERT INTO lineacarrito (carrito_id, articulo_id, importe) VALUES
(1, 1, 1299.99),
(2, 2, 1099.50),
(3, 3, 899.00),
(4, 4, 749.99),
(5, 5, 829.00);


-- Inserts para la tabla Metodos_pago
INSERT INTO metodos_pago (numTarjeta, cvv, mes_cad, anyo_cad, usuario) VALUES
('1234567890123456', '123', 12, 2025, 'user2025'),
('9876543210987654', '456', 11, 2024, 'javier_g'),
('8765432109876543', '789', 10, 2024, 'maria27'),
('5678901234567890', '321', 9, 2025, 'elena76'),
('4567890123456789', '654', 8, 2024, 'jose90'),
('3456789012345678', '987', 7, 2023, 'laura89');



-- Inserts para la tabla Lista_favoritos
INSERT INTO lista_favoritos (usuario_UName, articulo_id) VALUES
('user2025', 1),
('user2025', 2),
('javier_g', 3),
('maria27', 4),
('elena76', 5);