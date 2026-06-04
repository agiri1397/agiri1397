namespace HoneywellApp.Domain.Entities.Maestros;
public class ListaPrecioCliente
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public string CodigoCliente { get; set; } = string.Empty;
    public int CodigoListaPrecio { get; set; }
    public int CodigoListaPrecioNivel { get; set; }
}
