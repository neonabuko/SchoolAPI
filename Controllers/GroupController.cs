using Microsoft.AspNetCore.Mvc;
using WizardAPI.Entities.DTOs;
using WizardAPI.UseCase.GroupUseCases;

namespace WizardAPI.Controllers;

[ApiController]
public class GroupController(GroupUseCase useCase) : ControllerBase
{
    [HttpGet]
    [Route("/groups")]
    public async Task<ICollection<GroupOutDto>> Index()
    {
        return await useCase.GetAllGroupsAsync();
    }

    [HttpPost]
    [Route("/groups")]
    public async Task<IActionResult> Add(GroupDto groupDto)
    {
        await useCase.CreateGroupAsync(groupDto);
        return Ok();
    }
}