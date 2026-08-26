using SistemaILP.Ruteo.Application.Interfaces;

namespace SistemaILP.Ruteo.UI.Services;

/// <summary>
/// Single global light/dark mode switch for the whole app (login screen,
/// its 3-dot menu, and every screen inside the authenticated area).
/// Registered as a singleton so the preference and its change
/// notification are shared everywhere, and persisted so it survives
/// closing and reopening the app.
/// </summary>
public class ThemeState
{
    private const string PreferenceKey = "app_dark_mode";

    private readonly IPreferencesService _preferences;
    private bool _isDarkMode;

    public ThemeState(IPreferencesService preferences)
    {
        _preferences = preferences;
        _isDarkMode = _preferences.Get(PreferenceKey) == "true";
    }

    public bool IsDarkMode
    {
        get => _isDarkMode;
        set
        {
            if (_isDarkMode == value)
            {
                return;
            }

            _isDarkMode = value;
            _preferences.Set(PreferenceKey, value ? "true" : "false");
            OnChange?.Invoke();
        }
    }

    public event Action? OnChange;
}
