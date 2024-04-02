using Microsoft.AspNetCore.Mvc;
using WizardAPI.Entities.DTOs.Create;
using WizardAPI.Entities.DTOs.Edit;
using WizardAPI.Entities.DTOs.View;
using WizardAPI.UseCase.StudentUseCases;

namespace WizardAPI.Controllers;

[ApiController]
public class StudentController(StudentUseCase useCase) : ControllerBase
{
    [HttpGet]
    [Route("/students")]
    public async Task<ICollection<StudentViewDto>> Index()
    {
        return await useCase.GetAllStudentsAsync();
    }

    [HttpGet]
    [Route("/students/{id:int}")]
    public async Task<StudentViewDto> GetAsync(int id)
    {
        return await useCase.GetStudentAsync(id);
    }

    [HttpGet]
    [Route("/students/interactive-groups/{interactiveGroupId:int}")]
    public async Task<ICollection<StudentViewDto>> GetStudentsByInteractiveGroupId(int interactiveGroupId)
    {
        return await useCase.GetStudentsByInteractiveGroupIdAsync(interactiveGroupId);
    }
    
    [HttpGet]
    [Route("/students/by-name/{name}")]
    public async Task<ICollection<StudentViewDto>> QueryStudentsByNameAsync(string name)
    {
        return await useCase.QueryStudentsByName(name);
    }

    [HttpPost]
    [Route("/students")]
    public async Task<StudentViewDto> AddAsync(StudentCreateDto studentCreateDto)
    {
        return await useCase.CreateStudentAsync(studentCreateDto);
    }

    [HttpPut]
    [Route("/students/{id:int}")]
    public async Task EditAsync(int id, StudentEditDto studentEditDto)
    {
        await useCase.UpdateStudentAsync(id, studentEditDto);
    }

    [HttpDelete]
    [Route("/students/{id:int}")]
    public async Task DeleteAsync(int id)
    {
        await useCase.DeleteAsync(id);
    }
}