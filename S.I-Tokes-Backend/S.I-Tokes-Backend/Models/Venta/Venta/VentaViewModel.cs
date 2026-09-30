using Tokes.Entidades;

namespace S.I_Tokes_Backend.Models.Venta.Venta
{
    public class VentaViewModel
    {
        public string? NoVenta { get; set; }
        public int IdCliente { get; set; }
        public bool Credito { get; set; }
        public string? Observaciones { get; set; }
        public string? EnviarA { get; set; }
        public string UsuarioRegistro { get; set; } = null!;

        public ICollection<DetalleVentaViewModel> DetalleVenta { get; set; }
    }
}
