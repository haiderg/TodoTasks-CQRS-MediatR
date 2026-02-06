using MediatR;
using TodoTasks.Domain.Enums;

namespace TodoTasks.Application.Features.Category.Commands.UpdateCategory;

public record UpdateCategoryCommand : IRequest<Unit>
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public TaskColorEnum? Color { get; init; }
}
