using FluentAssertions;
using TodoTasks.Domain.Entities;
using TodoTasks.Domain.ValueObjects;
using Xunit;

namespace TodoTasks.Domain.Tests.Entities;

public class TodoTaskTests
{
    [Fact]
    public void Create_WithValidRequest_ShouldCreateTodoTask()
    {
        // Arrange
        var request = new CreateTodoTaskRequest
        {
            Title = "Test Task",
            Description = "Test Description",
            CategoryId = 1,
            DueDate = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var task = TodoTask.Create(request);

        // Assert
        task.Title.Should().Be("Test Task");
        task.Description.Should().Be("Test Description");
        task.CategoryId.Should().Be(1);
        task.IsCompleted.Should().BeFalse();
    }

    [Fact]
    public void Create_WithEmptyTitle_ShouldThrowArgumentException()
    {
        // Arrange
        var request = new CreateTodoTaskRequest { Title = "" };

        // Act & Assert
        var act = () => TodoTask.Create(request);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Title cannot be empty*");
    }

    [Fact]
    public void Complete_WhenNotCompleted_ShouldMarkAsCompleted()
    {
        // Arrange
        var request = new CreateTodoTaskRequest { Title = "Test Task" };
        var task = TodoTask.Create(request);

        // Act
        task.Complete();

        // Assert
        task.IsCompleted.Should().BeTrue();
        task.CompletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Complete_WhenAlreadyCompleted_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var request = new CreateTodoTaskRequest { Title = "Test Task" };
        var task = TodoTask.Create(request);
        task.Complete();

        // Act & Assert
        var act = () => task.Complete();
        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Task is already completed");
    }

    [Fact]
    public void IsOverdue_WhenDueDatePassedAndNotCompleted_ShouldReturnTrue()
    {
        // Arrange
        var request = new CreateTodoTaskRequest
        { 
            Title = "Test Task",
            DueDate = DateTime.UtcNow.AddDays(-1)
        };
        var task = TodoTask.Create(request);

        // Act & Assert
        task.IsOverdue.Should().BeTrue();
    }
}