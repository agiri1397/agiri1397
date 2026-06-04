namespace HoneywellApp.Domain.Entities.Maestros;
public class GrupoArticulo
{
    public int Id { get; set; }
    public int GrupoId { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public double? Valor { get; set; }
}
