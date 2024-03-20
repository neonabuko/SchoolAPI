using WizardAPI.Entities;
using WizardAPI.Entities.DTOs;
using WizardAPI.Entities.Extensions;
using WizardAPI.Repositories.WizardRepositoriesImpl;
using WizardAPI.UseCase.WizardUseCasesImpl;

namespace WizardAPI.UseCase.StudentUseCases;

public class StudentUseCase(WizardRepositoryImpl<Student> studentRepository) : WizardUseCaseImpl<Student>(studentRepository)
{
    public async Task CreateStudentAsync(StudentDto studentDto)
    {
        Student newStudent = new()
        {
            Name = studentDto.Name,
            Birthday = studentDto.Birthday
        };

        await studentRepository.CreateAsync(newStudent);
    }

    public async Task<ICollection<StudentOutDto>> GetAllStudentsAsync()
    {
        return (await studentRepository.GetAllAsync()).Select(t => t.AsOutDto()).ToList();
    }

    public async Task<StudentOutDto> GetStudentAsync(int id)
    {
        return (await studentRepository.GetAsync(id) ?? throw new NullReferenceException()).AsOutDto();
    }

    public async Task UpdateStudentAsync(int id, StudentDto studentDto)
    {
        var toUpdate = await studentRepository.GetAsync(id);
        if (toUpdate != null)
        {
            toUpdate.Name = studentDto.Name;
            toUpdate.Birthday = studentDto.Birthday;
        }

        if (toUpdate != null) await studentRepository.UpdateAsync(toUpdate);
    }
}