using Tokes.Entidades;

namespace S.I_Tokes_Backend.Models.Compra.Compra
{
    public class CompraViewModel
    {
        public string? NoOrden { get; set; }
        public int IdProveedor { get; set; }
        public bool Aprobada { get; set; }
        public string? Observaciones { get; set; }
        public string UsuarioRegistro { get; set; } = null!;

        public ICollection<DetalleCompraViewModel> Detalle { get; set; }
    }
}
