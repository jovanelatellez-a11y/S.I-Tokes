CREATE TABLE DetalleCXC ( 
	IdDetalleCXC int identity(1,1)  NOT NULL,
	IdCXC int NOT NULL,
	IdVenta int NOT NULL,
	Monto numeric(10,2) NOT NULL,
	NCuotas int NOT NULL,
	DiasCredito int NOT NULL,
	Saldo numeric(10,2) NOT NULL,
	Cancelado bit NOT NULL,
	FechaRegistro datetime,
	UsuarioRegistro nvarchar(50) NOT NULL,
	Estado bit NOT NULL
);

CREATE TABLE CXC ( 
	IdCXC int identity(1,1)  NOT NULL,
	NoCXC nvarchar(50),
	IdCliente int NOT NULL,
	Observaciones nvarchar(max),
	FechaRegistro datetime NOT NULL,
	UsuarioRegistro nvarchar(50) NOT NULL,
	Estado bit NOT NULL
);

CREATE TABLE DetalleVenta ( 
	IdDetalleVenta int identity(1,1)  NOT NULL,
	IdVenta int NOT NULL,
	IdProducto int NOT NULL,
	Cantidad numeric(10,2) NOT NULL,
	PrecioUnitario numeric(10,2) NOT NULL,
	Observaciones nvarchar(max)
);

CREATE TABLE Venta ( 
	IdVenta int identity(1,1)  NOT NULL,
	NoVenta nvarchar(50),
	IdCliente int NOT NULL,
	Credito bit NOT NULL,
	Observaciones nvarchar(max),
	EnviarA nvarchar(max),
	FechaRegistro datetime NOT NULL,
	UsuarioRegistro nvarchar(50) NOT NULL,
	Estado bit NOT NULL
);

CREATE TABLE DetalleCompra ( 
	IdDetalleCompra int identity(1,1)  NOT NULL,
	IdCompra int NOT NULL,
	IdProducto int NOT NULL,
	Cantidad numeric(10,2) NOT NULL,
	CostoUnitario numeric(10,2) NOT NULL,
	Observaciones nvarchar(50)
);

CREATE TABLE Compra ( 
	IdCompra int identity(1,1)  NOT NULL,
	NoOrden nvarchar(50),
	IdProveedor int NOT NULL,
	Aprobada bit NOT NULL,
	Observaciones nvarchar(max),
	FechaRegistro datetime NOT NULL,
	UsuarioRegistro nvarchar(50) NOT NULL,
	Estado bit NOT NULL
);

CREATE TABLE CategoriaCliente ( 
	IdCategoriaCliente int identity(1,1)  NOT NULL,
	Nombre nvarchar(50) NOT NULL,
	Descripcion nvarchar(max),
	FechaRegistro datetime NOT NULL,
	UsuarioRegistro nvarchar(50) NOT NULL,
	Estado bit NOT NULL
);

CREATE TABLE Cliente ( 
	IdCliente int identity(1,1)  NOT NULL,
	IdCategoriaCliente int NOT NULL,
	Codigo nvarchar(50),
	Direccion nvarchar(max),
	Telefono nvarchar(50),
	Departamento nvarchar(50),
	Municipio nvarchar(50),
	PersonaNatural bit NOT NULL,
	FechaRegistro datetime NOT NULL,
	UsuarioRegistro datetime NOT NULL,
	Estado bit NOT NULL
);

CREATE TABLE ProveedorProducto ( 
	IdProveedorProducto int identity(1,1)  NOT NULL,
	IdProveedor int NOT NULL,
	IdProducto int NOT NULL,
	Observaciones nvarchar(max),
	Predeterminado bit NOT NULL,
	FechaRegistro datetime NOT NULL,
	UsuarioRegistro bigint NOT NULL,
	Estado bit NOT NULL
);

CREATE TABLE TipoProveedor ( 
	IdTipoProveedor int identity(1,1)  NOT NULL,
	Nombre nvarchar(50) NOT NULL,
	Observaciones nvarchar(max),
	FechaRegistro datetime NOT NULL,
	UsuarioRegistro nvarchar(50) NOT NULL,
	Estado bit NOT NULL
);

CREATE TABLE Proveedor ( 
	IdProveedor int identity(1,1)  NOT NULL,
	IdTipoProveedor int NOT NULL,
	Nombre nvarchar(50) NOT NULL,
	Departamento nvarchar(50),
	Municipio nvarchar(50),
	Direccion nvarchar(max),
	Telefono nvarchar(50),
	FechaRegistro datetime NOT NULL,
	UsuarioRegistro bigint NOT NULL,
	Estado bit NOT NULL
);

CREATE TABLE UnidadMedida ( 
	IdUnidadMedida int identity(1,1)  NOT NULL,
	Abreviatura nvarchar(50),
	Nombre nvarchar(50) NOT NULL,
	FechaRegistro datetime NOT NULL,
	UsuarioRegistro nvarchar(50) NOT NULL,
	Estado bit NOT NULL
);

