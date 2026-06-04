namespace HoneywellApp.Domain.Entities.Maestros;
public class ClienteDireccion
{
    public int Id { get; set; }
    public int CodigoClienteDireccionId { get; set; }
    public string CodigoCliente { get; set; } = string.Empty;
    public string? Direccion { get; set; }
    public string? CodigoMunicipio { get; set; }
    public int? IdMunicipio { get; set; }
    public string? Latitud { get; set; }
    public string? Longitud { get; set; }
}
