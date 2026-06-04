namespace HoneywellApp.Application.Interfaces;

public interface ISyncService
{
    bool IsOnline { get; }
    Task<bool> CheckConnectivityAsync(CancellationToken cancellationToken = default);
    Task<bool> SyncAllAsync(CancellationToken cancellationToken = default);
    event EventHandler<bool>? ConnectivityChanged;
}
