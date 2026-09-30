using System;
using System.Collections.Generic;

namespace Tokes.Entidades
{
    public partial class CategoriaProducto
    {
        public CategoriaProducto()
        {
            SubCategoriaProds = new HashSet<SubCategoriaProd>();
        }

        public int IdCategoriaProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<SubCategoriaProd> SubCategoriaProds { get; set; }
    }
}
