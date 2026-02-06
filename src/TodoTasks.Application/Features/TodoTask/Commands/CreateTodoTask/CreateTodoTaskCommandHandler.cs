using AutoMapper;
using MediatR;
using TodoTasks.Application.Common.DTOs;
using TodoTasks.Domain.Entities;
using TodoTasks.Domain.Repositories;


namespace TodoTasks.Application.Features.TodoTask.Commands.CreateTodoTask;

public class CreateTodoTaskCommandHandler : IRequestHandler<CreateTodoTaskCommand, TodoTaskDto>
{
    private readonly ITodoTaskRepository _repository;
    private readonly IMapper _mapper;

    public CreateTodoTaskCommandHandler(ITodoTaskRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TodoTaskDto> Handle(CreateTodoTaskCommand request, CancellationToken cancellationToken)
    {
        var createRequest = _mapper.Map<Domain.ValueObjects.CreateTodoTaskRequest>(request);
        var task = Domain.Entities.TodoTask.Create(createRequest);
        await _repository.AddAsync(task, cancellationToken);
        return _mapper.Map<TodoTaskDto>(task);
    }
}
