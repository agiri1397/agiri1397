namespace HoneywellApp.Domain.Entities.Maestros;
public class PrecioEspecial
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public string CodigoCliente { get; set; } = string.Empty;
    public string CodigoArticulo { get; set; } = string.Empty;
    public int CodigoMoneda { get; set; }
    public DateTime? FechaInicial { get; set; }
    public DateTime? FechaFinal { get; set; }
    public decimal? PrecioExento { get; set; }
    public decimal? Precio { get; set; }
}
