namespace S.I_Tokes_Backend.Models.Producto.ProveedorProducto
{
    public class ProveedorProductoViewModel
    {
        public int IdProveedor { get; set; }
        public int IdProducto { get; set; }
        public string? Observaciones { get; set; }
        public bool Predeterminado { get; set; }
        public long UsuarioRegistro { get; set; }
    }
}
