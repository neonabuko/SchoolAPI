using Microsoft.AspNetCore.Mvc;
using WizardAPI.Entities.DTOs.Create;
using WizardAPI.Entities.DTOs.Edit;
using WizardAPI.Entities.DTOs.View;
using WizardAPI.UseCase.TeacherUseCases;

namespace WizardAPI.Controllers;

[ApiController]
public class TeacherController(TeacherUseCase teacherUseCase) : ControllerBase
{
    [HttpGet]
    [Route("/teachers")]
    public async Task<ICollection<TeacherViewDto>> Index()
    {
        return await teacherUseCase.GetAllTeachersAsync();
    }

    [HttpPost]
    [Route("/teachers")]
    public async Task<IActionResult> Add(TeacherCreateDto teacherCreateDto)
    {
        await teacherUseCase.CreateTeacherAsync(teacherCreateDto);
        return Ok();
    }

    [HttpPut]
    [Route("/teachers/{id:int}")]
    public async Task<IActionResult> Edit(int id, TeacherEditDto teacherEditDto)
    {
        await teacherUseCase.UpdateTeacherAsync(id, teacherEditDto);
        return Ok();
    }
    
    [HttpDelete]
    [Route("/teachers/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await teacherUseCase.DeleteAsync(id);
        return Ok();
    }
}