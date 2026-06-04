namespace HoneywellApp.Domain.Entities.Maestros;
public class RutaRegistroTipo
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public int CodigoRutaRegistroTipo { get; set; }
    public string? NombreRutaRegistro { get; set; }
}
