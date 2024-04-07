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

    [HttpGet]
    [Route("/teachers/{id:int}")]
    public async Task<TeacherViewDto> GetAsync(int id)
    {
        return await teacherUseCase.GetTeacherAsync(id);
    }

    [HttpPost]
    [Route("/teachers")]
    public async Task<IActionResult> Add(TeacherCreateDto teacherCreateDto)
    {
        try
        {
            var teacher = await teacherUseCase.CreateTeacherAsync(teacherCreateDto);
            return Ok(teacher);
        }
        catch (DbNameConflictException e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpGet]
    [Route("/teachers/by-name")]
    public async Task<ICollection<TeacherViewDto>> QueryTeacherByName(string name) {
        return await teacherUseCase.QueryTeachersByNameAsync(name);
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