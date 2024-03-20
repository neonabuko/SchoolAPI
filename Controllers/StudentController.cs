using Microsoft.AspNetCore.Mvc;
using WizardAPI.Entities.DTOs;
using WizardAPI.UseCase.StudentUseCases;

namespace WizardAPI.Controllers;

[ApiController]
public class StudentController(StudentUseCase useCase) : ControllerBase
{
    [HttpGet]
    [Route("/students")]
    public async Task<ICollection<StudentOutDto>> Index()
    {
        return await useCase.GetAllStudentsAsync();
    }

    [HttpPost]
    [Route("/students")]
    public async Task Add(StudentDto studentDto)
    {
        await useCase.CreateStudentAsync(studentDto);
    }
}