using TodoTasks.Domain.Enums;

namespace TodoTasks.Domain.ValueObjects;

public record UpdateCategoryRequest
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public TaskColorEnum? Color { get; init; }

    internal bool HasName => !string.IsNullOrEmpty(Name);
    internal bool HasDescription => Description is not null;
    internal bool HasColor => Enum.IsDefined(typeof(TaskColorEnum), Color);
}