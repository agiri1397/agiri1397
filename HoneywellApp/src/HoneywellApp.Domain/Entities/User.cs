namespace HoneywellApp.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? JwtToken { get; set; }
    public DateTime? TokenExpiry { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }
    public bool IsActive { get; set; } = true;

    public bool IsTokenValid()
    {
        return !string.IsNullOrEmpty(JwtToken)
            && TokenExpiry.HasValue
            && TokenExpiry.Value > DateTime.UtcNow;
    }
}
