using AutoMapper;
using MediatR;
using TodoTasks.Application.Common.DTOs;
using TodoTasks.Domain.Repositories;

namespace TodoTasks.Application.Features.Category.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;
    public CreateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categorySaveRequest = _mapper.Map<Domain.ValueObjects.CreateCategoryRequest>(request);
        var category = Domain.Entities.Category.Create(categorySaveRequest);
        var savedCategory = await _repository.AddAsync(category, cancellationToken);
        return _mapper.Map<CategoryDto>(savedCategory);
    }


}