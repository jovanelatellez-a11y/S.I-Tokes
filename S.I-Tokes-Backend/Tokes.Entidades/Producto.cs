using System;
using System.Collections.Generic;

namespace Tokes.Entidades
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
            DetalleVenta = new HashSet<DetalleVenta>();
            ProveedorProductos = new HashSet<ProveedorProducto>();
        }

        public int IdProducto { get; set; }
        public int IdSubCatProd { get; set; }
        public int IdUnidadMedida { get; set; }
        public string? Codigo { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public decimal Costo { get; set; }
        public decimal CantidadTotal { get; set; }
        public decimal CantidadMinima { get; set; }
        public byte[]? Imagen { get; set; }
        public string? Observaciones { get; set; }
        public string? TipoProducto { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual SubCategoriaProd IdSubCatProdNavigation { get; set; } = null!;
        public virtual UnidadMedida IdUnidadMedidaNavigation { get; set; } = null!;
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
        public virtual ICollection<ProveedorProducto> ProveedorProductos { get; set; }
    }
}
