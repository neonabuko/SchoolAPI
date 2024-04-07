using System.Globalization;
using WizardAPI.Entities;
using WizardAPI.Entities.DTOs.Create;
using WizardAPI.Entities.DTOs.Edit;
using WizardAPI.Entities.DTOs.View;
using WizardAPI.Entities.Extensions;
using WizardAPI.Repositories.WizardRepositoriesImpl;
using WizardAPI.UseCase.WizardUseCasesImpl;

namespace WizardAPI.UseCase.StudentUseCases;

public class StudentUseCase(WizardRepositoryImpl<Student> studentRepository): WizardUseCaseImpl<Student>(studentRepository)
{
    public async Task<StudentViewDto> CreateStudentAsync(StudentCreateDto studentCreateDto)
    {
        var name = studentCreateDto.Name;
        if (studentRepository.GetAllAsync().Result.Where(s => s.Name == name).ToList().Count > 0) 
        throw new DbNameConflictException("A student with the same name already exists.");
        
        var stringBirthday = studentCreateDto.Birthday ?? "01/01/2001";
        var dateTimeBirthday = DateTime.ParseExact(stringBirthday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        var dateOnlyBirthday = new DateOnly(dateTimeBirthday.Year, dateTimeBirthday.Month, dateTimeBirthday.Day);

        Student newStudent = new()
        {
            Name = studentCreateDto.Name,
            Birthday = dateOnlyBirthday,
            RegistrationId = int.Parse(studentCreateDto.RegistrationId),
            InteractiveGroupId = studentCreateDto.InteractiveGroupId
        };

        await studentRepository.CreateAsync(newStudent);
        return newStudent.AsViewDto();
    }

    public async Task<ICollection<StudentViewDto>> GetAllStudentsAsync()
    {
        return (await studentRepository.GetAllAsync()).Select(s => s.AsViewDto()).ToList();
    }

    public Task<ICollection<StudentViewDto>> QueryStudentsByName(string name)
    {
        return Task.FromResult<ICollection<StudentViewDto>>(
            studentRepository.GetAllAsync().Result.Where(
                    s => s.Name.StartsWith(name, StringComparison.CurrentCultureIgnoreCase))
                .Select(s => s.AsViewDto())
                .ToList());
    }

    public async Task<StudentViewDto> GetStudentAsync(int id)
    {
        return (await studentRepository.GetAsync(id) ?? throw new NullReferenceException()).AsViewDto();
    }

    public async Task<ICollection<StudentViewDto>> GetStudentsByInteractiveGroupIdAsync(int groupId)
    {
        var students = await studentRepository.GetAllAsync();
        return students
        .Where(s => s.InteractiveGroupId == groupId)
        .Select(s => s.AsViewDto()).ToList();
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