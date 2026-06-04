namespace HoneywellApp.Domain.Entities.Transaccionales;
public class Documento
{
    public int Id { get; set; }
    public string DocumentoId { get; set; } = string.Empty;
    public int CodigoCliente { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public int Estado { get; set; } = 0;
    public string? Destinatario { get; set; }
    public string? Origen { get; set; }
    public int Enviado { get; set; } = 0;
}
