namespace HoneywellApp.Domain.Entities.Maestros;
public class PromocionGrupoPromocion
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public int CodigoPromocionGrupo { get; set; }
    public int CodigoPromocion { get; set; }
    public int Cantidad { get; set; } = 0;
    public decimal Monto { get; set; } = 0;
    public decimal PorcentajeProporcional { get; set; } = 0;
    public int CantidadProporcional { get; set; } = 0;
}
