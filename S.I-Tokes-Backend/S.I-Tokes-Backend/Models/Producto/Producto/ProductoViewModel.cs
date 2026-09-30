namespace S.I_Tokes_Backend.Models.Producto.Producto
{
    public class ProductoViewModel
    {
        public int IdSubCatProd { get; set; }
        public int IdUnidadMedida { get; set; }
        public string? Codigo { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public decimal Costo { get; set; }
        public decimal CantidadTotal { get; set; }
        public decimal CantidadMinima { get; set; }
        public byte[]? Imagen { get; set; }
        public string? Observaciones { get; set; }
        public string? TipoProducto { get; set; }
        public string UsuarioRegistro { get; set; } = null!;
    }
}
