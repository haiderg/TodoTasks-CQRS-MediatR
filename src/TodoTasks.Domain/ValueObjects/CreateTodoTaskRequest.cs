namespace TodoTasks.Domain.ValueObjects;

public record CreateTodoTaskRequest
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public int? AssignedTo { get; init; }
    public DateTime? ReminderAt { get; init; }
    public DateTime? DueDate { get; init; }
    public int? CategoryId { get; init; }
    internal bool HasTitle => !string.IsNullOrWhiteSpace(Title);
}