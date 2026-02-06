using MediatR;
using TodoTasks.Application.Common.DTOs;
using TodoTasks.Domain.Enums;

namespace TodoTasks.Application.Features.Category.Commands.CreateCategory;

public record CreateCategoryCommand : IRequest<CategoryDto>
{
    public string Name { get; init; } = null!;

    public string? Description { get; init; }

    public TaskColorEnum? Color { get; init; }
}