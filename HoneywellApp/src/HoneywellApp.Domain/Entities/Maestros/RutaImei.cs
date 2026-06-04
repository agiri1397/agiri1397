namespace HoneywellApp.Domain.Entities.Maestros;
public class RutaImei
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public int CodigoRuta { get; set; }
    public string? CodigoUsuario { get; set; }
    public string? Imei { get; set; }
    public int UsuarioBloqueado { get; set; } = 0;
}
