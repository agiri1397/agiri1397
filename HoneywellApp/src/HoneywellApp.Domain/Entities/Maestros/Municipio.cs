namespace HoneywellApp.Domain.Entities.Maestros;
public class Municipio
{
    public int Id { get; set; }
    public int CodigoPais { get; set; }
    public int CodigoDepartamento { get; set; }
    public int CodigoMunicipio { get; set; }
    public string? NombreMunicipio { get; set; }
}
