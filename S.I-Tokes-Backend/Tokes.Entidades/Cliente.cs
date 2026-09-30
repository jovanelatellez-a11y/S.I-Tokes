using System;
using System.Collections.Generic;

namespace Tokes.Entidades
{
    public partial class Cliente
    {
        public Cliente()
        {
            Venta = new HashSet<Venta>();
        }

        public int IdCliente { get; set; }
        public int IdCategoriaCliente { get; set; }
        public string? Codigo { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Departamento { get; set; }
        public string? Municipio { get; set; }
        public bool PersonaNatural { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime UsuarioRegistro { get; set; }
        public bool Estado { get; set; }

        public virtual CategoriaCliente IdCategoriaClienteNavigation { get; set; } = null!;
        public virtual Cxc? Cxc { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
