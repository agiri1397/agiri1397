namespace HoneywellApp.Domain.Entities.Transaccionales;
public class Factura
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public string CodigoSerie { get; set; } = string.Empty;
    public int NumeroFactura { get; set; } = 0;
    public string? Firma { get; set; }
    public string CodigoBodega { get; set; } = string.Empty;
    public DateTime FechaFactura { get; set; }
    public string CodigoCliente { get; set; } = string.Empty;
    public int DiasCredito { get; set; }
    public int CodigoRuta { get; set; }
    public decimal MontoImpuesto { get; set; }
    public decimal MontoFactura { get; set; }
    public decimal SaldoFactura { get; set; } = 0;
    public string Direccion { get; set; } = string.Empty;
    public int? Anulada { get; set; }
    public int PedidoId { get; set; } = 0;
    public int Enviado { get; set; } = 0;
    public int Bloqueada { get; set; } = 0;
    public int Nueva { get; set; } = 0;
    public int Promocional { get; set; } = 0;
    public string? FacturaRefSerie { get; set; }
    public int? FacturaRefNumero { get; set; }
    public int Impresiones { get; set; } = 0;
    public string? SerieFel { get; set; }
    public int? NumeroFel { get; set; }
    public int? NumeroAcceso { get; set; }
    public string? Folio { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public string? Uuid { get; set; }
    public ICollection<FacturaDetalle> Detalles { get; set; } = new List<FacturaDetalle>();
}
