using MauiBlazorCleanArchitecture.Domain.Common;

namespace MauiBlazorCleanArchitecture.Domain.Entities;

/// <summary>
/// Sample entity used to demonstrate local persistence in SQLite,
/// scoped per authenticated user.
/// </summary>
public class TodoItem : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public bool IsCompleted { get; set; }

    public int UserId { get; set; }

    public User? User { get; set; }
}
