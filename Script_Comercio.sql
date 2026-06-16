CREATE DATABASE ComercioFrgp;
GO

USE ComercioFrgp;
GO

CREATE TABLE Usuarios (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    TipoUsuario VARCHAR(50) NOT NULL,
    Estado BIT DEFAULT 1
);

CREATE TABLE Clientes (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    DNI VARCHAR(20) UNIQUE NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    Telefono VARCHAR(50) NULL,
    FechaRegistro DATETIME DEFAULT GETDATE(),
    Estado BIT DEFAULT 1
);

CREATE TABLE Categorias (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(255) NOT NULL,
    CategoriaPadre_ID INT NULL,
    FOREIGN KEY (CategoriaPadre_ID) REFERENCES Categorias(ID)
);

CREATE TABLE Marcas (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(255) NOT NULL
);

CREATE TABLE Proveedores (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(255) NOT NULL,
    Telefono VARCHAR(50) NULL,
    Descripcion TEXT NULL,
    Estado BIT DEFAULT 1
);

CREATE TABLE Productos (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(100) UNIQUE NOT NULL,
    Nombre VARCHAR(255) NOT NULL,
    Descripcion TEXT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    PorcentajeGanancia DECIMAL(5,2) NOT NULL,
    StockActual INT DEFAULT 0 NOT NULL,
    StockMinimo INT DEFAULT 0 NOT NULL,
    Categoria_ID INT NOT NULL,
    Marca_ID INT NOT NULL,
    Estado BIT DEFAULT 1,
    FOREIGN KEY (Categoria_ID) REFERENCES Categorias(ID),
    FOREIGN KEY (Marca_ID) REFERENCES Marcas(ID)
);

CREATE TABLE Productos_Proveedores (
    Producto_ID INT NOT NULL,
    Proveedor_ID INT NOT NULL,
    PRIMARY KEY (Producto_ID, Proveedor_ID),
    FOREIGN KEY (Producto_ID) REFERENCES Productos(ID),
    FOREIGN KEY (Proveedor_ID) REFERENCES Proveedores(ID)
);

CREATE TABLE Ventas (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NumeroFactura VARCHAR(100) UNIQUE NOT NULL,
    FechaVenta DATETIME DEFAULT GETDATE() NOT NULL,
    Estado VARCHAR(50) DEFAULT 'Pendiente' NOT NULL,
    Total DECIMAL(10,2) NOT NULL,
    Cliente_ID INT NOT NULL, 
    Usuario_ID INT NOT NULL,
    FOREIGN KEY (Cliente_ID) REFERENCES Clientes(ID),
    FOREIGN KEY (Usuario_ID) REFERENCES Usuarios(ID)
);

CREATE TABLE Detalles_Venta (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Venta_ID INT NOT NULL,
    Producto_ID INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (Venta_ID) REFERENCES Ventas(ID),
    FOREIGN KEY (Producto_ID) REFERENCES Productos(ID)
);

CREATE TABLE Compras (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Proveedor_ID INT NOT NULL,
    Usuario_ID INT NOT NULL,
    FechaCompra DATETIME DEFAULT GETDATE() NOT NULL,
    Total DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (Proveedor_ID) REFERENCES Proveedores(ID),
    FOREIGN KEY (Usuario_ID) REFERENCES Usuarios(ID)
);

CREATE TABLE Detalles_Compra (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Compra_ID INT NOT NULL,
    Producto_ID INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioCosto DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (Compra_ID) REFERENCES Compras(ID),
    FOREIGN KEY (Producto_ID) REFERENCES Productos(ID)
);

INSERT INTO Usuarios (Username, PasswordHash, TipoUsuario) VALUES 
('admin', 'hash_admin_123', 'Administrador'),
('vendedor1', 'hash_vend_456', 'Vendedor');

INSERT INTO Clientes (DNI, Nombre, Apellido, Email, Telefono) VALUES 
('35123456', 'Francisco', 'Benitez', 'fran.benitez@email.com', '1145678901'),
('40123456', 'Belen', 'Gomez', 'belen.gomez@email.com', '1123456789');

INSERT INTO Categorias (Nombre, CategoriaPadre_ID) VALUES 
('Componentes PC', NULL),
('Placas de Video', 1),
('Procesadores', 1),
('Perifericos', NULL);

INSERT INTO Marcas (Nombre) VALUES 
('AMD'),
('NVIDIA'),
('ASUS');

INSERT INTO Proveedores (Nombre, Telefono, Descripcion) VALUES 
('Nexolibre Distri Hardware', '+54 9 11 4455-6677', 'Distribuidor mayorista principal de componentes'),
('Importadora Gaming Sur', '+54 9 11 9988-7766', 'Proveedor secundario, mejores tiempos de entrega');

INSERT INTO Productos (Codigo, Nombre, Descripcion, Precio, PorcentajeGanancia, StockActual, StockMinimo, Categoria_ID, Marca_ID) VALUES 
('VGA-RTX4060', 'Placa de Video ASUS Dual GeForce RTX 4060', '8GB GDDR6, ideal 1080p', 350000.00, 30.00, 15, 5, 2, 3),
('CPU-R55600', 'Procesador AMD Ryzen 5 5600', '6 Nucleos 12 Hilos, AM4', 150000.00, 25.00, 32, 10, 3, 1);

INSERT INTO Productos_Proveedores (Producto_ID, Proveedor_ID) VALUES 
(1, 1),
(1, 2),
(2, 1);

INSERT INTO Ventas (NumeroFactura, Estado, Total, Cliente_ID, Usuario_ID) VALUES 
('FAC-638066601500000000', 'Pagado', 350000.00, 1, 2);

INSERT INTO Detalles_Venta (Venta_ID, Producto_ID, Cantidad, PrecioUnitario) VALUES 
(1, 1, 1, 350000.00);

INSERT INTO Compras (Proveedor_ID, Usuario_ID, Total) VALUES 
(1, 1, 880000.00);

INSERT INTO Detalles_Compra (Compra_ID, Producto_ID, Cantidad, PrecioCosto) VALUES 
(1, 1, 2, 290000.00),
(1, 2, 2, 150000.00);