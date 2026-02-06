using AutoMapper;
using MediatR;
using TodoTasks.Application.Common.DTOs;
using TodoTasks.Application.Common.Exceptions;
using TodoTasks.Domain.Entities;
using TodoTasks.Domain.Repositories;

namespace TodoTasks.Application.Features.Category.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
    private readonly ICategoryRepository _category;
    private readonly IMapper _mapper;
    public UpdateCategoryCommandHandler(ICategoryRepository category, IMapper mapper)
    {
        _category = category;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _category.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException("Category", request.Id);

        var updateCategoryRequest = _mapper.Map<Domain.ValueObjects.UpdateCategoryRequest>(request);
        category.Update(updateCategoryRequest);

        await _category.UpdateAsync(category, cancellationToken);
        return Unit.Value;
    }
}