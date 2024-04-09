namespace SchoolAPI.Entities.DTOs.Edit;

public record LessonEditDto(
    string? Name,
    string? DateTime,
    string? Oral,
    string? HwDelivered,
    string? HwGrade,
    string? StudentPresent,
    string? StudentId
    );