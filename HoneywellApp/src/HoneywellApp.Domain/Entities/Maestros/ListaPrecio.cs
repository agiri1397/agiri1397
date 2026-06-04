namespace HoneywellApp.Domain.Entities.Maestros;
public class ListaPrecio
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public int CodigoListaPrecio { get; set; }
    public string? NombreListaPrecio { get; set; }
    public int CodigoMoneda { get; set; }
    public ICollection<ListaPrecioNivel> Niveles { get; set; } = new List<ListaPrecioNivel>();
}
