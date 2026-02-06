using Xunit;
using FluentAssertions;
using Moq;
using AutoMapper;
using MediatR;
using TodoTasks.Domain.Entities;
using TodoTasks.Domain.Repositories;
using TodoTasks.Domain.Enums;
using TodoTasks.Application.Features.Category.Commands.CreateCategory;
using TodoTasks.Application.Features.Category.Commands.UpdateCategory;
using TodoTasks.Application.Features.Category.Commands.DeleteCategory;
using TodoTasks.Application.Features.Category.Queries.GetAllCategories;
using TodoTasks.Application.Features.Category.Queries.GetCategoryById;
using TodoTasks.Application.Common.DTOs;
using TodoTasks.Application.Common.Models;
using TodoTasks.Application.Common.Exceptions;

namespace TodoTasks.Application.Tests.Handlers;

public class CategoryHandlerTests
{
    [Fact]
    public async Task CreateCategoryCommandHandler_WithValidRequest_ShouldCreateAndReturnCategoryDto()
    {
        // Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var mockMapper = new Mock<IMapper>();
        var handler = new CreateCategoryCommandHandler(mockRepository.Object, mockMapper.Object);
        
        var command = new CreateCategoryCommand { Name = "Test Category", Color = TaskColorEnum.Yellow, Description = "Test Description" };
        var domainRequest = new Domain.ValueObjects.CreateCategoryRequest { Name = "Test Category", Color = TaskColorEnum.Yellow, Description = "Test Description" };
        var category = Category.Create(domainRequest);
        var categoryDto = new CategoryDto { Id = 1, Name = "Test Category", Color = TaskColorEnum.Yellow, Description = "Test Description" };

        mockMapper.Setup(m => m.Map<Domain.ValueObjects.CreateCategoryRequest>(command)).Returns(domainRequest);
        mockRepository.Setup(r => r.AddAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>())).ReturnsAsync(category);
        mockMapper.Setup(m => m.Map<CategoryDto>(It.IsAny<Category>())).Returns(categoryDto);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Test Category");
        mockRepository.Verify(r => r.AddAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetAllCategoriesHandler_WithValidRequest_ShouldReturnPagedCategories()
    {
        // Arrange
        Mock<ICategoryRepository>? mockRepository = new Mock<ICategoryRepository>();
        var mockMapper = new Mock<IMapper>();
        var handler = new GetPagedCategoriesHandler(mockRepository.Object, mockMapper.Object);

        var query = new GetPagedCategoriesQuery(1, 10);
        var categories = new List<Category>
        {
            Category.Create(new Domain.ValueObjects.CreateCategoryRequest { Name = "First", Color = TaskColorEnum.Yellow }),
            Category.Create(new Domain.ValueObjects.CreateCategoryRequest { Name = "Second", Color = TaskColorEnum.Green }),
            Category.Create(new Domain.ValueObjects.CreateCategoryRequest { Name = "Third", Color = TaskColorEnum.Red })
        };
        var pagedResult = new Domain.ValueObjects.PagedResult<Category> { Items = categories, TotalCount = 3, PageNumber = 1, PageSize = 10 };
        var pagedResponse = new PagedResponse<CategoryDto> { Items = new List<CategoryDto>(), TotalCount = 3, PageNumber = 1, PageSize = 10 };

        mockMapper.Setup(m => m.Map<Domain.ValueObjects.PaginationRequest>(query)).Returns(new Domain.ValueObjects.PaginationRequest());
        mockRepository.Setup(r => r.GetPagedAsync(It.IsAny<Domain.ValueObjects.PaginationRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(pagedResult);
        mockMapper.Setup(m => m.Map<PagedResponse<CategoryDto>>(pagedResult)).Returns(pagedResponse);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.TotalCount.Should().Be(3);
        mockRepository.Verify(r => r.GetPagedAsync(It.IsAny<Domain.ValueObjects.PaginationRequest>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetCategoryByIdQueryHandler_WithValidId_ShouldReturnCategoryDto()
    {
        // Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var mockMapper = new Mock<IMapper>();
        var handler = new GetCategoryByIdQueryHandler(mockRepository.Object, mockMapper.Object);

        var categoryId = 1;
        var query = new GetCategoryByIdQuery(categoryId);
        var category = Category.Create(new Domain.ValueObjects.CreateCategoryRequest { Name = "Test", Color = TaskColorEnum.Red });
        var categoryDto = new CategoryDto { Id = 1, Name = "Test", Color = TaskColorEnum.Red };

        mockRepository.Setup(r => r.GetByIdAsync(categoryId, It.IsAny<CancellationToken>())).ReturnsAsync(category);
        mockMapper.Setup(m => m.Map<CategoryDto>(category)).Returns(categoryDto);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Test");
        mockRepository.Verify(r => r.GetByIdAsync(categoryId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetCategoryByIdQueryHandler_WithInvalidId_ShouldThrowNotFoundException()
    {
        // Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var mockMapper = new Mock<IMapper>();
        var handler = new GetCategoryByIdQueryHandler(mockRepository.Object, mockMapper.Object);

        var categoryId = -999;
        var query = new GetCategoryByIdQuery(categoryId);
        mockRepository.Setup(r => r.GetByIdAsync(categoryId, It.IsAny<CancellationToken>())).ReturnsAsync((Category?)null);

        // Act
        var act = () => handler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
        mockRepository.Verify(r => r.GetByIdAsync(categoryId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateCategoryCommandHandler_WithValidId_ShouldUpdateCategory()
    {
        // Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var mockMapper = new Mock<IMapper>();
        var handler = new UpdateCategoryCommandHandler(mockRepository.Object, mockMapper.Object);

        var categoryId = 1;
        var existingCategory = Category.Create(new Domain.ValueObjects.CreateCategoryRequest { Name = "Old Name", Color = TaskColorEnum.Yellow });
        var command = new UpdateCategoryCommand { Id = categoryId, Name = "Updated Name", Color = TaskColorEnum.Red, Description = "Updated Description" };
        var domainUpdateRequest = new Domain.ValueObjects.UpdateCategoryRequest { Name = "Updated Name", Color = TaskColorEnum.Red, Description = "Updated Description" };

        mockRepository.Setup(r => r.GetByIdAsync(categoryId, It.IsAny<CancellationToken>())).ReturnsAsync(existingCategory);
        mockMapper.Setup(m => m.Map<Domain.ValueObjects.UpdateCategoryRequest>(command)).Returns(domainUpdateRequest);
        mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
        mockRepository.Verify(r => r.GetByIdAsync(categoryId, It.IsAny<CancellationToken>()), Times.Once);
        mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateCategoryCommandHandler_WithInvalidId_ShouldThrowNotFoundException()
    {
        // Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var mockMapper = new Mock<IMapper>();
        var handler = new UpdateCategoryCommandHandler(mockRepository.Object, mockMapper.Object);

        var categoryId = -1;
        var command = new UpdateCategoryCommand { Id = categoryId, Name = "New Name" };

        mockRepository.Setup(r => r.GetByIdAsync(categoryId, It.IsAny<CancellationToken>())).ReturnsAsync((Category?)null);

        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
        mockRepository.Verify(r => r.GetByIdAsync(categoryId, It.IsAny<CancellationToken>()), Times.Once);
        mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task DeleteCategoryCommandHandler_WithValidId_ShouldDeleteCategory()
    {
        // Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var handler = new DeleteCategoryCommandHandler(mockRepository.Object);

        var categoryId = 1;
        var command = new DeleteCategoryCommand(categoryId);

        mockRepository.Setup(r => r.ExistsAsync(categoryId, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        mockRepository.Setup(r => r.DeleteAsync(categoryId, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
        mockRepository.Verify(r => r.ExistsAsync(categoryId, It.IsAny<CancellationToken>()), Times.Once);
        mockRepository.Verify(r => r.DeleteAsync(categoryId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteCategoryCommandHandler_WithInvalidId_ShouldThrowNotFoundException()
    {
        // Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        var handler = new DeleteCategoryCommandHandler(mockRepository.Object);

        var categoryId = -1;
        var command = new DeleteCategoryCommand(categoryId);

        mockRepository.Setup(r => r.ExistsAsync(categoryId, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
        mockRepository.Verify(r => r.ExistsAsync(categoryId, It.IsAny<CancellationToken>()), Times.Once);
        mockRepository.Verify(r => r.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}