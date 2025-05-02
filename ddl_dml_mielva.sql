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
DROP TABLE CompraDetalle;
DROP TABLE Compra;
DROP TABLE Ingrediente;
DROP TABLE Usuario;
DROP TABLE Empleado;
DROP TABLE Proveedor;
DROP TABLE Producto;

CREATE TABLE Producto (
  id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
  codigo VARCHAR(20) NOT NULL,
  descripcion VARCHAR(250) NOT NULL,
  unidadMedida VARCHAR(20) NOT NULL,
  saldo DECIMAL NOT NULL DEFAULT 0,
  precioVenta DECIMAL NOT NULL CHECK (precioVenta > 0)
);

CREATE TABLE Proveedor (
  id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
  nit BIGINT NOT NULL,
  razonSocial VARCHAR(150) NOT NULL,
  direccion VARCHAR(250) NULL,
  telefono VARCHAR(30) NOT NULL,
  representante VARCHAR(100) NOT NULL
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

CREATE TABLE Ingrediente (
    id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    codigo VARCHAR(20) NOT NULL UNIQUE, -- Código interno del ingrediente
    descripcion VARCHAR(250) NOT NULL,
    unidadMedida VARCHAR(20) NOT NULL,
    saldo DECIMAL NOT NULL DEFAULT 0, -- Stock actual del ingrediente
    precioCompra DECIMAL NULL, -- Último precio de compra (puede variar por proveedor)
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1 -- 1: Activo, 0: Inactivo, -1: Eliminado
);

CREATE TABLE Compra (
 id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    idProveedor INT NOT NULL,
    transaccion INT NOT NULL,
    fecha DATE NOT NULL DEFAULT GETDATE(),
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1, -- 1: Activo, 0: Inactivo, -1: Anulado
    CONSTRAINT FK_Compra_Proveedor FOREIGN KEY(idProveedor) REFERENCES Proveedor(id)
);

CREATE TABLE CompraDetalle (
    id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    idCompra INT NOT NULL,
    idIngrediente INT NOT NULL,
    cantidad DECIMAL NOT NULL CHECK (cantidad > 0),
    precioUnitario DECIMAL NOT NULL,
    total DECIMAL NOT NULL,
    usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    estado SMALLINT NOT NULL DEFAULT 1,
    CONSTRAINT FK_CompraDetalle_Compra FOREIGN KEY(idCompra) REFERENCES Compra(id),
    CONSTRAINT FK_CompraDetalle_Ingrediente FOREIGN KEY(idIngrediente) REFERENCES Ingrediente(id)
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
	CONSTRAINT fk_VentaDetalle_Venta FOREIGN KEY(idVenta) REFERENCES Venta(id),
	CONSTRAINT fk_VentaDetalle_Producto FOREIGN KEY(idProducto) REFERENCES Producto(id)
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
CONSTRAINT fk_VentaDetalle_Venta FOREIGN KEY(idVenta) REFERENCES Venta(id),
CONSTRAINT fk_VentaDetalle_Producto FOREIGN KEY(idProducto) REFERENCES
Producto(id)
);

ALTER TABLE Producto ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Producto ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Producto ADD estado SMALLINT NOT NULL DEFAULT 1; -- 1: Activo, 0: Inactivo, -1: Eliminado

ALTER TABLE Proveedor ADD usuarioRegistro VARCHAR(50) NOT NULL DEFAULT SUSER_NAME();
ALTER TABLE Proveedor ADD fechaRegistro DATETIME NOT NULL DEFAULT GETDATE();
ALTER TABLE Proveedor ADD estado SMALLINT NOT NULL DEFAULT 1; -- 1: Activo, 0: Inactivo, -1: Eliminado

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
	WHERE registroActivo = 1 AND CAST(nit AS VARCHAR) + razonSocial LIKE
	'%'+REPLACE(@parametro, ' ','%')+'%'
	ORDER BY registroActivo DESC, razonSocial ASC;

GO
ALTER PROC paVentaListar @parametro VARCHAR(100)
AS
	SELECT v.*, u.usuario, c.razonSocial
	FROM Venta v
	LEFT JOIN Usuario u ON v.idUsuario = u.id
	LEFT JOIN Cliente c ON v.idCliente = c.id
	WHERE v.registroActivo = 1 AND CAST(v.transaccion AS VARCHAR) + CAST(v.fecha AS VARCHAR) + u.usuario + c.razonSocial LIKE
	'%'+REPLACE(@parametro, ' ','%')+'%'
	ORDER BY v.registroActivo DESC, v.fecha DESC;

GO
ALTER PROC paCompraListar @parametro VARCHAR(100)
	AS
	SELECT c.*, p.razonSocial
	FROM Compra c
	LEFT JOIN Proveedor p ON c.idProveedor = p.id
	WHERE c.estado = 1 AND CAST(c.transaccion AS VARCHAR) + CAST(c.fecha AS VARCHAR) + p.razonSocial LIKE
	'%'+REPLACE(@parametro, ' ','%')+'%'
	ORDER BY c.estado DESC, c.fecha DESC;

GO
ALTER PROC paCompraDetalleListar @parametro VARCHAR(100)
	AS
	SELECT cd.*, p.descripcion, c.transaccion, c.fecha
	FROM CompraDetalle cd
	LEFT JOIN Producto p ON cd.idProducto = p.id
	LEFT JOIN Compra c ON cd.idCompra = c.id
	WHERE cd.estado = 1 AND CAST(cd.cantidad AS VARCHAR) + CAST(cd.precioUnitario AS VARCHAR) + CAST(cd.total AS VARCHAR) + p.descripcion + CAST(c.transaccion AS VARCHAR) + CAST(c.fecha AS VARCHAR) LIKE
	'%'+REPLACE(@parametro, ' ','%')+'%'
	ORDER BY cd.estado DESC, c.fecha DESC;

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

GO
ALTER PROC paProveedorListar @parametro VARCHAR(100)
	AS
	SELECT * FROM Proveedor
	WHERE estado = 1 AND CAST(nit AS VARCHAR) + razonSocial + telefono + representante LIKE
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
create PROC paCompraListar @parametro VARCHAR(100)
AS
SELECT c.*, p.razonSocial
FROM Compra c
	LEFT JOIN Proveedor p ON c.idProveedor = p.id
	WHERE c.estado = 1 AND CAST(c.transaccion AS VARCHAR) + CAST(c.fecha AS VARCHAR) + p.razonSocial LIKE
	'%'+REPLACE(@parametro, ' ','%')+'%'
ORDER BY c.estado DESC, c.fecha DESC;

GO
ALTER PROC paCompraDetalleListar @parametro VARCHAR(100)
	AS
	SELECT cd.*, p.descripcion, c.transaccion, c.fecha
	FROM CompraDetalle cd
	LEFT JOIN Ingrediente p ON cd.idIngrediente = p.id
	LEFT JOIN Compra c ON cd.idCompra = c.id
	WHERE cd.estado = 1 AND CAST(cd.cantidad AS VARCHAR) + CAST(cd.precioUnitario AS VARCHAR) + CAST(cd.total AS VARCHAR) + p.descripcion + CAST(c.transaccion AS VARCHAR) + CAST(c.fecha AS VARCHAR) LIKE
	'%'+REPLACE(@parametro, ' ','%')+'%'
ORDER BY cd.estado DESC, c.fecha DESC;

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

GO
ALTER PROC paProveedorListar @parametro VARCHAR(100)
	AS
	SELECT * FROM Proveedor
	WHERE estado = 1 AND CAST(nit AS VARCHAR) + razonSocial + telefono + representante LIKE
	'%'+REPLACE(@parametro, ' ','%')+'%'
ORDER BY estado DESC, razonSocial ASC;



EXEC paClienteListar 'mendieta';
EXEC paClienteListar 'mielva';
EXEC paProductoListar 'PASTEL varon';
EXEC paEmpleadoListar '';
EXEC paVentaListar '123456';
EXEC paCompraListar '654321';
EXEC paCompraDetalleListar '55';
EXEC paVentaDetalleListar '170';
EXEC paProveedorListar 'mielva';
EXEC paProductoListar 'pastel varon';
EXEC paEmpleadoListar 'juan';
EXEC paVentaListar '2025-04-01';
EXEC paCompraListar '2025-04-01';
EXEC paCompraDetalleListar '2025-04-01';
EXEC paVentaDetalleListar '2025-04-01';
EXEC paProveedorListar 'mielva';

-- DML *********************************
INSERT INTO Producto(codigo,descripcion,unidadMedida,saldo,precioVenta)
VALUES ('PT0254', 'Pastel para varón de Feliz Cumpleaños', 'Caja', 0, 85);

INSERT INTO Producto(codigo,descripcion,unidadMedida,saldo,precioVenta)
VALUES ('PT0253', 'Pastel para mujer de Feliz Cumpleaños', 'Caja', 0, 87);

INSERT INTO Producto(codigo,descripcion,unidadMedida,saldo,precioVenta)
VALUES ('EM3285', 'Empanada', 'Docena', 0, 10);

UPDATE Producto SET precioVenta=83 WHERE codigo='PT0254';
UPDATE Producto SET estado=-1 WHERE codigo='EM3285';


INSERT INTO Proveedor(nit,razonSocial,direccion,telefono,representante)
VALUES (123456789, 'Mielva CDR', 'Calle Loa N° 50', '71717171', 'Juan Pérez');

INSERT INTO Proveedor(nit,razonSocial,direccion,telefono,representante)
VALUES (987654321, 'Mauricio Mendieta', 'Calle Loa N° 50', '71717171', 'Juan Pérez');

UPDATE Proveedor SET telefono=78234568 WHERE nit='123456789';


INSERT INTO Empleado(cedulaIdentidad, nombres, primerApellido, segundoApellido, direccion, celular, cargo)
VALUES ('1234567', 'Juan', 'Pérez', 'López', 'Calle Loa N° 50', 71717171, 'Cajero');

INSERT INTO Empleado(cedulaIdentidad, nombres, primerApellido, segundoApellido, direccion, celular, cargo)
VALUES ('7654321', 'María', 'González', 'López', 'Calle Loa N° 50', 71717171, 'Cajero');

UPDATE Empleado SET celular=61580236 WHERE cedulaIdentidad='1234567';


INSERT INTO Usuario(idEmpleado, usuario, clave)
VALUES (1, 'jperez', '');

UPDATE Usuario SET clave='i0hcoO/nssY6WOs9pOp5Xw==' WHERE id=1;


INSERT INTO Ingrediente(codigo, descripcion, unidadMedida, saldo, precioCompra)
VALUES ('ING001', 'Harina de trigo', 'Kg', 100, 5.50);

INSERT INTO Ingrediente(codigo, descripcion, unidadMedida, saldo, precioCompra)
VALUES ('ING002', 'Azúcar', 'Kg', 50, 3.00);


INSERT INTO Compra(idProveedor, transaccion)
VALUES (1, 123456);

INSERT INTO Compra(idProveedor, transaccion)
VALUES (2, 654321);


INSERT INTO CompraDetalle(idCompra, idIngrediente, cantidad, precioUnitario, total)
VALUES (1, 1, 10, 5.50, 55);

INSERT INTO CompraDetalle(idCompra, idIngrediente, cantidad, precioUnitario, total)
VALUES (1, 2, 5, 3.00, 15);


INSERT INTO Cliente(nit, razonSocial)
VALUES (123456789, 'Mielva CDR');

INSERT INTO Cliente(nit, razonSocial)
VALUES (987654321, 'Mauricio Mendieta');


INSERT INTO Venta(idUsuario, idCliente, transaccion)
VALUES (1, 1, 123456);


INSERT INTO VentaDetalle(idVenta, idProducto, cantidad, precioUnitario, total)
VALUES (1, 1, 2, 85, 170);


SELECT * FROM Producto;
SELECT * FROM Proveedor;
SELECT * FROM Empleado;
SELECT * FROM Usuario;
SELECT * FROM Ingrediente;
SELECT * FROM Compra;
SELECT * FROM CompraDetalle;
SELECT * FROM Cliente;
SELECT * FROM Venta;
SELECT * FROM VentaDetalle;