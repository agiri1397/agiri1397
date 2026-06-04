namespace HoneywellApp.Domain.Entities.Maestros;
public class CuentaBancaria
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public string NumeroCuentaBancaria { get; set; } = string.Empty;
    public string NombreCuentaBancaria { get; set; } = string.Empty;
    public int CodigoBanco { get; set; }
    public string CodigoCuenta { get; set; } = string.Empty;
    public int CuentaBancariaId { get; set; }
}
