using Tokes.Entidades;

namespace S.I_Tokes_Backend.Models.Cliente.Cliente
{
    public class ClienteViewModel
    {
        public int IdCategoriaCliente { get; set; }
        public string? Codigo { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Departamento { get; set; }
        public string? Municipio { get; set; }
        public bool PersonaNatural { get; set; }
        public DateTime UsuarioRegistro { get; set; }
    }
}
