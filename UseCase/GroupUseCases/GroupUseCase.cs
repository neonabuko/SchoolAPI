using System.Globalization;
using WizardAPI.Entities;
using WizardAPI.Entities.DTOs;
using WizardAPI.Entities.Extensions;
using WizardAPI.Repositories.WizardRepositoriesImpl;
using WizardAPI.UseCase.WizardUseCasesImpl;

namespace WizardAPI.UseCase.GroupUseCases;

public class GroupUseCase(WizardRepositoryImpl<InteractiveGroup> groupRepository,
    WizardRepositoryImpl<Student> studentRepository,
    WizardRepositoryImpl<Teacher> teacherRepository) : WizardUseCaseImpl<InteractiveGroup>(groupRepository)
{
    public async Task CreateGroupAsync(GroupDto groupDto)
    {
        var students = new List<Student>();
        foreach (var id in groupDto.StudentIds)
        {
            students.Add(await studentRepository.GetAsync(id) ?? throw new NullReferenceException());
        }

        var teacher = await teacherRepository.GetAsync(groupDto.TeacherId) ?? throw new NullReferenceException();

        var toDateTime = groupDto.DateTime;
        DateTime.TryParseExact(toDateTime, "dd/MM HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None,
            out var dateTime);
        
        InteractiveGroup newInteractiveGroup = new()
        {
            Teacher = teacher,
            DateTime = dateTime,
            Students = students
        };

        await groupRepository.CreateAsync(newInteractiveGroup);
    }

    public async Task<ICollection<GroupOutDto>> GetAllGroupsAsync()
    {
        return (await groupRepository.GetAllAsync()).Select(g => g.AsOutDto()).ToList();
    }
}