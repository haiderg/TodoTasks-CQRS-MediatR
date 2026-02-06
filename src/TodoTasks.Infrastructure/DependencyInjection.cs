using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TodoTasks.Application.Common.Interfaces;
using TodoTasks.Domain.Repositories;
using TodoTasks.Infrastructure.Authentication;
using TodoTasks.Infrastructure.Repositories;


namespace TodoTasks.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<Application.Common.Authentication.JwtOptions>(configuration.GetSection("Jwt").Bind);
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            var databaseProvider = configuration.GetValue<string>("DatabaseProvider");
            services.AddDbContext<AppDbContext>(options =>
            {
                switch (databaseProvider?.ToLower())
                {
                    case "sqlserver":
                        options.UseSqlServer(
                            configuration.GetConnectionString("SQLServerConnection"),
                            b => b.MigrationsAssembly("TodoTasks.API"));
                        break;

                    case "postgresql":
                        options.UseNpgsql(
                            configuration.GetConnectionString("PostgresConnection"),
                            b => b.MigrationsAssembly("TodoTasks.API")
                            );
                        break;

                    default:
                        throw new InvalidOperationException(
                            $"Unsupported database provider: {databaseProvider}. " +
                            "Supported providers: SqlServer, PostgreSQL");
                }
            });

            services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }


}
