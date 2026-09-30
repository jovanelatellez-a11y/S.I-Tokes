using System;
using System.Collections.Generic;

namespace Tokes.Entidades
{
    public partial class UnidadMedida
    {
        public UnidadMedida()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdUnidadMedida { get; set; }
        public string? Abreviatura { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
