using SchoolAPI.Entities;
using SchoolAPI.Entities.DTOs.Create;
using SchoolAPI.Entities.DTOs.Edit;
using SchoolAPI.Entities.DTOs.View;
using SchoolAPI.Entities.Extensions;
using SchoolAPI.Repositories.SchoolRepositoriesImpl;
using SchoolAPI.UseCase.SchoolUseCasesImpl;

namespace SchoolAPI.UseCase.GroupUseCases;

public class GroupUseCase(SchoolRepositoryImpl<Group> GroupRepository) 
: SchoolUseCaseImpl<Group>(GroupRepository)
{
    public async Task<GroupViewDto> CreateGroupAsync(GroupCreateDto GroupCreateDto)
    {
        var teacherId = GroupCreateDto.TeacherId;
        var dateTime = GroupCreateDto.DateTime;
        if (teacherId != null) {
            if (GetGroupByTeacherAndDateTimeAsync((int)teacherId, dateTime).Result.Count > 0)
            throw new DbNameConflictException("Date/Time already taken by another group.");
        }

        Group newGroup = new()
        {
            Name = GroupCreateDto.Name,
            DateTime = DataConverters.StringToDateTime(GroupCreateDto.DateTime),
            TeacherId = GroupCreateDto.TeacherId
        };

        await GroupRepository.CreateAsync(newGroup);
        return newGroup.AsViewDto();
    }

    public async Task<ICollection<GroupViewDto>> GetAllGroupsAsync()
    {
        return (await GroupRepository.GetAllAsync()).Select(group => group.AsViewDto()).ToList();
    }

    public async Task<ICollection<GroupViewDto>> GetGroupByTeacherAndDateTimeAsync(int teacherId, string dateTime) {
        var formattedDateTime = DataConverters.StringToDateTime(dateTime);

        var Groups = await GroupRepository.GetAllAsync();
        return Groups
            .Where(i => i.TeacherId == teacherId)
            .Where(i => i.DateTime == formattedDateTime)
            .Select(i => i.AsViewDto())
            .ToList();
    }


    public Task<GroupViewDto> GetGroupAsync(int id)
    {
        return Task.FromResult((
            GroupRepository.GetAsync(id).Result ?? throw new NullReferenceException()
            ).AsViewDto());
    }

    public async Task<ICollection<GroupViewDto>> QueryGroupsByName(string name)
    {
        var Groups = await GroupRepository.GetAllAsync();
        return Groups
        .Where(s => s.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
        .Select(s => s.AsViewDto())
        .ToList();
    }
    
    public async Task UpdateGroupAsync(int id, GroupEditDto dto)
    {
        var groupToUpdate = await GroupRepository.GetAsync(id) 
                            ?? throw new NullReferenceException("Group not found.");
        
        groupToUpdate.Name = dto.Name ?? groupToUpdate.Name;
        if (dto.DateTime != null) {
            var formattedDateTime = DataConverters.StringToDateTime(dto.DateTime);
            groupToUpdate.DateTime =  formattedDateTime;
        }
        groupToUpdate.TeacherId = dto.TeacherId ?? groupToUpdate.TeacherId;

        await GroupRepository.UpdateAsync(groupToUpdate);
    }
}