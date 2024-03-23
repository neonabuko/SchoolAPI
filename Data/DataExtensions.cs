using Microsoft.EntityFrameworkCore;
using WizardAPI.Entities;
using WizardAPI.Repositories.GroupRepositories;
using WizardAPI.Repositories.StudentRepositories;
using WizardAPI.Repositories.TeacherRepositories;
using WizardAPI.Repositories.WizardClassRepositories;
using WizardAPI.Repositories.WizardRepositoriesImpl;
using WizardAPI.UseCase.InteractiveClassUseCases;
using WizardAPI.UseCase.InteractiveGroupUseCases;
using WizardAPI.UseCase.StudentUseCases;
using WizardAPI.UseCase.TeacherUseCases;
using WizardAPI.UseCase.WizardUseCasesImpl;

namespace WizardAPI.Data;

public static class DataExtensions
{
    public static async Task InitializeDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WizardContext>();
        await dbContext.Database.MigrateAsync();
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("WizardContext");
        services.AddSqlServer<WizardContext>(connectionString);
        
        // repositories
        services.AddScoped<TeacherRepository>();
        services.AddTransient<WizardRepositoryImpl<Teacher>, TeacherRepository>();

        services.AddScoped<WizardClassRepository>();
        services.AddTransient<WizardRepositoryImpl<InteractiveClass>, WizardClassRepository>();

        services.AddScoped<GroupRepository>();
        services.AddTransient<WizardRepositoryImpl<InteractiveGroup>, GroupRepository>();
        
        services.AddScoped<StudentRepository>();
        services.AddTransient<WizardRepositoryImpl<Student>, StudentRepository>();

        // use cases
        services.AddScoped<TeacherUseCase>();
        services.AddTransient<WizardUseCaseImpl<Teacher>, TeacherUseCase>();

        services.AddScoped<InteractiveClassUseCase>();
        services.AddTransient<WizardUseCaseImpl<InteractiveClass>, InteractiveClassUseCase>();

        services.AddScoped<InteractiveGroupUseCase>();
        services.AddTransient<WizardUseCaseImpl<InteractiveGroup>, InteractiveGroupUseCase>();

        services.AddScoped<StudentUseCase>();
        services.AddTransient<WizardUseCaseImpl<Student>, StudentUseCase>();
        
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        
        return services;
    }
}