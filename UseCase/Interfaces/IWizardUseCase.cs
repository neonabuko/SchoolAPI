namespace SchoolAPI.UseCase.Interfaces;

public interface ISchoolUseCase<T>
{
    Task CreateAsync(T entity);
    Task<ICollection<T>> GetAllAsync();
    Task<T> GetAsync(int id);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}