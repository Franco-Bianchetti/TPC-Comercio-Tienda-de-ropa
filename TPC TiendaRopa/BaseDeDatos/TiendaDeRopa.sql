USE master;
GO

IF DB_ID('TIENDA_ROPA_DB') IS NOT NULL
BEGIN
    ALTER DATABASE TIENDA_ROPA_DB
    SET SINGLE_USER
    WITH ROLLBACK IMMEDIATE;

    DROP DATABASE TIENDA_ROPA_DB;
END
GO

CREATE DATABASE TIENDA_ROPA_DB;
GO

USE TIENDA_ROPA_DB;
GO

/*======================================================
    TABLAS MAESTRAS
======================================================*/

CREATE TABLE Roles
(
    IdRol INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL UNIQUE
);
GO

CREATE TABLE Marcas
(
    IdMarca INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL UNIQUE,
    Activo BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Tipos
(
    IdTipo INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL UNIQUE,
    Activo BIT NOT NULL DEFAULT 1
);
GO

/*======================================================
    USUARIOS
======================================================*/

CREATE TABLE Usuarios
(
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,

    Usuario VARCHAR(50) NOT NULL UNIQUE,

    PasswordHash VARCHAR(255) NOT NULL,

    IdRol INT NOT NULL,

    Activo BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_Usuarios_Roles
        FOREIGN KEY(IdRol)
        REFERENCES Roles(IdRol)
);
GO

/*======================================================
    CLIENTES
======================================================*/

CREATE TABLE Clientes
(
    IdCliente INT IDENTITY(1,1) PRIMARY KEY,

    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,

    Dni VARCHAR(20) NOT NULL UNIQUE,

    Telefono VARCHAR(30),
    Email VARCHAR(100)
);
GO

/*======================================================
    PROVEEDORES
======================================================*/

CREATE TABLE Proveedores
(
    IdProveedor INT IDENTITY(1,1) PRIMARY KEY,

    RazonSocial VARCHAR(150) NOT NULL,

    Cuit VARCHAR(20) UNIQUE,

    Telefono VARCHAR(30),
    Email VARCHAR(100)
);
GO

/*======================================================
    PRODUCTOS
======================================================*/

CREATE TABLE Productos
(
    IdProducto INT IDENTITY(1,1) PRIMARY KEY,

    Codigo VARCHAR(30) NOT NULL UNIQUE,

    Nombre VARCHAR(150) NOT NULL,

    Talle VARCHAR(20) NOT NULL,

    Color VARCHAR(50) NOT NULL,

    IdMarca INT NOT NULL,

    IdTipo INT NOT NULL,

    StockActual INT NOT NULL DEFAULT 0,

    StockMinimo INT NOT NULL DEFAULT 0,

    PorcentajeGanancia DECIMAL(5,2) NOT NULL,

    Activo BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_Productos_Marcas
        FOREIGN KEY(IdMarca)
        REFERENCES Marcas(IdMarca),

    CONSTRAINT FK_Productos_Tipos
        FOREIGN KEY(IdTipo)
        REFERENCES Tipos(IdTipo)
);
GO

/*======================================================
    PRODUCTO - PROVEEDOR
======================================================*/

CREATE TABLE ProductoProveedor
(
    IdProducto INT NOT NULL,
    IdProveedor INT NOT NULL,

    PRIMARY KEY(IdProducto, IdProveedor),

    CONSTRAINT FK_PP_Producto
        FOREIGN KEY(IdProducto)
        REFERENCES Productos(IdProducto),

    CONSTRAINT FK_PP_Proveedor
        FOREIGN KEY(IdProveedor)
        REFERENCES Proveedores(IdProveedor)
);
GO

/*======================================================
    INGRESOS DE STOCK
======================================================*/

CREATE TABLE IngresosStock
(
    IdIngreso INT IDENTITY(1,1) PRIMARY KEY,

    IdProducto INT NOT NULL,

    Fecha DATETIME NOT NULL DEFAULT GETDATE(),

    Cantidad INT NOT NULL,

    PrecioCompra DECIMAL(18,2) NOT NULL,

    CONSTRAINT FK_IngresosStock_Productos
        FOREIGN KEY(IdProducto)
        REFERENCES Productos(IdProducto)
);
GO

/*======================================================
    COMPRAS
======================================================*/

CREATE TABLE Compras
(
    IdCompra INT IDENTITY(1,1) PRIMARY KEY,

    FechaCompra DATETIME NOT NULL DEFAULT GETDATE(),

    IdProveedor INT NOT NULL,

    Total DECIMAL(18,2) NOT NULL DEFAULT 0,

    CONSTRAINT FK_Compras_Proveedor
        FOREIGN KEY(IdProveedor)
        REFERENCES Proveedores(IdProveedor)
);
GO

CREATE TABLE DetalleCompras
(
    IdDetalleCompra INT IDENTITY(1,1) PRIMARY KEY,

    IdCompra INT NOT NULL,
    IdProducto INT NOT NULL,

    Cantidad INT NOT NULL,

    PrecioCompra DECIMAL(18,2) NOT NULL,

    Subtotal DECIMAL(18,2) NOT NULL,

    CONSTRAINT FK_DetalleCompra_Compra
        FOREIGN KEY(IdCompra)
        REFERENCES Compras(IdCompra),

    CONSTRAINT FK_DetalleCompra_Producto
        FOREIGN KEY(IdProducto)
        REFERENCES Productos(IdProducto)
);
GO

/*======================================================
    VENTAS
======================================================*/

CREATE TABLE Ventas
(
    IdVenta INT IDENTITY(1,1) PRIMARY KEY,

    NumeroFactura VARCHAR(20) NOT NULL UNIQUE,

    FechaVenta DATETIME NOT NULL DEFAULT GETDATE(),

    IdCliente INT NOT NULL,

    IdUsuario INT NOT NULL,

    Total DECIMAL(18,2) NOT NULL DEFAULT 0,

    CONSTRAINT FK_Ventas_Clientes
        FOREIGN KEY(IdCliente)
        REFERENCES Clientes(IdCliente),

    CONSTRAINT FK_Ventas_Usuarios
        FOREIGN KEY(IdUsuario)
        REFERENCES Usuarios(IdUsuario)
);
GO

CREATE TABLE DetalleVentas
(
    IdDetalleVenta INT IDENTITY(1,1) PRIMARY KEY,

    IdVenta INT NOT NULL,
    IdProducto INT NOT NULL,

    Cantidad INT NOT NULL,

    PrecioUnitario DECIMAL(18,2) NOT NULL,

    Subtotal DECIMAL(18,2) NOT NULL,

    CONSTRAINT FK_DetalleVenta_Venta
        FOREIGN KEY(IdVenta)
        REFERENCES Ventas(IdVenta),

    CONSTRAINT FK_DetalleVenta_Producto
        FOREIGN KEY(IdProducto)
        REFERENCES Productos(IdProducto)
);
GO

/*======================================================
    DATOS INICIALES
======================================================*/

INSERT INTO Roles (Nombre)
VALUES
('Administrador'),
('Vendedor');

INSERT INTO Usuarios
(
    Usuario,
    PasswordHash,
    IdRol
)
VALUES
(
    'admin',
    'admin123',
    1
),
(
    'vendedor',
    'vendedor123',
    2
);

INSERT INTO Marcas (Nombre)
VALUES
('Nike'),
('Adidas'),
('Puma'),
('Levis'),
('Topper');

INSERT INTO Tipos (Nombre)
VALUES
('Remeras'),
('Pantalones'),
('Buzos'),
('Camperas'),
('Shorts');

INSERT INTO Clientes
(
    Nombre,
    Apellido,
    Dni,
    Telefono,
    Email
)
VALUES
(
    'Juan',
    'Perez',
    '30111222',
    '1144441111',
    'juan@gmail.com'
),
(
    'Maria',
    'Gonzalez',
    '32999888',
    '1144442222',
    'maria@gmail.com'
);

INSERT INTO Proveedores
(
    RazonSocial,
    Cuit,
    Telefono,
    Email
)
VALUES
(
    'Textil Argentina SRL',
    '30123456789',
    '1145551111',
    'ventas@textilargentina.com'
),
(
    'Indumentaria Centro SA',
    '30987654321',
    '1145552222',
    'contacto@indumentariacentro.com'
);

INSERT INTO Productos
(
    Codigo,
    Nombre,
    Talle,
    Color,
    IdMarca,
    IdTipo,
    StockActual,
    StockMinimo,
    PorcentajeGanancia
)
VALUES
(
    'REM001',
    'Remera Deportiva Nike',
    'L',
    'Negro',
    1,
    1,
    20,
    5,
    80
),
(
    'PAN001',
    'Pantalon Adidas',
    '42',
    'Gris',
    2,
    2,
    15,
    5,
    70
),
(
    'BUZ001',
    'Buzo Puma',
    'M',
    'Azul',
    3,
    3,
    10,
    3,
    75
),
(
    'CAM001',
    'Campera Levis',
    'XL',
    'Negro',
    4,
    4,
    7,
    2,
    85
);

INSERT INTO ProductoProveedor
(
    IdProducto,
    IdProveedor
)
VALUES
(1,1),
(2,1),
(3,2),
(4,2);

INSERT INTO IngresosStock
(
    IdProducto,
    Cantidad,
    PrecioCompra
)
VALUES
(1,20,10000),
(2,15,18000),
(3,10,22000),
(4,7,35000);

SELECT 'TIENDA_ROPA_DB creada correctamente' AS Resultado;
GO