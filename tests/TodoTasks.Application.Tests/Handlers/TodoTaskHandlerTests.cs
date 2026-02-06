using Xunit;
using FluentAssertions;
using Moq;
using AutoMapper;
using TodoTasks.Domain.Entities;
using TodoTasks.Domain.Repositories;
using TodoTasks.Application.Features.TodoTask.Commands.CreateTodoTask;
using TodoTasks.Application.Features.TodoTask.Commands.UpdateTodoTask;
using TodoTasks.Application.Features.TodoTask.Commands.DeleteTodoTask;
using TodoTasks.Application.Features.TodoTask.Queries.GetTodoTaskById;
using TodoTasks.Application.Common.DTOs;
using TodoTasks.Application.Common.Exceptions;

namespace TodoTasks.Application.Tests.Handlers;

public class TodoTaskHandlerTests
{
    [Fact]
    public async Task CreateTodoTaskCommandHandler_WithValidRequest_ShouldCreateAndReturnDto()
    {
        // Arrange
        var mockRepository = new Mock<ITodoTaskRepository>();
        var mockMapper = new Mock<IMapper>();
        var handler = new CreateTodoTaskCommandHandler(mockRepository.Object, mockMapper.Object);
        var command = new CreateTodoTaskCommand { Title = "Test Task", Description = "Test Description" };
        var domainRequest = new Domain.ValueObjects.CreateTodoTaskRequest { Title = "Test Task", Description = "Test Description" };
        var task = TodoTask.Create(domainRequest);
        var taskDto = new TodoTaskDto { Id = 1, Title = "Test Task", Description = "Test Description" };

        mockMapper.Setup(m => m.Map<Domain.ValueObjects.CreateTodoTaskRequest>(command)).Returns(domainRequest);
        mockRepository.Setup(r => r.AddAsync(It.IsAny<TodoTask>(), It.IsAny<CancellationToken>())).ReturnsAsync(task);
        mockMapper.Setup(m => m.Map<TodoTaskDto>(It.IsAny<TodoTask>())).Returns(taskDto);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("Test Task");
        mockRepository.Verify(r => r.AddAsync(It.IsAny<TodoTask>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetTodoTaskByIdQueryHandler_WithValidId_ShouldReturnDto()
    {
        // Arrange
        var mockRepository = new Mock<ITodoTaskRepository>();
        var mockMapper = new Mock<IMapper>();
        var handler = new GetTodoTaskByIdQueryHandler(mockRepository.Object, mockMapper.Object);
        var query = new GetTodoTaskByIdQuery(1);
        var task = TodoTask.Create(new Domain.ValueObjects.CreateTodoTaskRequest { Title = "Test" });
        var taskDto = new TodoTaskDto { Id = 1, Title = "Test" };

        mockRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(task);
        mockMapper.Setup(m => m.Map<TodoTaskDto>(task)).Returns(taskDto);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("Test");
        mockRepository.Verify(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetTodoTaskByIdQueryHandler_WithInvalidId_ShouldThrowNotFoundException()
    {
        // Arrange
        var mockRepository = new Mock<ITodoTaskRepository>();
        var mockMapper = new Mock<IMapper>();
        var handler = new GetTodoTaskByIdQueryHandler(mockRepository.Object, mockMapper.Object);
        var query = new GetTodoTaskByIdQuery(-1);

        mockRepository.Setup(r => r.GetByIdAsync(-1, It.IsAny<CancellationToken>())).ReturnsAsync((TodoTask?)null);

        // Act
        var act = () => handler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
        mockRepository.Verify(r => r.GetByIdAsync(-1, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateTodoTaskCommandHandler_WithValidId_ShouldUpdateTask()
    {
        // Arrange
        var mockRepository = new Mock<ITodoTaskRepository>();
        var mockMapper = new Mock<IMapper>();
        var handler = new UpdateTodoTaskCommandHandler(mockRepository.Object, mockMapper.Object);
        var existingTask = TodoTask.Create(new Domain.ValueObjects.CreateTodoTaskRequest { Title = "Old Title" });
        var command = new UpdateTodoTaskCommand { Id = 1, Title = "New Title" };
        var domainUpdateRequest = new Domain.ValueObjects.UpdateTodoTaskRequest { Title = "New Title" };

        mockRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(existingTask);
        mockMapper.Setup(m => m.Map<Domain.ValueObjects.UpdateTodoTaskRequest>(command)).Returns(domainUpdateRequest);
        mockRepository.Setup(r => r.UpdateAsync(It.IsAny<TodoTask>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockRepository.Verify(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()), Times.Once);
        mockRepository.Verify(r => r.UpdateAsync(It.IsAny<TodoTask>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteTodoTaskCommandHandler_WithValidId_ShouldDeleteTask()
    {
        // Arrange
        var mockRepository = new Mock<ITodoTaskRepository>();
        var handler = new DeleteTodoTaskCommandHandler(mockRepository.Object);
        var command = new DeleteTodoTaskCommand(1);

        mockRepository.Setup(r => r.ExistsAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        mockRepository.Setup(r => r.DeleteAsync(1, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockRepository.Verify(r => r.ExistsAsync(1, It.IsAny<CancellationToken>()), Times.Once);
        mockRepository.Verify(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteTodoTaskCommandHandler_WithInvalidId_ShouldThrowNotFoundException()
    {
        // Arrange
        var mockRepository = new Mock<ITodoTaskRepository>();
        var handler = new DeleteTodoTaskCommandHandler(mockRepository.Object);
        var command = new DeleteTodoTaskCommand(-1);

        mockRepository.Setup(r => r.ExistsAsync(-1, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
        mockRepository.Verify(r => r.ExistsAsync(-1, It.IsAny<CancellationToken>()), Times.Once);
        mockRepository.Verify(r => r.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
