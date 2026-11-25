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

    Nombre VARCHAR(100) NOT NULL,

    Apellido VARCHAR(100) NOT NULL,

    Dni VARCHAR(20) NOT NULL UNIQUE,

    Usuario VARCHAR(50) NOT NULL UNIQUE,

    PasswordHash VARCHAR(255) NOT NULL,

    IdRol INT NOT NULL,

    Activo BIT NOT NULL DEFAULT 1,

    Codigo AS (
        CASE WHEN IdRol = 1 THEN 'A-' ELSE 'V-' END
        + RIGHT('000' + CAST(IdUsuario AS VARCHAR(10)), 3)
    ) PERSISTED,

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

    Telefono VARCHAR(30) NULL,

    Email VARCHAR(100) UNIQUE,

    Direccion VARCHAR(150) NULL,

    Activo BIT NOT NULL DEFAULT 1
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

    Telefono VARCHAR(30) NULL,

    Email VARCHAR(100) UNIQUE,

    Activo BIT NOT NULL DEFAULT 1
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

    IdProveedor INT NOT NULL,

    StockActual INT NOT NULL DEFAULT 0,

    StockMinimo INT NOT NULL DEFAULT 0,

    PorcentajeGanancia DECIMAL(5,2) NOT NULL,

    Activo BIT NOT NULL DEFAULT 1,

    CONSTRAINT CK_Productos_StockActual
        CHECK (StockActual >= 0),

    CONSTRAINT CK_Productos_StockMinimo
        CHECK (StockMinimo >= 0),

    CONSTRAINT CK_Productos_Ganancia
        CHECK (PorcentajeGanancia > 0),

    CONSTRAINT FK_Productos_Marcas
        FOREIGN KEY(IdMarca)
        REFERENCES Marcas(IdMarca),

    CONSTRAINT FK_Productos_Tipos
        FOREIGN KEY(IdTipo)
        REFERENCES Tipos(IdTipo),

    CONSTRAINT FK_Productos_Proveedor
        FOREIGN KEY(IdProveedor)
        REFERENCES Proveedores(IdProveedor)
);
GO

/*======================================================
    PRODUCTO PROVEEDOR
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
    INGRESOS STOCK
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

    IdUsuario INT NOT NULL,

    IdProveedor INT NOT NULL,

    TipoComprobante VARCHAR(20) NOT NULL DEFAULT 'FACTURA C',

    NumeroComprobante VARCHAR(30) NOT NULL DEFAULT '',

    Total DECIMAL(18,2) NOT NULL DEFAULT 0,

    CONSTRAINT FK_Compras_Usuarios
        FOREIGN KEY(IdUsuario)
        REFERENCES Usuarios(IdUsuario),

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

    Cantidad INT NOT NULL CHECK(Cantidad > 0),

    PrecioCompra DECIMAL(18,2) NOT NULL CHECK(PrecioCompra > 0),

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

    NumeroComprobante VARCHAR(20) NOT NULL,

    FechaVenta DATETIME NOT NULL DEFAULT GETDATE(),

    IdCliente INT NOT NULL,

    IdUsuario INT NOT NULL,

    Total DECIMAL(18,2) NOT NULL DEFAULT 0,

    TipoFactura VARCHAR(20) NOT NULL DEFAULT 'FACTURA C',

    FormaPago VARCHAR(30) NOT NULL DEFAULT 'EFECTIVO',

    CAE VARCHAR(20) NULL,

    VencimientoCAE DATE NULL,

    CONSTRAINT FK_Ventas_Clientes
        FOREIGN KEY(IdCliente)
        REFERENCES Clientes(IdCliente),

    CONSTRAINT FK_Ventas_Usuarios
        FOREIGN KEY(IdUsuario)
        REFERENCES Usuarios(IdUsuario)
);
GO

CREATE UNIQUE INDEX UX_Ventas_NumeroComprobante
    ON Ventas(NumeroComprobante)
    WHERE NumeroComprobante <> 'N/A';
GO

/*======================================================
    DETALLE VENTAS
======================================================*/

CREATE TABLE DetalleVentas
(
    IdDetalleVenta INT IDENTITY(1,1) PRIMARY KEY,

    IdVenta INT NOT NULL,

    IdProducto INT NOT NULL,

    Cantidad INT NOT NULL CHECK(Cantidad > 0),

    PrecioUnitario DECIMAL(18,2) NOT NULL CHECK(PrecioUnitario > 0),

    Subtotal DECIMAL(18,2) NOT NULL,

    CONSTRAINT FK_DetalleVentas_Venta
        FOREIGN KEY(IdVenta)
        REFERENCES Ventas(IdVenta),

    CONSTRAINT FK_DetalleVenta_Producto
        FOREIGN KEY(IdProducto)
        REFERENCES Productos(IdProducto)
);
GO

/*======================================================
    EMPRESA
======================================================*/

CREATE TABLE Empresa
(
    IdEmpresa INT IDENTITY(1,1) PRIMARY KEY,

    RazonSocial VARCHAR(100) NOT NULL,

    CUIT VARCHAR(20) NOT NULL,

    IngresosBrutos VARCHAR(30) NOT NULL,

    Direccion VARCHAR(150) NOT NULL,

    CondicionIVA VARCHAR(50) NOT NULL,

    PuntoVenta VARCHAR(10) NOT NULL,

    FechaInicioActividad DATE NOT NULL,

    Ciudad VARCHAR(100) NOT NULL
);
GO

/*======================================================
    DATOS INICIALES
======================================================*/

INSERT INTO Roles (Nombre)
VALUES
('Administrador'),
('Vendedor');
GO

