using SistemaILP.Ruteo.Application.Configuration;
using SistemaILP.Ruteo.Application.Interfaces;

namespace SistemaILP.Ruteo.Application.Services;

public class ConnectionSettingsService : IConnectionSettingsService
{
    private const string PreferenceKey = "ws_base_url_override";

    private readonly IPreferencesService _preferences;
    private readonly AppConfiguration _appConfiguration;

    public ConnectionSettingsService(IPreferencesService preferences, AppConfiguration appConfiguration)
    {
        _preferences = preferences;
        _appConfiguration = appConfiguration;
    }

    public Task<string> GetBaseUrlAsync()
    {
        var overridden = _preferences.Get(PreferenceKey);
        return Task.FromResult(string.IsNullOrWhiteSpace(overridden) ? _appConfiguration.WebService.BaseUrl : overridden);
    }

    public Task SetBaseUrlAsync(string baseUrl)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            throw new ArgumentException("La URL no puede estar vacía.", nameof(baseUrl));
        }

        var normalized = baseUrl.Trim();
        if (!normalized.EndsWith('/'))
        {
            normalized += "/";
        }

        _preferences.Set(PreferenceKey, normalized);
        return Task.CompletedTask;
    }

    public Task ResetToDefaultAsync()
    {
        _preferences.Set(PreferenceKey, string.Empty);
        return Task.CompletedTask;
    }

    public Task<bool> IsOverriddenAsync()
    {
        var overridden = _preferences.Get(PreferenceKey);
        return Task.FromResult(!string.IsNullOrWhiteSpace(overridden));
    }
}
