namespace SchoolAPI.Entities.DTOs.Edit;

public record GroupEditDto(
    string? Name,
    string? DateTime,
    int? TeacherId
    );