namespace HoneywellApp.Domain.Entities.Maestros;
public class PromocionGrupoDetalle
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public int CodigoPromocionGrupo { get; set; }
    public string CodigoArticulo { get; set; } = string.Empty;
    public DateTime? FechaInicial { get; set; }
    public DateTime? FechaFinal { get; set; }
    public decimal Factor { get; set; } = 1;
}
