namespace SistemaILP.Ruteo.Application.Interfaces;

/// <summary>
/// Simple, non-secure local key/value storage for user preferences
/// (theme, language, etc.) that should persist between app launches.
/// Distinct from ISecureStorageService, which is for sensitive session
/// data. Implemented in the Maui head project via Microsoft.Maui.Storage.Preferences.
/// </summary>
public interface IPreferencesService
{
    string? Get(string key);

    void Set(string key, string value);
}
