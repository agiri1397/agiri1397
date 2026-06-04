namespace HoneywellApp.Domain.Entities.Transaccionales;
public class RutaRegistro
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public string CodigoCliente { get; set; } = string.Empty;
    public int CodigoRuta { get; set; }
    public int CodigoRegistroTipo { get; set; }
    public int FechaDiaRuta { get; set; }
    public int FechaSemanRuta { get; set; }
    public DateTime FechaOperacion { get; set; } = DateTime.Now;
    public int Latitud { get; set; } = 0;
    public int Longitud { get; set; } = 0;
    public int Enviado { get; set; } = 0;
    public string? Uuid { get; set; }
}
