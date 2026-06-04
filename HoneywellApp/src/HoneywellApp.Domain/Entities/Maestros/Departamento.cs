namespace HoneywellApp.Domain.Entities.Maestros;
public class Departamento
{
    public int Id { get; set; }
    public int CodigoPais { get; set; }
    public int CodigoDepartamento { get; set; }
    public string? NombreDepartamento { get; set; }
    public ICollection<Municipio> Municipios { get; set; } = new List<Municipio>();
}
