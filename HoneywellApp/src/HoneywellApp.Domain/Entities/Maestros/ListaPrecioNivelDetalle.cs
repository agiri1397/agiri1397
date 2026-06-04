namespace HoneywellApp.Domain.Entities.Maestros;
public class ListaPrecioNivelDetalle
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public int CodigoListaPrecio { get; set; }
    public int CodigoListaPrecioNivel { get; set; }
    public string CodigoArticulo { get; set; } = string.Empty;
    public DateTime? FechaInicial { get; set; }
    public DateTime? FechaFinal { get; set; }
    public decimal? PrecioExento { get; set; }
    public decimal? PrecioGravado { get; set; }
}
