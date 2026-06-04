namespace HoneywellApp.Domain.Entities.Maestros;
public class Contingencia
{
    public int Id { get; set; }
    public int CodigoSerie { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public long NumeroActual { get; set; }
    public int CodigoRuta { get; set; }
    public string CodigoBodega { get; set; } = string.Empty;
    public long NumeroInicial { get; set; }
    public long NumeroFinal { get; set; }
}
