using SistemaILP.Ruteo.Domain.Entities;

namespace SistemaILP.Ruteo.Application.Interfaces;

public interface ITodoRepository
{
    Task<List<TodoItem>> GetAllForUserAsync(int userId);

    Task<TodoItem?> GetByIdAsync(int id, int userId);

    Task<TodoItem> AddAsync(TodoItem item);

    Task UpdateAsync(TodoItem item);

    Task DeleteAsync(TodoItem item);
}
