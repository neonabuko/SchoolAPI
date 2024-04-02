using Microsoft.AspNetCore.Mvc;
using WizardAPI.Entities.DTOs.Create;
using WizardAPI.Entities.DTOs.Edit;
using WizardAPI.Entities.DTOs.View;
using WizardAPI.UseCase.InteractiveGroupUseCases;
using WizardAPI.UseCase.StudentUseCases;

namespace WizardAPI.Controllers;

[ApiController]
public class InteractiveGroupController(InteractiveGroupUseCase groupUseCase, StudentUseCase studentUseCase) : ControllerBase
{
    [HttpGet]
    [Route("/interactive-groups")]
    public async Task<ICollection<InteractiveGroupViewDto>> Index()
    {
        return await groupUseCase.GetAllInteractiveGroupsAsync();
    }

    [HttpGet]
    [Route("/interactive-groups/{id:int}")]
    public async Task<InteractiveGroupViewDto> GetAsync(int id)
    {
        return await groupUseCase.GetInteractiveGroupAsync(id);
    }

    [HttpGet]
    [Route("/interactive-groups/by-name/{name}")]
    public async Task<ICollection<InteractiveGroupViewDto>> QueryInteractiveGroupsByName(string name)
    {
        return await groupUseCase.QueryInteractiveGroupsByName(name);
    }
    
    [HttpPost]
    [Route("/interactive-groups")]
    public async Task<IActionResult> AddAsync(InteractiveGroupCreateDto interactiveGroupCreateDto)
    {
        await groupUseCase.CreateInteractiveGroupAsync(interactiveGroupCreateDto);
        return Ok();
    }

    [HttpPut]
    [Route("/interactive-groups/{id:int}")]
    public async Task<IActionResult> Edit(int id, InteractiveGroupEditDto interactiveGroupEditDto)
    {
        await groupUseCase.UpdateInteractiveGroupAsync(id, interactiveGroupEditDto);
        return Ok();
    }

    [HttpDelete]
    [Route("/interactive-groups/{id:int}")]
    public async Task Delete(int id)
    {
        await groupUseCase.DeleteAsync(id);
    }
}