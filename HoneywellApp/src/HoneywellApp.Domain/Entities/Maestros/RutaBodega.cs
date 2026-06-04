namespace HoneywellApp.Domain.Entities.Maestros;
public class RutaBodega
{
    public int Id { get; set; }
    public string CodigoBodega { get; set; } = string.Empty;
    public string CodigoEmpresa { get; set; } = string.Empty;
    public int CodigoRuta { get; set; }
    public string NombreBodega { get; set; } = string.Empty;
}
