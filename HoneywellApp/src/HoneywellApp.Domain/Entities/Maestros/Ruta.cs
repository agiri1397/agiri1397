namespace HoneywellApp.Domain.Entities.Maestros;
public class Ruta
{
    public int Id { get; set; }
    public int CodigoRuta { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public string NombreRuta { get; set; } = string.Empty;
    public int CapturaExento { get; set; } = 0;
    public int CreaCliente { get; set; } = 0;
    public int AutoVenta { get; set; } = 0;
    public int RequiereCierre { get; set; } = 0;
    public string Maquina { get; set; } = "0";
    public string Establecimiento { get; set; } = "0";
}
