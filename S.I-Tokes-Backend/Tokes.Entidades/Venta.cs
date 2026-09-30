using System;
using System.Collections.Generic;

namespace Tokes.Entidades
{
    public partial class Venta
    {
        public Venta()
        {
            DetalleCxcs = new HashSet<DetalleCxc>();
            DetalleVenta = new HashSet<DetalleVenta>();
        }

        public int IdVenta { get; set; }
        public string? NoVenta { get; set; }
        public int IdCliente { get; set; }
        public bool Credito { get; set; }
        public string? Observaciones { get; set; }
        public string? EnviarA { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual ICollection<DetalleCxc> DetalleCxcs { get; set; }
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
    }
}
