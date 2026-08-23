namespace MauiBlazorCleanArchitecture.Maui;

// Fully qualified: our own "MauiBlazorCleanArchitecture.Application" project
// shares the "MauiBlazorCleanArchitecture" namespace prefix with this project,
// so the bare name "Application" would otherwise resolve to that namespace
// instead of Microsoft.Maui.Controls.Application (CS0118).
public partial class App : global::Microsoft.Maui.Controls.Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new MainPage()) { Title = "MauiBlazorCleanArchitecture" };
    }
}