CREATE TABLE Producto ( 
	IdProducto int identity(1,1)  NOT NULL,
	IdSubCatProd int NOT NULL,
	IdUnidadMedida int NOT NULL,
	Codigo nvarchar(50),
	Nombre nvarchar(50) NOT NULL,
	Precio numeric(10,2) NOT NULL,
	Costo numeric(10,2) NOT NULL,
	CantidadTotal numeric(10,2) NOT NULL,
	CantidadMinima numeric(10,2) NOT NULL,
	Imagen varbinary(max),
	Observaciones nvarchar(max),
	TipoProducto nvarchar(50),
	FechaRegistro datetime NOT NULL,
	UsuarioRegistro nvarchar(50) NOT NULL,
	Estado bit NOT NULL
);

CREATE TABLE SubCategoriaProd ( 
	IdSubCatProd int identity(1,1)  NOT NULL,
	IdCategoriaProducto int NOT NULL,
	Nombre nvarchar(50) NOT NULL,
	FechaRegistro datetime NOT NULL,
	UsuarioRegistro nvarchar(50) NOT NULL,
	Estado bit NOT NULL
);

CREATE TABLE CategoriaProducto ( 
	IdCategoriaProducto int identity(1,1)  NOT NULL,
	Nombre nvarchar(50) NOT NULL,
	FechaRegistro datetime NOT NULL,
	UsuarioRegistro nvarchar(50) NOT NULL,
	Estado bit NOT NULL
);


ALTER TABLE DetalleCXC ADD CONSTRAINT PK_DetalleCXC 
	PRIMARY KEY CLUSTERED (IdDetalleCXC);

ALTER TABLE CXC ADD CONSTRAINT PK_CXC 
	PRIMARY KEY CLUSTERED (IdCXC);

ALTER TABLE DetalleVenta ADD CONSTRAINT PK_DetalleVenta 
	PRIMARY KEY CLUSTERED (IdDetalleVenta);

ALTER TABLE Venta ADD CONSTRAINT PK_Venta 
	PRIMARY KEY CLUSTERED (IdVenta);

ALTER TABLE DetalleCompra ADD CONSTRAINT PK_DetalleCompra 
	PRIMARY KEY CLUSTERED (IdDetalleCompra);

ALTER TABLE Compra ADD CONSTRAINT PK_Compra 
	PRIMARY KEY CLUSTERED (IdCompra);

ALTER TABLE CategoriaCliente ADD CONSTRAINT PK_CategoriaCliente 
	PRIMARY KEY CLUSTERED (IdCategoriaCliente);

ALTER TABLE Cliente ADD CONSTRAINT PK_Cliente 
	PRIMARY KEY CLUSTERED (IdCliente);

ALTER TABLE ProveedorProducto ADD CONSTRAINT PK_ProveedorProducto 
	PRIMARY KEY CLUSTERED (IdProveedorProducto);

ALTER TABLE TipoProveedor ADD CONSTRAINT PK_TipoProveedor 
	PRIMARY KEY CLUSTERED (IdTipoProveedor);

ALTER TABLE Proveedor ADD CONSTRAINT PK_Proveedor 
	PRIMARY KEY CLUSTERED (IdProveedor);

ALTER TABLE UnidadMedida ADD CONSTRAINT PK_UnidadMedida 
	PRIMARY KEY CLUSTERED (IdUnidadMedida);

ALTER TABLE Producto ADD CONSTRAINT PK_Producto 
	PRIMARY KEY CLUSTERED (IdProducto);

ALTER TABLE SubCategoriaProd ADD CONSTRAINT PK_SubCategoriaProd 
	PRIMARY KEY CLUSTERED (IdSubCatProd);

ALTER TABLE CategoriaProducto ADD CONSTRAINT PK_CategoriaProducto 
	PRIMARY KEY CLUSTERED (IdCategoriaProducto);


ALTER TABLE DetalleCXC
	ADD CONSTRAINT UQ_DetalleCXC_IdDetalleCXC UNIQUE (IdDetalleCXC);

ALTER TABLE CXC
	ADD CONSTRAINT UQ_CXC_IdCliente UNIQUE (IdCliente);

ALTER TABLE CXC
	ADD CONSTRAINT UQ_CXC_IdCXC UNIQUE (IdCXC);

ALTER TABLE DetalleVenta
	ADD CONSTRAINT UQ_DetalleVenta_IdDetalleVenta UNIQUE (IdDetalleVenta);

ALTER TABLE Venta
	ADD CONSTRAINT UQ_Venta_IdVenta UNIQUE (IdVenta);

ALTER TABLE DetalleCompra
	ADD CONSTRAINT UQ_DetalleCompra_IdDetalleCompra UNIQUE (IdDetalleCompra);

