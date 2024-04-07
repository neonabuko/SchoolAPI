using WizardAPI.Entities;
using WizardAPI.Entities.DTOs.Create;
using WizardAPI.Entities.DTOs.Edit;
using WizardAPI.Entities.DTOs.View;
using WizardAPI.Entities.Extensions;
using WizardAPI.Repositories.WizardRepositoriesImpl;
using WizardAPI.UseCase.WizardUseCasesImpl;

namespace WizardAPI.UseCase.InteractiveGroupUseCases;

public class InteractiveGroupUseCase(WizardRepositoryImpl<InteractiveGroup> interactiveGroupRepository) 
: WizardUseCaseImpl<InteractiveGroup>(interactiveGroupRepository)
{
    public async Task<InteractiveGroupViewDto> CreateInteractiveGroupAsync(InteractiveGroupCreateDto interactiveGroupCreateDto)
    {
        var teacherId = interactiveGroupCreateDto.TeacherId;
        var dateTime = interactiveGroupCreateDto.DateTime;
        if (teacherId != null) {
            if (GetInteractiveGroupByTeacherAndDateTimeAsync((int)teacherId, dateTime).Result.Count > 0)
            throw new DbNameConflictException("Date/Time already taken by another group.");
        }

        InteractiveGroup newInteractiveGroup = new()
        {
            Name = interactiveGroupCreateDto.Name,
            DateTime = DataConverters.StringToDateTime(interactiveGroupCreateDto.DateTime),
            TeacherId = interactiveGroupCreateDto.TeacherId
        };

        await interactiveGroupRepository.CreateAsync(newInteractiveGroup);
        return newInteractiveGroup.AsViewDto();
    }

    public async Task<ICollection<InteractiveGroupViewDto>> GetAllInteractiveGroupsAsync()
    {
        return (await interactiveGroupRepository.GetAllAsync()).Select(group => group.AsViewDto()).ToList();
    }

    public async Task<ICollection<InteractiveGroupViewDto>> GetInteractiveGroupByTeacherAndDateTimeAsync(int teacherId, string dateTime) {
        var formattedDateTime = DataConverters.StringToDateTime(dateTime);

        var interactiveGroups = await interactiveGroupRepository.GetAllAsync();
        return interactiveGroups
            .Where(i => i.TeacherId == teacherId)
            .Where(i => i.DateTime == formattedDateTime)
            .Select(i => i.AsViewDto())
            .ToList();
    }


    public Task<InteractiveGroupViewDto> GetInteractiveGroupAsync(int id)
    {
        return Task.FromResult((
            interactiveGroupRepository.GetAsync(id).Result ?? throw new NullReferenceException()
            ).AsViewDto());
    }

    public async Task<ICollection<InteractiveGroupViewDto>> QueryInteractiveGroupsByName(string name)
    {
        var interactiveGroups = await interactiveGroupRepository.GetAllAsync();
        return interactiveGroups
        .Where(s => s.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
        .Select(s => s.AsViewDto())
        .ToList();
    }
    
    public async Task UpdateInteractiveGroupAsync(int id, InteractiveGroupEditDto dto)
    {
        var groupToUpdate = await interactiveGroupRepository.GetAsync(id) 
                            ?? throw new NullReferenceException("Group not found.");
        
        groupToUpdate.Name = dto.Name ?? groupToUpdate.Name;
        if (dto.DateTime != null) {
            var formattedDateTime = DataConverters.StringToDateTime(dto.DateTime);
            groupToUpdate.DateTime =  formattedDateTime;
        }
        groupToUpdate.TeacherId = dto.TeacherId ?? groupToUpdate.TeacherId;

        await interactiveGroupRepository.UpdateAsync(groupToUpdate);
    }
}