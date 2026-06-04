namespace HoneywellApp.Domain.Entities.Maestros;
public class BodegaArticulo
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public string CodigoBodega { get; set; } = string.Empty;
    public string CodigoArticulo { get; set; } = string.Empty;
    public decimal? CostoUnitario { get; set; }
    public int Existencia { get; set; } = 0;
    public int Transito { get; set; } = 0;
}
