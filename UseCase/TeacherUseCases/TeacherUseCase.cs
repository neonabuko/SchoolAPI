using WizardAPI.Entities;
using WizardAPI.Entities.DTOs.Create;
using WizardAPI.Entities.DTOs.Edit;
using WizardAPI.Entities.DTOs.View;
using WizardAPI.Entities.Extensions;
using WizardAPI.Repositories.WizardRepositoriesImpl;
using WizardAPI.UseCase.WizardUseCasesImpl;

namespace WizardAPI.UseCase.TeacherUseCases;

public class TeacherUseCase(WizardRepositoryImpl<Teacher> teacherClassRepository) : WizardUseCaseImpl<Teacher>(teacherClassRepository)
{
    public async Task CreateTeacherAsync(TeacherCreateDto teacherCreateDto)
    {
        Teacher newTeacher = new()
        {
            Name = teacherCreateDto.Name,
            Birthday = teacherCreateDto.Birthday
        };

        await teacherClassRepository.CreateAsync(newTeacher);
    }

    public async Task<ICollection<TeacherViewDto>> GetAllTeachersAsync()
    {
        return (await teacherClassRepository.GetAllAsync()).Select(t => t.AsViewDto()).ToList();
    }

    public async Task<TeacherViewDto> GetTeacherAsync(int id)
    {
        return (await teacherClassRepository.GetAsync(id) ?? throw new NullReferenceException()).AsViewDto();
    }

    public async Task UpdateTeacherAsync(int id, TeacherEditDto teacherEditDto)
    {
        var toUpdate = await teacherClassRepository.GetAsync(id) ?? throw new NullReferenceException();
        
        toUpdate.Name = teacherEditDto.Name ?? toUpdate.Name;
        toUpdate.Birthday = teacherEditDto.Birthday ?? toUpdate.Birthday;
        
        await teacherClassRepository.UpdateAsync(toUpdate);
    }
}