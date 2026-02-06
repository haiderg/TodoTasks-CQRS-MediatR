using MediatR;

namespace TodoTasks.Application.Features.TodoTask.Commands.DeleteTodoTask;

public record DeleteTodoTaskCommand(int Id): IRequest<Unit>;

