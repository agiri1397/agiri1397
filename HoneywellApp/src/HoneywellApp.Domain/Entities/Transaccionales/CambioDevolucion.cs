namespace HoneywellApp.Domain.Entities.Transaccionales;
public class CambioDevolucion
{
    public int Id { get; set; }
    public string CodigoUsuario { get; set; } = string.Empty;
    public string NumeroFactura { get; set; } = string.Empty;
    public string BodegaRecibe { get; set; } = string.Empty;
    public string? BodegaEntrega { get; set; }
    public int Tipo { get; set; }
    public string? Observaciones { get; set; }
    public int Bloqueado { get; set; } = 0;
    public int Confirmado { get; set; } = 0;
    public int Enviado { get; set; } = 0;
    public ICollection<CambioDevolucionDetalle> Detalles { get; set; } = new List<CambioDevolucionDetalle>();
}
