using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data;
using SchoolAPI.Repositories.Interfaces;

namespace SchoolAPI.Repositories.SchoolRepositoriesImpl;

public class SchoolRepositoryImpl<T>(SchoolContext context) : ISchoolRepository<T> where T : class
{
    public async Task CreateAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task<ICollection<T>> GetAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
    }
}