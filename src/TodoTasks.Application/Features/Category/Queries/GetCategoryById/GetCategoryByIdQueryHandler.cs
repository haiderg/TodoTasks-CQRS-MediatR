using AutoMapper;
using MediatR;
using TodoTasks.Application.Common.DTOs;
using TodoTasks.Application.Common.Exceptions;
using TodoTasks.Domain.Repositories;

namespace TodoTasks.Application.Features.Category.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException("Category", request.Id);
        return _mapper.Map<CategoryDto>(category);
    }


}