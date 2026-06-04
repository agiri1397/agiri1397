namespace HoneywellApp.Domain.Entities.Maestros;
public class Promocion
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public int CodigoPromocion { get; set; }
    public string? NombrePromocion { get; set; }
    public int PromocionOrigen { get; set; }
    public decimal Porcentaje { get; set; } = 0;
    public DateTime? FechaInicial { get; set; }
    public DateTime? FechaFinal { get; set; }
    public int Reguladora { get; set; } = 0;
}
