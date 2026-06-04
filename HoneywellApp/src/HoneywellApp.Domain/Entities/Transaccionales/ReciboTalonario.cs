namespace HoneywellApp.Domain.Entities.Transaccionales;
public class ReciboTalonario
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public int CodigoReciboTalonarioId { get; set; }
    public int CodigoRuta { get; set; }
    public int NumeroInicial { get; set; }
    public int NumeroFinal { get; set; }
    public int NumeroActual { get; set; }
    public string SerieRecibo { get; set; } = string.Empty;
}
