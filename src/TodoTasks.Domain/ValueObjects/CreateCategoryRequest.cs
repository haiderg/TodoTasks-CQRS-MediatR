using TodoTasks.Domain.Enums;

namespace TodoTasks.Domain.ValueObjects;

public record CreateCategoryRequest
{
    public string Name { get; init; } = null!;

    public string? Description { get; init; }

    public TaskColorEnum? Color { get; init; }
}