using System.Globalization;
using WizardAPI.Entities;
using WizardAPI.Entities.DTOs.Create;
using WizardAPI.Entities.DTOs.Edit;
using WizardAPI.Entities.DTOs.View;
using WizardAPI.Entities.Extensions;
using WizardAPI.Repositories.WizardRepositoriesImpl;
using WizardAPI.UseCase.WizardUseCasesImpl;

namespace WizardAPI.UseCase.TeacherUseCases;

public class TeacherUseCase(WizardRepositoryImpl<Teacher> teacherRepository) : WizardUseCaseImpl<Teacher>(teacherRepository)
{
    public async Task<TeacherViewDto> CreateTeacherAsync(TeacherCreateDto teacherCreateDto)
    {
        if (QueryTeachersByNameAsync(teacherCreateDto.Name).Result.Count > 0) 
        throw new DbNameConflictException("A Teacher with name " + teacherCreateDto.Name + " already exists.");
        
        var stringBirthday = teacherCreateDto.Birthday ?? "01/01/2001";
        var dateTimeBirthday = DateTime.ParseExact(stringBirthday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        var dateOnlyBirthday = new DateOnly(dateTimeBirthday.Year, dateTimeBirthday.Month, dateTimeBirthday.Day);
        
        Teacher newTeacher = new()
        {
            Name = teacherCreateDto.Name,
            Birthday = dateOnlyBirthday
        };

        await teacherRepository.CreateAsync(newTeacher);
        return newTeacher.AsViewDto();
    }

    public async Task<ICollection<TeacherViewDto>> GetAllTeachersAsync()
    {
        return (await teacherRepository.GetAllAsync()).Select(t => t.AsViewDto()).ToList();
    }

    public Task<ICollection<TeacherViewDto>> QueryTeachersByNameAsync(string name) {
        return Task.FromResult<ICollection<TeacherViewDto>>(
            teacherRepository.GetAllAsync().Result
            .Where(t => t.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
            .Select(t => t.AsViewDto()).ToList());
    }

    public async Task<TeacherViewDto> GetTeacherAsync(int id)
    {
        return (await teacherRepository.GetAsync(id) ?? throw new NullReferenceException()).AsViewDto();
    }

    public async Task UpdateTeacherAsync(int id, TeacherEditDto teacherEditDto)
    {
        var toUpdate = await teacherRepository.GetAsync(id) ?? throw new NullReferenceException();
        
        toUpdate.Name = teacherEditDto.Name ?? toUpdate.Name;
        toUpdate.Birthday = teacherEditDto.Birthday ?? toUpdate.Birthday;
        
        await teacherRepository.UpdateAsync(toUpdate);
    }
}