namespace SistemaILP.Ruteo.Application.Interfaces;

/// <summary>
/// Abstraction over the platform's secure/local storage, used to persist
/// the current session (logged-in user id) between app launches.
/// The concrete implementation lives in the MAUI head project because it
/// depends on Microsoft.Maui.Storage.SecureStorage, keeping this
/// contract platform-agnostic for the Application layer.
/// </summary>
public interface ISecureStorageService
{
    Task SetAsync(string key, string value);

    Task<string?> GetAsync(string key);

    void Remove(string key);
}
