using TodoTasks.Domain.Entities;
using TodoTasks.Domain.ValueObjects;

namespace TodoTasks.Domain.Repositories;

/// <summary>
/// Repository interface for managing task data access operations.
/// </summary>
/// <remarks>
/// Provides abstraction for CRUD operations and queries on tasks.
/// Implementations should handle database interactions and ensure data consistency.
/// </remarks>
public interface ITodoTaskRepository
{
    /// <summary>
    /// Retrieves a paginated list of tasks.
    /// </summary>
    /// <param name="request">The pagination request specifying page number and size.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paged result of tasks.</returns>
    Task<PagedResult<TodoTask>> GetPagedAsync(PaginationRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a task by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the task.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the task if found; otherwise, null.</returns>
    Task<TodoTask?> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all tasks belonging to a specific category.
    /// </summary>
    /// <param name="categoryId">The unique identifier of the category.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of tasks in the category.</returns>
    Task<IEnumerable<TodoTask>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a new task to the repository.
    /// </summary>
    /// <param name="todoTask">The task entity to add.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added task with its generated ID.</returns>
    Task<TodoTask> AddAsync(TodoTask todoTask, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing task in the repository.
    /// </summary>
    /// <param name="todoTask">The task entity with updated values.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateAsync(TodoTask todoTask, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a task from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the task to delete.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Checks whether a task with the specified identifier exists in the repository.
    /// </summary>
    /// <param name="id">The unique identifier of the task.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is true if the task exists; otherwise, false.</returns>
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
}
