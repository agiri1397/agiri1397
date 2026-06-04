namespace HoneywellApp.Domain.Entities.Maestros;
public class Pais
{
    public int Id { get; set; }
    public int CodigoPais { get; set; }
    public string? NombrePais { get; set; }
    public string? CodigoIso3166 { get; set; }
    public ICollection<Departamento> Departamentos { get; set; } = new List<Departamento>();
}
