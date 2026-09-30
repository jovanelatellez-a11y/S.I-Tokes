using System;
using System.Collections.Generic;

namespace Tokes.Entidades
{
    public partial class CategoriaCliente
    {
        public CategoriaCliente()
        {
            Clientes = new HashSet<Cliente>();
        }

        public int IdCategoriaCliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
