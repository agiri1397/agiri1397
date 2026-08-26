namespace SistemaILP.Ruteo.UI.Components.Messages;

/// <summary>
/// Servicio centralizado para mensajes "flotantes" (Snackbar) y de
/// confirmacion (Dialog), reutilizable desde cualquier pantalla (Login,
/// Preventa, Autoventa, Despachos, Configuracion, etc.). Los mensajes que
/// van pegados a un formulario (por ejemplo el error de Login) NO pasan
/// por este servicio: se muestran declarativamente con AppMessageAlert,
/// igual que cualquier otro componente Razor.
/// </summary>
public interface IMessageService
{
    void ShowSuccess(string message, string? title = null);

    void ShowError(string message, string? title = null);

    void ShowWarning(string message, string? title = null);

    void ShowInfo(string message, string? title = null);

    /// <summary>Muestra un MudDialog de confirmacion. true si el usuario confirmo.</summary>
    Task<bool> ShowConfirmationAsync(string message, string? title = null);
}
