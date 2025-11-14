-- Script de Creación de Base de Datos
-- Sistema de Logística de Envíos

use master
GO

-- Crear la base de datos
CREATE DATABASE LogisticaDB;
GO

USE LogisticaDB;
GO

-- Crear la entidad Cliente
CREATE TABLE Cliente (
    Cedula VARCHAR(15) PRIMARY KEY,
    Nombre VARCHAR(50),
    Direccion VARCHAR(100),
    Email VARCHAR(50)
);
GO

-- Crear la entidad Tipo de Producto
CREATE TABLE TipoProducto (
    TipoProductoID INT PRIMARY KEY,
    Nombre VARCHAR(50),
    Descripcion VARCHAR(300)
);
GO

-- Crear entidad Bodega
CREATE TABLE Bodega (
    BodegaID INT PRIMARY KEY,
    Ubicacion VARCHAR(50),
    Capacidad INT
);
GO

-- Crear entidad Puerto
CREATE TABLE Puerto (
    PuertoID INT PRIMARY KEY,
    Nombre VARCHAR(50),
    Ubicacion VARCHAR(50)
);
GO

-- Crear entidad Plan de Entrega
CREATE TABLE PlanDeEntrega (
    PlanID INT PRIMARY KEY,
    TipoProductoID INT,
    Cantidad INT,
    FechaRegistro DATE,
    FechaEntrega DATE,
    PrecioEnvio MONEY,
    NumeroGuia INT,
    ClienteCedula VARCHAR(15),
    FOREIGN KEY (TipoProductoID) REFERENCES TipoProducto(TipoProductoID),
    FOREIGN KEY (ClienteCedula) REFERENCES Cliente(Cedula)
);
GO

-- Crear entidad Envio Terrestre
CREATE TABLE EnvioTerrestre (
    EnvioTerrestreID INT PRIMARY KEY,
    PlacaVehiculo VARCHAR(6),
    BodegaEntregaID INT,
    PlanID INT,
    FOREIGN KEY (BodegaEntregaID) REFERENCES Bodega(BodegaID),
    FOREIGN KEY (PlanID) REFERENCES PlanDeEntrega(PlanID)
);
GO

-- Crear entidad Envio Maritimo
CREATE TABLE EnvioMaritimo (
    EnvioMaritimoID INT PRIMARY KEY,
    NumeroFlota VARCHAR(8),
    PuertoEntregaID INT,
    PlanID INT,
    FOREIGN KEY (PuertoEntregaID) REFERENCES Puerto(PuertoID),
    FOREIGN KEY (PlanID) REFERENCES PlanDeEntrega(PlanID)
);
GO

-- =====================================================
-- INSERCIÓN DE DATOS DE PRUEBA
-- =====================================================

-- Insertar Clientes
INSERT INTO Cliente (Cedula, Nombre, Direccion, Email) VALUES
('1029384756', 'Laura Gómez', 'Cra 45 #12-34, Medellín', 'laura.gomez@gmail.com'),
('1092837465', 'Carlos Martínez', 'Cl 80 #55-21, Bogotá', 'carlos.mtz@hotmail.com'),
('1002837466', 'Diana López', 'Av. Las Américas #102, Cali', 'diana.lopez@yahoo.com'),
('1039485723', 'Andrés Pérez', 'Cll 15 #23-40, Barranquilla', 'andres.perez@gmail.com'),
('1048573920', 'María Rodríguez', 'Carrera 9 #45-12, Bucaramanga', 'maria.rod@gmail.com');
GO

-- Insertar Tipos de Producto
INSERT INTO TipoProducto (TipoProductoID, Nombre, Descripcion) VALUES
(1, 'Electrodomésticos', 'Neveras, lavadoras, televisores y pequeños electrodomésticos'),
(2, 'Ropa y Calzado', 'Prendas de vestir, calzado deportivo y accesorios'),
(3, 'Alimentos', 'Productos perecederos y no perecederos de consumo masivo'),
(4, 'Material de Construcción', 'Cemento, ladrillos, acero y materiales pesados'),
(5, 'Tecnología', 'Equipos de cómputo, tablets y dispositivos móviles');
GO

-- Insertar Bodegas
INSERT INTO Bodega (BodegaID, Ubicacion, Capacidad) VALUES
(1, 'Zona Industrial de Itagüí', 500),
(2, 'Parque Logístico Siberia (Cundinamarca)', 800),
(3, 'Centro de Distribución Cali Sur', 600),
(4, 'Bodega Mamonal - Cartagena', 1000),
(5, 'Parque Industrial Bucaramanga', 400);
GO

-- Insertar Puertos
INSERT INTO Puerto (PuertoID, Nombre, Ubicacion) VALUES
(1, 'Puerto de Cartagena', 'Cartagena, Bolívar'),
(2, 'Puerto de Barranquilla', 'Barranquilla, Atlántico'),
(3, 'Puerto de Buenaventura', 'Buenaventura, Valle del Cauca'),
(4, 'Puerto de Santa Marta', 'Santa Marta, Magdalena'),
(5, 'Puerto de Tumaco', 'Tumaco, Nariño');
GO

-- Insertar Planes de Entrega
INSERT INTO PlanDeEntrega (PlanID, TipoProductoID, Cantidad, FechaRegistro, FechaEntrega, PrecioEnvio, NumeroGuia, ClienteCedula) VALUES
(1, 1, 25, '2025-01-10', '2025-01-18', 320000, 458923, '1029384756'),
(2, 2, 60, '2025-02-05', '2025-02-10', 150000, 458924, '1092837465'),
(3, 3, 200, '2025-03-02', '2025-03-05', 210000, 458925, '1002837466'),
(4, 4, 15, '2025-04-12', '2025-04-22', 800000, 458926, '1039485723'),
(5, 5, 40, '2025-05-15', '2025-05-22', 450000, 458927, '1048573920');
GO

-- Insertar Envíos Terrestres
INSERT INTO EnvioTerrestre (EnvioTerrestreID, PlacaVehiculo, BodegaEntregaID, PlanID) VALUES
(1, 'JKL245', 1, 1),
(2, 'MNO367', 2, 2),
(3, 'PQR489', 3, 3),
(4, 'STU512', 4, 4),
(5, 'VWX634', 5, 5);
GO

-- Insertar Envíos Marítimos
INSERT INTO EnvioMaritimo (EnvioMaritimoID, NumeroFlota, PuertoEntregaID, PlanID) VALUES
(1, 'FLTA001', 1, 1),
(2, 'FLTA002', 2, 2),
(3, 'FLTA003', 3, 3),
(4, 'FLTA004', 4, 4),
(5, 'FLTA005', 5, 5);
GO
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
