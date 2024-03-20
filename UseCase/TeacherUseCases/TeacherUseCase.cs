using WizardAPI.Entities;
using WizardAPI.Entities.DTOs;
using WizardAPI.Entities.Extensions;
using WizardAPI.Repositories.Interfaces;
using WizardAPI.Repositories.WizardRepositoriesImpl;
using WizardAPI.UseCase.WizardUseCasesImpl;

namespace WizardAPI.UseCase.TeacherUseCases;

public class TeacherUseCase(WizardRepositoryImpl<Teacher> teacherClassRepository) : WizardUseCaseImpl<Teacher>(teacherClassRepository)
{
    public async Task CreateTeacherAsync(TeacherDto teacherDto)
    {
        Teacher newTeacher = new()
        {
            Name = teacherDto.Name,
            Birthday = teacherDto.Birthday
        };

        await teacherClassRepository.CreateAsync(newTeacher);
    }

    public async Task<ICollection<TeacherDto>> GetAllTeachersAsync()
    {
        return (await teacherClassRepository.GetAllAsync()).Select(t => t.AsDto()).ToList();
    }

    public async Task<TeacherDto> GetTeacherAsync(int id)
    {
        return (await teacherClassRepository.GetAsync(id) ?? throw new NullReferenceException()).AsDto();
    }

    public async Task UpdateTeacherAsync(int id, TeacherDto teacherDto)
    {
        var toUpdate = await teacherClassRepository.GetAsync(id);
        if (toUpdate != null)
        {
            toUpdate.Name = teacherDto.Name;
            toUpdate.Birthday = teacherDto.Birthday;
        }

        if (toUpdate != null) await teacherClassRepository.UpdateAsync(toUpdate);
    }
}