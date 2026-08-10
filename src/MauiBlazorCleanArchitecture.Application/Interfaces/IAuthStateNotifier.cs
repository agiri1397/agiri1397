namespace MauiBlazorCleanArchitecture.Application.Interfaces;

/// <summary>
/// Lets the Application layer trigger a re-evaluation of Blazor's
/// authentication state after login/logout without depending on
/// Microsoft.AspNetCore.Components.Authorization directly.
/// Implemented by the CustomAuthenticationStateProvider in Infrastructure.
/// </summary>
public interface IAuthStateNotifier
{
    void NotifyAuthenticationStateChanged();
}
