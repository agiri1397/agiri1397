namespace SistemaILP.Ruteo.Application.Configuration;

/// <summary>
/// Configuracion del backend HTTP. Preventa/Autoventa/Despachos
/// comparten la misma Base URL por ahora; PreventaNicaragua (etapa
/// futura) podra apuntar a una distinta sin cambiar esta clase, solo
/// el valor con el que se construye.
/// </summary>
public class WebServiceConfiguration
{
    /// <summary>
    /// URL base del backend, DEBE terminar en "/" (los endpoints son
    /// rutas relativas, p. ej. "validarUsuario", igual que en Retrofit).
    /// </summary>
    public required string BaseUrl { get; init; }
}
