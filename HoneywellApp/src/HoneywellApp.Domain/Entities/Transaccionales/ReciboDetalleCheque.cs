namespace HoneywellApp.Domain.Entities.Transaccionales;
public class ReciboDetalleCheque
{
    public int Id { get; set; }
    public int ReciboId { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public int? CodigoCuenta { get; set; }
    public int? NumeroCheque { get; set; }
    public DateTime? FechaCheque { get; set; }
    public int Postfechado { get; set; } = 0;
    public decimal Monto { get; set; }
}
