namespace HoneywellApp.MauiBlazor.Services;

public class ThemeService
{
    private const string PrefKey = "dark_mode";
    public bool IsDark { get; private set; }
    public event Action? OnChanged;

    public void Init(string? stored)
    {
        IsDark = stored == "1";
    }

    public void Toggle()
    {
        IsDark = !IsDark;
        OnChanged?.Invoke();
    }

    public string StorageValue => IsDark ? "1" : "0";
}
