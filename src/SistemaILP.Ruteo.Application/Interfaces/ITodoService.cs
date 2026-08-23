using SistemaILP.Ruteo.Application.Common;
using SistemaILP.Ruteo.Application.DTOs;

namespace SistemaILP.Ruteo.Application.Interfaces;

public interface ITodoService
{
    Task<List<TodoItemDto>> GetMyTodosAsync();

    Task<Result<TodoItemDto>> SaveAsync(SaveTodoItemDto dto);

    Task<Result> DeleteAsync(int id);

    Task<Result> ToggleCompletedAsync(int id);
}
