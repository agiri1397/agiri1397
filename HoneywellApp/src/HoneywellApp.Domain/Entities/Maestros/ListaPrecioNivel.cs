namespace HoneywellApp.Domain.Entities.Maestros;
public class ListaPrecioNivel
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public int CodigoListaPrecio { get; set; }
    public int CodigoListaPrecioNivel { get; set; }
    public string? NombreListaPrecioNivel { get; set; }
    public ICollection<ListaPrecioNivelDetalle> Detalles { get; set; } = new List<ListaPrecioNivelDetalle>();
}
