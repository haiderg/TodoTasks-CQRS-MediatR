using TodoTasks.Domain.Entities;
using TodoTasks.Domain.ValueObjects;

namespace TodoTasks.Domain.Repositories;

/// <summary>
/// Repository interface for managing category data access operations.
/// </summary>
/// <remarks>
/// Provides abstraction for CRUD operations and queries on categories.
/// Implementations should handle database interactions and ensure data consistency.
/// </remarks>
public interface ICategoryRepository
{
    /// <summary>
    /// Retrieves all categories from the repository.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of all categories.</returns>
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a paginated list of categories.
    /// </summary>
    /// <param name="request">The pagination request specifying page number and size.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paged result of categories.</returns>
    Task<PagedResult<Category>> GetPagedAsync(PaginationRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a category by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the category.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the category if found; otherwise, null.</returns>
    Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a new category to the repository.
    /// </summary>
    /// <param name="category">The category entity to add.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added category with its generated ID.</returns>
    Task<Category> AddAsync(Category category, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing category in the repository.
    /// </summary>
    /// <param name="category">The category entity with updated values.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateAsync(Category category, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a category from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the category to delete.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Checks whether a category with the specified identifier exists in the repository.
    /// </summary>
    /// <param name="id">The unique identifier of the category.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is true if the category exists; otherwise, false.</returns>
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
}
