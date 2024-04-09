using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Entities.DTOs.Create;
using SchoolAPI.Entities.DTOs.Edit;
using SchoolAPI.Entities.DTOs.View;
using SchoolAPI.UseCase.LessonUseCases;

namespace SchoolAPI.Controllers;

[ApiController]
public class LessonController(LessonUseCase useCase) : ControllerBase
{
    [HttpGet("/lessons")]
    public async Task<ICollection<LessonViewDto>> GetAllAsync() => await useCase.GetAllLessonesAsync();

    [HttpGet("/lessons/{id:int}")]
    public async Task<LessonViewDto> GetAsync(int id) => await useCase.GetLessonAsync(id);

    [HttpGet("/lessons/student/{studentId:int}")]
    public async Task<ICollection<LessonViewDto>> GetAllLessonsFromStudentAsync(int studentId) => await useCase.GetAllLessonsFromStudentAsync(studentId);

    [HttpGet("/lessons/student/{studentId:int}/today")]
    public async Task<LessonViewDto> GetStudentsMostRecentLessonAsync(int studentId) => await useCase.GetStudentsMostRecentLessonAsync(studentId);
    
    [HttpGet("/lessons/groups/{groupId:int}/today")]
    public async Task<ICollection<StudentViewDto>> GetStudentsThatHaveLessonTodayAsync(int groupId) => await useCase.GetStudentsThatHaveLessonTodayAsync(groupId);

    [HttpPost("/lessons")]
    public async Task<IActionResult> AddAsync(LessonCreateDto dto)
    {
        try
        {
            var Lesson = await useCase.CreateLessonAsync(dto);
            return Ok(Lesson);
        }
        catch (DbNameConflictException e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpPut("/lessons/{id:int}")]
    public async Task UpdateAsync(int id, LessonEditDto dto) => await useCase.UpdateLessonAsync(id, dto);

    [HttpDelete("/lessons/{id:int}")]
    public async Task DeleteAsync(int id) => await useCase.DeleteAsync(id);
}