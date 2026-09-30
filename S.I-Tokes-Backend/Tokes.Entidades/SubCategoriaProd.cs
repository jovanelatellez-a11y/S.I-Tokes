using System;
using System.Collections.Generic;

namespace Tokes.Entidades
{
    public partial class SubCategoriaProd
    {
        public SubCategoriaProd()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdSubCatProd { get; set; }
        public int IdCategoriaProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual CategoriaProducto IdCategoriaProductoNavigation { get; set; } = null!;
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
