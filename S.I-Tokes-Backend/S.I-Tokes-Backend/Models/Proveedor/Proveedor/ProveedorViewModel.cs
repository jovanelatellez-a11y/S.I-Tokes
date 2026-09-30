namespace S.I_Tokes_Backend.Models.Proveedor.Proveedor
{
    public class ProveedorViewModel
    {
        public int IdTipoProveedor { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Departamento { get; set; }
        public string? Municipio { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public long UsuarioRegistro { get; set; }
    }
}
