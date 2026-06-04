namespace HoneywellApp.Domain.Entities.Transaccionales;
public class PedidoNegociacionDetalle
{
    public int Id { get; set; }
    public int NumeroPedido { get; set; }
    public string? Llave { get; set; }
    public string CodigoCliente { get; set; } = string.Empty;
    public string CodigoArticulo { get; set; } = string.Empty;
    public int CantidadMinima { get; set; }
    public DateTime FechaVigencia { get; set; }
    public double PorcentajeDescuento { get; set; }
}
