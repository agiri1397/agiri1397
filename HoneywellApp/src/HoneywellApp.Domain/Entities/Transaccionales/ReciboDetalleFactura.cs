namespace HoneywellApp.Domain.Entities.Transaccionales;
public class ReciboDetalleFactura
{
    public int Id { get; set; }
    public string NumeroReciboId { get; set; } = string.Empty;
    public string CodigoSerie { get; set; } = string.Empty;
    public int NumeroFactura { get; set; }
    public decimal MontoPago { get; set; }
}
