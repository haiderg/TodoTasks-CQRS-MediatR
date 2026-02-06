# TodoTasks API - MediatR Edition

A production-ready **ASP.NET Core 10** Web API showcasing enterprise architecture patterns with **MediatR CQRS** implementation, demonstrating advanced architectural practices for scalable task management systems.

## 🌐 Live Demo

**Swagger UI:** [https://todotasks-cqrs-mediatr-production.up.railway.app/swagger/index.html](https://todotasks-cqrs-mediatr-production.up.railway.app/swagger/index.html)

**API Base URL:** `https://todotasks-cqrs-mediatr-production.up.railway.app/api`

## 🏗️ Architecture

**Clean Architecture** with **CQRS Pattern** via MediatR:

```
src/
├── TodoTasks.API/              # Web API Layer (Controllers, Program.cs)
├── TodoTasks.Application/      # Business Logic Layer (MediatR Handlers, Queries, Commands)
│   ├── Features/               # CQRS Feature Folders
│   │   ├── TodoTask/
│   │   │   ├── Commands/       # Write Operations (CreateTodoTask, UpdateTodoTask, etc.)
│   │   │   └── Queries/        # Read Operations (GetTodoTasks, GetTodoTaskById, etc.)
│   │   └── Category/
│   │       ├── Commands/
│   │       └── Queries/
│   └── Common/                 # Cross-cutting Concerns
│       ├── Behaviors/          # MediatR Pipeline Behaviors (Validation, Logging)
│       ├── Mappings/           # AutoMapper Profiles
│       └── DTOs/               # Data Transfer Objects
├── TodoTasks.Domain/           # Domain Layer (Entities, Value Objects, Repositories)
└── TodoTasks.Infrastructure/   # Data Access Layer (EF Core, Repositories)
```

## 🚀 Key Features

- **CQRS Pattern** with MediatR for command/query separation
- **Clean Architecture** with strict dependency inversion
- **Domain-Driven Design** principles with rich domain models
- **MediatR Pipeline Behaviors** for cross-cutting concerns (validation, logging)
- **FluentValidation** integrated with MediatR validation behavior
- **AutoMapper** for DTO mapping
- **JWT Authentication** for secure API access
- **Entity Framework Core 10** with SQL Server or PostgreSQL
- **Repository Pattern** for data access abstraction
- **Swagger/OpenAPI** documentation with Bearer token support
- **Unit Tests** for Application and Domain layers
- **Dependency Injection** throughout all layers

## 🛠️ Technologies

- **.NET 10** - Latest framework version
- **ASP.NET Core Web API** - RESTful API framework
- **MediatR 12.x** - CQRS pattern implementation
- **FluentValidation** - Declarative validation rules
- **AutoMapper** - Object-to-object mapping
- **Entity Framework Core 10** - ORM for data access
- **SQL Server / PostgreSQL** - Database providers (switchable)
- **JWT Bearer Authentication** - Secure token-based authentication
- **Swashbuckle.AspNetCore 6.9** - API documentation
- **xUnit** - Unit testing framework
- **C# 13** - Latest language features

## 🎯 CQRS Implementation

### Command Pattern (Write Operations)
Commands represent state-changing operations. Each command has:
- **Command Class** - Implements `IRequest<TResponse>`
- **Command Handler** - Implements `IRequestHandler<TCommand, TResponse>`
- **Command Validator** - FluentValidation rules

Example structure:
```
Features/TodoTask/Commands/
├── CreateTodoTask/
│   ├── CreateTodoTaskCommand.cs
│   ├── CreateTodoTaskCommandHandler.cs
│   └── CreateTodoTaskCommandValidator.cs
├── UpdateTodoTask/
│   ├── UpdateTodoTaskCommand.cs
│   ├── UpdateTodoTaskCommandHandler.cs
│   └── UpdateTodoTaskCommandValidator.cs
└── DeleteTodoTask/
    ├── DeleteTodoTaskCommand.cs
    ├── DeleteTodoTaskCommandHandler.cs
    └── DeleteTodoTaskCommandValidator.cs
```

### Query Pattern (Read Operations)
Queries represent data retrieval operations. Each query has:
- **Query Class** - Implements `IRequest<TResponse>`
- **Query Handler** - Implements `IRequestHandler<TQuery, TResponse>`

Example structure:
```
Features/TodoTask/Queries/
├── GetPagedTodoTasks/
│   ├── GetPagedTodoTasksQuery.cs
│   └── GetPagedTodoTasksQueryHandler.cs
├── GetTodoTaskById/
│   ├── GetTodoTaskByIdQuery.cs
│   └── GetTodoTaskByIdQueryHandler.cs
└── GetAllTodoTasks/
    ├── GetAllTodoTasksQuery.cs
    └── GetAllTodoTasksQueryHandler.cs
```

## 🔧 Technical Highlights

### MediatR Pipeline Behaviors
**ValidationBehavior** - Automatically validates all commands/queries before execution:
- Runs FluentValidation rules
- Throws validation exceptions on failure
- Eliminates repetitive validation code in handlers

### Dependency Injection Configuration
```csharp
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetPagedTodoTasksQuery).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(CreateTodoTaskCommandValidator).Assembly);
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
```

### Clean Separation of Concerns
- **Controllers** - Only handle HTTP concerns (routing, status codes)
- **MediatR Handlers** - Contain business logic
- **Domain Layer** - Pure business rules, no framework dependencies
- **Infrastructure** - Database and external service concerns

## 📋 Domain Model

### TodoTask Entity
- Rich domain model with encapsulated business logic
- State changes through domain methods
- Domain-level validation
- Support for categories, due dates, and assignments

### Category Entity
- Color-coded task categorization
- Validation for name length and format
- Update tracking with timestamps

## 🚦 Getting Started

### Prerequisites
- .NET 10 SDK
- SQL Server (LocalDB or full instance) OR PostgreSQL
- Visual Studio 2024 or VS Code

### Setup
1. Clone the repository
2. **Choose your database provider** in `appsettings.json`:
   ```json
   "DatabaseProvider": "SqlServer",  // or "PostgreSQL"
   ```
3. Run database migrations:
   ```bash
   dotnet ef database update --project src/TodoTasks.API
   ```
4. Start the application:
   ```bash
   dotnet run --project src/TodoTasks.API
   ```

### API Documentation
Navigate to `/swagger` in development mode to explore endpoints and test with JWT authentication.

## 🎯 API Endpoints

### Authentication
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/auth/register` | Register new user |
| POST | `/api/auth/login` | Login and get JWT token |

### Tasks (Protected)
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/todotasks` | Get all tasks (paginated) |
| GET | `/api/todotasks/{id}` | Get task by ID |
| POST | `/api/todotasks` | Create new task |
| PUT | `/api/todotasks/{id}` | Update existing task |
| DELETE | `/api/todotasks/{id}` | Delete task |
| POST | `/api/todotasks/{id}/complete` | Mark task as complete |

### Categories (Protected)
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/categories` | Get all categories |
| GET | `/api/categories/{id}` | Get category by ID |
| POST | `/api/categories` | Create new category |
| PUT | `/api/categories/{id}` | Update existing category |
| DELETE | `/api/categories/{id}` | Delete category |

## 🏆 Skills Demonstrated

### Advanced ASP.NET Core Expertise
- ✅ **CQRS Pattern** - Command Query Responsibility Segregation
- ✅ **MediatR Integration** - Request/response pipeline with behaviors
- ✅ **Pipeline Behaviors** - Cross-cutting concerns (validation, logging)
- ✅ **JWT Authentication** - Token-based security
- ✅ **Dependency Injection** - Advanced DI container usage
- ✅ **Middleware Pipeline** - HTTP request processing
- ✅ **Swagger Integration** - API documentation with Bearer auth

### Architecture & Design Patterns
- ✅ **Clean Architecture** - Layered application design
- ✅ **CQRS Pattern** - Command/Query separation
- ✅ **Repository Pattern** - Data access abstraction
- ✅ **Dependency Inversion** - Interface-based programming
- ✅ **Domain-Driven Design** - Rich domain models
- ✅ **SOLID Principles** - Clean, maintainable code
- ✅ **Feature Folder Structure** - Organized by business features

### Entity Framework Core
- ✅ **Code-First Migrations** - Database schema management
- ✅ **DbContext Configuration** - Database context setup
- ✅ **Relationship Mapping** - Entity relationships
- ✅ **Seed Data** - Initial data population
- ✅ **Async Operations** - Non-blocking database operations

### Validation & Mapping
- ✅ **FluentValidation** - Declarative validation rules
- ✅ **MediatR Behaviors** - Automatic validation pipeline
- ✅ **AutoMapper** - Object mapping with profiles
- ✅ **DTO Pattern** - Request/response data transfer

### Testing
- ✅ **Unit Tests** - Application layer handler tests
- ✅ **Domain Tests** - Entity and value object tests
- ✅ **xUnit Framework** - Modern testing practices
- ✅ **Test Isolation** - Independent test execution

### DevOps & CI/CD
- ✅ **GitHub Actions** - Automated CI/CD pipeline
- ✅ **Self-Contained Deployment** - .NET 10 to Railway deployment
- ✅ **Automated Build & Deploy** - Push to production on commit
- ✅ **Docker Integration** - Containerized deployment

### Modern C# Features
- ✅ **Nullable Reference Types** - Null safety
- ✅ **Record Types** - Immutable data structures
- ✅ **Pattern Matching** - Modern C# syntax
- ✅ **Primary Constructors** - Concise constructor syntax
- ✅ **Global Using Statements** - Reduced boilerplate

## 📈 Implemented Features

- ✅ **CQRS with MediatR** - Command/Query separation
- ✅ **MediatR Pipeline Behaviors** - Validation and cross-cutting concerns
- ✅ **FluentValidation** - Declarative validation rules
- ✅ **AutoMapper** - Object mapping
- ✅ **Authentication & Authorization (JWT)** - Secure token-based auth
- ✅ **Unit Tests** - Application and Domain layer coverage
- ✅ **CI/CD Pipeline** - GitHub Actions automated deployment to Railway
- [ ] Integration Tests
- [ ] Docker containerization
- [ ] Caching with Redis
- [ ] Logging with Serilog
- [ ] Health checks
- [ ] Rate limiting
- [ ] API versioning

## 💡 Why MediatR?

MediatR provides several benefits for enterprise applications:

1. **Decoupling** - Controllers don't directly reference business logic
2. **Testability** - Handlers can be tested independently
3. **Reusability** - Commands/Queries can be invoked from multiple sources
4. **Extensibility** - Pipeline behaviors enable cross-cutting concerns
5. **Maintainability** - Clear separation between read and write operations
6. **Scalability** - Easy to implement async operations and caching

---

*This project demonstrates production-ready ASP.NET Core development with advanced architectural patterns suitable for enterprise applications and scalable systems.*
