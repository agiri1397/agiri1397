namespace HoneywellApp.Domain.Entities.Maestros;
public class ClienteTipo
{
    public int Id { get; set; }
    public string CodigoCliente { get; set; } = string.Empty;
    public int CodigoTipo { get; set; }
    public int Prioridad { get; set; }
}
