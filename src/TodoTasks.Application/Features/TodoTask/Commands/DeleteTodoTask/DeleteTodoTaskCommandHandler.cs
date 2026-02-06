using MediatR;
using TodoTasks.Application.Common.Exceptions;
using TodoTasks.Domain.Repositories;

namespace TodoTasks.Application.Features.TodoTask.Commands.DeleteTodoTask;

public class DeleteTodoTaskCommandHandler : IRequestHandler<DeleteTodoTaskCommand, Unit>
{
    private readonly ITodoTaskRepository _todoTaskRepository;

    public DeleteTodoTaskCommandHandler(ITodoTaskRepository todoTaskRepository)
    {
        _todoTaskRepository = todoTaskRepository;
    }

    public async Task<Unit> Handle(DeleteTodoTaskCommand request, CancellationToken cancellationToken)
    {
        var exists = await _todoTaskRepository.ExistsAsync(request.Id, cancellationToken);
        if (!exists)
            throw new NotFoundException("TodoTask", request.Id);

        await _todoTaskRepository.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }

   
}