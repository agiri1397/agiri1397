namespace HoneywellApp.Domain.Entities.Maestros;
public class ClienteCuentaBancaria
{
    public int Id { get; set; }
    public string CodigoCliente { get; set; } = string.Empty;
    public string NumeroCuenta { get; set; } = string.Empty;
    public int CodigoBanco { get; set; }
    public string NombreCuenta { get; set; } = string.Empty;
    public int ClienteCuentaBancariaId { get; set; }
}
