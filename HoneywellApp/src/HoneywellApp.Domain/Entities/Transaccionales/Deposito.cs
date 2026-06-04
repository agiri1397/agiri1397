namespace HoneywellApp.Domain.Entities.Transaccionales;
public class Deposito
{
    public int Id { get; set; }
    public string CodigoEmpresa { get; set; } = string.Empty;
    public int CodigoCuentaBancariaId { get; set; }
    public int CodigoRuta { get; set; }
    public string CodigoUsuario { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.Now;
    public string BoletaSerie { get; set; } = string.Empty;
    public string BoletaConsecutivo { get; set; } = string.Empty;
    public decimal MontoEfectivo { get; set; }
    public decimal MontoCheque { get; set; }
    public int Cerrado { get; set; } = 0;
    public int Enviado { get; set; } = 0;
    public string FechaCreacion { get; set; } = string.Empty;
    public string HoraCreacion { get; set; } = string.Empty;
}
