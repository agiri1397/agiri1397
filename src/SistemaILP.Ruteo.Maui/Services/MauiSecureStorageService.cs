using SistemaILP.Ruteo.Application.Interfaces;

namespace SistemaILP.Ruteo.Maui.Services;

/// <summary>
/// Platform implementation of ISecureStorageService backed by
/// Microsoft.Maui.Storage.SecureStorage (Keychain on iOS/macCatalyst,
/// KeyStore on Android, DPAPI on Windows).
/// </summary>
public class MauiSecureStorageService : ISecureStorageService
{
    public Task SetAsync(string key, string value) => SecureStorage.Default.SetAsync(key, value);

    public Task<string?> GetAsync(string key) => SecureStorage.Default.GetAsync(key);

    public void Remove(string key) => SecureStorage.Default.Remove(key);
}
