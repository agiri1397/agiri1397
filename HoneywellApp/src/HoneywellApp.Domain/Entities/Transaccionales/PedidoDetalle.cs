namespace HoneywellApp.Domain.Entities.Transaccionales;
public class PedidoDetalle
{
    public int Id { get; set; }
    public int NumeroPedido { get; set; }
    public int PedidoDetalleId { get; set; } = 0;
    public string CodigoArticulo { get; set; } = string.Empty;
    public string CodigoMedidaUnidad { get; set; } = string.Empty;
    public int Cantidad { get; set; } = 0;
    public decimal CostoUnitario { get; set; } = 0;
    public decimal PrecioSinDescuento { get; set; } = 0;
    public decimal PrecioConDescuento { get; set; } = 0;
    public decimal PrecioSinDescuentoAEnviar { get; set; } = 0;
    public decimal PrecioConDescuentoAEnviar { get; set; } = 0;
    public decimal PorcentajeDescuento { get; set; } = 0;
    public decimal MontoBaseDescuento { get; set; } = 0;
    public decimal MontoDescuento { get; set; } = 0;
    public decimal MontoBaseImpuesto { get; set; } = 0;
    public decimal MontoImpuesto { get; set; } = 0;
    public int Enviado { get; set; } = 0;
    public decimal Cajas { get; set; } = 0;
    public decimal MontoLinea { get; set; } = 0;
    public int LineaSincronizada { get; set; } = 0;
    public ICollection<PedidoDetalleImpuesto> Impuestos { get; set; } = new List<PedidoDetalleImpuesto>();
}
