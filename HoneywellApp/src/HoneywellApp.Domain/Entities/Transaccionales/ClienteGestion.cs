namespace HoneywellApp.Domain.Entities.Transaccionales;
public class ClienteGestion
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public string? CodigoClienteGestion { get; set; }
    public int CodigoRuta { get; set; }
    public string? NombreCliente { get; set; }
    public string? NombrePropietario { get; set; }
    public string? IdentificacionTributaria { get; set; }
    public string? IdentificacionPersonal { get; set; }
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
    public string? Colonia { get; set; }
    public int? Zona { get; set; }
    public int? CodigoPais { get; set; }
    public int? CodigoDepartamento { get; set; }
    public int? CodigoMunicipio { get; set; }
    public string? Categoria { get; set; }
    public string? Subcanal { get; set; }
    public decimal Latitud { get; set; } = 0;
    public decimal Longitud { get; set; } = 0;
    public DateTime? FechaOperacion { get; set; }
    public int Enviado { get; set; } = 0;
}
