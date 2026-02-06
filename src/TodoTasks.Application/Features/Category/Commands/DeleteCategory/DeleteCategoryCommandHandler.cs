using MediatR;
using TodoTasks.Application.Common.Exceptions;
using TodoTasks.Domain.Repositories;

namespace TodoTasks.Application.Features.Category.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly ICategoryRepository _category;

    public DeleteCategoryCommandHandler(ICategoryRepository category)
    {
        _category = category;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var exists = await _category.ExistsAsync(request.Id, cancellationToken);
        if (!exists)
            throw new NotFoundException("Category", request.Id);

        await _category.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }

}