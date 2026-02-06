namespace TodoTasks.Domain.ValueObjects;

public record UpdateTodoTaskRequest
{
    public string? Title { get; init; }
    public string? Description { get; init; }
    public int? AssignedTo { get; init; }
    public int? CategoryId { get; init; }
    public DateTime? ReminderAt { get; init; }
    public DateTime? DueDate { get; init; }

    internal bool HasTitle => !string.IsNullOrEmpty(Title);
    internal bool HasDescription => Description != null;
    internal bool HasAssignedTo => AssignedTo.HasValue;
    internal bool HasCategoryId => CategoryId.HasValue;
    internal bool HasReminderAt => ReminderAt.HasValue;
    internal bool HasDueDate => DueDate.HasValue;
}