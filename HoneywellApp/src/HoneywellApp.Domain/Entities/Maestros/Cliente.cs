namespace HoneywellApp.Domain.Entities.Maestros;
public class Cliente
{
    public int Id { get; set; }
    public string CodigoCliente { get; set; } = string.Empty;
    public string? NombreCliente { get; set; }
    public string? Telefono { get; set; }
    public string? IdentificacionTributaria { get; set; }
    public int Estado { get; set; } = 1;
    public string CodigoCredito { get; set; } = "0";
    public string? RazonSocial { get; set; }
    public string? Tipo { get; set; }
    public string? Enviado { get; set; }
    public string? IdPersonal { get; set; }
    public string? IdTipoPersonal { get; set; }
    public ICollection<ClienteDireccion> Direcciones { get; set; } = new List<ClienteDireccion>();
    public ICollection<ClienteImpuesto> Impuestos { get; set; } = new List<ClienteImpuesto>();
    public ClienteDatos? Datos { get; set; }
}
