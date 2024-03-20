using Microsoft.AspNetCore.Mvc;
using WizardAPI.Entities.DTOs;
using WizardAPI.UseCase.TeacherUseCases;

namespace WizardAPI.Controllers;

[ApiController]
public class TeacherController(TeacherUseCase teacherUseCase) : ControllerBase
{
    [HttpGet]
    [Route("/teachers")]
    public async Task<ICollection<TeacherDto>> Index()
    {
        return await teacherUseCase.GetAllTeachersAsync();
    }

    [HttpPost]
    [Route("/teachers")]
    public async Task<IActionResult> Add(TeacherDto teacherDto)
    {
        await teacherUseCase.CreateTeacherAsync(teacherDto);
        return Ok();
    }

    [HttpPut]
    [Route("/teachers/{id:int}")]
    public async Task<IActionResult> Edit(int id, TeacherDto teacherDto)
    {
        await teacherUseCase.UpdateTeacherAsync(id, teacherDto);
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