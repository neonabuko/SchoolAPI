using System.Globalization;
using WizardAPI.Entities;
using WizardAPI.Entities.DTOs.Create;
using WizardAPI.Entities.DTOs.Edit;
using WizardAPI.Entities.DTOs.View;
using WizardAPI.Entities.Extensions;
using WizardAPI.Repositories.WizardRepositoriesImpl;
using WizardAPI.UseCase.WizardUseCasesImpl;

namespace WizardAPI.UseCase.InteractiveGroupUseCases;

public class InteractiveGroupUseCase(WizardRepositoryImpl<InteractiveGroup> interactiveGroupRepository) : WizardUseCaseImpl<InteractiveGroup>(interactiveGroupRepository)
{
    public async Task CreateInteractiveGroupAsync(InteractiveGroupCreateDto interactiveGroupCreateDto)
    {
        var time = interactiveGroupCreateDto.Time;
        var timeOnly = new TimeOnly();
        if (DateTime.TryParseExact(time, "HH:mm", null, DateTimeStyles.None, out var dateTime))
        {
            var timeSpan = dateTime.TimeOfDay;
            timeOnly = TimeOnly.FromTimeSpan(timeSpan);
        }

        InteractiveGroup newInteractiveGroup = new()
        {
            Name = interactiveGroupCreateDto.Name,
            DaysOfTheWeek = interactiveGroupCreateDto.DaysOfTheWeek,
            Time = timeOnly
        };

        await interactiveGroupRepository.CreateAsync(newInteractiveGroup);
    }

    public async Task<ICollection<InteractiveGroupViewDto>> GetAllInteractiveGroupsAsync()
    {
        return (await interactiveGroupRepository.GetAllAsync()).Select(group => group.AsViewDto()).ToList();
    }

    public Task<InteractiveGroupViewDto> GetInteractiveGroupAsync(int id)
    {
        return Task.FromResult((
            interactiveGroupRepository.GetAsync(id).Result ?? throw new NullReferenceException()
            ).AsViewDto());
    }

    public async Task UpdateInteractiveGroupAsync(int id, InteractiveGroupEditDto dto)
    {
        var groupToUpdate = await interactiveGroupRepository.GetAsync(id) 
                            ?? throw new NullReferenceException("Group not found.");
        
        groupToUpdate.Name = dto.Name ?? groupToUpdate.Name;
        groupToUpdate.DaysOfTheWeek = dto.DaysOfTheWeek ?? groupToUpdate.DaysOfTheWeek;
        groupToUpdate.Time = dto.Time ?? groupToUpdate.Time;
        groupToUpdate.TeacherId = dto.TeacherId ?? groupToUpdate.TeacherId;

        await interactiveGroupRepository.UpdateAsync(groupToUpdate);
    }
}