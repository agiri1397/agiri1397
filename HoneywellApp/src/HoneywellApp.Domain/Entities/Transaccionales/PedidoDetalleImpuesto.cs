namespace HoneywellApp.Domain.Entities.Transaccionales;
public class PedidoDetalleImpuesto
{
    public int Id { get; set; }
    public int NumeroPedido { get; set; }
    public int PedidoDetalleId { get; set; }
    public int ImpuestoDetalleId { get; set; }
    public decimal Monto { get; set; } = 0;
    public decimal Porcentaje { get; set; } = 0;
}
