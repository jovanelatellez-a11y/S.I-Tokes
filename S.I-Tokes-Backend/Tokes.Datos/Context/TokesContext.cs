using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Tokes.Entidades;

namespace Tokes.Datos
{
    public partial class TokesContext : DbContext
    {
        public TokesContext()
        {
        }

        public TokesContext(DbContextOptions<TokesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoriaCliente> CategoriaClientes { get; set; } = null!;
        public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<Cxc> Cxcs { get; set; } = null!;
        public virtual DbSet<DetalleCompra> DetalleCompras { get; set; } = null!;
        public virtual DbSet<DetalleCxc> DetalleCxcs { get; set; } = null!;
        public virtual DbSet<DetalleVenta> DetalleVenta { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Proveedor> Proveedors { get; set; } = null!;
        public virtual DbSet<ProveedorProducto> ProveedorProductos { get; set; } = null!;
        public virtual DbSet<SubCategoriaProd> SubCategoriaProds { get; set; } = null!;
        public virtual DbSet<TipoProveedor> TipoProveedors { get; set; } = null!;
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; } = null!;
        public virtual DbSet<Venta> Venta { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Tokes;User ID=sa;Password=r1812;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaCliente>(entity =>
            {
                entity.HasKey(e => e.IdCategoriaCliente);

                entity.ToTable("CategoriaCliente");

                entity.HasIndex(e => e.IdCategoriaCliente, "UQ_CategoriaCliente_IdCategoriaCliente")
                    .IsUnique();

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.UsuarioRegistro).HasMaxLength(50);
            });

            modelBuilder.Entity<CategoriaProducto>(entity =>
            {
                entity.HasKey(e => e.IdCategoriaProducto);

                entity.ToTable("CategoriaProducto");

                entity.HasIndex(e => e.IdCategoriaProducto, "UQ_CategoriaProducto_IdCategoriaProducto")
                    .IsUnique();

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.UsuarioRegistro).HasMaxLength(50);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("Cliente");

                entity.HasIndex(e => e.IdCliente, "UQ_Cliente_IdCliente")
                    .IsUnique();

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.Departamento).HasMaxLength(50);

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Municipio).HasMaxLength(50);

                entity.Property(e => e.Telefono).HasMaxLength(50);

                entity.Property(e => e.UsuarioRegistro).HasColumnType("datetime");

                entity.HasOne(d => d.IdCategoriaClienteNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdCategoriaCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cliente_CategoriaCliente");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.IdCompra);

                entity.ToTable("Compra");

                entity.HasIndex(e => e.IdCompra, "UQ_Compra_IdCompra")
                    .IsUnique();

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.NoOrden).HasMaxLength(50);

