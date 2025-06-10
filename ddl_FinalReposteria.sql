CREATE DATABASE FinalReposteria;

GO
USE [master];
GO
CREATE LOGIN  [usrmielva] WITH PASSWORD = N'123456',
	DEFAULT_DATABASE = [FinalReposteria],
	CHECK_EXPIRATION = OFF,
	CHECK_POLICY = ON

USE FinalReposteria
GO
DROP USER [usrmielva]
GO

DROP LOGIN [usrmielva]


GO
USE [FinalReposteria]
GO
CREATE USER usrmielva FOR LOGIN usrmielva
GO
ALTER ROLE [db_owner] ADD MEMBER usrmielva
GO

DROP TABLE VentaDetalle;
DROP TABLE Venta;
DROP TABLE Cliente;
DROP TABLE Usuario;
DROP TABLE Empleado;
DROP TABLE Cargo;
DROP TABLE Producto;
DROP TABLE UnidadMedida;
DROP PROC paProductoListar;
DROP PROC paEmpleadoListar;
DROP PROC paClienteListar;
DROP PROC paVentaClienteListar;
DROP PROC paVentaListar;
DROP PROC paProductoVentaListar;


CREATE TABLE UnidadMedida(
	  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	  descripcion VARCHAR(20) NOT NULL UNIQUE
);

CREATE TABLE Producto (
	  id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	  idUnidadMedida INT NOT NULL,
	  codigo VARCHAR(20) NOT NULL,
	  descripcion VARCHAR(250) NOT NULL,
	  --unidadMedida VARCHAR(20) NOT NULL,
	  saldo DECIMAL NOT NULL DEFAULT 0,
	  precioVenta DECIMAL (10,2) NOT NULL CHECK (precioVenta > 0)
	  CONSTRAINT fk_Producto_UnidadMedida FOREIGN KEY(idUnidadMedida) REFERENCES UnidadMedida(id)
);

