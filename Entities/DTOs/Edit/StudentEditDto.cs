namespace SchoolAPI.Entities.DTOs.Edit;

public record StudentEditDto(
    string? Name,
    DateOnly? Birthday,
    int? GroupId
    );