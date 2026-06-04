namespace HoneywellApp.Domain.Entities.Transaccionales;
public class FacturaDetalle
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public string CodigoSerie { get; set; } = string.Empty;
    public int NumeroFactura { get; set; }
    public int NumeroLinea { get; set; }
    public string CodigoArticulo { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal Monto { get; set; }
    public decimal MontoImpuesto { get; set; }
    public string NombreArticulo { get; set; } = string.Empty;
    public string MedidaUnidad { get; set; } = string.Empty;
    public int Unidad { get; set; }
    public int Decimal { get; set; }
    public int Unidades { get; set; }
    public int PrecioUnitario { get; set; }
    public decimal Total { get; set; }
}
