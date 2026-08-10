using MauiBlazorCleanArchitecture.Application.Common;
using MauiBlazorCleanArchitecture.Application.DTOs;

namespace MauiBlazorCleanArchitecture.Application.Interfaces;

public interface ITodoService
{
    Task<List<TodoItemDto>> GetMyTodosAsync();

    Task<Result<TodoItemDto>> SaveAsync(SaveTodoItemDto dto);

    Task<Result> DeleteAsync(int id);

    Task<Result> ToggleCompletedAsync(int id);
}
