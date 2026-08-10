namespace MauiBlazorCleanArchitecture.Application.DTOs;

public class TodoItemDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedAtUtc { get; set; }
}

public class SaveTodoItemDto
{
    public int? Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public bool IsCompleted { get; set; }
}
