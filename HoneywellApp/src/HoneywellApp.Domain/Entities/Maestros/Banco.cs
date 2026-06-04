namespace HoneywellApp.Domain.Entities.Maestros;
public class Banco
{
    public int Id { get; set; }
    public int CodigoBanco { get; set; }
    public string NombreBanco { get; set; } = string.Empty;
    public string? Contacto { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
}
