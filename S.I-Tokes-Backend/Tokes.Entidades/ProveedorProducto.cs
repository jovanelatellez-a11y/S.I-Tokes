using System;
using System.Collections.Generic;

namespace Tokes.Entidades
{
    public partial class ProveedorProducto
    {
        public int IdProveedorProducto { get; set; }
        public int IdProveedor { get; set; }
        public int IdProducto { get; set; }
        public string? Observaciones { get; set; }
        public bool Predeterminado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public long UsuarioRegistro { get; set; }
        public bool Estado { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;
        public virtual Proveedor IdProveedorNavigation { get; set; } = null!;
    }
}
