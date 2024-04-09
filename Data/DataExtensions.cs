using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Entities;
using SchoolAPI.Repositories.GroupRepositories;
using SchoolAPI.Repositories.StudentRepositories;
using SchoolAPI.Repositories.TeacherRepositories;
using SchoolAPI.Repositories.LessonRepositories;
using SchoolAPI.Repositories.SchoolRepositoriesImpl;
using SchoolAPI.UseCase.LessonUseCases;
using SchoolAPI.UseCase.GroupUseCases;
using SchoolAPI.UseCase.StudentUseCases;
using SchoolAPI.UseCase.TeacherUseCases;
using SchoolAPI.UseCase.SchoolUseCasesImpl;

namespace SchoolAPI.Data;

public static class DataExtensions
{
    public static async Task InitializeDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SchoolContext>();
        await dbContext.Database.MigrateAsync();
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SchoolContext");
        services.AddSqlServer<SchoolContext>(connectionString);
        
        // repositories
        services.AddScoped<TeacherRepository>();
        services.AddTransient<SchoolRepositoryImpl<Teacher>, TeacherRepository>();

        services.AddScoped<LessonRepository>();
        services.AddTransient<SchoolRepositoryImpl<Lesson>, LessonRepository>();

        services.AddScoped<GroupRepository>();
        services.AddTransient<SchoolRepositoryImpl<Group>, GroupRepository>();
        
        services.AddScoped<StudentRepository>();
        services.AddTransient<SchoolRepositoryImpl<Student>, StudentRepository>();

        // use cases
        services.AddScoped<TeacherUseCase>();
        services.AddTransient<SchoolUseCaseImpl<Teacher>, TeacherUseCase>();

        services.AddScoped<LessonUseCase>();
        services.AddTransient<SchoolUseCaseImpl<Lesson>, LessonUseCase>();

        services.AddScoped<GroupUseCase>();
        services.AddTransient<SchoolUseCaseImpl<Group>, GroupUseCase>();

        services.AddScoped<StudentUseCase>();
        services.AddTransient<SchoolUseCaseImpl<Student>, StudentUseCase>();
        
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen();

        const string AuthScheme = "cookie";
        
        services.AddAuthentication(AuthScheme)
            .AddCookie(AuthScheme);

        services.AddAuthorization();

        services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<SchoolContext>()
        .AddDefaultTokenProviders();

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