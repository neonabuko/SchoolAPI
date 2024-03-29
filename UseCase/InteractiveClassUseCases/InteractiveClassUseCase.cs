using System.Globalization;
using WizardAPI.Entities;
using WizardAPI.Entities.DTOs.Create;
using WizardAPI.Entities.DTOs.Edit;
using WizardAPI.Entities.DTOs.View;
using WizardAPI.Entities.Extensions;
using WizardAPI.Repositories.WizardRepositoriesImpl;
using WizardAPI.UseCase.WizardUseCasesImpl;

namespace WizardAPI.UseCase.InteractiveClassUseCases;

public class InteractiveClassUseCase(WizardRepositoryImpl<InteractiveClass> interactiveClassRepository,
    WizardRepositoryImpl<Student> studentRepository) : WizardUseCaseImpl<InteractiveClass>(interactiveClassRepository)
{

    public async Task CreateInteractiveClassAsync(InteractiveClassCreateDto interactiveClassCreateDto)
    {
        var dateString = interactiveClassCreateDto.DateTime;
        DateTime.TryParseExact(dateString, "dd/MM HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None,
            out var dateTime);

        InteractiveClass newInteractiveClass = new()
        {
            Lesson = interactiveClassCreateDto.Lesson,
            DateTime = dateTime,
            Oral = interactiveClassCreateDto.Oral,
            HwDelivered = bool.Parse(interactiveClassCreateDto.HwDelivered ?? "0"),
            HwGrade = interactiveClassCreateDto.HwGrade,
            StudentPresent = bool.Parse(interactiveClassCreateDto.StudentPresent ?? "0"),
            StudentId = interactiveClassCreateDto.StudentId
        };

        await interactiveClassRepository.CreateAsync(newInteractiveClass);
    }

    public async Task<ICollection<InteractiveClassViewDto>> GetAllInteractiveClassesAsync()
    {
        return (await interactiveClassRepository.GetAllAsync()).Select(i => i.AsViewDto()).ToList();
    }

    public Task<ICollection<InteractiveClassViewDto>> GetInteractiveClassesByStudentIdAsync(int studentId)
    {
        return Task.FromResult<ICollection<InteractiveClassViewDto>>(interactiveClassRepository.GetAllAsync().Result
            .Where(i => i.StudentId == studentId)
            .Select(i => i.AsViewDto()).ToList());
    }

    public Task<InteractiveClassViewDto> GetFirstInteractiveClassScheduledForTodayByStudentIdAsync(int studentId)
    {
        return Task.FromResult<InteractiveClassViewDto>(
            interactiveClassRepository.GetAllAsync().Result
            .Where(i => i.StudentId == studentId)
            .Where(i => i.DateTime.Day == DateTime.Now.Day
            ).Select(i => i.AsViewDto()).ToList().First());
    }

    public Task<ICollection<StudentViewDto>> GetStudentsThatHaveClassTodayByGroupId(int groupId)
    {
        var classes = interactiveClassRepository.GetAllAsync().Result
            .Where(i => i.DateTime.Date == DateTime.Now.Date).ToList();
        
        return Task.FromResult<ICollection<StudentViewDto>>(classes.Select(
                iClass => 
                    studentRepository.GetAsync(iClass.StudentId ?? throw new NullReferenceException()).Result 
                    ?? throw new NullReferenceException())
            .ToList().Where(s => s.InteractiveGroupId == groupId).Select(s => s.AsViewDto()).ToList());
    }

    public async Task<InteractiveClassViewDto> GetInteractiveClassAsync(int id)
    {
        return (await interactiveClassRepository.GetAsync(id) ?? throw new NullReferenceException()).AsViewDto();
    }

    public async Task UpdateInteractiveClassAsync(int id, InteractiveClassEditDto interactiveClassEditDto)
    {
        var toUpdate = await interactiveClassRepository.GetAsync(id) ?? throw new NullReferenceException();
        
        toUpdate.Lesson = interactiveClassEditDto.Lesson ?? toUpdate.Lesson;
        toUpdate.DateTime = interactiveClassEditDto.DateTime != null 
            ? DataConverters.StringToDateTime(interactiveClassEditDto.DateTime) 
            : toUpdate.DateTime;
        toUpdate.Oral = interactiveClassEditDto.Oral ?? toUpdate.Oral;
        toUpdate.HwDelivered = interactiveClassEditDto.HwDelivered ?? toUpdate.HwDelivered;
        toUpdate.HwGrade = interactiveClassEditDto.HwGrade ?? toUpdate.HwGrade;
        toUpdate.StudentPresent = interactiveClassEditDto.StudentPresent ?? toUpdate.StudentPresent;
        toUpdate.StudentId = interactiveClassEditDto.StudentId ?? toUpdate.StudentId;
        
        await interactiveClassRepository.UpdateAsync(toUpdate);
    }
}