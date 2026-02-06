using Microsoft.EntityFrameworkCore;
using TodoTasks.Domain.Entities;
using TodoTasks.Domain.Enums;

namespace TodoTasks.Infrastructure;

public class AppDbContext : DbContext
{

    public AppDbContext() { }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<TodoTask> TodoTasks { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure table names for PostgreSQL (lowercase)
        modelBuilder.Entity<TodoTask>().ToTable("todotasks");
        modelBuilder.Entity<Category>().ToTable("categories");

        modelBuilder.Entity<Category>().HasData(
            new { Id = 1, Name = "Work", Description = "Work related tasks", Color = TaskColorEnum.Yellow, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new { Id = 2, Name = "Personal", Description = "Personal tasks", Color = TaskColorEnum.Red, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new { Id = 3, Name = "Shopping", Description = "Shopping list items", Color = TaskColorEnum.Green, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
        
        modelBuilder.Entity<TodoTask>().HasData(
            new { Id = 1, Title = "Complete project proposal", Description = "Finish the Q1 project proposal document", CategoryId = 1, AssignedTo = 1, IsCompleted = false, DueDate = new DateTime(2024, 12, 31, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new { Id = 2, Title = "Buy groceries", Description = "Milk, bread, eggs, and fruits", CategoryId = 3, AssignedTo = 1, IsCompleted = false, DueDate = new DateTime(2024, 12, 25, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new { Id = 3, Title = "Schedule dentist appointment", Description = "Annual checkup", CategoryId = 2, AssignedTo = 1, IsCompleted = true, CompletedAt = new DateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}