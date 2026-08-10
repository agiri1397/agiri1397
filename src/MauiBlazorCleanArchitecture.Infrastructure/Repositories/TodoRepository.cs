using MauiBlazorCleanArchitecture.Application.Interfaces;
using MauiBlazorCleanArchitecture.Domain.Entities;
using MauiBlazorCleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MauiBlazorCleanArchitecture.Infrastructure.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext _context;

    public TodoRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<TodoItem>> GetAllForUserAsync(int userId) =>
        _context.TodoItems.Where(t => t.UserId == userId).ToListAsync();

    public Task<TodoItem?> GetByIdAsync(int id, int userId) =>
        _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

    public async Task<TodoItem> AddAsync(TodoItem item)
    {
        _context.TodoItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task UpdateAsync(TodoItem item)
    {
        _context.TodoItems.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TodoItem item)
    {
        _context.TodoItems.Remove(item);
        await _context.SaveChangesAsync();
    }
}