ALTER TABLE Compra
	ADD CONSTRAINT UQ_Compra_IdCompra UNIQUE (IdCompra);

ALTER TABLE CategoriaCliente
	ADD CONSTRAINT UQ_CategoriaCliente_IdCategoriaCliente UNIQUE (IdCategoriaCliente);

ALTER TABLE Cliente
	ADD CONSTRAINT UQ_Cliente_IdCliente UNIQUE (IdCliente);

ALTER TABLE ProveedorProducto
	ADD CONSTRAINT UQ_ProveedorProducto_IdProveedorProducto UNIQUE (IdProveedorProducto);

ALTER TABLE TipoProveedor
	ADD CONSTRAINT UQ_TipoProveedor_IdTipoProveedor UNIQUE (IdTipoProveedor);

ALTER TABLE Proveedor
	ADD CONSTRAINT UQ_Proveedor_IdProveedor UNIQUE (IdProveedor);

ALTER TABLE UnidadMedida
	ADD CONSTRAINT UQ_UnidadMedida_IdUnidadMedida UNIQUE (IdUnidadMedida);

ALTER TABLE Producto
	ADD CONSTRAINT UQ_Producto_IdProducto UNIQUE (IdProducto);

ALTER TABLE SubCategoriaProd
	ADD CONSTRAINT UQ_SubCategoriaProd_IdSubCatProd UNIQUE (IdSubCatProd);

ALTER TABLE CategoriaProducto
	ADD CONSTRAINT UQ_CategoriaProducto_IdCategoriaProducto UNIQUE (IdCategoriaProducto);


ALTER TABLE DetalleCXC ADD CONSTRAINT FK_DetalleCXC_CXC 
	FOREIGN KEY (IdCXC) REFERENCES CXC (IdCXC);

ALTER TABLE DetalleCXC ADD CONSTRAINT FK_DetalleCXC_Venta 
	FOREIGN KEY (IdVenta) REFERENCES Venta (IdVenta);

ALTER TABLE CXC ADD CONSTRAINT FK_CXC_Cliente 
	FOREIGN KEY (IdCliente) REFERENCES Cliente (IdCliente);

ALTER TABLE DetalleVenta ADD CONSTRAINT FK_DetalleVenta_Producto 
	FOREIGN KEY (IdProducto) REFERENCES Producto (IdProducto);

ALTER TABLE DetalleVenta ADD CONSTRAINT FK_DetalleVenta_Venta 
	FOREIGN KEY (IdVenta) REFERENCES Venta (IdVenta);

ALTER TABLE Venta ADD CONSTRAINT FK_Venta_Cliente 
	FOREIGN KEY (IdCliente) REFERENCES Cliente (IdCliente);

ALTER TABLE DetalleCompra ADD CONSTRAINT FK_DetalleCompra_Compra 
	FOREIGN KEY (IdCompra) REFERENCES Compra (IdCompra);

ALTER TABLE DetalleCompra ADD CONSTRAINT FK_DetalleCompra_Producto 
	FOREIGN KEY (IdProducto) REFERENCES Producto (IdProducto);

ALTER TABLE Compra ADD CONSTRAINT FK_Compra_Proveedor 
	FOREIGN KEY (IdProveedor) REFERENCES Proveedor (IdProveedor);

ALTER TABLE Cliente ADD CONSTRAINT FK_Cliente_CategoriaCliente 
	FOREIGN KEY (IdCategoriaCliente) REFERENCES CategoriaCliente (IdCategoriaCliente);

ALTER TABLE ProveedorProducto ADD CONSTRAINT FK_ProveedorProducto_Producto 
	FOREIGN KEY (IdProducto) REFERENCES Producto (IdProducto);

ALTER TABLE ProveedorProducto ADD CONSTRAINT FK_ProveedorProducto_Proveedor 
	FOREIGN KEY (IdProveedor) REFERENCES Proveedor (IdProveedor);

ALTER TABLE Proveedor ADD CONSTRAINT FK_Proveedor_TipoProveedor 
	FOREIGN KEY (IdTipoProveedor) REFERENCES TipoProveedor (IdTipoProveedor);

ALTER TABLE Producto ADD CONSTRAINT FK_Producto_SubCategoriaProd 
	FOREIGN KEY (IdSubCatProd) REFERENCES SubCategoriaProd (IdSubCatProd);

ALTER TABLE Producto ADD CONSTRAINT FK_Producto_UnidadMedida 
	FOREIGN KEY (IdUnidadMedida) REFERENCES UnidadMedida (IdUnidadMedida);

ALTER TABLE SubCategoriaProd ADD CONSTRAINT FK_SubCategoriaProd_CategoriaProducto 
	FOREIGN KEY (IdCategoriaProducto) REFERENCES CategoriaProducto (IdCategoriaProducto);
