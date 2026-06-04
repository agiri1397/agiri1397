namespace HoneywellApp.Domain.Entities.Maestros;
public class PromocionRuta
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public int CodigoPromocion { get; set; }
    public int CodigoRuta { get; set; }
}
