namespace HoneywellApp.Domain.Entities.Maestros;
public class PromocionCanal
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public int CodigoPromocion { get; set; }
    public int CodigoCanal { get; set; }
}
