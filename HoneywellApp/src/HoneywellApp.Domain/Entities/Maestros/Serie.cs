namespace HoneywellApp.Domain.Entities.Maestros;
public class Serie
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public string CodigoSerie { get; set; } = string.Empty;
    public long NumeroActual { get; set; }
    public int CodigoRuta { get; set; }
    public string CodigoBodega { get; set; } = string.Empty;
    public string NumeroAutorizacionFiscal { get; set; } = string.Empty;
    public long NumeroInicial { get; set; }
    public long NumeroFinal { get; set; }
    public int Electronica { get; set; } = 0;
    public DateTime? FechaAutorizacion { get; set; }
}
