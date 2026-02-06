using AutoMapper;
using MediatR;
using TodoTasks.Application.Common.DTOs;
using TodoTasks.Application.Common.Exceptions;
using TodoTasks.Application.Common.Models;
using TodoTasks.Domain.Repositories;


namespace TodoTasks.Application.Features.TodoTask.Queries.GetPagedTodoTasks;

public class GetPagedTodoTasksQueryHandler : IRequestHandler<GetPagedTodoTasksQuery, PagedResponse<TodoTaskDto>>
{
    private readonly ITodoTaskRepository _repository;
    private readonly IMapper _mapper;

    public GetPagedTodoTasksQueryHandler(ITodoTaskRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResponse<TodoTaskDto>> Handle(GetPagedTodoTasksQuery request, CancellationToken cancellationToken)
    {
        Domain.ValueObjects.PaginationRequest paginationRequest = new Domain.ValueObjects.PaginationRequest { PageNumber = request.PageNumber, PageSize = request.PageSize };
        var pagedResult = await _repository.GetPagedAsync(paginationRequest, cancellationToken);
        return new PagedResponse<TodoTaskDto>
        {
            Items = _mapper.Map<List<TodoTaskDto>>(pagedResult.Items),
            TotalCount = pagedResult.TotalCount,
            PageNumber = pagedResult.PageNumber,
            PageSize = pagedResult.PageSize
        };
    }

}