namespace SchoolAPI.Entities.DTOs.Edit;

public record TeacherEditDto(
    string? Name,
    DateOnly? Birthday
    );