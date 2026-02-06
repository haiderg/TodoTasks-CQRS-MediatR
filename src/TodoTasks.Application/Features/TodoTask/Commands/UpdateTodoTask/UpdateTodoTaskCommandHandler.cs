using AutoMapper;
using MediatR;
using TodoTasks.Application.Common.Exceptions;
using TodoTasks.Domain.Repositories;

namespace TodoTasks.Application.Features.TodoTask.Commands.UpdateTodoTask;

public class UpdateTodoTaskCommandHandler : IRequestHandler<UpdateTodoTaskCommand, Unit>
{
    private readonly ITodoTaskRepository _taskRepository;
    private readonly IMapper _mapper;
    public UpdateTodoTaskCommandHandler(ITodoTaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateTodoTaskCommand request, CancellationToken cancellationToken)
    {
        var todoTask = await _taskRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException("TodoTask", request.Id);
        var todoTaskUpdateRequest = _mapper.Map<Domain.ValueObjects.UpdateTodoTaskRequest>(request);

        todoTask.Update(todoTaskUpdateRequest);
        await _taskRepository.UpdateAsync(todoTask, cancellationToken);
        return Unit.Value;
    }



}