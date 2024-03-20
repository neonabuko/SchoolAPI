using WizardAPI.Repositories.Interfaces;
using WizardAPI.UseCase.Interfaces;

namespace WizardAPI.UseCase.WizardUseCasesImpl;

public class WizardUseCaseImpl<T>(IWizardRepository<T> groupRepository) : IWizardUseCase<T> where T : class
{
    public async Task CreateAsync(T entity)
    {
        await groupRepository.CreateAsync(entity);
    }

    public async Task<ICollection<T>> GetAllAsync()
    {
        return await groupRepository.GetAllAsync();
    }

    public async Task<T> GetAsync(int id)
    {
        return await groupRepository.GetAsync(id) ?? throw new NullReferenceException();
    }

    public async Task UpdateAsync(T entity)
    {
        await groupRepository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await groupRepository.GetAsync(id);
        if (entity != null) await groupRepository.DeleteAsync(entity);
    }
}