                entity.Property(e => e.UsuarioRegistro).HasMaxLength(50);

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Compra_Proveedor");
            });

            modelBuilder.Entity<Cxc>(entity =>
            {
                entity.HasKey(e => e.IdCxc);

                entity.ToTable("CXC");

                entity.HasIndex(e => e.IdCxc, "UQ_CXC_IdCXC")
                    .IsUnique();

                entity.HasIndex(e => e.IdCliente, "UQ_CXC_IdCliente")
                    .IsUnique();

                entity.Property(e => e.IdCxc).HasColumnName("IdCXC");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.NoCxc)
                    .HasMaxLength(50)
                    .HasColumnName("NoCXC");

                entity.Property(e => e.UsuarioRegistro).HasMaxLength(50);

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithOne(p => p.Cxc)
                    .HasForeignKey<Cxc>(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CXC_Cliente");
            });

            modelBuilder.Entity<DetalleCompra>(entity =>
            {
                entity.HasKey(e => e.IdDetalleCompra);

                entity.ToTable("DetalleCompra");

                entity.HasIndex(e => e.IdDetalleCompra, "UQ_DetalleCompra_IdDetalleCompra")
                    .IsUnique();

                entity.Property(e => e.Cantidad).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.CostoUnitario).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Observaciones).HasMaxLength(50);

                entity.HasOne(d => d.IdCompraNavigation)
                    .WithMany(p => p.DetalleCompras)
                    .HasForeignKey(d => d.IdCompra)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetalleCompra_Compra");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleCompras)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetalleCompra_Producto");
            });

            modelBuilder.Entity<DetalleCxc>(entity =>
            {
                entity.HasKey(e => e.IdDetalleCxc);

                entity.ToTable("DetalleCXC");

                entity.HasIndex(e => e.IdDetalleCxc, "UQ_DetalleCXC_IdDetalleCXC")
                    .IsUnique();

                entity.Property(e => e.IdDetalleCxc).HasColumnName("IdDetalleCXC");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.IdCxc).HasColumnName("IdCXC");

                entity.Property(e => e.Monto).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Ncuotas).HasColumnName("NCuotas");

                entity.Property(e => e.Saldo).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.UsuarioRegistro).HasMaxLength(50);

                entity.HasOne(d => d.IdCxcNavigation)
                    .WithMany(p => p.DetalleCxcs)
                    .HasForeignKey(d => d.IdCxc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetalleCXC_CXC");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.DetalleCxcs)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetalleCXC_Venta");
            });

            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.HasKey(e => e.IdDetalleVenta);

                entity.HasIndex(e => e.IdDetalleVenta, "UQ_DetalleVenta_IdDetalleVenta")
                    .IsUnique();

                entity.Property(e => e.Cantidad).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.PrecioUnitario).HasColumnType("numeric(10, 2)");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetalleVenta_Producto");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetalleVenta_Venta");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.ToTable("Producto");

                entity.HasIndex(e => e.IdProducto, "UQ_Producto_IdProducto")
                    .IsUnique();

                entity.Property(e => e.CantidadMinima).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.CantidadTotal).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Codigo).HasMaxLength(50);

                entity.Property(e => e.Costo).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Precio).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.TipoProducto).HasMaxLength(50);

                entity.Property(e => e.UsuarioRegistro).HasMaxLength(50);

                entity.HasOne(d => d.IdSubCatProdNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdSubCatProd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Producto_SubCategoriaProd");

                entity.HasOne(d => d.IdUnidadMedidaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdUnidadMedida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Producto_UnidadMedida");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor);

                entity.ToTable("Proveedor");

                entity.HasIndex(e => e.IdProveedor, "UQ_Proveedor_IdProveedor")
                    .IsUnique();

                entity.Property(e => e.Departamento).HasMaxLength(50);

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Municipio).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Telefono).HasMaxLength(50);

                entity.HasOne(d => d.IdTipoProveedorNavigation)
                    .WithMany(p => p.Proveedors)
                    .HasForeignKey(d => d.IdTipoProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proveedor_TipoProveedor");
            });

            modelBuilder.Entity<ProveedorProducto>(entity =>
            {
                entity.HasKey(e => e.IdProveedorProducto);

                entity.ToTable("ProveedorProducto");

                entity.HasIndex(e => e.IdProveedorProducto, "UQ_ProveedorProducto_IdProveedorProducto")
                    .IsUnique();

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.ProveedorProductos)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProveedorProducto_Producto");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.ProveedorProductos)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProveedorProducto_Proveedor");
            });

            modelBuilder.Entity<SubCategoriaProd>(entity =>
            {
                entity.HasKey(e => e.IdSubCatProd);

                entity.ToTable("SubCategoriaProd");

                entity.HasIndex(e => e.IdSubCatProd, "UQ_SubCategoriaProd_IdSubCatProd")
                    .IsUnique();

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.UsuarioRegistro).HasMaxLength(50);

                entity.HasOne(d => d.IdCategoriaProductoNavigation)
                    .WithMany(p => p.SubCategoriaProds)
                    .HasForeignKey(d => d.IdCategoriaProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubCategoriaProd_CategoriaProducto");
            });

            modelBuilder.Entity<TipoProveedor>(entity =>
            {
                entity.HasKey(e => e.IdTipoProveedor);

                entity.ToTable("TipoProveedor");

                entity.HasIndex(e => e.IdTipoProveedor, "UQ_TipoProveedor_IdTipoProveedor")
                    .IsUnique();

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.UsuarioRegistro).HasMaxLength(50);
            });

            modelBuilder.Entity<UnidadMedida>(entity =>
            {
                entity.HasKey(e => e.IdUnidadMedida);

                entity.HasIndex(e => e.IdUnidadMedida, "UQ_UnidadMedida_IdUnidadMedida")
                    .IsUnique();

                entity.Property(e => e.Abreviatura).HasMaxLength(50);

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.UsuarioRegistro).HasMaxLength(50);
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.IdVenta);

                entity.HasIndex(e => e.IdVenta, "UQ_Venta_IdVenta")
                    .IsUnique();

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.NoVenta).HasMaxLength(50);

                entity.Property(e => e.UsuarioRegistro).HasMaxLength(50);

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Venta_Cliente");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
