namespace HoneywellApp.Domain.Entities.Maestros;
public class RutaLibro
{
    public int Id { get; set; }
    public string? CodigoEmpresa { get; set; }
    public int CodigoRuta { get; set; }
    public string CodigoCliente { get; set; } = string.Empty;
    public int OrdenVisita { get; set; } = 999;
    public int Semana { get; set; }
    public int Dia { get; set; }
    public string Direccion { get; set; } = string.Empty;
    public int Estado { get; set; } = 0;
}
