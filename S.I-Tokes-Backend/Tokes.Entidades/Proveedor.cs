using System;
using System.Collections.Generic;

namespace Tokes.Entidades
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Compras = new HashSet<Compra>();
            ProveedorProductos = new HashSet<ProveedorProducto>();
        }

        public int IdProveedor { get; set; }
        public int IdTipoProveedor { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Departamento { get; set; }
        public string? Municipio { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public DateTime FechaRegistro { get; set; }
        public long UsuarioRegistro { get; set; }
        public bool Estado { get; set; }

        public virtual TipoProveedor IdTipoProveedorNavigation { get; set; } = null!;
        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<ProveedorProducto> ProveedorProductos { get; set; }
    }
}
