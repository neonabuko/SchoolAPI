using WizardAPI.Entities;
using WizardAPI.Entities.DTOs.Create;
using WizardAPI.Entities.DTOs.Edit;
using WizardAPI.Entities.DTOs.View;
using WizardAPI.Entities.Extensions;
using WizardAPI.Repositories.WizardRepositoriesImpl;
using WizardAPI.UseCase.WizardUseCasesImpl;

namespace WizardAPI.UseCase.StudentUseCases;

public class StudentUseCase(WizardRepositoryImpl<Student> studentRepository) : WizardUseCaseImpl<Student>(studentRepository)
{
    public async Task CreateStudentAsync(StudentCreateDto studentCreateDto)
    {
        Student newStudent = new()
        {
            Name = studentCreateDto.Name,
            Birthday = studentCreateDto.Birthday
        };

        await studentRepository.CreateAsync(newStudent);
    }

    public async Task<ICollection<StudentViewDto>> GetAllStudentsAsync()
    {
        return (await studentRepository.GetAllAsync()).Select(s => s.AsViewDto()).ToList();
    }

    public async Task<StudentViewDto> GetStudentAsync(int id)
    {
        return (await studentRepository.GetAsync(id) ?? throw new NullReferenceException()).AsViewDto();
    }

    public Task<ICollection<StudentViewDto>> GetStudentsByInteractiveGroupIdAsync(int groupId)
    {
        return Task.FromResult<ICollection<StudentViewDto>>(studentRepository.GetAllAsync().Result
            .Where(s => s.InteractiveGroupId == groupId)
            .Select(s => s.AsViewDto()).ToList());
    }

    public async Task UpdateStudentAsync(int id, StudentEditDto studentEditDto)
    {
        var toUpdate = await studentRepository.GetAsync(id);
        if (toUpdate != null)
        {
            toUpdate.Name = studentEditDto.Name ?? toUpdate.Name;
            toUpdate.Birthday = studentEditDto.Birthday ?? toUpdate.Birthday;
            toUpdate.InteractiveGroupId = studentEditDto.InteractiveGroupId ?? toUpdate.InteractiveGroupId;
        }

        if (toUpdate != null) await studentRepository.UpdateAsync(toUpdate);
    }
}