using Microsoft.JSInterop;

namespace HoneywellApp.MauiBlazor.Services;

public class ThemeService
{
    private const string PrefKey = "dark_mode";
    private readonly IJSRuntime _js;
    private bool _isDark;

    public bool IsDark => _isDark;
    public event Action? OnChanged;

    public ThemeService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task InitAsync()
    {
        var stored = await _js.InvokeAsync<string?>("localStorage.getItem", PrefKey);
        _isDark = stored == "1";
        await ApplyAsync();
    }

    public async Task ToggleAsync()
    {
        _isDark = !_isDark;
        await _js.InvokeVoidAsync("localStorage.setItem", PrefKey, _isDark ? "1" : "0");
        await ApplyAsync();
        OnChanged?.Invoke();
    }

    private async Task ApplyAsync()
    {
        await _js.InvokeVoidAsync("HoneywellApp.setTheme", _isDark ? "dark" : "light");
    }
}
