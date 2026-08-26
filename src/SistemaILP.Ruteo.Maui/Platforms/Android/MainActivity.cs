using Android.App;
using Android.Content.PM;
using Android.OS;

namespace SistemaILP.Ruteo.Maui;

[Activity(
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    ScreenOrientation = ScreenOrientation.Portrait,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    // The app has a single native page hosting the BlazorWebView, so there
    // is no MAUI-level navigation stack to pop. Rather than letting the
    // hardware/gesture back button fall through to any WebView-internal
    // history (which could step back into an authenticated screen after
    // logging out, or out of the login screen), the back button always
    // backgrounds the app instead - the same as pressing Home.
    public override void OnBackPressed()
    {
        MoveTaskToBack(true);
    }
}
