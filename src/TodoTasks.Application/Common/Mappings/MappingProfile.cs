using AutoMapper;
using TodoTasks.Application.Common.DTOs;
using TodoTasks.Application.Features.TodoTask.Commands.CreateTodoTask;
using TodoTasks.Domain.Entities;
using TodoTasks.Domain.ValueObjects;

namespace TodoTasks.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TodoTask, TodoTaskDto>();
        CreateMap<TodoTaskDto, TodoTask>();
        //.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

        CreateMap<Domain.ValueObjects.PaginationRequest, Application.Common.Models.PaginationRequest>();
        CreateMap<Application.Common.Models.PaginationRequest, Domain.ValueObjects.PaginationRequest>();

        CreateMap(typeof(Domain.ValueObjects.PagedResult<>), typeof(Application.Common.Models.PagedResponse<>));
        CreateMap(typeof(Application.Common.Models.PagedResponse<>), typeof(Domain.ValueObjects.PagedResult<>));

        CreateMap<CategoryDto, Category>();
        CreateMap<Category, CategoryDto>();

        CreateMap<Domain.ValueObjects.CreateTodoTaskRequest, Features.TodoTask.Commands.CreateTodoTask.CreateTodoTaskCommand>();
        CreateMap<Features.TodoTask.Commands.CreateTodoTask.CreateTodoTaskCommand, Domain.ValueObjects.CreateTodoTaskRequest>();

        CreateMap<Domain.ValueObjects.CreateCategoryRequest, Features.Category.Commands.CreateCategory.CreateCategoryCommand>();
        CreateMap<Features.Category.Commands.CreateCategory.CreateCategoryCommand, Domain.ValueObjects.CreateCategoryRequest>();

        CreateMap<Domain.ValueObjects.UpdateCategoryRequest, Features.Category.Commands.UpdateCategory.UpdateCategoryCommand>()
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<Features.Category.Commands.UpdateCategory.UpdateCategoryCommand, Domain.ValueObjects.UpdateCategoryRequest>();

        CreateMap<Domain.ValueObjects.UpdateTodoTaskRequest, Features.TodoTask.Commands.UpdateTodoTask.UpdateTodoTaskCommand>()
        .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<Features.TodoTask.Commands.UpdateTodoTask.UpdateTodoTaskCommand, Domain.ValueObjects.UpdateTodoTaskRequest>();

    }
}
