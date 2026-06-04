namespace HoneywellApp.Domain.Entities.Maestros;
public class PromocionDetalle
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public int CodigoPromocion { get; set; }
    public string CodigoArticulo { get; set; } = string.Empty;
    public int Obligatorio { get; set; }
    public int Cantidad { get; set; } = 0;
    public decimal? PorcentajeParticipacion { get; set; }
}
