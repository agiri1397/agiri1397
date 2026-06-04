using HoneywellApp.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace HoneywellApp.Infrastructure.Services;

public class SyncService : ISyncService
{
    private readonly ILogger<SyncService> _logger;
    private bool _isOnline;

    public bool IsOnline => _isOnline;
    public event EventHandler<bool>? ConnectivityChanged;

    public SyncService(ILogger<SyncService> logger)
    {
        _logger = logger;
        Connectivity.ConnectivityChanged += OnConnectivityChanged;
        _isOnline = Connectivity.NetworkAccess == NetworkAccess.Internet;
    }

    public async Task<bool> CheckConnectivityAsync(CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        _isOnline = Connectivity.NetworkAccess == NetworkAccess.Internet;
        return _isOnline;
    }

    public async Task<bool> SyncAllAsync(CancellationToken cancellationToken = default)
    {
        if (!await CheckConnectivityAsync(cancellationToken))
        {
            _logger.LogInformation("Sync skipped: no internet connection");
            return false;
        }
        return true;
    }

    private void OnConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
    {
        var wasOnline = _isOnline;
        _isOnline = e.NetworkAccess == NetworkAccess.Internet;
        if (wasOnline != _isOnline)
        {
            _logger.LogInformation("Connectivity changed: {Status}", _isOnline ? "Online" : "Offline");
            ConnectivityChanged?.Invoke(this, _isOnline);
        }
    }
}
