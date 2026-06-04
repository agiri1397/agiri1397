namespace HoneywellApp.Domain.Entities.Maestros;
public class Usuario
{
    public int Id { get; set; }
    public string CodigoUsuario { get; set; } = string.Empty;
    public string CodigoVendedor { get; set; } = string.Empty;
    public string NombreVendedor { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int SesionActiva { get; set; } = 0;
    public long UltimaSincronizacion { get; set; } = 0;
}
