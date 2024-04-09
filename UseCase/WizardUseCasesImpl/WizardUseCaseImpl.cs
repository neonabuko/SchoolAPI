using SchoolAPI.Repositories.Interfaces;
using SchoolAPI.UseCase.Interfaces;

namespace SchoolAPI.UseCase.SchoolUseCasesImpl;

public class SchoolUseCaseImpl<T>(ISchoolRepository<T> groupRepository) : ISchoolUseCase<T> where T : class
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