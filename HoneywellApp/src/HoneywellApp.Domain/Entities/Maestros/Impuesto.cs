namespace HoneywellApp.Domain.Entities.Maestros;
public class Impuesto
{
    public int Id { get; set; }
    public string CodigoImpuesto { get; set; } = string.Empty;
    public string? CodigoImpuestoExento { get; set; }
    public string? NombreImpuesto { get; set; }
    public ICollection<ImpuestoDetalle> Detalles { get; set; } = new List<ImpuestoDetalle>();
}
