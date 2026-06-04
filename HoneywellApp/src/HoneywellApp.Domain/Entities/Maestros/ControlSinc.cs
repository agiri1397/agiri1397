namespace HoneywellApp.Domain.Entities.Maestros;
public class ControlSinc
{
    public int Id { get; set; }
    public string NombreTabla { get; set; } = string.Empty;
    public string CodigoUsuario { get; set; } = string.Empty;
    public DateTime UltimaSincronizacion { get; set; } = DateTime.Now;
    public int ResultadoSincronizacion { get; set; }
}
