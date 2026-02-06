using MediatR;
using TodoTasks.Application.Common.DTOs;

namespace TodoTasks.Application.Features.TodoTask.Commands.CreateTodoTask;

public record CreateTodoTaskCommand : IRequest<TodoTaskDto>
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public DateTime? DueDate { get; init; }
    public int? CategoryId { get; init; }
    public int? AssignedTo { get; init; }
    public DateTime? ReminderAt { get; init; }
}
