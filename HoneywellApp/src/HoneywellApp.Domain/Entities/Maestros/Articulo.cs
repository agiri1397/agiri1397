namespace HoneywellApp.Domain.Entities.Maestros;
public class Articulo
{
    public int Id { get; set; }
    public string CodigoArticulo { get; set; } = string.Empty;
    public string NombreArticulo { get; set; } = string.Empty;
    public string? NombreCorto { get; set; }
    public int? Grupo { get; set; }
    public string CodigoMedidaUnidad { get; set; } = string.Empty;
    public int? Aplica { get; set; }
    public ICollection<ArticuloImpuesto> Impuestos { get; set; } = new List<ArticuloImpuesto>();
}
