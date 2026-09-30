using Tokes.Entidades;

namespace S.I_Tokes_Backend.Models.Cliente.CategoriaCliente
{
    public class CategoriaClienteViewModel
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string UsuarioRegistro { get; set; } = null!;
    }
}
