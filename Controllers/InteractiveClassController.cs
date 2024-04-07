using Microsoft.AspNetCore.Mvc;
using WizardAPI.Entities.DTOs.Create;
using WizardAPI.Entities.DTOs.Edit;
using WizardAPI.Entities.DTOs.View;
using WizardAPI.UseCase.InteractiveClassUseCases;

namespace WizardAPI.Controllers;

[ApiController]
public class InteractiveClassController(InteractiveClassUseCase useCase) : ControllerBase
{
    [HttpGet]
    [Route("/interactive-classes")]
    public async Task<ICollection<InteractiveClassViewDto>> Index()
    {
        return await useCase.GetAllInteractiveClassesAsync();
    }

    [HttpGet]
    [Route("/interactive-classes/{id:int}")]
    public async Task<InteractiveClassViewDto> GetInteractiveClassAsync(int id)
    {
        return await useCase.GetInteractiveClassAsync(id);
    }

    [HttpGet]
    [Route("/interactive-classes/student/{studentId:int}")]
    public async Task<ICollection<InteractiveClassViewDto>> GetInteractiveClassesByStudentId(int studentId)
    {
        return await useCase.GetInteractiveClassesByStudentIdAsync(studentId);
    }

    [HttpGet]
    [Route("/interactive-classes/student/{studentId:int}/today")]
    public async Task<InteractiveClassViewDto> GetFirstInteractiveClassScheduledForTodayByStudentIdAsync(
        int studentId)
    {
        return await useCase.GetFirstInteractiveClassScheduledForTodayByStudentIdAsync(studentId);
    }
    
    [HttpGet]
    [Route("/interactive-classes/interactive-group/{groupId:int}/today")]
    public async Task<ICollection<StudentViewDto>> GetStudentsThatHaveClassTodayByGroupId(int groupId)
    {
        return await useCase.GetStudentsThatHaveClassTodayByGroupId(groupId);
    }

    [HttpPost]
    [Route("/interactive-classes")]
    public async Task<IActionResult> AddAsync(InteractiveClassCreateDto interactiveClassCreateDto)
    {
        try
        {
            var interactiveClass = await useCase.CreateInteractiveClassAsync(interactiveClassCreateDto);
            return Ok(interactiveClass);
        }
        catch (DbNameConflictException e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpPut]
    [Route("/interactive-classes/{id:int}")]
    public async Task EditAsync(int id, InteractiveClassEditDto interactiveClassEditDto)
    {
        await useCase.UpdateInteractiveClassAsync(id, interactiveClassEditDto);
    }

    [HttpDelete]
    [Route("/interactive-classes/{id:int}")]
    public async Task DeleteAsync(int id)
    {
        await useCase.DeleteAsync(id);
    }
}