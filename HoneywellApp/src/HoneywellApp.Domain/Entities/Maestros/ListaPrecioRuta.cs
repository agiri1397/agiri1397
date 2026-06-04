namespace HoneywellApp.Domain.Entities.Maestros;
public class ListaPrecioRuta
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public int CodigoRuta { get; set; }
    public int? CodigoListaPrecio { get; set; }
    public int CodigoListaPrecioNivel { get; set; }
}
