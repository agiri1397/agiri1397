namespace HoneywellApp.Domain.Entities.Maestros;
public class RutaCliente
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public int CodigoRuta { get; set; }
    public string CodigoCliente { get; set; } = string.Empty;
}
