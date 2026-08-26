using MudBlazor;
using SistemaILP.Ruteo.Application.Common;

namespace SistemaILP.Ruteo.UI.Components.Messages;

/// <summary>
/// Unico punto donde se decide que icono y que Severity de MudBlazor
/// corresponde a cada MessageType. Si se necesita cambiar un icono en
/// toda la app, se cambia aqui unicamente.
/// </summary>
public static class MessageIcons
{
    public static string For(MessageType type) => type switch
    {
        MessageType.Success => Icons.Material.Filled.CheckCircle,
        MessageType.Error => Icons.Material.Filled.Error,
        MessageType.Warning => Icons.Material.Filled.Warning,
        MessageType.Info => Icons.Material.Filled.Info,
        MessageType.Confirmation => Icons.Material.Filled.HelpOutline,
        _ => Icons.Material.Filled.Info
    };

    public static Severity SeverityFor(MessageType type) => type switch
    {
        MessageType.Success => Severity.Success,
        MessageType.Error => Severity.Error,
        MessageType.Warning => Severity.Warning,
        MessageType.Info => Severity.Info,
        MessageType.Confirmation => Severity.Info,
        _ => Severity.Normal
    };
}
