CREATE DATABASE TIENDA_ROPA_DB;
GO
USE TIENDA_ROPA_DB;
GO
CREATE TABLE Marcas (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(50) NOT NULL
);
CREATE TABLE Tipos (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(50) NOT NULL
);
INSERT INTO Marcas (Nombre) VALUES ('Oversize'), ('Casual'), ('Kids'), ('Deporte'), ('Basica');
INSERT INTO Tipos (Nombre) VALUES ('Remeras'), ('Pantalones'), ('Buzos'), ('Camperas'), ('Shorts');