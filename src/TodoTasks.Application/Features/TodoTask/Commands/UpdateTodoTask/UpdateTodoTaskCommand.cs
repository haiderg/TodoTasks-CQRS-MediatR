using MediatR;
using System.Text.Json.Serialization;

namespace TodoTasks.Application.Features.TodoTask.Commands.UpdateTodoTask;

public record UpdateTodoTaskCommand : IRequest<Unit>
{
    [JsonIgnore]
    public int Id { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public int? AssignedTo { get; init; }
    public int? CategoryId { get; init; }
    public DateTime? ReminderAt { get; init; }
    public DateTime? DueDate { get; init; }
}