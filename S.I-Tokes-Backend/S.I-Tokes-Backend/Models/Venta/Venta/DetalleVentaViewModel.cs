namespace S.I_Tokes_Backend.Models.Venta.Venta
{
    public class DetalleVentaViewModel
    {
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string? Observaciones { get; set; }
    }
}