INSERT INTO Usuarios
(
    Nombre,
    Apellido,
    Dni,
    Usuario,
    PasswordHash,
    IdRol
)
VALUES
(
    'JUAN',
    'PEREZ',
    '30000001',
    'jperez',
    'jperez123',
    1
),
(
    'ANA',
    'GOMEZ',
    '30000002',
    'agomez',
    'agomez123',
    2
),
(
    'LUCAS',
    'RUIZ',
    '30000003',
    'lruiz',
    'lruiz123',
    2
);
GO

INSERT INTO Marcas (Nombre)
VALUES
('NIKE'),
('ADIDAS'),
('PUMA'),
('LEVIS'),
('TOPPER');
GO

INSERT INTO Tipos (Nombre)
VALUES
('REMERAS'),
('PANTALONES'),
('BUZOS'),
('CAMPERAS'),
('SHORTS');
GO

INSERT INTO Clientes
(
    Nombre,
    Apellido,
    Dni,
    Telefono,
    Email,
    Direccion
)
VALUES
(
    'JUAN',
    'PEREZ',
    '30111222',
    '1144441111',
    'juan@gmail.com',
    'AV. SIEMPREVIVA 742, SAN ISIDRO'
),
(
    'MARIA',
    'GONZALEZ',
    '32999888',
    '1144442222',
    'maria@gmail.com',
    'AV. LIBERTADOR 1520, VICENTE LOPEZ'
);
GO

INSERT INTO Proveedores
(
    RazonSocial,
    Cuit,
    Telefono,
    Email
)
VALUES
(
    'TEXTIL ARGENTINA SRL',
    '30123456789',
    '1145551111',
    'ventas@textilargentina.com'
),
(
    'INDUMENTARIA CENTRO SA',
    '30987654321',
    '1145552222',
    'contacto@indumentariacentro.com'
);
GO

INSERT INTO Productos
(
    Codigo,
    Nombre,
    Talle,
    Color,
    IdMarca,
    IdTipo,
    IdProveedor,
    StockActual,
    StockMinimo,
    PorcentajeGanancia
)
VALUES
(
    'REM001',
    'REMERA DEPORTIVA NIKE',
    'L',
    'NEGRO',
    1,
    1,
    1,
    20,
    5,
    80
),
(
    'PAN001',
    'PANTALON ADIDAS',
    '42',
    'GRIS',
    2,
    2,
    1,
    15,
    5,
    70
),
(
    'BUZ001',
    'BUZO PUMA',
    'M',
    'AZUL',
    3,
    3,
    2,
    10,
    3,
    75
),
(
    'CAM001',
    'CAMPERA LEVIS',
    'XL',
    'NEGRO',
    4,
    4,
    2,
    7,
    2,
    85
),
(
    'REM002',
    'REMERA DEPORTIVA NIKE',
    'L',
    'BLANCA',
    1,
    1,
    1,
    10,
    5,
    15
);
GO

INSERT INTO ProductoProveedor
VALUES
(1,1),
(2,1),
(3,2),
(4,2),
(5,1);
GO

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
(4,7,35000),
(5,10,5000);
GO

INSERT INTO Compras
(
    IdUsuario,
    IdProveedor,
    TipoComprobante,
    NumeroComprobante,
    Total
)
VALUES
(
    1,
    1,
    'FACTURA C',
    '0001-00001234',
    100000
),
(
    1,
    1,
    'FACTURA C',
    '0001-00001235',
    80000
);
GO

INSERT INTO DetalleCompras
(
    IdCompra,
    IdProducto,
    Cantidad,
    PrecioCompra,
    Subtotal
)
VALUES
(
    1,
    1,
    10,
    5000,
    50000
),
(
    1,
    2,
    1,
    50000,
    50000
);
GO

/*======================================================
    INSERT EMPRESA
======================================================*/

INSERT INTO Empresa
(
    RazonSocial,
    CUIT,
    IngresosBrutos,
    Direccion,
    CondicionIVA,
    PuntoVenta,
    FechaInicioActividad,
    Ciudad
)
VALUES
(
    'Tienda de Ropa SRL',
    '30-71234567-8',
    '901-123456-7',
    'Av. Mitre 1234',
    'IVA RESPONSABLE INSCRIPTO',
    '0001',
    '2021-05-01',
    '(C1425ABC) CABA'
);
GO

/*======================================================
    INSERT VENTAS
======================================================*/

INSERT INTO Ventas
(
    NumeroComprobante,
    FechaVenta,
    IdCliente,
    IdUsuario,
    Total,
    TipoFactura,
    FormaPago,
    CAE,
    VencimientoCAE
)
VALUES
(
    '0001-00000001',
    GETDATE(),
    1,
    1,
    14400,
    'FACTURA B',
    'EFECTIVO',
    '202611220001',
    '2026-12-02'
),
(
    'N/A',
    GETDATE(),
    2,
    1,
    30600,
    'PRESUPUESTO',
    'TRANSFERENCIA',
    'N/A',
    NULL
);
GO

/*======================================================
    INSERT DETALLEVENTAS
======================================================*/

INSERT INTO DetalleVentas
(
    IdVenta,
    IdProducto,
    Cantidad,
    PrecioUnitario,
    Subtotal
)
VALUES
(
    1,
    1,
    1,
    18000,
    18000
),
(
    2,
    2,
    1,
    30600,
    30600
);
GO

SELECT 'TIENDA_ROPA_DB creada correctamente' AS Resultado;
GO

ALTER TABLE Usuarios
ADD Aprobado BIT NOT NULL DEFAULT 1;