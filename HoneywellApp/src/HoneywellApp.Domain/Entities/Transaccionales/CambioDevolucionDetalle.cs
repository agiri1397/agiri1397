namespace HoneywellApp.Domain.Entities.Transaccionales;
public class CambioDevolucionDetalle
{
    public int Id { get; set; }
    public int CambioId { get; set; }
    public string CodigoArticulo { get; set; } = string.Empty;
    public int CantidadDevuelta { get; set; }
    public decimal TotalReembolso { get; set; } = 0;
}
