using AutoMapper;
using MediatR;
using TodoTasks.Application.Common.DTOs;
using TodoTasks.Application.Common.Models;
using TodoTasks.Domain.Repositories;

namespace TodoTasks.Application.Features.Category.Queries.GetAllCategories;

public class GetPagedCategoriesHandler : IRequestHandler<GetPagedCategoriesQuery, PagedResponse<CategoryDto>>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;
    public GetPagedCategoriesHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResponse<CategoryDto>> Handle(GetPagedCategoriesQuery request, CancellationToken cancellationToken)
    {
        var paginationRequest = new Domain.ValueObjects.PaginationRequest { PageNumber = request.PageNumber, PageSize = request.PageSize };
        var pagedCategories = await _repository.GetPagedAsync(paginationRequest, cancellationToken);
        return _mapper.Map<PagedResponse<CategoryDto>>(pagedCategories);
    }
}