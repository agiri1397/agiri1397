namespace HoneywellApp.Domain.Entities.Transaccionales;
public class BackOrder
{
    public int Id { get; set; }
    public string? Serie { get; set; }
    public int? Correlativo { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public string Producto { get; set; } = string.Empty;
    public int CantidadRequerida { get; set; }
    public int CantidadDisponible { get; set; }
    public int RutaId { get; set; }
    public DateTime Fecha { get; set; }
    public string? Comentario { get; set; }
    public int PedidoId { get; set; }
    public int Enviado { get; set; } = 0;
}
