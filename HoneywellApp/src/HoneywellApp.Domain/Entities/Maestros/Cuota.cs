namespace HoneywellApp.Domain.Entities.Maestros;
public class Cuota
{
    public int Id { get; set; }
    public string Categoria { get; set; } = string.Empty;
    public decimal Requerido { get; set; } = 0;
    public decimal Alcanzado { get; set; } = 0;
}