CREATE TABLE Cargo (
	  id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	  descripcion VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Empleado (
	  id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	  idCargo INT NOT NULL,
	  cedulaIdentidad VARCHAR(12) NOT NULL,
	  nombres VARCHAR(30) NOT NULL,
	  primerApellido VARCHAR(30) NULL,
	  segundoApellido VARCHAR(30) NULL,
	  direccion VARCHAR(250) NOT NULL,
	  celular BIGINT NOT NULL,
	  CONSTRAINT FK_Empleado_Cargo FOREIGN KEY (idCargo) REFERENCES Cargo(id)
);

CREATE TABLE Usuario (
	  id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	  idEmpleado INT NOT NULL,
	  usuario VARCHAR(20) NOT NULL,
	  clave VARCHAR(250) NOT NULL,
	  CONSTRAINT FK_Usuario_Empleado FOREIGN KEY (idEmpleado) REFERENCES Empleado(id)
);

CREATE TABLE Cliente (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	nit VARCHAR(14) NOT NULL UNIQUE,
	razonSocial VARCHAR(100) NOT NULL,
	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1
);

CREATE TABLE Venta (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	idUsuario INT NOT NULL,
	idCliente INT NOT NULL,
	transaccion INT NOT NULL,
	fecha DATE NOT NULL DEFAULT GETDATE(),
	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1,
	CONSTRAINT fk_Venta_Usuario FOREIGN KEY(idUsuario) REFERENCES Usuario(id),
	CONSTRAINT fk_Venta_Cliente FOREIGN KEY(idCliente) REFERENCES Cliente(id)
);

CREATE TABLE VentaDetalle (
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	idVenta INT NOT NULL,
	idProducto INT NOT NULL,
	cantidad DECIMAL NOT NULL CHECK (cantidad > 0),
	precioUnitario DECIMAL(10,2) NOT NULL,
	total DECIMAL(10,2) NOT NULL,
	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1,
	CONSTRAINT fk_VentaDetalle_Producto FOREIGN KEY(idProducto) REFERENCES Producto(id),
	CONSTRAINT fk_VentaDetalle_Venta FOREIGN KEY(idVenta) REFERENCES Venta(id)
	);

ALTER TABLE UnidadMedida ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE UnidadMedida ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE UnidadMedida ADD estado SMALLINT NOT NULL DEFAULT 1; -- -1:Eliminado, 0: Inactivo, 1: Activo

ALTER TABLE Producto ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Producto ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Producto ADD estado SMALLINT NOT NULL DEFAULT 1; -- 1: Activo, 0: Inactivo, -1: Eliminado

ALTER TABLE Empleado ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Empleado ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Empleado ADD estado SMALLINT NOT NULL DEFAULT 1; -- 1: Activo, 0: Inactivo, -1: Eliminado

ALTER TABLE Usuario ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Usuario ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Usuario ADD estado SMALLINT NOT NULL DEFAULT 1; -- 1: Activo, 0: Inactivo, -1: Eliminado

ALTER TABLE Cargo ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Cargo ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Cargo ADD estado SMALLINT NOT NULL DEFAULT 1; -- 1: Activo, 0: Inactivo, -1: Eliminado

--********************************************************
GO
CREATE PROC paProductoListar @parametro VARCHAR(100)
AS
  SELECT p.id, p.codigo, p.descripcion, um.descripcion AS unidadMedida, p.saldo, p.precioVenta,
		 p.usuarioRegistro, p.fechaRegistro, p.estado
  FROM Producto p
  INNER JOIN UnidadMedida um ON um.id = p.idUnidadMedida
  WHERE p.estado<>-1 AND p.codigo+p.descripcion+um.descripcion LIKE '%'+REPLACE(@parametro,' ','%')+'%'
  ORDER BY p.estado DESC, p.descripcion ASC;

GO
CREATE PROC paEmpleadoListar @parametro VARCHAR(100)
AS
  SELECT ISNULL(u.usuario,'--') AS usuario,e.id, e.cedulaIdentidad, e.nombres, e.primerApellido, e.segundoApellido, c.descripcion AS cargo,
		 e.direccion, e.celular, e.usuarioRegistro, e.fechaRegistro, e.estado
  FROM Empleado e
  LEFT JOIN Usuario u ON e.id = u.idEmpleado
  INNER JOIN Cargo c ON c.id = e.idCargo
  WHERE e.estado<>-1 
	AND e.cedulaIdentidad+e.nombres+c.descripcion+ISNULL(e.primerApellido,'')+ISNULL(e.segundoApellido,'') LIKE '%'+REPLACE(@parametro,' ','%')+'%'
  ORDER BY e.estado DESC, e.nombres ASC, e.primerApellido ASC;

GO
CREATE PROC paClienteListar @parametro VARCHAR(100)
AS
SELECT *
FROM Cliente
WHERE estado = 1
  AND (CAST(nit AS VARCHAR(20)) + razonSocial)
      LIKE '%' + REPLACE(@parametro, ' ', '%') + '%'
ORDER BY estado DESC, razonSocial ASC;
	SELECT * FROM Cliente
	WHERE estado = 1 AND CAST(nit AS VARCHAR) + razonSocial LIKE
	'%'+REPLACE(@parametro, ' ','%')+'%'
	ORDER BY estado DESC, razonSocial ASC;

GO
CREATE PROC paVentaClienteListar @nit VARCHAR(14)
AS
SELECT *
FROM Cliente
WHERE estado = 1
  AND nit = @nit
ORDER BY estado DESC, razonSocial ASC;

GO
CREATE PROC paVentaListar @parametro VARCHAR(100)
AS
SELECT 
    v.id AS idVenta,
    v.transaccion,
    v.fecha,
    c.razonSocial AS Cliente,
    c.nit AS NIT,
    u.usuario AS Usuario,
    p.descripcion AS Producto,
    vd.cantidad,
    vd.precioUnitario,
    vd.total,
    v.usuarioRegistro,
    v.fechaRegistro,
    v.estado
FROM Venta v
INNER JOIN Cliente c ON v.idCliente = c.id
INNER JOIN Usuario u ON v.idUsuario = u.id
INNER JOIN VentaDetalle vd ON v.id = vd.idVenta
INNER JOIN Producto p ON vd.idProducto = p.id
WHERE v.estado = 1
  AND (
        CAST(v.transaccion AS VARCHAR) +
        CAST(v.fecha AS VARCHAR) +
        ISNULL(u.usuario, '') +
        ISNULL(c.razonSocial, '') +
        ISNULL(c.nit, '') +
        ISNULL(p.descripcion, '') +
        ISNULL(p.codigo, '')
      ) LIKE '%' + REPLACE(@parametro, ' ', '%') + '%'
ORDER BY v.fecha DESC, v.transaccion DESC;

GO
CREATE PROC paProductoVentaListar
AS
SELECT id, codigo, descripcion, precioVenta, 
       (SELECT descripcion FROM UnidadMedida WHERE id = p.idUnidadMedida) AS unidadMedida
FROM Producto p
WHERE estado = 1
ORDER BY descripcion;



EXEC paClienteListar 'mendieta';
EXEC paClienteListar 'mielva';
EXEC paEmpleadoListar '';
EXEC paVentaListar '123456';
EXEC paProductoListar '';
EXEC paEmpleadoListar 'juan';
EXEC paVentaListar 'mielva';         -- Buscar por razonSocial
EXEC paVentaListar '123456';         -- Buscar por transacción o NIT
EXEC paVentaListar 'pastel';         -- Buscar por producto vendido
EXEC paVentaListar 'jperez';         -- Buscar por usuario que registró la venta
EXEC paVentaListar '2025-06-04';     -- Buscar por fecha

-- DML *********************************
INSERT INTO UnidadMedida(descripcion)
VALUES ('Unidad'),('Docena');

INSERT INTO Cargo(descripcion)
VALUES ('Pastelero'),('Cajero'),('Limpieza'),('Repartidor'),('Mantenimiento');

INSERT INTO Producto(codigo,descripcion,idUnidadMedida,saldo,precioVenta)
VALUES ('PT0254', 'Pastel de cumpleaños para Varón', 1, 0, 85);

INSERT INTO Producto(codigo,descripcion,idUnidadMedida,saldo,precioVenta)
VALUES ('PT0255', 'Pastel de cumpleaños para Mujer', 1, 0, 87);

INSERT INTO Producto(codigo,descripcion,idUnidadMedida,saldo,precioVenta)
VALUES ('EM3285', 'Empanada', 2, 0, 10);

UPDATE Producto SET precioVenta=85 WHERE codigo='PT0255';
UPDATE Producto SET estado=-1 WHERE codigo='EM3285';

INSERT INTO Producto(codigo,descripcion,idUnidadMedida,saldo,precioVenta)
VALUES ('PT0256', 'Pastel de cumpleaños para Varón', 1, 0, 65);

INSERT INTO Producto(codigo,descripcion,idUnidadMedida,saldo,precioVenta)
VALUES ('PT0257', 'Pastel de cumpleaños para Mujer', 1, 0, 65);

INSERT INTO Producto(codigo,descripcion,idUnidadMedida,saldo,precioVenta)
VALUES ('PT0258', 'Pastel normal para Varón', 1, 0, 85);

INSERT INTO Producto(codigo,descripcion,idUnidadMedida,saldo,precioVenta)
VALUES ('PT0259', 'Pastel normal para Mujer', 1, 0, 85);

INSERT INTO Producto(codigo,descripcion,idUnidadMedida,saldo,precioVenta)
VALUES ('EM0301', 'Empanada', 1, 0, 3.50);

INSERT INTO Producto(codigo,descripcion,idUnidadMedida,saldo,precioVenta)
VALUES ('GT0321', 'Galleta de Naranja', 1, 0, 0.50);

INSERT INTO Producto(codigo,descripcion,idUnidadMedida,saldo,precioVenta)
VALUES ('GT0322', 'Galleta de Maicena', 1, 0, 0.50);


INSERT INTO Empleado(cedulaIdentidad, nombres, primerApellido, segundoApellido, direccion, celular, idCargo)
VALUES ('1234567', 'Juan', 'Pérez', 'López', 'Calle Loa N° 50', 71717171, 2);

INSERT INTO Empleado(cedulaIdentidad, nombres, primerApellido, segundoApellido, direccion, celular, idCargo)
VALUES ('7654321', 'María', 'González', 'López', 'Calle Loa N° 50', 71717171, 2);

UPDATE Empleado SET celular=61580236 WHERE cedulaIdentidad='1234567';


INSERT INTO Usuario(idEmpleado, usuario, clave)
VALUES (1, 'jperez', '');

UPDATE Usuario SET clave='i0hcoO/nssY6WOs9pOp5Xw==' WHERE id=1;

INSERT INTO Usuario(idEmpleado, usuario, clave)
VALUES (2, 'kevinCardenas', '');

UPDATE Usuario SET clave='i0hcoO/nssY6WOs9pOp5Xw==' WHERE id=2;

INSERT INTO Usuario(idEmpleado, usuario, clave)
VALUES (3, 'samuAuza', '');

UPDATE Usuario SET clave='i0hcoO/nssY6WOs9pOp5Xw==' WHERE id=3;


INSERT INTO Cliente(nit, razonSocial)
VALUES (123456789, 'FinalReposteria CDR');

INSERT INTO Cliente(nit, razonSocial)
VALUES (987654321, 'Mauricio Mendieta');


INSERT INTO Venta(idUsuario, idCliente, transaccion)
VALUES (1, 1, 123456);


INSERT INTO VentaDetalle(idVenta, idProducto, cantidad, precioUnitario, total)
VALUES (1, 1, 2, 85, 170);

SELECT * FROM UnidadMedida;
SELECT * FROM Producto;
SELECT * FROM Empleado;
SELECT * FROM Usuario;
SELECT * FROM Cliente;
SELECT * FROM Venta;
SELECT * FROM VentaDetalle;