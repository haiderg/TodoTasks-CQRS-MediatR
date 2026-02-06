namespace TodoTasks.Application.Common.DTOs;

/// <summary>
/// Data transfer object for task information.
/// </summary>
/// <remarks>
/// Used to transfer task data between the API layer and clients.
/// This immutable record ensures data consistency in API responses.
/// </remarks>
public record TodoTaskDto
{
    /// <summary>
    /// Gets the unique identifier of the task.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets the title of the task.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the optional detailed description of the task.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets the date and time when the task is due.
    /// </summary>
    public DateTime? DueDate { get; init; }

    /// <summary>
    /// Gets a value indicating whether the task has been completed.
    /// </summary>
    public bool IsCompleted { get; init; }

    /// <summary>
    /// Gets the date and time when the task was marked as completed, or null if not completed.
    /// </summary>
    public DateTime? CompletedAt { get; init; }

    /// <summary>
    /// Gets the unique identifier of the category this task belongs to.
    /// </summary>
    public int CategoryId { get; init; }

    /// <summary>
    /// Gets the user ID to whom the task is assigned.
    /// </summary>
    public int AssignedTo { get; init; }

    /// <summary>
    /// Gets the date and time when a reminder should be sent for this task.
    /// </summary>
    public DateTime? ReminderAt { get; init; }

    /// <summary>
    /// Gets the category this task belongs to.
    /// </summary>
    public CategoryDto Category { get; init; } = null!;

    /// <summary>
    /// Gets the date and time when the task was created.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets the date and time when the task was last updated, or null if never updated.
    /// </summary>
    public DateTime? UpdatedAt { get; init; }
}
