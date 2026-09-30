namespace S.I_Tokes_Backend.Models.Compra.Compra
{
    public class DetalleCompraViewModel
    {
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CostoUnitario { get; set; }
        public string? Observaciones { get; set; }
    }
}
