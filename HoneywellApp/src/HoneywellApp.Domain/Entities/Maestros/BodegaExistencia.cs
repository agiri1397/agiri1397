namespace HoneywellApp.Domain.Entities.Maestros;
public class BodegaExistencia
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public string CodigoBodega { get; set; } = string.Empty;
    public string CodigoArticulo { get; set; } = string.Empty;
    public int? ExistenciaInicial { get; set; }
    public int? ExistenciaFinal { get; set; }
    public int? ExistenciaFisico { get; set; }
}
