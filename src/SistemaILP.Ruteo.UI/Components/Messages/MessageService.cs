using MudBlazor;
using SistemaILP.Ruteo.Application.Common;

namespace SistemaILP.Ruteo.UI.Components.Messages;

public class MessageService : IMessageService
{
    private readonly ISnackbar _snackbar;
    private readonly IDialogService _dialogService;

    public MessageService(ISnackbar snackbar, IDialogService dialogService)
    {
        _snackbar = snackbar;
        _dialogService = dialogService;
    }

    public void ShowSuccess(string message, string? title = null) => Show(MessageType.Success, message, title);

    public void ShowError(string message, string? title = null) => Show(MessageType.Error, message, title);

    public void ShowWarning(string message, string? title = null) => Show(MessageType.Warning, message, title);

    public void ShowInfo(string message, string? title = null) => Show(MessageType.Info, message, title);

    private void Show(MessageType type, string message, string? title)
    {
        var text = string.IsNullOrWhiteSpace(title) ? message : $"{title}: {message}";

        _snackbar.Add(text, MessageIcons.SeverityFor(type), options =>
        {
            options.Icon = MessageIcons.For(type);
        });
    }

    public async Task<bool> ShowConfirmationAsync(string message, string? title = null)
    {
        var parameters = new DialogParameters
        {
            ["Text"] = message
        };

        var dialogReference = await _dialogService.ShowAsync<ConfirmDialog>(title ?? "Confirmar", parameters);
        var result = await dialogReference.Result;

        return result is not null && !result.Canceled;
    }
}
