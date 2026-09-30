using System;
using System.Collections.Generic;

namespace Tokes.Entidades
{
    public partial class Cxc
    {
        public Cxc()
        {
            DetalleCxcs = new HashSet<DetalleCxc>();
        }

        public int IdCxc { get; set; }
        public string? NoCxc { get; set; }
        public int IdCliente { get; set; }
        public string? Observaciones { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual ICollection<DetalleCxc> DetalleCxcs { get; set; }
    }
}
