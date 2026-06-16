CREATE DATABASE ComercioFrgp;
GO

USE ComercioFrgp;
GO

CREATE TABLE Usuarios (
    ID INT PRIMARY KEY,
    Nombre VARCHAR(255),
    Apellido VARCHAR(255),
    Email VARCHAR(255),
    PasswordHash VARCHAR(255),
    Telefono VARCHAR(50),
    FechaRegistro DATE
);

CREATE TABLE Categorias (
    ID INT PRIMARY KEY,
    Nombre VARCHAR(255),
    CategoriaPadre_ID INT NULL,
    FOREIGN KEY (CategoriaPadre_ID) REFERENCES Categorias(ID)
);

CREATE TABLE Marcas (
    ID INT PRIMARY KEY,
    Nombre VARCHAR(255)
);

CREATE TABLE Proveedor (
    ID INT PRIMARY KEY,
    Nombre VARCHAR(255),
    Telefono VARCHAR(50),
    Descripcion TEXT
);

CREATE TABLE Productos (
    ID INT PRIMARY KEY,
    CodigoSKU VARCHAR(100),
    Nombre VARCHAR(255),
    Descripcion TEXT,
    PrecioEspecial DECIMAL(10,2),
    PrecioLista DECIMAL(10,2),
    StockFisico INT,
    Categoria_ID INT,
    Marca_ID INT,
    Proveedor_ID INT,
    FOREIGN KEY (Categoria_ID) REFERENCES Categorias(ID),
    FOREIGN KEY (Marca_ID) REFERENCES Marcas(ID),
    FOREIGN KEY (Proveedor_ID) REFERENCES Proveedor(ID)
);

CREATE TABLE Ventas (
    ID INT PRIMARY KEY,
    FechaVenta DATETIME,
    Estado VARCHAR(50),
    Total DECIMAL(10,2),
    Usuario_ID INT,
    FOREIGN KEY (Usuario_ID) REFERENCES Usuarios(ID)
);

CREATE TABLE Detalles_Venta (
    ID INT PRIMARY KEY,
    Venta_ID INT,
    Producto_ID INT,
    Cantidad INT,
    PrecioUnitario DECIMAL(10,2),
    FOREIGN KEY (Venta_ID) REFERENCES Ventas(ID),
    FOREIGN KEY (Producto_ID) REFERENCES Productos(ID)
);

CREATE TABLE Compras (
    ID INT PRIMARY KEY,
    Proveedor_ID INT,
    FechaCompra DATETIME,
    Total DECIMAL(10,2),
    FOREIGN KEY (Proveedor_ID) REFERENCES Proveedor(ID)
);

CREATE TABLE Detalles_Compra (
    ID INT PRIMARY KEY,
    Compra_ID INT,
    Producto_ID INT,
    Cantidad INT,
    PrecioCosto DECIMAL(10,2),
    FOREIGN KEY (Compra_ID) REFERENCES Compras(ID),
    FOREIGN KEY (Producto_ID) REFERENCES Productos(ID)
);


INSERT INTO Usuarios (ID, Nombre, Apellido, Email, PasswordHash, Telefono, FechaRegistro) VALUES 
(1, 'Francisco', 'Benitez', 'fran.benitez@email.com', 'hashedpwd_8291', '1145678901', '2026-01-15'),
(2, 'Belen', 'Gomez', 'belen.gomez@email.com', 'hashedpwd_3321', '1123456789', '2026-03-20');

INSERT INTO Categorias (ID, Nombre, CategoriaPadre_ID) VALUES 
(1, 'Componentes PC', NULL),
(2, 'Placas de Video', 1),
(3, 'Procesadores', 1),
(4, 'Perifericos', NULL);

INSERT INTO Marcas (ID, Nombre) VALUES 
(1, 'AMD'), 
(2, 'NVIDIA'), 
(3, 'ASUS');

INSERT INTO Proveedor (ID, Nombre, Telefono, Descripcion) VALUES 
(1, 'Nexolibre Distri Hardware', '+54 9 11 4455-6677', 'Distribuidor mayorista principal de componentes'),
(2, 'Importadora Gaming Sur', '+54 9 11 9988-7766', 'Proveedor secundario, mejores tiempos de entrega');

INSERT INTO Productos (ID, CodigoSKU, Nombre, Descripcion, PrecioEspecial, PrecioLista, StockFisico, Categoria_ID, Marca_ID, Proveedor_ID) VALUES 
(1, 'VGA-RTX4060', 'Placa de Video ASUS Dual GeForce RTX 4060', '8GB GDDR6, ideal 1080p', 350000.00, 385000.00, 15, 2, 3, 1),
(2, 'CPU-R55600', 'Procesador AMD Ryzen 5 5600', '6 Nucleos 12 Hilos, AM4', 185000.00, 205000.00, 32, 3, 1, 1);

INSERT INTO Ventas (ID, FechaVenta, Estado, Total, Usuario_ID) VALUES 
(1, '2026-06-03 10:15:00', 'Pagado', 350000.00, 1);

INSERT INTO Detalles_Venta (ID, Venta_ID, Producto_ID, Cantidad, PrecioUnitario) VALUES 
(1, 1, 1, 1, 350000.00);

INSERT INTO Compras (ID, Proveedor_ID, FechaCompra, Total) VALUES 
(1, 1, '2026-05-15 09:30:00', 880000.00);

INSERT INTO Detalles_Compra (ID, Compra_ID, Producto_ID, Cantidad, PrecioCosto) VALUES 
(1, 1, 1, 2, 290000.00),
(2, 1, 2, 2, 150000.00);

