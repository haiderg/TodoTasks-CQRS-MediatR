using MediatR;
using TodoTasks.Application.Common.DTOs;
using TodoTasks.Domain.Repositories;
using AutoMapper;
using TodoTasks.Application.Common.Exceptions;

namespace TodoTasks.Application.Features.TodoTask.Queries.GetTodoTaskById;

public class GetTodoTaskByIdQueryHandler : IRequestHandler<GetTodoTaskByIdQuery, TodoTaskDto>
{
    private readonly ITodoTaskRepository _repository;
    private readonly IMapper _mapper;

    public GetTodoTaskByIdQueryHandler(ITodoTaskRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TodoTaskDto> Handle(GetTodoTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _repository.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException("TodoTask", request.Id);
        return _mapper.Map<TodoTaskDto>(task);
    }
}