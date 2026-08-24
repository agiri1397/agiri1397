using SistemaILP.Ruteo.Application.Interfaces;

namespace SistemaILP.Ruteo.Maui.Services;

/// <summary>
/// Platform implementation of IPreferencesService backed by
/// Microsoft.Maui.Storage.Preferences (plain, non-encrypted local
/// storage - appropriate for non-sensitive settings like theme choice).
/// </summary>
public class MauiPreferencesService : IPreferencesService
{
    public string? Get(string key) => Preferences.Default.Get<string?>(key, null);

    public void Set(string key, string value) => Preferences.Default.Set(key, value);
}
