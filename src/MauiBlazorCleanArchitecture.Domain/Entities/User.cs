using MauiBlazorCleanArchitecture.Domain.Common;

namespace MauiBlazorCleanArchitecture.Domain.Entities;

/// <summary>
/// Local user account persisted in SQLite. Password is never stored in
/// plain text: only the PBKDF2 hash and its salt are kept.
/// </summary>
public class User : BaseEntity
{
    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string PasswordSalt { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
}
