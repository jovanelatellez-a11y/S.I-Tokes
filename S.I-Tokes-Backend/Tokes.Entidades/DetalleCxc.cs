using System;
using System.Collections.Generic;

namespace Tokes.Entidades
{
    public partial class DetalleCxc
    {
        public int IdDetalleCxc { get; set; }
        public int IdCxc { get; set; }
        public int IdVenta { get; set; }
        public decimal Monto { get; set; }
        public int Ncuotas { get; set; }
        public int DiasCredito { get; set; }
        public decimal Saldo { get; set; }
        public bool Cancelado { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual Cxc IdCxcNavigation { get; set; } = null!;
        public virtual Venta IdVentaNavigation { get; set; } = null!;
    }
}
