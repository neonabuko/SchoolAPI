using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Entities.DTOs.Create;
using SchoolAPI.Entities.DTOs.Edit;
using SchoolAPI.Entities.DTOs.View;
using SchoolAPI.UseCase.StudentUseCases;

namespace SchoolAPI.Controllers;

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
    [Route("/students/groups/{GroupId:int}")]
    public async Task<ICollection<StudentViewDto>> GetStudentsByGroupId(int GroupId)
    {
        return await useCase.GetStudentsByGroupIdAsync(GroupId);
    }
    
    [HttpGet]
    [Route("/students/by-name")]
    public async Task<ICollection<StudentViewDto>> QueryStudentsByNameAsync(string name)
    {
        return await useCase.QueryStudentsByName(name);
    }

    [HttpPost]
    [Route("/students")]
    public async Task<IActionResult> AddAsync(StudentCreateDto studentCreateDto)
    {
        try
        {
            var studentDto = await useCase.CreateStudentAsync(studentCreateDto);
            return Ok(studentDto);
        }
        catch (DbNameConflictException e)
        {
            return Conflict(e.Message);
        }
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