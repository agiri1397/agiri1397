namespace HoneywellApp.Domain.Entities.Maestros;
public class ImpuestoDetalle
{
    public int Id { get; set; }
    public string? CodigoImpuesto { get; set; }
    public int ImpuestoDetalleId { get; set; }
    public DateTime? FechaInicial { get; set; }
    public DateTime? FechaFinal { get; set; }
    public decimal Porcentaje { get; set; } = 0;
    public string? CodigoCuenta { get; set; }
}
