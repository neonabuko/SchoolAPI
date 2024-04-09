using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Entities.DTOs.Create;
using SchoolAPI.Entities.DTOs.Edit;
using SchoolAPI.Entities.DTOs.View;
using SchoolAPI.UseCase.GroupUseCases;

namespace SchoolAPI.Controllers;

[ApiController]
public class GroupController(GroupUseCase groupUseCase) : ControllerBase
{
    [HttpGet("/groups")]
    public async Task<ICollection<GroupViewDto>> Index() => await groupUseCase.GetAllGroupsAsync();
    
    [HttpGet("/groups/{id:int}")]
    public async Task<GroupViewDto> GetAsync(int id) => await groupUseCase.GetGroupAsync(id);
    

    [HttpGet ("/groups/by-name")]
    public async Task<ICollection<GroupViewDto>> QueryGroupsByName(string name) => await groupUseCase.QueryGroupsByName(name);
    
    [HttpPost("/groups")]
    public async Task<IActionResult> AddAsync(GroupCreateDto GroupCreateDto)
    {
        try
        {
            var Group = await groupUseCase.CreateGroupAsync(GroupCreateDto);
            return Ok(Group);
        }
        catch (DbNameConflictException e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpPut("/groups/{id:int}")]
    public async Task<IActionResult> Edit(int id, GroupEditDto GroupEditDto)
    {
        await groupUseCase.UpdateGroupAsync(id, GroupEditDto);
        return Ok();
    }

    [HttpDelete("/groups/{id:int}")]
    public async Task Delete(int id) => await groupUseCase.DeleteAsync(id);
}