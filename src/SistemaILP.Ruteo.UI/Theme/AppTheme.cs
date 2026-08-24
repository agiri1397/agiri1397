using MudBlazor;

namespace SistemaILP.Ruteo.UI.Theme;

/// <summary>
/// Single source of truth for the app's visual identity, derived from
/// the Industria La Popular brand (institutional blue + warm accent).
/// Used once in App.razor via MudThemeProvider, so every MudBlazor
/// component (buttons, inputs, cards, nav bar, dialogs, etc.) picks it
/// up automatically - no per-page styling needed.
///
/// TODO: once the official logo file is available, replace these
/// approximated hex values with the exact brand colors extracted from it.
/// </summary>
public static class AppTheme
{
    private const string BrandBlue = "#1D5FAE";
    private const string BrandBlueDark = "#5B9BDD";
    private const string BrandOrange = "#F2A340";

    public static readonly MudTheme Default = new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = BrandBlue,
            Secondary = BrandOrange,
            AppbarBackground = BrandBlue,
            AppbarText = "#FFFFFF",
            Background = "#F7F9FC",
            Surface = "#FFFFFF",
            DrawerBackground = "#FFFFFF"
        },
        PaletteDark = new PaletteDark
        {
            Primary = BrandBlueDark,
            Secondary = BrandOrange,
            AppbarBackground = "#14273D",
            AppbarText = "#FFFFFF",
            DrawerBackground = "#1A1A27"
        }
    };
}
