namespace HoneywellApp.Domain.Entities.Maestros;
public class PromocionGrupo
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public int CodigoPromocionGrupo { get; set; }
    public string NombrePromocionGrupo { get; set; } = string.Empty;
    public DateTime? FechaInicial { get; set; }
    public DateTime? FechaFinal { get; set; }
}
