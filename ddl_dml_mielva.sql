CREATE DATABASE Mielva;

GO
USE [master];
GO
CREATE LOGIN  [usrmielva] WITH PASSWORD = N'123456',
	DEFAULT_DATABASE = [Mielva],
	CHECK_EXPIRATION = OFF,
	CHECK_POLICY = ON

GO
USE [Mielva]
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
DROP TABLE Producto;

CREATE TABLE Producto (
  id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
  codigo VARCHAR(20) NOT NULL,
  descripcion VARCHAR(250) NOT NULL,
  unidadMedida VARCHAR(20) NOT NULL,
  saldo DECIMAL NOT NULL DEFAULT 0,
  precioVenta DECIMAL NOT NULL CHECK (precioVenta > 0)
);

CREATE TABLE Empleado (
  id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
  cedulaIdentidad VARCHAR(12) NOT NULL,
  nombres VARCHAR(30) NOT NULL,
  primerApellido VARCHAR(30) NULL,
  segundoApellido VARCHAR(30) NULL,
  direccion VARCHAR(250) NOT NULL,
  celular BIGINT NOT NULL,
  cargo VARCHAR(50) NOT NULL
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
	nit BIGINT NOT NULL,
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
	precioUnitario DECIMAL NOT NULL,
	total DECIMAL NOT NULL,
	usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
	fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
	estado SMALLINT NOT NULL DEFAULT 1,
	CONSTRAINT fk_VentaDetalle_Producto FOREIGN KEY(idProducto) REFERENCES Producto(id)
	);


ALTER TABLE Producto ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Producto ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Producto ADD estado SMALLINT NOT NULL DEFAULT 1; -- 1: Activo, 0: Inactivo, -1: Eliminado

ALTER TABLE Empleado ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Empleado ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Empleado ADD estado SMALLINT NOT NULL DEFAULT 1; -- 1: Activo, 0: Inactivo, -1: Eliminado

ALTER TABLE Usuario ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Usuario ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Usuario ADD estado SMALLINT NOT NULL DEFAULT 1; -- 1: Activo, 0: Inactivo, -1: Eliminado

--********************************************************
GO
ALTER PROC paProductoListar @parametro VARCHAR(100)
AS
  SELECT * FROM Producto
  WHERE estado<>-1 AND codigo+descripcion+unidadMedida LIKE '%'+REPLACE(@parametro,' ','%')+'%'
  ORDER BY estado DESC, descripcion ASC;


GO
ALTER PROC paEmpleadoListar @parametro VARCHAR(100)
AS
    SELECT *
    FROM Empleado
    WHERE estado <> -1 -- Excluir empleados eliminados
      AND (
          cedulaIdentidad + nombres + ISNULL(primerApellido, '') + ISNULL(segundoApellido, '') + direccion + cargo
          LIKE '%' + REPLACE(@parametro, ' ', '%') + '%'
      )
    ORDER BY estado DESC, nombres ASC, primerApellido ASC;


GO
ALTER PROC paClienteListar @parametro VARCHAR(100)
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
ALTER PROC paVentaListar @parametro VARCHAR(100)
AS
	SELECT v.*, u.usuario, c.razonSocial
	FROM Venta v
	LEFT JOIN Usuario u ON v.idUsuario = u.id
	LEFT JOIN Cliente c ON v.idCliente = c.id
	WHERE v.estado = 1 AND CAST(v.transaccion AS VARCHAR) + CAST(v.fecha AS VARCHAR) + u.usuario + c.razonSocial LIKE
	'%'+REPLACE(@parametro, ' ','%')+'%'
	ORDER BY v.estado DESC, v.fecha DESC;

GO
ALTER PROC paVentaDetalleListar @parametro VARCHAR(100)
	AS
	SELECT vd.*, p.descripcion, v.transaccion, v.fecha
	FROM VentaDetalle vd
	LEFT JOIN Producto p ON vd.idProducto = p.id
	LEFT JOIN Venta v ON vd.idVenta = v.id
	WHERE vd.estado = 1 AND CAST(vd.cantidad AS VARCHAR) + CAST(vd.precioUnitario AS VARCHAR) + CAST(vd.total AS VARCHAR) + p.descripcion + CAST(v.transaccion AS VARCHAR) + CAST(v.fecha AS VARCHAR) LIKE
	'%'+REPLACE(@parametro, ' ','%')+'%'
	ORDER BY vd.estado DESC, v.fecha DESC;


EXEC paClienteListar 'mendieta';
EXEC paClienteListar 'mielva';
EXEC paEmpleadoListar '';
EXEC paVentaListar '123456';
EXEC paVentaDetalleListar '170';
EXEC paProductoListar 'pastel varon';
EXEC paEmpleadoListar 'juan';

-- DML *********************************
INSERT INTO Producto(codigo,descripcion,unidadMedida,saldo,precioVenta)
VALUES ('PT0254', 'Pastel para varón de Feliz Cumpleaños', 'Caja', 0, 85);

INSERT INTO Producto(codigo,descripcion,unidadMedida,saldo,precioVenta)
VALUES ('PT0253', 'Pastel para mujer de Feliz Cumpleaños', 'Caja', 0, 87);

INSERT INTO Producto(codigo,descripcion,unidadMedida,saldo,precioVenta)
VALUES ('EM3285', 'Empanada', 'Docena', 0, 10);

UPDATE Producto SET precioVenta=83 WHERE codigo='PT0254';
UPDATE Producto SET estado=-1 WHERE codigo='EM3285';


INSERT INTO Empleado(cedulaIdentidad, nombres, primerApellido, segundoApellido, direccion, celular, cargo)
VALUES ('1234567', 'Juan', 'Pérez', 'López', 'Calle Loa N° 50', 71717171, 'Cajero');

INSERT INTO Empleado(cedulaIdentidad, nombres, primerApellido, segundoApellido, direccion, celular, cargo)
VALUES ('7654321', 'María', 'González', 'López', 'Calle Loa N° 50', 71717171, 'Cajero');

UPDATE Empleado SET celular=61580236 WHERE cedulaIdentidad='1234567';


INSERT INTO Usuario(idEmpleado, usuario, clave)
VALUES (1, 'jperez', '');

UPDATE Usuario SET clave='i0hcoO/nssY6WOs9pOp5Xw==' WHERE id=1;


INSERT INTO Cliente(nit, razonSocial)
VALUES (123456789, 'Mielva CDR');

INSERT INTO Cliente(nit, razonSocial)
VALUES (987654321, 'Mauricio Mendieta');


INSERT INTO Venta(idUsuario, idCliente, transaccion)
VALUES (1, 1, 123456);


INSERT INTO VentaDetalle(idVenta, idProducto, cantidad, precioUnitario, total)
VALUES (1, 1, 2, 85, 170);


SELECT * FROM Producto;
SELECT * FROM Empleado;
SELECT * FROM Usuario;
SELECT * FROM Cliente;
SELECT * FROM Venta;
SELECT * FROM VentaDetalle;