using System;
using System.Collections.Generic;

namespace Tokes.Entidades
{
    public partial class TipoProveedor
    {
        public TipoProveedor()
        {
            Proveedors = new HashSet<Proveedor>();
        }

        public int IdTipoProveedor { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Observaciones { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<Proveedor> Proveedors { get; set; }
    }
}
