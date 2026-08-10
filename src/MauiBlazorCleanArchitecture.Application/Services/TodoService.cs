using MauiBlazorCleanArchitecture.Application.Common;
using MauiBlazorCleanArchitecture.Application.DTOs;
using MauiBlazorCleanArchitecture.Application.Interfaces;
using MauiBlazorCleanArchitecture.Domain.Entities;

namespace MauiBlazorCleanArchitecture.Application.Services;

/// <summary>
/// CRUD use cases backed by the SQLite-persisted TodoItem entity,
/// always scoped to the currently logged-in user.
/// </summary>
public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly ISecureStorageService _secureStorage;

    public TodoService(ITodoRepository todoRepository, ISecureStorageService secureStorage)
    {
        _todoRepository = todoRepository;
        _secureStorage = secureStorage;
    }

    public async Task<List<TodoItemDto>> GetMyTodosAsync()
    {
        var userId = await GetCurrentUserIdAsync();
        if (userId is null)
        {
            return new List<TodoItemDto>();
        }

        var items = await _todoRepository.GetAllForUserAsync(userId.Value);
        return items
            .OrderByDescending(i => i.CreatedAtUtc)
            .Select(MapToDto)
            .ToList();
    }

    public async Task<Result<TodoItemDto>> SaveAsync(SaveTodoItemDto dto)
    {
        var userId = await GetCurrentUserIdAsync();
        if (userId is null)
        {
            return Result<TodoItemDto>.Failure("Debes iniciar sesión.");
        }

        if (string.IsNullOrWhiteSpace(dto.Title))
        {
            return Result<TodoItemDto>.Failure("El título es obligatorio.");
        }

        if (dto.Id is null)
        {
            var created = await _todoRepository.AddAsync(new TodoItem
            {
                Title = dto.Title.Trim(),
                Notes = dto.Notes,
                IsCompleted = dto.IsCompleted,
                UserId = userId.Value
            });

            return Result<TodoItemDto>.Success(MapToDto(created));
        }

        var existing = await _todoRepository.GetByIdAsync(dto.Id.Value, userId.Value);
        if (existing is null)
        {
            return Result<TodoItemDto>.Failure("Elemento no encontrado.");
        }

        existing.Title = dto.Title.Trim();
        existing.Notes = dto.Notes;
        existing.IsCompleted = dto.IsCompleted;
        existing.UpdatedAtUtc = DateTime.UtcNow;

        await _todoRepository.UpdateAsync(existing);

        return Result<TodoItemDto>.Success(MapToDto(existing));
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var userId = await GetCurrentUserIdAsync();
        if (userId is null)
        {
            return Result.Failure("Debes iniciar sesión.");
        }

        var existing = await _todoRepository.GetByIdAsync(id, userId.Value);
        if (existing is null)
        {
            return Result.Failure("Elemento no encontrado.");
        }

        await _todoRepository.DeleteAsync(existing);
        return Result.Success();
    }

    public async Task<Result> ToggleCompletedAsync(int id)
    {
        var userId = await GetCurrentUserIdAsync();
        if (userId is null)
        {
            return Result.Failure("Debes iniciar sesión.");
        }

        var existing = await _todoRepository.GetByIdAsync(id, userId.Value);
        if (existing is null)
        {
            return Result.Failure("Elemento no encontrado.");
        }

        existing.IsCompleted = !existing.IsCompleted;
        existing.UpdatedAtUtc = DateTime.UtcNow;
        await _todoRepository.UpdateAsync(existing);

        return Result.Success();
    }

    private async Task<int?> GetCurrentUserIdAsync()
    {
        var idText = await _secureStorage.GetAsync(AuthService.SessionUserIdKey);
        return int.TryParse(idText, out var id) ? id : null;
    }

    private static TodoItemDto MapToDto(TodoItem item) => new()
    {
        Id = item.Id,
        Title = item.Title,
        Notes = item.Notes,
        IsCompleted = item.IsCompleted,
        CreatedAtUtc = item.CreatedAtUtc
    };
}
