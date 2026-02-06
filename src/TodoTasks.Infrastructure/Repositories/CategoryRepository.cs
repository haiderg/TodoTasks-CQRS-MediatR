using Microsoft.EntityFrameworkCore;
using TodoTasks.Domain.Entities;
using TodoTasks.Domain.Repositories;
using TodoTasks.Domain.ValueObjects;

namespace TodoTasks.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;
    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Category> AddAsync(Category category, CancellationToken cancellationToken)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);
        return category;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var category = await GetByIdAsync(id, cancellationToken);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Categories.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Categories.ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Categories.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<PagedResult<Category>> GetPagedAsync(PaginationRequest request, CancellationToken cancellationToken)
    {
        int totalCount = await _context.Categories.CountAsync(cancellationToken);
        var items = await _context.Categories
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize).ToListAsync(cancellationToken);

        return new PagedResult<Category>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
        
    public async Task UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync(cancellationToken);
    }
}