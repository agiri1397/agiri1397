namespace HoneywellApp.Domain.Entities.Transaccionales;
public class PedidoBeneficioDetalle
{
    public int Id { get; set; }
    public int NumeroPedido { get; set; }
    public string CodigoArticulo { get; set; } = string.Empty;
    public string CodigoMedidaUnidad { get; set; } = string.Empty;
    public int CodigoPromocionOrigen { get; set; }
    public int CodigoPromocion { get; set; }
    public int CodigoPromocionGrupoOrigen { get; set; }
    public int Cantidad { get; set; } = 0;
    public decimal Cajas { get; set; } = 0;
    public decimal PorcentajeDescuento { get; set; } = 0;
    public string LineasOrigen { get; set; } = string.Empty;
    public string LineasDestino { get; set; } = "0";
    public decimal PrecioBruto { get; set; } = 0;
    public decimal MontoDescuento { get; set; } = 0;
}
