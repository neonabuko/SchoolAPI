using Microsoft.AspNetCore.Mvc;
using WizardAPI.Entities.DTOs;
using WizardAPI.UseCase.WizardClassUseCases;

namespace WizardAPI.Controllers;

[ApiController]
public class WizardClassController(WizardClassUseCase useCase) : ControllerBase
{
    [HttpGet]
    [Route("/wizard-classes")]
    public async Task<ICollection<WizardClassOutDto>> Index()
    {
        return await useCase.GetAllWizardClassesAsync();
    }

    [HttpPost]
    [Route("/wizard-classes/add")]
    public async Task Add(WizardClassDto wizardClassDto)
    {
        await useCase.CreateWizardClassAsync(wizardClassDto);
    }

    [HttpDelete]
    [Route("/wizard-classes/{id:int}")]
    public async Task Delete(int id)
    {
        await useCase.DeleteAsync(id);
    }
}