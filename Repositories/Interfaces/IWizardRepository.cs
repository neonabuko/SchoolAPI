namespace WizardAPI.Repositories.Interfaces;

public interface IWizardRepository<T>
{
    Task CreateAsync(T entity);
    Task<ICollection<T>> GetAllAsync();
    Task<T?> GetAsync(int id);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}