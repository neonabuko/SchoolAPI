using Microsoft.AspNetCore.Mvc;

namespace SchoolAPI.Entities.DTOs.Create;

public record TeacherCreateDto(
    string Name,
    string? Birthday
    );