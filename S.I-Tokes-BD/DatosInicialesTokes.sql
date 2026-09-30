-- 1. Categorías y subcategorías
INSERT INTO CategoriaProducto (Nombre, FechaRegistro, UsuarioRegistro, Estado)
VALUES ('Electrónica', GETDATE(), 'admin', 1);

INSERT INTO SubCategoriaProd (IdCategoriaProducto, Nombre, FechaRegistro, UsuarioRegistro, Estado)
VALUES (1, 'Celulares', GETDATE(), 'admin', 1);

-- 2. Unidad de medida
INSERT INTO UnidadMedida (Abreviatura, Nombre, FechaRegistro, UsuarioRegistro, Estado)
VALUES ('u', 'Unidad', GETDATE(), 'admin', 1);

-- 3. Productos
INSERT INTO Producto (IdSubCatProd, IdUnidadMedida, Codigo, Nombre, Precio, Costo, CantidadTotal, CantidadMinima, Imagen, Observaciones, TipoProducto, FechaRegistro, UsuarioRegistro, Estado)
VALUES (1, 1, 'P001', 'Smartphone A1', 250.00, 180.00, 50, 5, NULL, 'Alta gama', 'Producto terminado', GETDATE(), 'admin', 1);

-- 4. Clientes y categorías
INSERT INTO CategoriaCliente (Nombre, Descripcion, FechaRegistro, UsuarioRegistro, Estado)
VALUES ('Regular', 'Cliente regular', GETDATE(), 'admin', 1);

INSERT INTO Cliente (IdCategoriaCliente, Codigo, Direccion, Telefono, Departamento, Municipio, PersonaNatural, FechaRegistro, UsuarioRegistro, Estado)
VALUES (1, 'C001', 'Managua, Nicaragua', '8888-8888', 'Managua', 'Managua', 1, GETDATE(), GETDATE(), 1);

-- 5. Tipo proveedor y proveedor
INSERT INTO TipoProveedor (Nombre, Observaciones, FechaRegistro, UsuarioRegistro, Estado)
VALUES ('Mayorista', 'Proveedor mayorista', GETDATE(), 'admin', 1);

INSERT INTO Proveedor (IdTipoProveedor, Nombre, Departamento, Municipio, Direccion, Telefono, FechaRegistro, UsuarioRegistro, Estado)
VALUES (1, 'Distribuidora XYZ', 'Managua', 'Managua', 'Carretera Norte', '2233-4455', GETDATE(), 1, 1);

-- 6. ProveedorProducto
INSERT INTO ProveedorProducto (IdProveedor, IdProducto, Observaciones, Predeterminado, FechaRegistro, UsuarioRegistro, Estado)
VALUES (1, 1, 'Suministro principal', 1, GETDATE(), 1, 1);

-- 7. Compra y detalle
INSERT INTO Compra (NoOrden, IdProveedor, Aprobada, Observaciones, FechaRegistro, UsuarioRegistro, Estado)
VALUES ('OC-001', 1, 1, 'Compra inicial de smartphones', GETDATE(), 'admin', 1);

INSERT INTO DetalleCompra (IdCompra, IdProducto, Cantidad, CostoUnitario, Observaciones)
VALUES (1, 1, 10, 180.00, 'Lote inicial');

-- 8. Venta y detalle
INSERT INTO Venta (NoVenta, IdCliente, Credito, Observaciones, EnviarA, FechaRegistro, UsuarioRegistro, Estado)
VALUES ('V-001', 1, 1, 'Venta con crédito', 'Managua', GETDATE(), 'admin', 1);

INSERT INTO DetalleVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Observaciones)
VALUES (1, 1, 1, 250.00, 'Primer venta');

-- 9. CXC y DetalleCXC
INSERT INTO CXC (NoCXC, IdCliente, Observaciones, FechaRegistro, UsuarioRegistro, Estado)
VALUES ('CXC-001', 1, 'Crédito por venta V-001', GETDATE(), 'admin', 1);

INSERT INTO DetalleCXC (IdCXC, IdVenta, Monto, NCuotas, DiasCredito, Saldo, Cancelado, FechaRegistro, UsuarioRegistro, Estado)
VALUES (1, 1, 250.00, 5, 30, 250.00, 0, GETDATE(), 'admin', 1);
