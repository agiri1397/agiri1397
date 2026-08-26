namespace SistemaILP.Ruteo.Maui;

// Fully qualified: our own "SistemaILP.Ruteo.Application" project
// shares the "SistemaILP.Ruteo" namespace prefix with this project,
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
        return new Window(new MainPage()) { Title = "SistemaILP.Ruteo" };
    }
}
