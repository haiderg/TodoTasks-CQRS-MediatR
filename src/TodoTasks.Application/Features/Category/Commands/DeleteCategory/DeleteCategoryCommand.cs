using MediatR;

namespace TodoTasks.Application.Features.Category.Commands.DeleteCategory;

public record DeleteCategoryCommand(int Id) : IRequest<Unit>;

