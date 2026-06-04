namespace HoneywellApp.Domain.Entities.Transaccionales;
public class Pedido
{
    public int Id { get; set; }
    public int NumeroPedido { get; set; }
    public int CodigoRuta { get; set; }
    public string CodigoBodega { get; set; } = string.Empty;
    public string CodigoCliente { get; set; } = string.Empty;
    public int ClienteDireccionId { get; set; }
    public string CodigoCredito { get; set; } = string.Empty;
    public DateTime? FechaPedido { get; set; }
    public DateTime? FechaEntrega { get; set; }
    public string? Referencia { get; set; }
    public string? NumeroOrdenCompra { get; set; }
    public int Exento { get; set; } = 0;
    public string? Observacion { get; set; }
    public DateTime? FechaOperacion { get; set; }
    public string? CodigoUsuario { get; set; }
    public int Enviado { get; set; } = 0;
    public int Confirmado { get; set; } = 0;
    public int Bloqueado { get; set; } = 0;
    public int ClienteNuevo { get; set; } = 0;
    public int Promocional { get; set; } = 0;
    public string? FacturaRefSerie { get; set; }
    public int? FacturaRefNumero { get; set; }
    public string? FacturaSeriFel { get; set; }
    public int? FacturaNumeroFel { get; set; }
    public int? FacturaNumeroAccesoFel { get; set; }
    public string Uuid { get; set; } = string.Empty;
    public int? NumeroPedidoRemoto { get; set; }
    public int Sincronizado { get; set; } = 0;
    public int Editado { get; set; } = 1;
    public ICollection<PedidoDetalle> Detalles { get; set; } = new List<PedidoDetalle>();
    public ICollection<PedidoBeneficioDetalle> Beneficios { get; set; } = new List<PedidoBeneficioDetalle>();
}
