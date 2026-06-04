namespace HoneywellApp.Domain.Entities.Maestros;
public class Negociacion
{
    public int Id { get; set; }
    public string? Llave { get; set; }
    public string CodigoCliente { get; set; } = string.Empty;
    public string CodigoArticulo { get; set; } = string.Empty;
    public int CantidadMinima { get; set; }
    public DateTime FechaVigencia { get; set; }
    public double PorcentajeDescuento { get; set; }
}
