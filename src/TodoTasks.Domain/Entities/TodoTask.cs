using TodoTasks.Domain.ValueObjects;

namespace TodoTasks.Domain.Entities;

/// <summary>
/// Represents a task to be completed.
/// </summary>
/// <remarks>
/// A TodoTask is a unit of work with a title, optional description, due date, and completion status.
/// Tasks can be assigned to users, categorized, and tracked for completion.
/// </remarks>
public class TodoTask : Entity
{
    /// <summary>
    /// Gets the title of the task.
    /// </summary>
    /// <remarks>
    /// The title is required and must be between 1 and 50 characters.
    /// </remarks>
    public string Title { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the optional detailed description of the task.
    /// </summary>
    /// <remarks>
    /// The description cannot exceed 500 characters.
    /// </remarks>
    public string? Description { get; private set; }

    /// <summary>
    /// Gets the user ID to whom the task is assigned.
    /// </summary>
    public int AssignedTo { get; private set; }

    /// <summary>
    /// Gets the date and time when a reminder should be sent for this task.
    /// </summary>
    public DateTime? ReminderAt { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the task has been completed.
    /// </summary>
    public bool IsCompleted { get; private set; }

    /// <summary>
    /// Gets the date and time when the task is due.
    /// </summary>
    public DateTime? DueDate { get; private set; }

    /// <summary>
    /// Gets the date and time when the task was marked as completed, or null if not completed.
    /// </summary>
    public DateTime? CompletedAt { get; private set; }

    /// <summary>
    /// Gets the unique identifier of the category this task belongs to.
    /// </summary>
    public int CategoryId { get; private set; }

    /// <summary>
    /// Gets the category this task belongs to.
    /// </summary>
    public Category Category { get; private set; } = null!;

    private TodoTask() { } // For EF Core

    private TodoTask(CreateTodoTaskRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("Title cannot be empty", nameof(request.Title));

        if (request.Title.Length > 50)
            throw new ArgumentException("Title cannot exceed 50 characters", nameof(request.Title));

        if (request.Description?.Length > 500)
            throw new ArgumentException("Description cannot exceed 500 characters", nameof(request.Description));

        Title = request.Title.Trim();
        Description = request.Description?.Trim();
        DueDate = request.DueDate;
        AssignedTo = request.AssignedTo ?? 0;
        ReminderAt = request.ReminderAt;
        CategoryId = request.CategoryId ?? 0;
        IsCompleted = false;
    }

    /// <summary>
    /// Creates a new task instance.
    /// </summary>
    /// <param name="request">The creation request containing the task data.</param>
    /// <returns>A new TodoTask instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the title is empty, exceeds 50 characters, or description exceeds 500 characters.</exception>
    public static TodoTask Create(CreateTodoTaskRequest request)
    {
        return new TodoTask(request);
    }

    /// <summary>
    /// Marks the task as completed.
    /// </summary>
    /// <remarks>
    /// Sets the CompletedAt timestamp to the current UTC time and updates the UpdatedAt timestamp.
    /// </remarks>
    /// <exception cref="InvalidOperationException">Thrown when the task is already completed.</exception>
    public void Complete()
    {
        if (IsCompleted)
            throw new InvalidOperationException("Task is already completed");

        IsCompleted = true;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the task with new information.
    /// </summary>
    /// <param name="request">The update request containing the new task data.</param>
    /// <remarks>
    /// Only properties specified in the request are updated. The UpdatedAt timestamp is automatically set to the current UTC time.
    /// </remarks>
    /// <exception cref="ArgumentException">Thrown when the title is empty, exceeds 50 characters, or description exceeds 500 characters.</exception>
    public void Update(UpdateTodoTaskRequest request)
    {
        if (request.HasTitle)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new ArgumentException("Title cannot be empty", nameof(request.Title));

            if (request.Title.Length > 50)
                throw new ArgumentException("Title cannot exceed 50 characters", nameof(request.Title));

            Title = request.Title.Trim();
        }

        if (request.HasDescription && request.Description?.Length > 500)
            throw new ArgumentException(message: "Description cannot exceed 500 characters", nameof(request.Description));

        if (request.HasDescription)
            Description = request.Description?.Trim();

        if (request.HasAssignedTo)
            AssignedTo = request.AssignedTo!.Value;

        if (request.HasCategoryId)
            CategoryId = request.CategoryId!.Value;

        if (request.HasReminderAt)
            ReminderAt = request.ReminderAt;

        if (request.HasDueDate)
            DueDate = request.DueDate;

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets a value indicating whether the task is overdue.
    /// </summary>
    /// <remarks>
    /// A task is considered overdue if it has a due date, is not completed, and the current time is past the due date.
    /// </remarks>
    public bool IsOverdue => DueDate.HasValue && !IsCompleted && DateTime.UtcNow > DueDate.Value;
}
