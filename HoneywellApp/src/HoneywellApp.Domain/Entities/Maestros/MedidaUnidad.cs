namespace HoneywellApp.Domain.Entities.Maestros;
public class MedidaUnidad
{
    public int Id { get; set; }
    public string CodigoMedidaUnidad { get; set; } = string.Empty;
    public string NombreMedidaUnidad { get; set; } = string.Empty;
    public int Unidad { get; set; }
    public decimal Decimal { get; set; }
}
