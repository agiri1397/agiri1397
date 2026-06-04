namespace HoneywellApp.Application.DTOs;

public class LoginResponse
{
    public bool Success { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? TokenExpiry { get; set; }
    public string? Email { get; set; }
    public string? ErrorMessage { get; set; }
    public bool IsOfflineLogin { get; set; }

    public static LoginResponse Succeeded(string token, DateTime expiry, string email, bool offline = false) =>
        new()
        {
            Success = true,
            Token = token,
            TokenExpiry = expiry,
            Email = email,
            IsOfflineLogin = offline
        };

    public static LoginResponse Failed(string message) =>
        new()
        {
            Success = false,
            ErrorMessage = message
        };
}
