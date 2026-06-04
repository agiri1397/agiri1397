namespace HoneywellApp.Domain.Entities.Maestros;
public class ClienteDatos
{
    public int Id { get; set; }
    public string CodigoCliente { get; set; } = string.Empty;
    public int Estado { get; set; } = 1;
    public decimal LimiteCredito { get; set; } = 0;
    public decimal Saldo { get; set; } = 0;
    public decimal PorcentajeDescuento { get; set; } = 0;
}
