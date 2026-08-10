using MauiBlazorCleanArchitecture.Domain.Entities;

namespace MauiBlazorCleanArchitecture.Application.Interfaces;

public interface ITodoRepository
{
    Task<List<TodoItem>> GetAllForUserAsync(int userId);

    Task<TodoItem?> GetByIdAsync(int id, int userId);

    Task<TodoItem> AddAsync(TodoItem item);

    Task UpdateAsync(TodoItem item);

    Task DeleteAsync(TodoItem item);
}
