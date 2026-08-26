using System.Text.Json.Serialization;

namespace SistemaILP.Ruteo.Application.DTOs;

/// <summary>
/// Cuerpo del POST a "validarUsuario". Migracion literal de
/// WsLoginDTO.java (LoganSquare): el JSON debe seguir usando "clave"
/// para la contrasena y "usuario"/"imei" tal cual - NO renombrar estas
/// claves aunque las propiedades C# usen nombres mas claros.
/// El JSON original de Android tecnicamente hereda tambien de
/// WsResultDTO (resultado/mensaje/numeroFactura) por una particularidad
/// de esa clase, pero el request real que espera el servidor es
/// unicamente { usuario, clave, imei }.
/// </summary>
public class WsLoginRequestDto
{
    [JsonPropertyName("usuario")]
    public string Usuario { get; set; } = string.Empty;

    [JsonPropertyName("clave")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("imei")]
    public string Imei { get; set; } = string.Empty;
}

/// <summary>
/// Migracion literal de WsResultDTO.java: resultado base de cualquier
/// transaccion via Web Service. resultado == 1 indica exito (ver
/// LoginService.SUCCESSFULL_LOGIN); cualquier otro valor es fallo, con
/// el detalle en "mensaje".
/// </summary>
public class WsResultDto
{
    [JsonPropertyName("resultado")]
    public int Resultado { get; set; } = 1;

    [JsonPropertyName("mensaje")]
    public string? Mensaje { get; set; }

    [JsonPropertyName("numeroFactura")]
    public long NumeroFactura { get; set; }
}

/// <summary>
/// Migracion literal de ConfiguracionDTO.java. Se conserva completa
/// (no se descarta nada tras el login) porque la app Android existente
/// vuelca todos estos valores a SharedPreferences para uso posterior
/// por los modulos de Preventa/Autoventa/Despachos.
/// </summary>
public class ConfiguracionDto
{
    [JsonPropertyName("codigoRegistroTipoPedido")]
    public int CodigoRegistroTipoPedido { get; set; }

    [JsonPropertyName("codigoRegistroTipoActualizarGeoreferencia")]
    public int CodigoRegistroTipoActualizarGeoreferencia { get; set; }

    [JsonPropertyName("codigoRegistroNoVenta")]
    public string? CodigoRegistroNoVenta { get; set; }

    [JsonPropertyName("codigoRegistroNoVentaBloquear")]
    public string? CodigoRegistroNoVentaBloquear { get; set; }

    [JsonPropertyName("codigoImpuestoPorDefecto")]
    public string? CodigoImpuestoPorDefecto { get; set; }

    [JsonPropertyName("modalidadDeCalculoDePrecios")]
    public int ModalidadDeCalculoDePrecios { get; set; }

    [JsonPropertyName("codigoImpuestoAcumulado")]
    public string? CodigoImpuestoAcumulado { get; set; }

    // El Java original anota estos dos campos con
    // @JsonField(name = DataBaseContract.UsuarioEntry.COLUMN_CODIGO_VENDEDOR/COLUMN_NOMBRE_VENDEDOR),
    // cuyos valores reales son "vendedor" y "nombre" (ver UsuarioEntry) -
    // NO son "codigoVendedor"/"nombreVendedor" en el JSON.
    [JsonPropertyName("vendedor")]
    public string CodigoVendedor { get; set; } = string.Empty;

    [JsonPropertyName("nombre")]
    public string NombreVendedor { get; set; } = string.Empty;

    [JsonPropertyName("porcentajeFacturacionPromocional")]
    public double PorcentajeFacturacionPromocional { get; set; }

    [JsonPropertyName("pais")]
    public int Pais { get; set; }

    [JsonPropertyName("otrasOpcionesUrl")]
    public string? OtrasOpcionesUrl { get; set; }

    [JsonPropertyName("urlGuateFacturas")]
    public string? UrlGuateFacturas { get; set; }

    [JsonPropertyName("UrlConsultaFEL")]
    public string? UrlConsultaFEL { get; set; }

    [JsonPropertyName("UrlFELServer")]
    public string? UrlFELServer { get; set; }
}

/// <summary>Migracion literal de WsLoginResultDTO.java.</summary>
public class WsLoginResultDto : WsResultDto
{
    [JsonPropertyName("configuracion")]
    public ConfiguracionDto? Configuracion { get; set; }
}
