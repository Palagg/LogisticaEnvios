-- Script de Creación de Base de Datos
-- Sistema de Logística de Envíos

-- Crear la base de datos
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'LogisticaDB')
BEGIN
    CREATE DATABASE LogisticaDB;
END
GO

USE LogisticaDB;
GO

-- Tabla Clientes
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Cliente')
BEGIN
    CREATE TABLE Cliente (
        Cedula NVARCHAR(15) PRIMARY KEY,
        Nombre NVARCHAR(50) NOT NULL,
        Direccion NVARCHAR(100),
        Email NVARCHAR(50)
    );
END
GO

-- Tabla Bodega
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Bodega')
BEGIN
    CREATE TABLE Bodega (
        BodegaID INT PRIMARY KEY IDENTITY(1,1),
        Ubicacion NVARCHAR(50) NOT NULL,
        Capacidad INT NOT NULL
    );
END
GO

-- Tabla Puerto
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Puerto')
BEGIN
    CREATE TABLE Puerto (
        PuertoID INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(20) NOT NULL,
        Ubicacion NVARCHAR(50) NOT NULL
    );
END
GO

-- Tabla TipoProducto
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TipoProducto')
BEGIN
    CREATE TABLE TipoProducto (
        TipoProductoID INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(50) NOT NULL,
        Descripcion NVARCHAR(300)
    );
END
GO

-- Tabla PlanDeEntrega
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PlanDeEntrega')
BEGIN
    CREATE TABLE PlanDeEntrega (
        PlanID INT PRIMARY KEY IDENTITY(1,1),
        TipoProductoID INT NOT NULL,
        Cantidad INT NOT NULL,
        FechaRegistro DATE NOT NULL,
        FechaEntrega DATE NOT NULL,
        PrecioEnvio DECIMAL(18,2) NOT NULL,
        NumeroGuia INT NOT NULL,
        ClienteCedula NVARCHAR(15) NOT NULL,
        FOREIGN KEY (TipoProductoID) REFERENCES TipoProducto(TipoProductoID),
        FOREIGN KEY (ClienteCedula) REFERENCES Cliente(Cedula)
    );
END
GO

-- Tabla EnvioTerrestre
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EnvioTerrestre')
BEGIN
    CREATE TABLE EnvioTerrestre (
        EnvioTerrestreID INT PRIMARY KEY IDENTITY(1,1),
        PlacaVehiculo NVARCHAR(6) NOT NULL,
        BodegaEntregaID INT NOT NULL,
        PlanID INT NOT NULL,
        FOREIGN KEY (BodegaEntregaID) REFERENCES Bodega(BodegaID),
        FOREIGN KEY (PlanID) REFERENCES PlanDeEntrega(PlanID)
    );
END
GO

-- Tabla EnvioMaritimo
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EnvioMaritimo')
BEGIN
    CREATE TABLE EnvioMaritimo (
        EnvioMaritimoID INT PRIMARY KEY IDENTITY(1,1),
        NumeroFlota NVARCHAR(8) NOT NULL,
        PuertoEntregaID INT NOT NULL,
        PlanID INT NOT NULL,
        FOREIGN KEY (PuertoEntregaID) REFERENCES Puerto(PuertoID),
        FOREIGN KEY (PlanID) REFERENCES PlanDeEntrega(PlanID)
    );
END
GO

-- Datos de prueba

-- Clientes
INSERT INTO Cliente (Cedula, Nombre, Direccion, Email) VALUES
('1234567890', 'Juan Pérez', 'Calle 1 #10-20', 'juan.perez@email.com'),
('0987654321', 'María García', 'Carrera 5 #20-30', 'maria.garcia@email.com'),
('1122334455', 'Carlos López', 'Avenida 7 #15-25', 'carlos.lopez@email.com');
GO

-- Bodegas
INSERT INTO Bodega (Ubicacion, Capacidad) VALUES
('Zona Industrial Norte', 5000),
('Zona Industrial Sur', 3000),
('Puerto Principal', 7000);
GO

-- Puertos
INSERT INTO Puerto (Nombre, Ubicacion) VALUES
('Puerto Atlántico', 'Costa Atlántica'),
('Puerto Pacífico', 'Costa Pacífica'),
('Puerto Central', 'Zona Central');
GO

-- Tipos de Producto
INSERT INTO TipoProducto (Nombre, Descripcion) VALUES
('Electrónicos', 'Productos electrónicos y tecnológicos'),
('Alimentos', 'Productos alimenticios perecederos y no perecederos'),
('Textiles', 'Ropa y productos textiles');
GO

-- Planes de Entrega (requiere que existan clientes y tipos de producto)
INSERT INTO PlanDeEntrega (TipoProductoID, Cantidad, FechaRegistro, FechaEntrega, PrecioEnvio, NumeroGuia, ClienteCedula) VALUES
(1, 100, '2025-01-01', '2025-01-15', 250000.00, 1001, '1234567890'),
(2, 50, '2025-01-05', '2025-01-10', 150000.00, 1002, '0987654321'),
(3, 200, '2025-01-10', '2025-01-25', 300000.00, 1003, '1122334455');
GO

-- Envíos Terrestres
INSERT INTO EnvioTerrestre (PlacaVehiculo, BodegaEntregaID, PlanID) VALUES
('ABC123', 1, 1),
('XYZ789', 2, 2);
GO

-- Envíos Marítimos
INSERT INTO EnvioMaritimo (NumeroFlota, PuertoEntregaID, PlanID) VALUES
('FLT-001', 1, 3);
GO

PRINT 'Base de datos creada e inicializada exitosamente';
GO
