namespace SistemaILP.Ruteo.Application.Interfaces;

/// <summary>
/// Base URL efectiva del Web Service. Por defecto es la de
/// AppConfiguration.WebService.BaseUrl (la de la variante/build), pero
/// puede sobreescribirse desde Configuración (por ejemplo para apuntar
/// a otro ambiente durante soporte/pruebas) - esa sobreescritura se
/// persiste localmente y sobrevive a reinicios de la app.
/// </summary>
public interface IConnectionSettingsService
{
    Task<string> GetBaseUrlAsync();

    Task SetBaseUrlAsync(string baseUrl);

    Task ResetToDefaultAsync();

    /// <summary>True si el usuario sobreescribió la Base URL por defecto.</summary>
    Task<bool> IsOverriddenAsync();
}
