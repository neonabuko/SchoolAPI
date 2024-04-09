namespace SchoolAPI.Repositories.Interfaces;

public interface ISchoolRepository<T>
{
    Task CreateAsync(T entity);
    Task<ICollection<T>> GetAllAsync();
    Task<T?> GetAsync(int id);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}