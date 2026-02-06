using Microsoft.EntityFrameworkCore;
using TodoTasks.Domain.Entities;
using TodoTasks.Domain.Repositories;
using TodoTasks.Domain.ValueObjects;

namespace TodoTasks.Infrastructure.Repositories;

public class TodoTaskRepository : ITodoTaskRepository
{
    private readonly AppDbContext _context;
    public TodoTaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TodoTask?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.TodoTasks
        .Include(x => x.Category)
        .FirstAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<PagedResult<TodoTask>> GetPagedAsync(PaginationRequest request, CancellationToken cancellationToken)
    {
        var totalCount = await _context.TodoTasks.CountAsync(cancellationToken);
        var items = await _context.TodoTasks
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Include(x => x.Category)
            .ToListAsync(cancellationToken);

        return new PagedResult<TodoTask>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<IEnumerable<TodoTask>> GetByAssignedToAsync(int assignedTo, CancellationToken cancellationToken)
    {
        return await _context.TodoTasks
            .Where(t => t.AssignedTo == assignedTo)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TodoTask>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken)
    {
        return await _context.TodoTasks
            .Where(t => t.CategoryId == categoryId)
            .Include(x => x.Category)
            .ToListAsync(cancellationToken);
    }

    public async Task<TodoTask> AddAsync(TodoTask todoTask, CancellationToken cancellationToken)
    {
        _context.TodoTasks.Add(todoTask);
        await _context.SaveChangesAsync(cancellationToken);
        return todoTask;
    }

    public async Task UpdateAsync(TodoTask todoTask, CancellationToken cancellationToken)
    {
        _context.TodoTasks.Update(todoTask);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var task = await GetByIdAsync(id, cancellationToken);
        if (task != null)
        {
            _context.TodoTasks.Remove(task);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.TodoTasks.AnyAsync(t => t.Id == id, cancellationToken);
    }
}