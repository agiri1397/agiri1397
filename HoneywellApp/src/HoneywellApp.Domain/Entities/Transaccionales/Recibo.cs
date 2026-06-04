namespace HoneywellApp.Domain.Entities.Transaccionales;
public class Recibo
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public string CodigoCliente { get; set; } = string.Empty;
    public int CodigoRuta { get; set; }
    public string Usuario { get; set; } = string.Empty;
    public int NumeroRecibo { get; set; }
    public string SerieRecibo { get; set; } = string.Empty;
    public DateTime FechaRecibo { get; set; } = DateTime.Now;
    public decimal Monto { get; set; }
    public int? Anulado { get; set; }
    public int? Enviado { get; set; }
    public ICollection<ReciboDetalleCheque> DetallesCheque { get; set; } = new List<ReciboDetalleCheque>();
    public ICollection<ReciboDetalleFactura> DetallesFactura { get; set; } = new List<ReciboDetalleFactura>();
}
